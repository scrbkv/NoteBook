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
using NoteBook;

namespace View
{
    public partial class MainWindow : Window, IView
    {
        public event UserModifiedHandler UserModified;
        public event UserDeletedHandler UserDeleted;
        public event SaveUserHandler SaveUser;
        public event SearchHandler Search;
        public event NeedToUpdateHandler NeedToUpdate;

        private EditWindow editWindow;

        public MainWindow()
        {
            InitializeComponent();
            this.Records.CanUserAddRows = false;
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
            this.editWindow = new EditWindow(new Record());
            this.editWindow.ApplyChanges += EditWindow_ApplyChanges;
            this.editWindow.RecordModified += EditWindow_RecordModified;
            editWindow.ShowDialog();
        }

        public void StartApp()
        {
            this.NeedToUpdate();
            this.ShowDialog();
        }

        private void Records_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.Records.SelectedIndex != -1)
            {
                this.EditButton.IsEnabled = true;
                this.DeleteButton.IsEnabled = true;
            }
            else
            {
                this.EditButton.IsEnabled = false;
                this.DeleteButton.IsEnabled = false;
            }
        }

        private void LoginSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Search(new SearchStruct(SearchStruct.SubjectEnum.login, LoginSearch.Text));
        }

        private void NameSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Search(new SearchStruct(SearchStruct.SubjectEnum.name, NameSearch.Text));
        }

        private void SurnameSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Search(new SearchStruct(SearchStruct.SubjectEnum.surname, SurnameSearch.Text));
        }

        private void PositionSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Search(new SearchStruct(SearchStruct.SubjectEnum.position, PositionSearch.Text));
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Records.SelectedIndex != -1)
                this.UserDeleted(this.Records.SelectedItem as Record);
        }

        private void MenuItem_Edit(object sender, RoutedEventArgs e)
        {
            this.editWindow = new EditWindow(this.Records.Items[this.Records.SelectedIndex] as Record);
            this.editWindow.ApplyChanges += EditWindow_ApplyChanges;
            this.editWindow.RecordModified += EditWindow_RecordModified;
            this.editWindow.ShowDialog();
        }

        private void MenuItem_Delete(object sender, MouseButtonEventArgs e)
        {

        }

        private void Records_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            this.SaveUser(this.Records.Items[this.Records.SelectedIndex] as Record);
        }
    }
}
