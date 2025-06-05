using System;
using System.Reflection;

namespace Pixelsmao.UnityCommonSolution.Extensions
{
    public static class FieldInfoExtensions
    {
        public static bool TryGetValue(this FieldInfo field, object owner, out object value)
        {
            var fieldValue = field.FieldType.GetDefaultValue();
            try
            {
                fieldValue = field.GetValue(owner);
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