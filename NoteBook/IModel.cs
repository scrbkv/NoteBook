using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace NoteBook
{ 
    public delegate void DBUpdatedHandler(List<Record> data);
    public delegate void IncorrectRecordHandler(ErrorStruct errStruct);
    public delegate void ConnectionErrorHandler();

    interface IModel
    {
        event DBUpdatedHandler DBUpdated;
        event IncorrectRecordHandler IncorrectRecord;
        event ConnectionErrorHandler ConnectionError;

        ErrorStruct AddRecord(Record record);
        bool DeleteRecord(Guid recordUid);
        ErrorStruct CheckRecord(Record record);

        List<Record> GetRecords();
    }

    class Model : IModel
    {
        private string _connectStr;

        public Model()
        {
            _connectStr = "server=localhost;user=root;database=users_base;password=rootme";
        }

        public event DBUpdatedHandler DBUpdated;
        public event IncorrectRecordHandler IncorrectRecord;
        public event ConnectionErrorHandler ConnectionError;

        public ErrorStruct AddRecord(Record user)
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
                string sql = "INSERT INTO users_table (id, login, password, name, second_name, surname, initials, position) VALUES (" + user.Id + "'" + user.Username + "','" + user.Password + "','" + user.Name + "','" + user.SecondName + "','" + user.Surname + "','" + user.Initials + "','" + user.Position + "')";

                command = new MySqlCommand(sql, connection);
                command.ExecuteNonQuery();
            }
            else
            {

            }

            connection.Close();
            return new ErrorStruct();
        }

        public ErrorStruct CheckRecord(Record record)
        {
            return new ErrorStruct();
        }

        public bool DeleteRecord(Guid recordUid)
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
            return true;
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

            foreach (DataRow data in dt.Rows)
            {
                list.Add(new Record());
            }

            connection.Close();
            return list;
        }
    }
}
