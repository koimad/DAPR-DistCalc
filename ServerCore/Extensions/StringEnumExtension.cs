using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace ServerCore.Extensions
{
    public static class StringEnumExtension
    {
        #region Methods

        #region Public Methods

        public static String GetDescription<T>(this T e) where T : IConvertible
        {
            String description = null;

            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = Enum.GetValues(type);

                foreach (Int32 val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        MemberInfo[] memInfo = type.GetMember(type.GetEnumName(val));
                        Object[] descriptionAttributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                        if (descriptionAttributes.Length > 0)
                        {
                            description = ((DescriptionAttribute) descriptionAttributes[0]).Description;
                        }

                        break;
                    }
                }
            }

            return description;
        }

        #endregion

        #endregion
    }
}