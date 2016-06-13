using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Genetec_Web.Models.Item
{
    public class Types
    {
        public Parameters parameters = new Parameters();
        public List<Type> List = new List<Type>();

        public Types()
        {
            Import();
        }

        public void Import()
        {
            MySqlConnection conn = Database.GetConnection();
            DataTable dt = new DataTable();
            dt.Clear();
            MySqlCommand cmd = new MySqlCommand("Types_List", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter da = new MySqlDataAdapter();

            try
            {
                conn.Open();

                da.SelectCommand = cmd;
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    int id = int.Parse(row["ID"].ToString());
                    String name = row["Name"].ToString();
                    Boolean bulkQty = int.Parse(row["BulkQty"].ToString()) == 1;
                    Boolean canRent = int.Parse(row["CanRent"].ToString()) == 1;

                    List.Add(new Type(this, name, bulkQty, canRent, id));
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

        public void Reset()
        {
            List.Clear();
            Import();

            //Maybe? 
            parameters.Reset();
        }

        public void Add(Type type)
        {
            MySqlConnection conn = Database.GetConnection();

            try
            {
                MySqlCommand cmd = new MySqlCommand("Type_Add", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("?tid", MySqlDbType.Int32));
                cmd.Parameters["?tid"].Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("?name", type.Name);
                cmd.Parameters["?name"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?bulkQty", type.BulkQuantity);
                cmd.Parameters["?bulkQty"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?canRent", type.CanRent);
                cmd.Parameters["?canRent"].Direction = ParameterDirection.Input;

                conn.Open();
                Int64 retval = (Int64)cmd.ExecuteNonQuery();

                // If No error in DB
                if (retval == 1)
                {
                    type.ID = (int)cmd.Parameters["?tid"].Value;
                    if (type.ID != 0) List.Add(type);
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

        public void Modify(Type type)
        {
            MySqlConnection conn = Database.GetConnection();

            try
            {
                MySqlCommand cmd = new MySqlCommand("Type_Modify", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("?tid", type.ID);
                cmd.Parameters["?tid"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?name", type.Name);
                cmd.Parameters["?name"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?bulkQty", type.BulkQuantity);
                cmd.Parameters["?bulkQty"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?canRent", type.CanRent);
                cmd.Parameters["?canRent"].Direction = ParameterDirection.Input;

                conn.Open();
                Int64 retval = (Int64)cmd.ExecuteNonQuery();

                // If No error in DB
                if (retval == 1)
                {
                    List.Remove(List.Find(x => x.ID == type.ID));
                    List.Add(type);
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

        public void Delete(Type type)
        {
            Delete(type.ID);
        }

        public void Delete(int typeID)
        {
            MySqlConnection conn = Database.GetConnection();

            try
            {
                MySqlCommand cmd = new MySqlCommand("Type_Delete", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("?tid", typeID);
                cmd.Parameters["?tid"].Direction = ParameterDirection.Input;

                conn.Open();
                Int64 retval = (Int64)cmd.ExecuteNonQuery();

                // If No error in DB
                if (retval == 1)
                    List.Remove(List.Find(x => x.ID == typeID));
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