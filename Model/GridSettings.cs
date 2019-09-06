using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fittings.Model
{
    public static class GridSettings
    {
        /// <summary>
        /// Путь к файлу с настройками грида проектов.
        /// </summary>
        public static string GridProjectSettings = "ApplicationSettings//gridProjectSettings.xml";

        /// <summary>
        /// Путь к файлу с настройками грида фиттингов.
        /// </summary>
        public static string GridFittingSettings = "ApplicationSettings//gridFittingSettings.xml";

        /// <summary>
        /// Путь к файлу с настройками грида проектов на влкадке "Проекты".
        /// </summary>
        public static string GridFullProjectSettings = "ApplicationSettings//gridFullProjectSettings.xml";

        /// <summary>
        /// Путь к файлу с настройками грида оборудования на влкадке "Оборудования".
        /// </summary>
        public static string GridEquipmentsSettings = "ApplicationSettings//gridEquipmentsSettings.xml";
    }
}
