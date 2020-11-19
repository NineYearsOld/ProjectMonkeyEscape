using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMonkey
{
    public class DbControl
    {

        string _connStr = "Data Source=.\\SQLEXPRESS;Initial Catalog=monkeyDb;Integrated Security=True";
        private SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(_connStr);
            return connection;
        }
        public void UploadMonkeyData(Monkey monkey, Grove grove, int seqNr)
        {

                SqlConnection connection = GetConnection();
                try
                {

                    string query = "insert into dbo.monkeyrecords(recordid, monkeyid, monkeyname, woodid, seqnr, treeid, x, y) values (@recordid, @monkeyid, @monkeyname, @woodid, @seqnr, @treeid, @x, @y)";
                    connection.Open();
                    using SqlCommand command = connection.CreateCommand();
                    command.Parameters.Add(new SqlParameter("@recordID", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@monkeyID", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@monkeyName", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@woodID", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@seqNr", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@treeID", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@x", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@y", SqlDbType.Int));
                    command.CommandText = query;
                    command.Parameters["@recordID"].Value = GetLastRecord("monkeyrecords", monkey.treeId);
                    command.Parameters["@monkeyID"].Value = monkey.monkeyTag;
                    command.Parameters["@monkeyName"].Value = monkey.monkeyName;
                    command.Parameters["@woodID"].Value = grove.GroveTag;
                    command.Parameters["@seqNr"].Value = seqNr;
                    command.Parameters["@treeID"].Value = monkey.treeId;
                    command.Parameters["@x"].Value = monkey.monkeyX;
                    command.Parameters["@y"].Value = monkey.monkeyY;
                    command.ExecuteNonQuery();

                }

                catch (Exception)
                {

                    throw;
                }

                finally
                {
                    connection.Close();
                }

        }
        int GetLastRecord(string table, int treeId)
        {
            SqlDataReader reader;
            int record = 0;
            string query = $"select count ('{table}') from monkeyrecords";
            SqlCommand command;
            SqlConnection connection = GetConnection();

            try
            {
                connection.Open();
                command = new SqlCommand(query, connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    record = (int)reader.GetValue(0);
                }
            }
            catch (Exception)
            {

                throw;
            }finally
            {
                connection.Close();
            }
            treeId++;
            record = record+treeId;
            Random r = new Random();
            record = r.Next();

            return record;
        }

    }
}
