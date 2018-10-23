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
            string check = "SELECT name FROM users_table WHERE login = \"" + user.Login + "\"";
            MySqlCommand command = new MySqlCommand(check, connection);
            command.ExecuteNonQuery();

            if (command.ExecuteScalar() != null)
                return false;

            return true;
        }

        public void Add(Record user)
        {
            string sql = "INSERT INTO users_table (login, password, name, second_name, surname, position) VALUES ('" + user.Login + "','" + user.Password + "','" + user.Name + "','" + user.SecondName + "','" + user.Surname + "','" + user.Position + "')";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        public void Replace(Record user)
        {
            string sql = "REPLACE INTO users_table (login, password, name, second_name, surname, position) VALUES ('" + user.Login + "','" + user.Password + "','" + user.Name + "','" + user.SecondName + "','" + user.Surname + "','" + user.Position + "')";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        public void Delete(Record user)
        {
            string sql = "DELETE FROM users_table WHERE login = \"" + user.Login + "\"";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        public DataTable Find(SearchStruct user)
        {
            MySqlCommand command = new MySqlCommand("SELECT'" + user.Subject + "'FROM users_table WHERE '" + user.Subject +"'='" + user.Text + "'", connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable dt = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(dt);

            return dt;
        }

        public DataTable Get()
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM users_table", connection);                      
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable dt = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(dt);

            return dt;
        }
    }
}
