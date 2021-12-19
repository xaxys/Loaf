using Microsoft.Windows.ApplicationModel.Resources;

namespace Loaf
{
    internal static class ResourceExtensions
    {
        private static readonly ResourceLoader _resLoader = new();

        public static string GetLocalized(this string resourceKey)
        {
            return _resLoader.GetString(resourceKey);
        }
    }
}
