using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using ServerCore.Extensions;
using ServerCore.MiddleWare.Wrappers;

namespace ServerCore.MiddleWare
{
    public class ResponseMiddleware
    {
        #region Members

        private readonly ILogger<ResponseMiddleware> _logger;
        private readonly RequestDelegate _next;
        private readonly ResponseOptions _options;

        #endregion

        #region Constructor

        public ResponseMiddleware(RequestDelegate next, ResponseOptions options, ILogger<ResponseMiddleware> logger)
        {
            _next = next;
            _options = options;
            _logger = logger;
        }

        #endregion

        #region Methods

        #region Private Methods

        private String ConvertToJsonString(Int32 code, Object content)
        {
            return JsonConvert.SerializeObject(new ApiResponse(APiResponseMessageStatus.Success.GetDescription(), content, code, GetApiVersion()), JSONSettings());
        }


        private String ConvertToJsonString(ApiResponse apiResponse)
        {
            return JsonConvert.SerializeObject(apiResponse, JSONSettings());
        }


        private String ConvertToJsonString(Object rawJson)
        {
            return JsonConvert.SerializeObject(rawJson, JSONSettings());
        }


        private async Task<String> FormatRequest(HttpRequest request)
        {
            request.EnableBuffering();

            Byte[] buffer = new Byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            String bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body.Seek(0, SeekOrigin.Begin);

            return $"{request.Method} {request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }


        private async Task<String> FormatResponse(Stream bodyStream)
        {
            bodyStream.Seek(0, SeekOrigin.Begin);
            String plainBodyText = await new StreamReader(bodyStream).ReadToEndAsync();
            bodyStream.Seek(0, SeekOrigin.Begin);

            return plainBodyText;
        }


        private String GetApiVersion()
        {
            return String.IsNullOrEmpty(_options.ApiVersion) ? "1.0.0.0" : _options.ApiVersion;
        }


        private ApiResponse GetErrorResponse(Int32 code, ApiError apiError)
        {
            return new ApiResponse(code, apiError) {Version = GetApiVersion()};
        }


        private ApiResponse GetSuccessResponse(ApiResponse apiResponse)
        {
            if (apiResponse.Version.Equals("1.0.0.0"))
            {
                apiResponse.Version = GetApiVersion();
            }

            return apiResponse;
        }


        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            ApiError apiError;
            Int32 code;

            if (exception is ApiException ex)
            {
                if (ex.IsModelValidationError)
                {
                    apiError = new ApiError(APiResponseMessageStatus.ValidationError.GetDescription(), ex.Errors)
                    {
                        ReferenceErrorCode = ex.ReferenceErrorCode, ReferenceDocumentLink = ex.ReferenceDocumentLink
                    };

                    _logger.Log(LogLevel.Warning, ex, $"[{ex.StatusCode}]: {APiResponseMessageStatus.ValidationError.GetDescription()}");
                }
                else
                {
                    apiError = new ApiError(ex.Message) {ReferenceErrorCode = ex.ReferenceErrorCode, ReferenceDocumentLink = ex.ReferenceDocumentLink};

                    _logger.Log(LogLevel.Warning, ex, $"[{ex.StatusCode}]: {APiResponseMessageStatus.Exception.GetDescription()}");
                }

                code = ex.StatusCode;
                context.Response.StatusCode = code;
            }
            else if (exception is UnauthorizedAccessException)
            {
                apiError = new ApiError(APiResponseMessageStatus.UnAuthorized.GetDescription());
                code = (Int32) HttpStatusCode.Unauthorized;
                context.Response.StatusCode = code;

                _logger.Log(LogLevel.Warning, exception, $"[{code}]: {APiResponseMessageStatus.UnAuthorized.GetDescription()}");
            }
            else
            {
                String exceptionMessage = APiResponseMessageStatus.Unhandled.GetDescription();
#if !DEBUG
                var message = exceptionMessage;
                string stackTrace = null;
#else
                String message = $"{exceptionMessage} {exception.GetBaseException().Message}";
                String stackTrace = exception.StackTrace;
#endif

                apiError = new ApiError(message) {Details = stackTrace};
                code = (Int32) HttpStatusCode.InternalServerError;
                context.Response.StatusCode = code;

                _logger.Log(LogLevel.Error, exception, $"[{code}]: {exceptionMessage}");
            }

            String jsonString = ConvertToJsonString(GetErrorResponse(code, apiError));

            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(jsonString);
        }


        private Task HandleNotSuccessRequestAsync(HttpContext context, Object body, Int32 code)
        {
            ApiError apiError;

            if (code == (Int32) HttpStatusCode.NotFound)
            {
                apiError = new ApiError(APiResponseMessageStatus.NotFound.GetDescription());
            }
            else if (code == (Int32) HttpStatusCode.NoContent)
            {
                apiError = new ApiError(APiResponseMessageStatus.NotContent.GetDescription());
            }
            else if (code == (Int32) HttpStatusCode.MethodNotAllowed)
            {
                apiError = new ApiError(APiResponseMessageStatus.MethodNotAllowed.GetDescription());
            }
            else if (code == (Int32) HttpStatusCode.UnprocessableEntity)
            {
                String bodyText = !body.ToString().IsValidJson() ? ConvertToJsonString(body) : body.ToString();

                Dictionary<String, String[]> errors = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<String, String[]>>(bodyText);
                
                apiError = new ApiError(APiResponseMessageStatus.ValidationError.GetDescription(), errors.Select(f=> new ValidationError(f.Key ,f.Value )));
            }             
            else
            {
                apiError = new ApiError(APiResponseMessageStatus.Unknown.GetDescription());
            }

            context.Response.StatusCode = code;

            String jsonString = ConvertToJsonString(GetErrorResponse(code, apiError));

            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(jsonString);
        }


        private Task HandleSuccessRequestAsync(HttpContext context, Object body, Int32 code)
        {
            String jsonString;

            String bodyText = !body.ToString().IsValidJson() ? ConvertToJsonString(body) : body.ToString();

            dynamic bodyContent = JsonConvert.DeserializeObject<dynamic>(bodyText);
            Type type = bodyContent?.GetType();

            if (type == typeof(JObject))
            {
                ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(bodyText);
                jsonString = apiResponse.StatusCode != code || apiResponse.Result != null ||
                    apiResponse.StatusCode == code && apiResponse.Result == null
                    ? ConvertToJsonString(GetSuccessResponse(apiResponse))
                    : (String)ConvertToJsonString(code, bodyContent);
            }
            else
            {
                jsonString = ConvertToJsonString(code, bodyContent);
            }

            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(jsonString);
        }


        private Boolean IsSwagger(HttpContext context)
        {
            return context.Request.Path.StartsWithSegments("/swagger");
        }


        private JsonSerializerSettings JSONSettings()
        {
            return new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver(), Converters = new List<JsonConverter> {new StringEnumConverter()}};
        }

        #endregion

        #region Public Methods

        public async Task InvokeAsync(HttpContext context)
        {
            if (IsSwagger(context))
            {
                await _next(context);
            }
            else
            {
                await using (MemoryStream bodyStream = new MemoryStream())
                {
                    String request = await FormatRequest(context.Request);

                    Stream originalBodyStream = context.Response.Body;

                    try
                    {
                        context.Response.Body = bodyStream;

                        await _next.Invoke(context);

                        context.Response.Body = originalBodyStream;
                        
                        if (context.Response.StatusCode == (Int32) HttpStatusCode.OK)
                        {
                            String bodyAsText = await FormatResponse(bodyStream);
                            await HandleSuccessRequestAsync(context, bodyAsText, context.Response.StatusCode);
                        }
                        else
                        {
                            String bodyAsText = await FormatResponse(bodyStream);
                            await HandleNotSuccessRequestAsync(context, bodyAsText, context.Response.StatusCode);
                        }
                    }
                    catch (Exception ex)
                    {
                        await HandleExceptionAsync(context, ex);
                        bodyStream.Seek(0, SeekOrigin.Begin);
                        await bodyStream.CopyToAsync(originalBodyStream);
                    }
                    finally
                    {
                        _logger.Log(LogLevel.Information, $@"Request: {request} Responded with [{context.Response.StatusCode}]");
                    }
                }
            }
        }

        #endregion

        #endregion
    }
}