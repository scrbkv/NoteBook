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
using System.Collections.ObjectModel;

namespace View
{
    public partial class MainWindow : Window, IView
    { 
        public event UserModifiedHandler UserModified;
        public event UserDeletedHandler UserDeleted;
        public event SaveUserHandler SaveUser;
        public event SearchHandler Search;
        public event NeedToUpdateHandler NeedToUpdate;
        public event ReplaceUserHandler ReplaceUser;

        private EditWindow editWindow;
        public static List<string> Positions { get; set; } = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            this.Records.CanUserAddRows = false;
            this.DataContext = this;
        }

        private void EditWindow_RecordModified(Record record)
        {
            this.UserModified(record);
        }

        private void EditWindow_ApplyChanges(Record record)
        {
            this.ReplaceUser(record);
        }

        public void IncorrectData(ErrorStruct error)
        {
            this.editWindow.IncorrectData(error);
        }

        public void Update(List<Record> records)
        {
            //this.Records.Items.Clear();
            this.Records.ItemsSource = records;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            this.editWindow = new EditWindow(Positions);
            this.editWindow.ApplyChanges += EditWindow_ApplyChanges;
            this.editWindow.RecordModified += EditWindow_RecordModified;
            this.editWindow.AddNewRecord += EditWindow_AddNewRecord;
            editWindow.ShowDialog();
        }

        private void EditWindow_AddNewRecord(Record record)
        {
            this.SaveUser(record);
        }

        public void StartApp()
        {
            this.NeedToUpdate();
            this.ShowDialog();
        }

        private void Records_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.Records.SelectedIndex >= 0)
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
            this.Search(this.SearchText.Text);
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Records.SelectedIndex != -1)
            {
                MessageBoxResult result = MessageBox.Show( "Действительно удалить пользователя?", "Подтверждение", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                    this.UserDeleted(this.Records.SelectedItem as Record);
            }
            this.SearchText.Text = "";
        }

        private void MenuItem_Edit(object sender, RoutedEventArgs e)
        {
            this.editWindow = new EditWindow(this.Records.Items[this.Records.SelectedIndex] as Record, Positions);
            this.editWindow.ApplyChanges += EditWindow_ApplyChanges;
            this.editWindow.RecordModified += EditWindow_RecordModified;
            this.editWindow.AddNewRecord += EditWindow_AddNewRecord;
            this.editWindow.ShowDialog();
        }

        private void MenuItem_Delete(object sender, MouseButtonEventArgs e)
        {
            this.UserDeleted(this.Records.SelectedItem as Record);
        }

        private void Records_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            this.ReplaceUser(this.Records.SelectedItem as Record);
        }

        public void UpdatePositions(List<string> positions)
        {
            MainWindow.Positions = positions;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            this.editWindow = new EditWindow(this.Records.Items[this.Records.SelectedIndex] as Record, Positions);
            this.editWindow.ApplyChanges += EditWindow_ApplyChanges;
            this.editWindow.RecordModified += EditWindow_RecordModified;
            this.editWindow.AddNewRecord += EditWindow_AddNewRecord;
            this.editWindow.ShowDialog();
        }

        private void Records_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Records.BeginEdit();
        }

        private void Records_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (this.Records.SelectedIndex != -1)
                {
                    MessageBoxResult result = MessageBox.Show("Подтверждение", "Действительно удалить пользователя?", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                        this.UserDeleted(this.Records.SelectedItem as Record);
                }
                this.SearchText.Text = "";
            }
        }
    }
}
