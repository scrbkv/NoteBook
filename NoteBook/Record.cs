using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBook
{
    public class Record
    {
        public Guid Id { get; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string SecondName { get; set; }
        public string Position { get; set; }
        public string Initials { get; set; }

        public Record()
        {
            Id = Guid.NewGuid();
        }
    }

    public class ErrorStruct
    {
        public enum PassStrength { Low, Medium, High }
        ErrorStruct()
        {
            Correct = true;
        }

        ErrorStruct(bool login, bool firstname, bool secondName, bool surname, PassStrength strength)
        {
            Correct = false;

            PWStrength = strength;
            IncorrectLogin = !login;
            IncorrectFirstName = !firstname;
            IncorrectSecondName = !secondName;
            IncorrectSurname = !surname;
        }

        PassStrength PWStrength { get; }
        bool IncorrectLogin { get; }
        bool IncorrectFirstName { get; }
        bool IncorrectSecondName { get; }
        bool IncorrectSurname { get; }

        bool Correct { get; }
    }
}
