using System.Collections.Specialized;

namespace FileHelperHeaders.QuartzLog4net
{
    public static class ArgExtensions
    {
        public static string GetValue(this NameValueCollection values, string name)
        {
            return GetValue(values, name, null);
        }

        /// <summary>
        /// Retrieves the named value from the specified <see cref="NameValueCollection"/>.
        /// </summary>
        /// <param name="values">may be null</param>
        /// <param name="name">the value's key</param>
        /// <param name="defaultValue">the default value, if not found</param>
        /// <returns>if <paramref name="values"/> is not null, the value returned by values[name]. <c>null</c> otherwise.</returns>
        public static string GetValue(this NameValueCollection values, string name, string defaultValue)
        {
            if (values != null)
            {
                foreach (var key in values.AllKeys)
                {
                    if (string.Compare(name, key, true) == 0)
                    {
                        return values[name];
                    }
                }
            }
            return defaultValue;
        }
    }
}