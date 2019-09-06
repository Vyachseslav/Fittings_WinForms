using System;
using System.Configuration;

namespace Fittings.Connections
{
    /// <summary>
    /// Класс соединений.
    /// </summary>
    public static class Connection
    {
        public static string SQLServerName { get; set; } = @"DESKTOP-LM4OHF4\SQLEXPRESS";
        public static string DataBaseName { get; set; } = @"IP_Sarukhanov";
        public static bool IntegratedSecurity { get; set; } = false;
        public static string Login { get; set; } = "SelyakovVA";
        public static string Password { get; set; } = "12345";

        public static bool IsConnectionString { get; set; } = true;
        public static string ConString { get; set; } = @"workstation id=Saruhanov.mssql.somee.com;packet size=4096;user id=freeman_SQLLogin_1;pwd=o6dvuetdvj;data source=Saruhanov.mssql.somee.com;persist security info=False;initial catalog=Saruhanov";

        public static string GetSQLConnection
        {
            get
            {
                return String.Format(@"Data Source={0};Initial Catalog={1};Integrated Security={2}",
                    SQLServerName,
                    DataBaseName,
                    IntegratedSecurity);
            }
        }

        /// <summary>
        /// Получить строку подключения к базе данных.
        /// </summary>
        public static string MainSqlConnection
        {
            get
            {
                if (IsConnectionString)
                {
                    return ConString;
                }
                else
                {
                    if (IntegratedSecurity)
                    {
                        return String.Format(@"Data Source={0};Initial Catalog={1};Integrated Security={2}",
                        SQLServerName,
                        DataBaseName,
                        IntegratedSecurity);
                    }
                    else
                    {
                        return String.Format(@"Data Source={0};Initial Catalog={1};User Id={2};Password={3}",
                        SQLServerName,
                        DataBaseName,
                        Login,
                        Password);
                    }
                }

                /*if (IntegratedSecurity)
                {
                    return String.Format(@"Data Source={0};Initial Catalog={1};Integrated Security={2}",
                    SQLServerName,
                    DataBaseName,
                    IntegratedSecurity);
                    //return ConString;
                }
                else if (IsConnectionString)
                {
                    return ConString;
                }
                else
                {
                    return String.Format(@"Data Source={0};Initial Catalog={1};User Id={2};Password={3}",
                    SQLServerName,
                    DataBaseName,
                    Login,
                    Password);

                    //@"Data Source=.\SQLEXPRESS;Initial Catalog=usersdb;User Id = sa; Password = 1234567fd";";
                }*/
            }
        }

        /// <summary>
        /// Получить строку подключения к базе данных.
        /// </summary>
        //public static string MainSqlConnection
        //{
        //    get { return ConfigurationManager.ConnectionStrings["DefaultSQLConnection"].ConnectionString; }
        //}
    }
}
