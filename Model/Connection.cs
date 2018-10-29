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

        public Connection()
        {

        }

        public void Connect(string ip, string user, string dataBase, string password)
        {

            _connectStr = "server=" + ip + ";user=" + user + ";database=" + dataBase + ";password=" + password;
            connection = new MySqlConnection(_connectStr);
            connection.Open();
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
            string sql = "INSERT INTO user (login, password, name, second_name, surname, position) VALUES ('" + user.Login + "','" + user.Password + "','" + user.Name + "','" + user.SecondName + "','" + user.Surname + "','" + user.Position + "')";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.ExecuteNonQuery();            
        }

        public void Replace(Record user)
        {
            string sql = "REPLACE INTO user (login, password, name, second_name, surname, position) VALUES ('" + user.Login + "','" + user.Password + "','" + user.Name + "','" + user.SecondName + "','" + user.Surname + "','" + user.Position + "')";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        public void Delete(Record user)
        {
            string sql = "DELETE FROM user WHERE login = \"" + user.Login + "\"";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.ExecuteNonQuery();            
        }

        public DataTable Find(string str)
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM user WHERE * LIKE '%" + str + "%'", connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();           

            adapter.SelectCommand = command;
            adapter.Fill(dt);
            command = new MySqlCommand("SELECT positionID FROM positions WHERE position LIKE '%" + str + "%'", connection);
            adapter.SelectCommand = command;
            adapter.Fill(dt1);

            dt.Columns.Add("position", typeof(string));

            foreach (DataRow row in dt.Rows)
            {
                command = new MySqlCommand("SELECT position_id FROM user_positions WHERE login_id ='" + row[0].ToString() + "'", connection);
                int pos_id = (int)command.ExecuteScalar();
                command = new MySqlCommand("SELECT position FROM positions WHERE positionID ='" + pos_id + "'", connection);
                row[5] = command.ExecuteScalar().ToString();
            }

            return dt;
        }

        public DataTable GetPos()
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM positions", connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable dt = new DataTable();
           
            return dt;
        }

        public DataTable Get()
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM user", connection);                      
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable dt = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(dt);           

            return dt;
        }
    }
}
