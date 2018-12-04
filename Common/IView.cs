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
    public delegate void SearchHandler(string text);
    public delegate void NeedToUpdateHandler();
    public delegate void ReplaceUserHandler(Record record);
    public delegate void ConnectionHandler(string ip, string user, string dataBase, string password);

    public interface IView
    {
        event UserModifiedHandler UserModified;
        event UserDeletedHandler UserDeleted;
        event SaveUserHandler SaveUser;
        event SearchHandler Search;
        event NeedToUpdateHandler NeedToUpdate;
        event ReplaceUserHandler ReplaceUser;
        event ConnectionHandler ConnectToDB;

        void Update(List<Record> records);
        void IncorrectData(ErrorStruct error);
        void StartApp();
        void UpdatePositions(List<string> positions);
        void Connection(bool success);
    }

}
