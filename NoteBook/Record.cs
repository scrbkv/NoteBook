﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBook
{
    public class Record
    {        
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string SecondName { get; set; }
        public string Position { get; set; }
        public string Initials { get; set; }

        public Record(string Login, string Password, string Name, string Surname, string SecondName, string Position)
        {           
            this.Login = Login;
            this.Password = Password;
            this.Name = Name;
            this.Surname = Surname;
            this.SecondName = SecondName;
            this.Position = Position;            
        }

        public Record()
        {

        }
    }

    public class ErrorStruct
    {
        public enum PassStrength { Low, Medium, High }
        public ErrorStruct()
        {
            Correct = true;
        }

        public ErrorStruct(bool login, bool firstname, bool secondName, bool surname, PassStrength strength)
        {
            Correct = false;

            PWStrength = strength;
            IncorrectLogin = !login;
            IncorrectFirstName = !firstname;
            IncorrectSecondName = !secondName;
            IncorrectSurname = !surname;
        }

        public static bool operator !=(ErrorStruct left, bool right)
        {
            return left.Correct != right;
        }

        public static bool operator ==(ErrorStruct left, bool right)
        {
            return left.Correct == right;
        }

        public PassStrength PWStrength { get; }
        public bool IncorrectLogin { get; }
        public bool IncorrectFirstName { get; }
        public bool IncorrectSecondName { get; }
        public bool IncorrectSurname { get; }

        public bool Correct { get; }

        public override bool Equals(object obj)
        {
            var @struct = obj as ErrorStruct;
            return @struct != null &&
                   Correct == @struct.Correct;
        }

        public override int GetHashCode()
        {
            var hashCode = -2026850220;
            hashCode = hashCode * -1521134295 + PWStrength.GetHashCode();
            hashCode = hashCode * -1521134295 + IncorrectLogin.GetHashCode();
            hashCode = hashCode * -1521134295 + IncorrectFirstName.GetHashCode();
            hashCode = hashCode * -1521134295 + IncorrectSecondName.GetHashCode();
            hashCode = hashCode * -1521134295 + IncorrectSurname.GetHashCode();
            hashCode = hashCode * -1521134295 + Correct.GetHashCode();
            return hashCode;
        }
    }
}
