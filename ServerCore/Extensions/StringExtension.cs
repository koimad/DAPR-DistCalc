using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ServerCore.Extensions
{
    public static class StringExtension
    {
        #region Methods

        #region Public Methods

        public static Boolean IsValidJson(this String text)
        {
            text = text.Trim();
            if (text.StartsWith("{") && text.EndsWith("}") || //For object
                text.StartsWith("[") && text.EndsWith("]")) //For array
                try
                {
                    JToken obj = JToken.Parse(text);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    return false;
                }
                catch (Exception ex)
                {
                    return false;
                }

            return false;
        }

        #endregion

        #endregion
    }
}