using System;

using Newtonsoft.Json;

namespace ServerCore.MiddleWare.Wrappers
{
    public class ValidationError
    {
        #region Properties

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public String Field { get; }

        public String[] Messages { get; }

        #endregion

        #region Constructor

        public ValidationError(String field, String[] messages)
        {
            Field = field;
            Messages = messages;
        }

        #endregion
    }
}