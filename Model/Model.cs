using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Security.Cryptography;
using NoteBook;

namespace Model
{

    public class Model : IModel
    {        
        private Connection connection;
        private static Random random = new Random((int)DateTime.Now.Ticks);

        private string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        public Model()
        {
            connection = new Connection();
            connection.Connect("localhost", "root", "testdb", "rootme");
        }

        public void Connect(string ip, string user, string dataBase, string password)
        {
            connection.Connect(ip, user, dataBase, password);
        }

        public event DBUpdatedHandler DBUpdated;
        public event IncorrectRecordHandler IncorrectRecord;

        public ErrorStruct ReplaceRecord(Record user)
        {
            ErrorStruct check = CheckRecord(user);

            if (check == true)
            {                
                connection.Replace(user);
            }                         

            this.DBUpdated(GetRecords());
            return check;
        }

        public ErrorStruct AddRecord(Record user)
        {
            ErrorStruct check = CheckRecord(user);

            if (check == true)
            {
                string salt = RandomString(10);
                for (int i = 0; i < 1000; ++i)
                {
                    user.Password += salt;
                    user.Password = GetHashString(user.Password);
                }
                user.Password += "(" + salt + ")";
                connection.Add(user);
            }            

            this.DBUpdated(GetRecords());
            return check;
        }

        private string GetHashString(string str)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(str);

            MD5CryptoServiceProvider CSP = new MD5CryptoServiceProvider();

            byte[] byteHash = CSP.ComputeHash(bytes);

            string hash = string.Empty;

            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return hash;
        }

        public ErrorStruct CheckRecord(Record user)
        {                
            CheckPassword check = new CheckPassword();
            ErrorStruct error;
            bool login = true;
            bool firstName = true;
            bool secondName = true;
            bool surname = true;            
            ErrorStruct.PassStrength password = new ErrorStruct.PassStrength();
            if (user.Surname == "" || !char.IsUpper(user.Surname[0]) || user.Surname.Length > 50)            
                return new ErrorStruct(true, true, true, true, ErrorStruct.PassStrength.Low, false);                               
            else if (user.Name == "" || !char.IsUpper(user.Name[0]) || user.Name.Length > 50)
                return new ErrorStruct(true, true, true, true, ErrorStruct.PassStrength.Low, false);
            else if (user.SecondName == "" || !char.IsUpper(user.SecondName[0]) || user.SecondName.Length > 50)
                return new ErrorStruct(true, true, true, true, ErrorStruct.PassStrength.Low, false);

            else if (user.Password.Length > 50)
                return new ErrorStruct(true, true, true, true, ErrorStruct.PassStrength.Low, false);

            else if (user.Password == "" || check.CheckPass(user.Password) <= 2)
                password = ErrorStruct.PassStrength.Low;
            else if (check.CheckPass(user.Password) == 3 || check.CheckPass(user.Password) == 4)
                password = ErrorStruct.PassStrength.Medium;
            else if (check.CheckPass(user.Password) > 4)
                password = ErrorStruct.PassStrength.High;

            else if (user.Login == "" || user.Login.Length > 50)
                return new ErrorStruct(true, true, true, true, ErrorStruct.PassStrength.Low, false);
            else
                login = connection.NonExisting(user);

            return new ErrorStruct(true, true, true, true, ErrorStruct.PassStrength.Low, false);
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

           // this.DBUpdated(GetRecords());
           return true;
        }

        public List<Record> GetRecords(string str)
        {
            if (str == "")
                return GetRecords();

            return connection.Find(str);
        }

        public List<Record> GetRecords()
        {                        
           List<Record> list = new List<Record>();
           DataTable dt = connection.Get();

            foreach (DataRow data in dt.Rows)
            {
                list.Add(new Record(data[0].ToString(), data[1].ToString(), data[2].ToString(), data[4].ToString(), data[3].ToString(), data[5].ToString()));
            }
            
            return list;
        }

        public List<string> GetPositions()
        {            
            List<string> list = new List<string>();
            DataTable dt = connection.GetPos();

            foreach (DataRow data in dt.Rows)
            {
                list.Add(data[1].ToString());
            }

            return list;
        }
    }
}
