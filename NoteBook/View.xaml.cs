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
        public View()
        {
            InitializeComponent();
        }

        public event UserModifiedHandler UserModified;
        public event UserAddedHandler UserAdded;
        public event UserDeletedHandler UserDeleted;

        public void Update(List<Record> records)
        {
            this.Records.ItemsSource = records;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            EditWindow editWindow = new EditWindow(new Record());
            editWindow.ShowDialog();
        }
    }
}
