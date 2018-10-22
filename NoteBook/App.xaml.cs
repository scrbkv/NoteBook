using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace NoteBook
{
    public partial class App : Application
    {
        [STAThread]
        public static void Main()
        {
            Presenter presenter = new Presenter();
        }
    }
}
