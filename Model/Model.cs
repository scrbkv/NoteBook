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
       
        }

        public void Connect(string ip, string user, string dataBase, string password)
        {
            connection = new Connection();
            connection.Connect(ip, user, dataBase, password);
        }

        public event DBUpdatedHandler DBUpdated;
        public event IncorrectRecordHandler IncorrectRecord;

        public ErrorStruct AddRecord(Record user)
        {
            if (!connection.Existing(user))
                connection.Add(user);
            else
                connection.Replace(user);
           
            return new ErrorStruct();
        }

        public ErrorStruct CheckRecord(Record user)
        {          
            CheckPassword check = new CheckPassword();
            bool login = true;
            bool firstName = true;
            bool secondName = true;
            bool surname = true;            
            ErrorStruct.PassStrength password = new ErrorStruct.PassStrength();            
            if (!char.IsUpper(user.SecondName[0]))
                firstName = false;
            if (!char.IsUpper(user.Surname[0]))
                secondName = false;
            if (!char.IsUpper(user.Name[0]))
                firstName = false;

            if (check.CheckPass(user.Password) <= 2)
                password = ErrorStruct.PassStrength.Low;
            else if (check.CheckPass(user.Password) == 3 || check.CheckPass(user.Password) == 4)
                password = ErrorStruct.PassStrength.Medium;
            else
                password = ErrorStruct.PassStrength.High;

            login = connection.Existing(user);           

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

           return true;
        }

        public List<Record> Find()
        {
            return new List<Record>();
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

        public bool DeleteRecord(Guid recordUid)
        {
            throw new NotImplementedException();
        }
    }
}
