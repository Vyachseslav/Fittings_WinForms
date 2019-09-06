using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fittings.Computing
{
    /// <summary>
    /// Класс для расчетов расходов.
    /// </summary>
    public class Expenses
    {
        /// <summary>
        /// Класс, содержащий необходимые для расчета константы.
        /// </summary>
        private Constants UserConst { get; set; }

        public Expenses()
        {
            UserConst = new Constants();
        }

        /// <summary>
        /// Вычисляет транспортный расход.
        /// </summary>
        /// <param name="fuelVolume">Объем топлива (л).</param>
        /// <param name="itemCount">Кол-во товара (шт).</param>
        /// <returns></returns>
        public double TransportExpenses(double fuelVolume, int itemCount)
        {
            return ((fuelVolume * UserConst.GasolineCost) / itemCount);
        }

        /// <summary>
        /// Вычисляет транспортный расход.
        /// </summary>
        /// <param name="distance">Расстояние (км).</param>
        /// <param name="itemCount">Кол-во товара (шт).</param>
        /// <returns></returns>
        public double TransportExpensesByDistance(int distance, int itemCount)
        {
            return (((distance / UserConst.AverageFuelConsumption) * UserConst.GasolineCost) / itemCount);
        }

        /// <summary>
        /// Вычисляет стоимость трудозатрат.
        /// </summary>
        /// <param name="hoursCount">Кол-во отработанных часов (ч).</param>
        /// <param name="workerCount">Число сотрудников.</param>
        /// <returns></returns>
        public double LaborExpenditures(double hoursCount, int workerCount)
        {
            return (hoursCount * UserConst.HourWorkCost * workerCount);
        }

        /// <summary>
        /// Вычисляет стоимость проекта с учетом процента от проекта.
        /// </summary>
        /// <param name="totalSum"></param>
        /// <returns></returns>
        public double TotalProjectSumWithPercent(double totalSum)
        {
            double constanta = UserConst.PercentOfProject / 100;
            return (constanta * totalSum) + totalSum;
        }

    }
}
