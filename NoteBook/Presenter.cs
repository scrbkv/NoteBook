using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace NoteBook
{
    class Presenter
    {
        private IModel model;
        private IView view;

        public Presenter()
        {
            this.model = new Model.Model();            
            this.view = new View.MainWindow();

            model.IncorrectRecord += Model_IncorrectRecord;
            model.DBUpdated += Model_DBUpdated;
            view.SaveUser += View_SaveUser;
            view.ReplaceUser += View_ReplaceUser;
            view.UserDeleted += View_UserDeleted;
            view.UserModified += View_UserModified;
            view.Search += View_Search;
            view.NeedToUpdate += View_NeedToUpdate;
            view.ConnectToDB += View_ConnectToDB;
            view.UpdatePositions(model.GetPositions());
            view.UpdatePositions(model.GetPositions());
            view.StartApp();
        }

        private void View_ConnectToDB(string ip, string user, string dataBase, string password)
        {
            view.Connection(model.Connect(ip, user, dataBase, password));
        }

        private void View_ReplaceUser(Record record)
        {
            model.ReplaceRecord(record);
        }

        private void View_NeedToUpdate()
        {
            view.Update(model.GetRecords());
        }

        private void View_Search(string text)
        {
            view.Update(model.GetRecords(text));
        }

        private void Model_DBUpdated(List<Record> data)
        {
            view.Update(data);
        }

        private void View_UserModified(Record record)
        {
            var errStruct = model.CheckRecord(record);
            view.IncorrectData(errStruct);
        }

        private void View_UserDeleted(Record user)
        {
            model.DeleteRecord(user);
        }

        private void View_SaveUser(Record record)
        {
            model.AddRecord(record);
        }

        private void Model_IncorrectRecord(ErrorStruct errStruct)
        {
            view.IncorrectData(errStruct);
        }
    }
}
