using DevExpress.Office.Utils;
using Fittings.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Fittings.Model
{
    public class EquipmentModel
    {
        private string _sqlAddText    = "exec AddEquipment N'{0}', N'{1}', {2}"; // Запрос добавления оборудования.
        private string _sqlEditText   = "exec EditEquipment {0}, N'{1}', N'{2}', {3}"; // Запрос редуктирования оборудования.
        private string _sqlRemoveText = "exec DropEquipment {0}"; // Запрос удаления оборудования.
        private string _sqlShowText   = "exec ShowEquipments"; // Запрос показа списка оборудования.


        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        /// <summary>
        /// Добавить оборудование.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public int Add(string name, string description, double price)
        {
            string sqlExpression = String.Format(_sqlAddText, name, description, price);

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

        /// <summary>
        /// Добавить оборудование.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public int Add()
        {
            string sqlExpression = String.Format(_sqlAddText, Name, Description, Price);

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

        /// <summary>
        /// Редактировать оборудование.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public int Edit(int id, string name, string description, double price)
        {
            string sqlExpression = String.Format(_sqlEditText, id, name, description, price);

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

        /// <summary>
        /// Редактировать оборудование.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public int Edit()
        {
            string sqlExpression = String.Format(_sqlEditText, Id, Name, Description, Price);

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

        /// <summary>
        /// Удалить оборудование.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Показать список оборудования.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Получить список оборудования в виде листа моделей EquipmentModel.
        /// </summary>
        /// <returns></returns>
        public List<EquipmentModel> GetListEquipmentsAsModel()
        {
            List<EquipmentModel> list = new List<EquipmentModel>();

            using (SqlConnection conn = new SqlConnection(Connection.MainSqlConnection))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = _sqlShowText;                    
                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                list.Add(new EquipmentModel()
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Description = reader.GetString(2),
                                    Price = double.Parse(reader.GetValue(3).ToString())
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Sql error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        list = null;
                    }
                }
            }

            return list;
        }
    }
}
