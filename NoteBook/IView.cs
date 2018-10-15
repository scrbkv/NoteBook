using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBook
{
    public delegate void UserModifiedHandler(Record record);
    public delegate void UserDeletedHandler(Guid recordUid);
    public delegate void SaveUserHandler(Record record);

    interface IView
    {
        event UserModifiedHandler UserModified;
        event UserDeletedHandler UserDeleted;
        event SaveUserHandler SaveUser;

        void Update(List<Record> records);
        void IncorrectData(Record record, ErrorStruct error);
    }

}
