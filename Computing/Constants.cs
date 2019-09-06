using Fittings.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Fittings
{
    /// <summary>
    /// Класс, содержащий перечень основных констант.
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// Список констант для расчета цен.
        /// </summary>
        public DataTable MoneyConstates { get; set; }
        
        /// <summary>
        /// Стоимость литра бензина (руб/л).
        /// </summary>
        public double GasolineCost { get; set; } 

        /// <summary>
        /// Средний расход топлива (л/100км).
        /// </summary>
        public double AverageFuelConsumption { get; set; }

        /// <summary>
        /// Стоимость часа работы (руб).
        /// </summary>
        public double HourWorkCost { get; set; }

        /// <summary>
        /// Процент надбавки за проект.
        /// </summary>
        public double PercentOfProject { get; set; }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public Constants()
        {
            MoneyConstates = LoadConstants();
            foreach (DataRow row in MoneyConstates.Rows)
            {
                if (row["Name"].ToString() == "GasolineCost")
                {
                    GasolineCost = double.Parse(row["Price"].ToString());
                }
                if (row["Name"].ToString() == "AverageFuelConsumption")
                {
                    AverageFuelConsumption = double.Parse(row["Price"].ToString());
                }
                if (row["Name"].ToString() == "HourWorkCost")
                {
                    HourWorkCost = double.Parse(row["Price"].ToString());
                }
                if (row["Name"].ToString() == "PercentOfCost")
                {
                    PercentOfProject = double.Parse(row["Price"].ToString());
                }
            }
        }

        /// <summary>
        /// Загружает константы, определенные пользователем.
        /// </summary>
        public DataTable LoadConstants()
        {
            using (SqlConnection conn = new SqlConnection(Connection.MainSqlConnection))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    string commandText = "exec ShowConstantes";
                    cmd.CommandText = commandText;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);

                    //FittingList = ds.Tables[0];

                    return ds.Tables[0];
                }
            }
        }

        /// <summary>
        /// Сохраняет настроенные пользователем константы.
        /// </summary>
        public void SaveConstants()
        {
            SaveConstant(1, GasolineCost);
            SaveConstant(2, AverageFuelConsumption);
            SaveConstant(3, HourWorkCost);
            SaveConstant(4, PercentOfProject);
        }

        private void SaveConstant(int id, double price)
        {
            string sql = String.Format("exec EditConstant_2 {0}, {1}",
                id,
                price);
            using (SqlConnection connection = new SqlConnection(Connection.MainSqlConnection))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Sql error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
