using System;
using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace ServerCore.MiddleWare.Wrappers
{
    [DataContract]
    public class ApiResponse
    {
        #region Properties

        [DataMember]
        public String Version { get; set; }

        [DataMember]
        public Int32 StatusCode { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String Message { get; set; }

        [DataMember]
        public Boolean IsError { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public ApiError ResponseException { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Object Result { get; set; }

        #endregion

        #region Constructor

        [JsonConstructor]
        public ApiResponse(String message, Object result, Int32 statusCode, String apiVersion)
        {
            StatusCode = statusCode;
            Message = message;
            Result = result;
            Version = apiVersion;
            IsError = false;
        }


        public ApiResponse(Int32 statusCode, ApiError apiError)
        {
            StatusCode = statusCode;
            ResponseException = apiError;
            IsError = true;
        }

        #endregion
    }
}