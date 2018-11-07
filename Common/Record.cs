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
        public Guid Idg { get; }
        public string Id { get; }

        public Record()
        {
            Login = "";
            Password = "";
            Name = "";
            Surname = "";
            SecondName = "";
            Position = "";
            Idg = Guid.NewGuid();
            Id = Idg.ToString();
        }

        public Record(string login, string password, string name, string surname, string secondName, string position, string id)
        {
            Login = login;
            Password = password;
            Name = name;
            Surname = surname;
            SecondName = secondName;
            Position = position;
            Id = id;        
        }        
    }
}
