using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc.ModelBinding;
using ServerCore.MiddleWare.Wrappers;

namespace ServerCore.Extensions
{
    public static class ModelStateExtension
    {
        #region Methods

        #region Public Methods

        public static IEnumerable<ValidationError> AllErrors(this ModelStateDictionary modelState)
        {
            return modelState.Keys.Select(key => new ValidationError(key, modelState[key].Errors.Select(x => x.ErrorMessage).ToArray()));
        }

        #endregion

        #endregion
    }
}