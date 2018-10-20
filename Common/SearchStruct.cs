using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBook
{
    public class SearchStruct
    {
        public string _searchStr;
        public enum SearchField { login, name, surname, secondName, position };
        public SearchField Field { get; set; }
        
        public SearchStruct()
        {

        }

        public SearchStruct(string searchString, SearchField field)
        {
            _searchStr = searchString;
            Field = field;
        }
    }
}
