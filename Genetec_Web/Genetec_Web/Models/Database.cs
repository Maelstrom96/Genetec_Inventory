using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Genetec_Web.Models
{
    public static class Database
    {
        static String Con_String = "Server=" + Properties.Settings.Default.Db_IP
            +"; Database=" + Properties.Settings.Default.Db_Name
            +"; Uid=" + Properties.Settings.Default.Db_Username
            +"; Pwd=" + Properties.Settings.Default.Db_Password + ";";

        public static MySqlConnection GetConnection()
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = Con_String;

            return conn;
        }
    }
}