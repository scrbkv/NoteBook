using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace NoteBook
{

    public delegate void DBUpdatedHandler(List<Record> data);
    public delegate void IncorrectRecordHandler(ErrorStruct errStruct);

    public interface IModel
    {
        event DBUpdatedHandler DBUpdated;
        event IncorrectRecordHandler IncorrectRecord;

        ErrorStruct AddRecord(Record record);
        bool DeleteRecord(Guid recordUid);
        ErrorStruct CheckRecord(Record record);

        List<Record> GetRecords();
    }

   
}
