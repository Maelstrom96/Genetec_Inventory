using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Genetec_Web.Models.Inventory.Location
{
    public class Rooms : List<Room>
    {
        private Buildings Buildings;

        public Rooms(ref Buildings buildings)
        {
            // Save ref to buildings
            Buildings = buildings;

            this.AddRange(GetRooms());
        }

        public List<Room> GetRooms()
        {
            List<Room> Rooms = new List<Room>();

            MySqlConnection conn = Database.GetConnection();
            DataTable dt = new DataTable();
            dt.Clear();
            MySqlCommand cmd = new MySqlCommand("Rooms_List", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter da = new MySqlDataAdapter();

            try
            {
                conn.Open();

                da.SelectCommand = cmd;
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    Building building = Buildings.Find(x => x.ID == int.Parse(row["Building"].ToString()));

                    Rooms.Add(new Room(int.Parse(row["ID"].ToString()), row["Name"].ToString(), row["Description"].ToString(), building, short.Parse(row["Floor"].ToString())));
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }
            return Rooms;
        }

        public void AddRoom(Room room)
        {

        }
    }
}