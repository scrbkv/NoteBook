using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBook
{
    class Presenter
    {
        private IModel model;
        private IView view;

        public Presenter()
        {
            this.model = new Model();
            this.view = new View();

            model.IncorrectRecord += Model_IncorrectRecord;
            model.DBUpdated += Model_DBUpdated;
            view.SaveUser += View_SaveUser;
            view.UserDeleted += View_UserDeleted;
            view.UserModified += View_UserModified;
        }

        private void View_UserModified(Record record)
        {
            throw new NotImplementedException();
        }

        private void View_UserDeleted(Guid recordUid)
        {
            model.DeleteRecord(recordUid);
        }

        private void View_SaveUser(Record record)
        {
            model.AddRecord(ref record);
        }

        private void Model_DBUpdated(string newState)
        {
            view.Update()
        }

        private void Model_IncorrectRecord(ErrorStruct errStruct)
        {
            view.IncorrectData();
        }
    }
}
