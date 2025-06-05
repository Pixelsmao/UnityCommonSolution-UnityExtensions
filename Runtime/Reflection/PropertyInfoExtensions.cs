using System;
using System.Reflection;

namespace Pixelsmao.UnityCommonSolution.Extensions
{
    public static class PropertyInfoExtensions
    {
        public static bool TryGetValue(this PropertyInfo property, object owner, out object value)
        {
            var fieldValue = property.PropertyType.GetDefaultValue();
            try
            {
                fieldValue = property.GetValue(owner);
                value = fieldValue;
                return true;
            }
            catch (Exception)
            {
                value = fieldValue;
                return false;
            }
        }
    }
}