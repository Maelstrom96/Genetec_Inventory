using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Genetec_Web.Models
{
    public static class Buildings
    {
        public static List<Building> cacheBuildings;

        public static List<Building> GetBuildings()
        {
            List<Building> buildings = new List<Building>();

            MySqlConnection conn = Database.GetConnection();
            DataTable dt = new DataTable();
            dt.Clear();
            MySqlCommand cmd = new MySqlCommand("Buildings_List", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter da = new MySqlDataAdapter();

            try
            {
                conn.Open();

                da.SelectCommand = cmd;
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                    buildings.Add(new Building(int.Parse(row["ID"].ToString()), row["Name"].ToString(), row["City"].ToString()));
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }
            cacheBuildings = buildings;
            return buildings;
        }
    }
}