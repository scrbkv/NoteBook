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

    interface IModel
    {
        event DBUpdatedHandler DBUpdated;
        event IncorrectRecordHandler IncorrectRecord;

        ErrorStruct AddRecord(Record record);
        bool DeleteRecord(Record record);
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

        public ErrorStruct AddRecord(Record user)
        {
            MySqlConnection connection = new MySqlConnection(_connectStr);

            connection.Open();

            string check = "SELECT name FROM users_table WHERE id = " + user.Login;
            MySqlCommand command = new MySqlCommand(check, connection);
            string sql = "";
            command.ExecuteNonQuery();

            ErrorStruct error = new ErrorStruct();
            bool login = true;
            bool firstName = true;
            bool secondName = true;
            bool surname = true;
            ErrorStruct.PassStrength password = new ErrorStruct.PassStrength();
            if (!char.IsUpper(user.Name[0]))
                login = false;
            if (!char.IsUpper(user.SecondName[0]))
                firstName = false;
            if (!char.IsUpper(user.Surname[0]))
                secondName = false;
            if (!char.IsUpper(user.Name[0]))
                login = false;

            if (command.ExecuteScalar() != null)
                sql = "INSERT INTO users_table (login, password, name, second_name, surname, initials, position) VALUES ('" + user.Login + "','" + user.Password + "','" + user.Name + "','" + user.SecondName + "','" + user.Surname + "','" + user.Initials + "','" + user.Position + "')";
            else
                sql = "REPLACE INTO users_table (login, password, name, second_name, surname, initials, position) VALUES ('" + user.Login + "','" + user.Password + "','" + user.Name + "','" + user.SecondName + "','" + user.Surname + "','" + user.Initials + "','" + user.Position + "')";


            command = new MySqlCommand(sql, connection);
            command.ExecuteNonQuery();

            connection.Close();
            return new ErrorStruct();
        }

        public ErrorStruct CheckRecord(Record record)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRecord(Record record)
        {
           MySqlConnection connection = new MySqlConnection(_connectStr);

            try
            {
                connection.Open();

                string sql = "DELETE FROM users_table WHERE login = \"" + record.Login + "\"";

                MySqlCommand command = new MySqlCommand(sql, connection);
                command.ExecuteNonQuery();

                connection.Close();
            }
            catch(Exception e)
            {
                return false;
            }

           return true;
        }


        public List<Record> GetRecords()
        {
            MySqlConnection connection = new MySqlConnection(_connectStr);
            
            connection.Open();             

            List<Record> list = new List<Record>();
            MySqlCommand command = new MySqlCommand("SELECT * FROM table", connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable dt = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(dt);

            foreach (DataRow data in dt.Rows)
            {
                list.Add(new Record(data[0].ToString(), data[1].ToString(), data[2].ToString(), data[3].ToString(), data[4].ToString(), data[5].ToString()));
            }

            connection.Close();
            return list;
        }
    }
}
