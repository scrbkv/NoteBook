using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBook
{
    public delegate void DBUpdatedHandler(string newState);
    public delegate void IncorrectRecordHandler();

    interface IModel
    {
        event DBUpdatedHandler DBUpdated;
        event IncorrectRecordHandler IncorrectRecord;

        void AddRecord(ref Record record);
        void DeleteRecord();
        void EditRecord(ref Record record);
        List<Record> GetRecords();
    }
}
