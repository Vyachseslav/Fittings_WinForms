

namespace Fittings.UI
{
    /// <summary>
    /// Статический класс для получения информации о продукте.
    /// </summary>
    public static class Informations
    {
        /// <summary>
        /// Получить версию файла сборки в текстовом виде.
        /// </summary>
        /// <returns></returns>
        public static string GetFileVersion()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo versionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            return versionInfo.FileVersion;
        }

        /// <summary>
        /// Получить версию сборки в текстовом виде.
        /// </summary>
        /// <returns></returns>
        public static string GetAssemblyVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
}
