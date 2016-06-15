using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Genetec_Web.Models.Inventory.Location
{
    public class Room
    {
        private int id;
        public int ID { get { return id; } }
        private String name;
        public String Name { get { return name; } }
        private String description;
        public String Description { get { return description; } }
        public Building Building;
        private short floor;
        public short Floor { get { return floor; } }

        public Room(int _id, String _name, String _description, Building _building, short _floor)
        {
            id = _id;
            name = _name;
            description = _description;
            Building = _building;
            floor = _floor;
        }

        public void Modify(String _name, String _description, Building _building, short _floor)
        {
            MySqlConnection conn = Database.GetConnection();

            try
            {
                MySqlCommand cmd = new MySqlCommand("Room_Modify", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("?rid", ID);
                cmd.Parameters["?rid"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?name", Name);
                cmd.Parameters["?name"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?description", Description);
                cmd.Parameters["?description"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?building", Building.ID);
                cmd.Parameters["?building"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?floor", Floor);
                cmd.Parameters["?floor"].Direction = ParameterDirection.Input;

                conn.Open();
                Int64 retval = (Int64)cmd.ExecuteNonQuery();

                // If No error in DB
                if (retval == 1)
                {
                    name = _name;
                    description = _description;
                    Building = _building;
                    floor = _floor;
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }
        }
    }
}