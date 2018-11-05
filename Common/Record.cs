using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBook
{
    public class Record
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }
        public string Password { get; set; }

        public Record()
        {
            Login = "";
            Password = "";
            Name = "";
            Surname = "";
            SecondName = "";
            Position = "";
        }

        public Record(string login, string password, string name, string surname, string secondName, string position)
        {
            Login = login;
            Password = password;
            Name = name;
            Surname = surname;
            SecondName = secondName;
            Position = position;
        }
    }
}
