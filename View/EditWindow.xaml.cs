using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using NoteBook;

namespace View
{
    public delegate void ApplyChangesHandler(Record record);
    public delegate void RecordModifiedHandler(Record record);
    public delegate void AddRecordHandler(Record record);

    public partial class EditWindow : Window
    {
        private Record record;

        public event ApplyChangesHandler ApplyChanges;
        public event RecordModifiedHandler RecordModified;
        public event AddRecordHandler AddNewRecord;

        bool correct = false;
        bool edit;

        public EditWindow(List<string> positions)
        {
            InitializeComponent();
            record = new Record();
            this.Position.ItemsSource = positions;

            edit = false;

            this.Surname.Text = "Фамилия";
            this.FirstName.Text = "Имя";
            this.SecondName.Text = "Отчество";
            this.Login.Text = "Логин";
            this.Password.Text = "Пароль";

            this.Surname.GotFocus += Surname_MouseDown;
            this.FirstName.GotFocus += FirstName_MouseDown;
            this.SecondName.GotFocus += SecondName_MouseDown;
            this.Login.GotFocus += Login_MouseDown;
            this.Password.GotFocus += Password_MouseDown;

            this.Surname.TextChanged += TextChanged;
            this.FirstName.TextChanged += TextChanged;
            this.SecondName.TextChanged += TextChanged;
            this.Login.TextChanged += TextChanged;
            this.Password.TextChanged += TextChanged;
        }

        private void Password_MouseDown(object sender, RoutedEventArgs e)
        {
            if (this.Password.Text == "Пароль")
                this.Password.Text = "";
        }

        private void Login_MouseDown(object sender, RoutedEventArgs e)
        {
            if (this.Login.Text == "Логин")
                this.Login.Text = "";
        }

        private void SecondName_MouseDown(object sender, RoutedEventArgs e)
        {
            if (this.SecondName.Text == "Отчество")
                this.SecondName.Text = "";
        }

        private void FirstName_MouseDown(object sender, RoutedEventArgs e)
        {
            if (this.FirstName.Text == "Имя")
                this.FirstName.Text = "";
        }

        private void Surname_MouseDown(object sender, RoutedEventArgs e)
        {
            if (this.Surname.Text == "Фамилия")
                this.Surname.Text = "";
        }

        public EditWindow(Record user, List<string> positions)
        {
            InitializeComponent();
            record = user;

            edit = true;
            
            this.Position.ItemsSource = positions;
            this.Surname.Text = user.Surname;
            this.FirstName.Text = user.Name;
            this.SecondName.Text = user.SecondName;
            this.Position.SelectedIndex = positions.IndexOf(user.Position);
            this.Login.Text = user.Login;
            this.Password.Text = "";

            this.Surname.TextChanged += TextChanged;
            this.FirstName.TextChanged += TextChanged;
            this.SecondName.TextChanged += TextChanged;
            this.Login.TextChanged += TextChanged;
            this.Password.TextChanged += TextChanged;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            this.RecordModified(this.record);
            if (correct)
            {
                if (edit)
                    this.ApplyChanges(record);
                else
                    this.AddNewRecord(record);

                this.Close();
            }
        }

        public void IncorrectData(ErrorStruct errStruct)
        {
            this.correct = errStruct.Correct;
            this.Save.IsEnabled = correct;
            this.FirstNamePopUp.IsOpen = errStruct.IncorrectFirstName;
            this.SecondNamePopUp.IsOpen = errStruct.IncorrectSecondName;
            this.SurnamePopUp.IsOpen = errStruct.IncorrectSurname;
            this.LoginPopUp.IsOpen = errStruct.IncorrectLogin;

            switch (errStruct.PWStrength)
            {
                case ErrorStruct.PassStrength.Low:
                    this.PWStrength.Value = 33;
                    break;
                case ErrorStruct.PassStrength.Medium:
                    this.PWStrength.Value = 66;
                    break;
                case ErrorStruct.PassStrength.High:
                    this.PWStrength.Value = 100;
                    break;
            }
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            record.Name = this.FirstName.Text == "Имя" ? "" : this.FirstName.Text;
            record.Surname = this.Surname.Text == "Фамилия" ? "" : this.Surname.Text;
            record.SecondName = this.SecondName.Text == "Отчество" ? "" : this.SecondName.Text;
            record.Position = (string)this.Position.SelectedItem;
            record.Login = this.Login.Text == "Логин" ? "" : this.Login.Text;
            record.Password = this.Password.Text == "Пароль" ? "" : this.Password.Text;

            this.RecordModified(this.record);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.record.Position = (string)this.Position.SelectedItem;
        }
    }
}
