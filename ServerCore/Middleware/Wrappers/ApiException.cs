using System;
using System.Collections.Generic;

namespace ServerCore.MiddleWare.Wrappers
{
    public class ApiException : Exception
    {
        #region Properties

        public Int32 StatusCode { get; set; }

        public Boolean IsModelValidationError { get; set; }

        public IEnumerable<ValidationError> Errors { get; set; }

        public String ReferenceErrorCode { get; set; }

        public String ReferenceDocumentLink { get; set; }

        #endregion

        #region Constructor

        public ApiException(String message,
            Int32 statusCode = 500,
            String errorCode = "",
            String refLink = "") :
            base(message)
        {
            IsModelValidationError = false;
            StatusCode = statusCode;
            ReferenceErrorCode = errorCode;
            ReferenceDocumentLink = refLink;
        }


        public ApiException(IEnumerable<ValidationError> errors, Int32 statusCode = 400)
        {
            IsModelValidationError = true;
            StatusCode = statusCode;
            Errors = errors;
        }


        public ApiException(Exception ex, Int32 statusCode = 500) : base(ex.Message)
        {
            IsModelValidationError = false;
            StatusCode = statusCode;
        }

        #endregion
    }
}