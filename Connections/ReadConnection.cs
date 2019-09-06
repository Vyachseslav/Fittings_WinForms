using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Fittings.Connections
{
    public class ReadConnection
    {
        public void GetUserConnection()
        {
            try
            {
                using (StreamReader sw = new StreamReader("connectionSetting.txt"))
                {
                    string file = sw.ReadToEnd();
                    string[] lines = file.Split('\n');

                    Connection.SQLServerName = lines[0];
                    Connection.DataBaseName = lines[1];
                    Connection.IntegratedSecurity = bool.Parse(lines[2]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void GetUserExtendedConnection()
        {
            Connection.SQLServerName = Properties.Settings.Default.serverName;
            Connection.DataBaseName = Properties.Settings.Default.databaseName;
            Connection.IntegratedSecurity = Properties.Settings.Default.integratedSecurity;
            Connection.Login = Properties.Settings.Default.login;
            Connection.Password = Properties.Settings.Default.password;
            Connection.IsConnectionString = Properties.Settings.Default.IsConnectionString;
            Connection.ConString = Properties.Settings.Default.UserConnectionString;
        }
    }
}
