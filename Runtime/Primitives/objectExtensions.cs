namespace Pixelsmao.UnityCommonSolution.Extensions
{
    public static class objectExtensions
    {
        public static bool EqualsNull(this object self)
        {
            return self.Equals(null);
        }

        public static bool IsNull(this object self)
        {
            return self == null;
        }
    }
}