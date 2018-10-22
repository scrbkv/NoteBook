﻿using System;
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
            view.UserDeleted += View_UserDeleted;
            view.UserModified += View_UserModified;
            view.Search += View_Search;
            view.NeedToUpdate += View_NeedToUpdate;
            view.StartApp();
        }

        private void View_NeedToUpdate()
        {
            view.Update(model.GetRecords());
        }

        private void View_Search(SearchStruct searchStruct)
        {
            view.Update(model.GetRecords(searchStruct));
        }

        private void Model_DBUpdated(List<Record> data)
        {
            view.Update(data);
        }

        private void View_UserModified(Record record)
        {
            var errStruct = model.CheckRecord(record);
            if (errStruct == false)
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
