﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBook
{
    public delegate void UserModifiedHandler(Record record);
    public delegate void UserAddedHandler(Record record);
    public delegate void UserDeletedHandler(Guid recordUid);

    interface IView
    {
        event UserModifiedHandler UserModified;
        event UserAddedHandler UserAdded;
        event UserDeletedHandler UserDeleted;

        void Update(List<Record> records);
    }

}
