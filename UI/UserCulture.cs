using System;

namespace Fittings.UI
{
    internal class UserCulture
    {
        /// <summary>
        /// Установить культуру на форме.
        /// </summary>
        /// <param name="languageCode">Код языка (0-английский, 1-русский).По умолчанию русский.</param>
        public void SetCultureOnForm(int languageCode = 0)
        {
            string language = "ru-RU";
            switch (languageCode)
            {
                case 0:
                    language = "en-EN";
                    break;
                case 1:
                    language = "ru-RU";
                    break;
            }

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(language);

            if (!String.IsNullOrEmpty(Properties.Settings.Default.Language))
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.Language);
            }
        }

        /// <summary>
        /// Преобразовать число в представление даты.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetDateLikeString(int date)
        {
            if (date < 10)
                return string.Format("0{0}", date);
            else
                return string.Format("{0}", date);
        }
    }
}
