using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBook
{
    public class SearchStruct
    {
        public enum SubjectEnum { login, name, surname, second_name, position};
        public SubjectEnum Subject { get; }
        public string Text { get; }
        public SearchStruct(SubjectEnum subject, string text)
        {
            Subject = subject;
            Text = text;
        }
    }
}
