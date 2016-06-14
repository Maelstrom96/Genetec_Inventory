using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;

namespace Genetec_Web.Models
{
    public class Parameters
    {
        public List<Parameter> List = new List<Parameter>();

        public Parameters()
        {
            Import();
        }

        /// <summary>
        /// Import ID and Value of parameters from database
        /// </summary>
        private void Import()
        {
            MySqlConnection conn = Database.GetConnection();
            DataTable dt = new DataTable();
            dt.Clear();
            MySqlCommand cmd = new MySqlCommand("Parameters_List", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter da = new MySqlDataAdapter();

            try
            {
                conn.Open();

                da.SelectCommand = cmd;
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                    List.Add(new Parameter(int.Parse(row["ID"].ToString()), row["Parameter"].ToString()));
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Reset the current local cache and resync with database
        /// </summary>
        public void Reset()
        {
            List.Clear();
            Import();
        }

        /// <summary>
        /// Delete a parameter from database and local cache
        /// </summary>
        /// <param name="ID">ID of parameter</param>
        public void Delete(int ID)
        {
            MySqlConnection conn = Database.GetConnection();

            try
            {
                MySqlCommand cmd = new MySqlCommand("Parameter_Delete", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("?pid", ID);
                cmd.Parameters["?pid"].Direction = ParameterDirection.Input;

                conn.Open();
                Int64 retval = (Int64)cmd.ExecuteNonQuery();

                // If No error in DB and a row got deleted
                if (retval == 1) List.RemoveAll(item => ID == item.ID);
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Add a parameter to database and local cache
        /// </summary>
        public void Add(String value)
        {
            MySqlConnection conn = Database.GetConnection();
            int id;

            try
            {
                MySqlCommand cmd = new MySqlCommand("Parameter_Add", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("?pid", MySqlDbType.Int32));
                cmd.Parameters["?pid"].Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("?value", value);
                cmd.Parameters["?value"].Direction = ParameterDirection.Input;

                conn.Open();
                int retval = (int)cmd.ExecuteNonQuery();

                if (retval == 1)
                {
                    id = (int)cmd.Parameters["?pid"].Value;
                    if (id != 0) List.Add(new Parameter(id, value));
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

        /// <summary>
        /// Modify a parameter in the database and in the local cache
        /// </summary>
        /// <param name="param"></param>
        public void Modify(Parameter param)
        {
            MySqlConnection conn = Database.GetConnection();

            try
            {
                MySqlCommand cmd = new MySqlCommand("Parameter_Modify", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("?pid", param.ID);
                cmd.Parameters["?pid"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?value", param.Key);
                cmd.Parameters["?value"].Direction = ParameterDirection.Input;

                conn.Open();
                int retval = (int)cmd.ExecuteNonQuery();

                if (retval == 1)
                {
                    Parameter parameter = List.Find(x => x.ID == param.ID);
                    parameter.Key = param.Key;
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