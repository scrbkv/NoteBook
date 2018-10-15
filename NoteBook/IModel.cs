using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace NoteBook
{

    public delegate void DBUpdatedHandler(string newState);
    public delegate void IncorrectRecordHandler();

    interface IModel
    {
        event DBUpdatedHandler DBUpdated;
        event IncorrectRecordHandler IncorrectRecord;

        void AddRecord(ref Record record);
        void DeleteRecord(ref Record user);
        void EditRecord(ref Record record);

        List<Record> GetRecords();
    }

    class Model : IModel
    {
        private string _connectStr;

        public Model()
        {
            _connectStr = "server=localhost;user=root;database=users_base;password=rootme";
        }
        public void AddRecord(ref Record user)
        {
            MySqlConnection connection = new MySqlConnection(_connectStr);

            try
            {
                Console.WriteLine("Openning Connection ...");

                connection.Open();

                Console.WriteLine("Connection successful!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            string check = "SELECT name FROM users_table WHERE id = " + user.id;
            MySqlCommand command = new MySqlCommand(check, connection);
            command.ExecuteNonQuery();

            if (command.ExecuteScalar() != null)
            {
                user.id = Guid.NewGuid();
                string sql = "INSERT INTO users_table (id, login, password, name, second_name, surname, initials, position) VALUES (" + user.id + "'" + user.login + "','" + user.password + "','" + user.name + "','" + user.secondName + "','" + user.surname + "','" + user.initials + "','" + user.position + "')";

                command = new MySqlCommand(sql, connection);
                command.ExecuteNonQuery();
            }
            else
            {

            }

            connection.Close();
        }

        public void DeleteRecord(ref Record user)
        {
            MySqlConnection connection = new MySqlConnection(_connectStr);

            try
            {
                Console.WriteLine("Openning Connection ...");

                connection.Open();

                Console.WriteLine("Connection successful!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            string sql = "DELETE FROM users_table WHERE login = \"" + user.login + "\"";

            MySqlCommand command = new MySqlCommand(sql, connection);
            command.ExecuteNonQuery();

            connection.Close();
        }

        public void EditRecord(ref Record user)
        {
            MySqlConnection connection = new MySqlConnection(_connectStr);

            try
            {
                Console.WriteLine("Openning Connection ...");

                connection.Open();

                Console.WriteLine("Connection successful!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            string sql = "REPLACE INTO users_table (id, login, password, name, second_name, surname, initials, position) VALUES (" + user.id + "'" + user.login + "','" + user.password + "','" + user.name + "','" + user.secondName + "','" + user.surname + "','" + user.initials + "','" + user.position + "')";
            MySqlCommand command = new MySqlCommand(sql, connection);

            command = new MySqlCommand(sql, connection);
            command.ExecuteNonQuery();

            connection.Close();
        }

        public List<Record> GetRecords()
        {
            MySqlConnection connection = new MySqlConnection(_connectStr);

            try
            {
                Console.WriteLine("Openning Connection ...");

                connection.Open();

                Console.WriteLine("Connection successful!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            List<Record> list = new List<Record>();
            MySqlCommand command = new MySqlCommand("SELECT * FROM table", connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable dt = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                list.Add(new Record());
            }

            connection.Close();
            return list;
        }
    }
}
