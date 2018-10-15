using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NoteBook
{
    public partial class View : Window, IView
    {
        public event UserModifiedHandler UserModified;
        public event UserDeletedHandler UserDeleted;
        public event SaveUserHandler SaveUser;
        private EditWindow editWindow;

        public View()
        {
            InitializeComponent();
            this.editWindow.ApplyChanges += EditWindow_ApplyChanges;
            this.editWindow.RecordModified += EditWindow_RecordModified;
        }

        private void EditWindow_RecordModified(Record record)
        {
            this.UserModified(record);
        }

        private void EditWindow_ApplyChanges(Record record)
        {
            this.SaveUser(record);
        }

        public void IncorrectData(ErrorStruct error)
        {
            this.editWindow.IncorrectData(error);
        }

        public void Update(List<Record> records)
        {
            this.Records.ItemsSource = records;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            editWindow = new EditWindow(new Record());
            editWindow.ShowDialog();
        }
    }
}
