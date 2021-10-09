using System;
using System.Collections.Generic;

namespace ServerCore.MiddleWare.Wrappers
{
    public class ApiError
    {
        #region Properties

        public String ExceptionMessage { get; set; }

        public String Details { get; set; }

        public String ReferenceErrorCode { get; set; }

        public String ReferenceDocumentLink { get; set; }

        public IEnumerable<ValidationError> ValidationErrors { get; set; }

        #endregion

        #region Constructor

        public ApiError(String message)
        {
            ExceptionMessage = message;
        }


        public ApiError(String message, IEnumerable<ValidationError> validationErrors)
        {
            ExceptionMessage = message;
            ValidationErrors = validationErrors;
        }

        #endregion
    }
}