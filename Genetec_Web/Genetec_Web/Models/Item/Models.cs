using Genetec_Web.Models.Item;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Genetec_Web.Models.Item
{
    public class Models
    {
        private Types types;
        List<Model> List = new List<Model>();

        public Models(ref Types types_class)
        {
            types = types_class;
            Import();
        }

        private void Import()
        {
            MySqlConnection conn = Database.GetConnection();
            DataTable dt = new DataTable();
            dt.Clear();
            MySqlCommand cmd = new MySqlCommand("Models_List", conn);
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
                    String value = row["Model"].ToString();
                    String description = row["Description"].ToString();
                    int type_id = int.Parse(row["Item_Type"].ToString());

                    Type type = types.List.Find(x => x.ID == type_id);

                    List.Add(new Model(id, value, description, ref type));
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
        }

        public void Add(Model model)
        {
            MySqlConnection conn = Database.GetConnection();
            int id;

            try
            {
                MySqlCommand cmd = new MySqlCommand("Model_Add", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("?mid", MySqlDbType.Int32));
                cmd.Parameters["?mid"].Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("?value", model.Value);
                cmd.Parameters["?value"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?description", model.Description);
                cmd.Parameters["?description"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?type", model.type.ID);
                cmd.Parameters["?type"].Direction = ParameterDirection.Input;

                conn.Open();
                int retval = (int)cmd.ExecuteNonQuery();

                if (retval == 1)
                {
                    id = (int)cmd.Parameters["?mid"].Value;
                    Type type = model.type;
                    if (id != 0) List.Add(new Model(id, model.Value, model.Description, ref type));
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

        public void Modify(Model model)
        {
            MySqlConnection conn = Database.GetConnection();

            try
            {
                MySqlCommand cmd = new MySqlCommand("Model_Modify", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("?mid", model.ID);
                cmd.Parameters["?mid"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?value", model.Value);
                cmd.Parameters["?value"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?description", model.Description);
                cmd.Parameters["?description"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?type", model.type.ID);
                cmd.Parameters["?type"].Direction = ParameterDirection.Input;

                conn.Open();
                Int64 retval = (Int64)cmd.ExecuteNonQuery();

                // If No error in DB
                if (retval == 1)
                {
                    Model m = List.Find(x => x.ID == model.ID);
                    m.Value = model.Value;
                    m.Description = model.Description;
                    m.type = model.type;
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

        public void Delete()
        {
            // Do I want to be able to delete a Model? 
            /*
            MySqlConnection conn = Database.GetConnection();

            try
            {
                MySqlCommand cmd = new MySqlCommand("Model_Delete", conn);
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
            */
        }
    }
}