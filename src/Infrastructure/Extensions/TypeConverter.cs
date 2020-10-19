using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Extensions
{
    public static class TypeConverter
    {
        public static T To<T>(this object input)
        {
            try
            {
                return (T)Convert.ChangeType(input, typeof(T));
            }
            catch
            {
                throw new Exception("Variable is not converting");
            }
        }
    }
}
