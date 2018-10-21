using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBook
{
    public delegate void UserModifiedHandler(Record record);
    public delegate void UserDeletedHandler(Record record);
    public delegate void SaveUserHandler(Record record);
    public delegate void SearchHandler(SearchStruct searchStruct);

    public interface IView
    {
        event UserModifiedHandler UserModified;
        event UserDeletedHandler UserDeleted;
        event SaveUserHandler SaveUser;
        event SearchHandler Search;

        void Update(List<Record> records);
        void IncorrectData(ErrorStruct error);
        void StartApp();
    }

}
