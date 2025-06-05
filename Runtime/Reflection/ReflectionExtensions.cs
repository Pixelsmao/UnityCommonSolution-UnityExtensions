
namespace Pixelsmao.UnityCommonSolution.Extensions
{
    public static class ReflectionExtensions
    {
        public static object GetFieldValue(this object self, string fieldName)
        {
            var type = self.GetType();
            var fieldInfo = type.GetField(fieldName);
            return fieldInfo?.GetValue(self);
        }
        public static object GetPropertyValue(this object self, string propertyName, object[] index = null)
        {
            var type = self.GetType();
            var propertyInfo = type.GetProperty(propertyName);
            return propertyInfo?.GetValue(self, index);
        }
        public static object ExecuteMethod(this object self, string methodName, params object[] args)
        {
            var type = self.GetType();
            var methodInfo = type.GetMethod(methodName);
            return methodInfo?.Invoke(self, args);
        } 
    }
}