using Fittings.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Fittings.Model
{
    public class ProjectEquipmentModel
    {
        private string _sqlAddText = "exec AddEquipmentToProject {0}, {1}, {2}, {3}"; // Запрос добавления оборудования.
        private string _sqlEditText = "exec EditEquipmentToProject {0}, {1}, {2}, {3}, {4}"; // Запрос редуктирования оборудования.
        private string _sqlRemoveText = "exec DropEquipmentToProject {0}"; // Запрос удаления оборудования.
        private string _sqlShowText = "exec ShowEquipmentToProject"; // Запрос показа списка оборудования.

        public int Id { get; set; }
        public int ProjectID { get; set; }
        public int EquipmentID { get; set; }
        public string EquipmentName { get; set; }
        public double EquipmetnUsingPercent { get; set; }
        public double EquipmetnPrice { get; set; }
        public double AmortizationPrice { get; set; } // Автовычисляемое поле.

        /// <summary>
        /// Список всех оборудований.
        /// </summary>
        public List<Model.EquipmentModel> EquipmentModels { get; set; }

        public ProjectEquipmentModel()
        {
            EquipmetnPrice = 0;
            AmortizationPrice = 0;

            EquipmentModels = new Model.EquipmentModel().GetListEquipmentsAsModel();
        }

        public int Add()
        {
            string sqlExpression = String.Format(_sqlAddText, ProjectID, EquipmentID, EquipmetnUsingPercent, EquipmetnPrice);

            using (SqlConnection connection = new SqlConnection(Connection.MainSqlConnection))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                try
                {
                    int id = int.Parse(command.ExecuteScalar().ToString());
                    return id;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Sql error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
            }
        }

        public int Edit()
        {
            string sqlExpression = String.Format(_sqlEditText, Id, ProjectID, EquipmentID, EquipmetnUsingPercent, EquipmetnPrice);

            using (SqlConnection connection = new SqlConnection(Connection.MainSqlConnection))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                try
                {
                    command.ExecuteNonQuery();
                    return Id;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Sql error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
            }
        }

        public int Remove(int id)
        {
            string sqlExpression = String.Format(_sqlRemoveText, id);

            using (SqlConnection connection = new SqlConnection(Connection.MainSqlConnection))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                try
                {
                    command.ExecuteNonQuery();
                    return id;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Sql error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
            }
        }

        public DataTable Show()
        {
            using (SqlConnection conn = new SqlConnection(Connection.MainSqlConnection))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = _sqlShowText;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    try
                    {
                        da.Fill(ds);
                        return ds.Tables[0];
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Sql error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                }
            }
        }

        public void GetDataByEquipmentID()
        {
            Model.EquipmentModel model = EquipmentModels.Find(x => x.Id == this.EquipmentID);
            this.EquipmentName = model.Name;
            this.EquipmetnPrice = model.Price;
        }
    }
}
