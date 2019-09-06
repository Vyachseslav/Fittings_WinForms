using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Fittings.Model
{
    public class ComponentGroupModel
    {
        public class ComponentModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Price { get; set; }
        }


        public int Id { get; set; }
        public string Name { get; set; }

        public List<ComponentModel> Components { get; set; }

        public ComponentGroupModel()
        {
            Components = new List<ComponentModel>();
        }

        public ComponentGroupModel(int id, string name)
        {
            Id = id;
            Name = name;
            Components = new List<ComponentModel>();

            if (id > 0 && !string.IsNullOrEmpty(name))
            {
                LoadComponents();
            }
        }

        public void LoadComponents()
        {
            string sqlExpression = String.Format("exec ShowFittingsToComponentGroup N'{0}'", Name);
            using (SqlConnection connection = new SqlConnection(Connections.Connection.MainSqlConnection))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Components.Add(new ComponentModel()
                        {
                            Id = int.Parse(reader.GetValue(0).ToString()),
                            Name = reader.GetValue(1).ToString(),
                            Price = double.Parse(reader.GetValue(2).ToString())
                        });
                    }
                }
            }
        }
    }
}
