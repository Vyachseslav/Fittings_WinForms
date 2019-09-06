using Fittings.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fittings
{
    public class SqlMethods
    {
        public DataTable LoadProjects(string sqlRequest)
        {
            using (SqlConnection conn = new SqlConnection(Connection.MainSqlConnection))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sqlRequest;

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

                    //return ds.Tables[0];
                }
            }
        }

        /// <summary>
        /// Метод для удаления записей из базы.
        /// </summary>
        /// <param name="sqlRequest">Sql запрос.</param>
        /// <returns></returns>
        public bool Delete(string sqlRequest)
        {
            using (SqlConnection connection = new SqlConnection(Connection.MainSqlConnection))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlRequest, connection);

                try
                {
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка sql", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
    }
}
