using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using NoteBook;

namespace Model
{

    public class Model : IModel
    {        
        private Connection connection;

        public Model()
        {
            connection = new Connection();
            connection.Connect("localhost", "root", "users_base", "rootme");
        }

        public void Connect(string ip, string user, string dataBase, string password)
        {
            connection.Connect(ip, user, dataBase, password);
        }

        public event DBUpdatedHandler DBUpdated;
        public event IncorrectRecordHandler IncorrectRecord;

        public ErrorStruct AddRecord(Record user)
        {
            ErrorStruct check = CheckRecord(user);            

            if (check == true)
                connection.Add(user);
            //else
            //    connection.Replace(user);

            this.DBUpdated(GetRecords());
            return check;
        }

        public ErrorStruct CheckRecord(Record user)
        {                
            CheckPassword check = new CheckPassword();
            bool login = true;
            bool firstName = true;
            bool secondName = true;
            bool surname = true;            
            ErrorStruct.PassStrength password = new ErrorStruct.PassStrength();            
            if (user.SecondName == "" || !char.IsUpper(user.SecondName[0]))
                firstName = false;
            if (user.Surname == "" || !char.IsUpper(user.Surname[0]))
                secondName = false;
            if (user.Name == "" || !char.IsUpper(user.Name[0]))
                firstName = false;

            if (user.Password == "" || check.CheckPass(user.Password) <= 2)
                password = ErrorStruct.PassStrength.Low;
            else if (check.CheckPass(user.Password) == 3 || check.CheckPass(user.Password) == 4)
                password = ErrorStruct.PassStrength.Medium;
            else
                password = ErrorStruct.PassStrength.High;

            if (user.Login == "")
                login = false;
            else
                login = connection.NonExisting(user);                        

            return new ErrorStruct(login, firstName, secondName, surname, password);
            //throw new NotImplementedException();
        }

        public bool DeleteRecord(Record record)
        {           
            try
            {
               connection.Delete(record);
            }
            catch(Exception)
            {
                return false;
            }

            this.DBUpdated(GetRecords());
           return true;
        }

        public List<Record> GetRecords(SearchStruct obj)
        {
            List<Record> list = new List<Record>();
            DataTable dt = connection.Find(obj);

            foreach (DataRow data in dt.Rows)
            {
                list.Add(new Record(data[0].ToString(), data[1].ToString(), data[2].ToString(), data[3].ToString(), data[4].ToString(), data[5].ToString()));
            }

            return list;
        }

        public List<Record> GetRecords()
        {                        
           List<Record> list = new List<Record>();
           DataTable dt = connection.Get();

            foreach (DataRow data in dt.Rows)
            {
                list.Add(new Record(data[0].ToString(), data[1].ToString(), data[2].ToString(), data[3].ToString(), data[4].ToString(), data[5].ToString()));
            }
            
            return list;
        }
    }
}
