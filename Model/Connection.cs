using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using NoteBook;

namespace NoteBook
{
    class Connection
    {
        private string _connectStr;
        MySqlConnection connection;
        List<string> ls;

        public Connection()
        {

        }

        public void Connect(string ip, string user, string dataBase, string password)
        {

            _connectStr = "server=" + ip + ";user=" + user + ";database=" + dataBase + ";password=" + password;
            connection = new MySqlConnection(_connectStr);
            connection.Open();
            ls = new List<string> { "login", "password", "name", "second_name", "surname" };
        }

        ~Connection()
        {
            connection.Close();
        }

        public bool NonExisting(Record user)
        {
            string check = "SELECT name FROM user WHERE login = \"" + user.Login + "\"";
            MySqlCommand command = new MySqlCommand(check, connection);
            command.ExecuteNonQuery();

            if (command.ExecuteScalar() != null)
                return false;

            return true;
        }        

        public void Add(Record user)
        {                       
            MySqlCommand command = new MySqlCommand("SELECT positionID FROM positions WHERE position = '" + user.Position + "'", connection);            
            string sql = "INSERT INTO user (id, login, password, name, second_name, surname, positionID) VALUES ('" + user.Id + "','" + user.Login + "','" + user.Password + "','" + user.Name + "','" + user.SecondName + "','" + user.Surname + "'," + command.ExecuteScalar().ToString() + ")";
            command = new MySqlCommand(sql, connection);
            command.ExecuteNonQuery();

        }

        //public void Replace(string login)
        //{
        //    string sql = "UPDATE user SET " + e + "','" + user.SecondName + "','" + user.Surname + "','" + user.Position + "')";
        //    MySqlCommand command = new MySqlCommand(sql, connection);
        //    command.ExecuteNonQuery();
        //}

        public void Replace(Record user)
        {
            string sql = "REPLACE INTO user (id, login, password, name, second_name, surname, positionID) VALUES ('" + user.Id + "','"+ user.Login + "','" + user.Password + "','" + user.Name + "','" + user.SecondName + "','" + user.Surname + "','" + user.Position + "')";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        public void Delete(Record user)
        {
            string sql = "DELETE FROM user WHERE id = \"" + user.Id + "\"";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.ExecuteNonQuery();            
        }

        public List<Record> Find(string str, List<string> PosList)
        {
            List<Record> list = new List<Record>();                     
            MySqlCommand command = new MySqlCommand("SELECT * FROM user WHERE * LIKE '%" + str + "%'", connection);           
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            command = new MySqlCommand("SELECT positionID, position FROM positions WHERE position LIKE '%" + str + "%'", connection);
            adapter.SelectCommand = command;
            adapter.Fill(dt1);

            foreach (DataRow row in dt1.Rows)
            {
                command = new MySqlCommand("SELECT * FROM user WHERE positionID LIKE '%" + row[0] + "%'", connection);
                adapter.SelectCommand = command;
                adapter.Fill(dt2);

                foreach (DataRow data in dt2.Rows)
                {
                    list.Add(new Record(data[1].ToString(), data[2].ToString(), data[3].ToString(), data[5].ToString(), data[4].ToString(), row[1].ToString(), data[0].ToString()));
                }
            }

            foreach (string st in ls)
            {
                command = new MySqlCommand("SELECT * FROM user WHERE " + st + " LIKE '%" + str + "%'", connection);
                adapter.SelectCommand = command;
                adapter.Fill(dt);
                foreach (DataRow data in dt.Rows)
                {
                    list.Add(new Record(data[1].ToString(), data[2].ToString(), data[3].ToString(), data[5].ToString(), data[4].ToString(), PosList[(int)data[6] - 1].ToString(), data[0].ToString()));
                }
            }                        
           
            var result = list.GroupBy(x => x.Login).Select(x => x.First()).ToList();

            return result;
        }

        public DataTable GetPos()
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM positions", connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable dt = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(dt);

            return dt;
        }

        public DataTable Get()
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM user", connection);                      
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable dt = new DataTable();            

            adapter.SelectCommand = command;
            adapter.Fill(dt);
            dt.Columns.Add("positions");

            foreach (DataRow data in dt.Rows)
            {
                command = new MySqlCommand("SELECT position FROM positions WHERE positionID = " + data[6], connection);
                data["positions"] = command.ExecuteScalar();
            }

            return dt;
        }
    }
}
