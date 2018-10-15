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
using System.Windows.Shapes;

namespace NoteBook
{
    /// <summary>
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public delegate void ApplyChangesHandler(Record record);
    public delegate void RecordModifiedHandler(Record record);
    public partial class EditWindow : Window
    {
        private Record record;

        public event ApplyChangesHandler ApplyChanges;
        public event RecordModifiedHandler RecordModified;

        public EditWindow(Record user)
        {
            record = user;
            InitializeComponent();
            this.Surname.Text = user.Surname == "" ? "Фамилия" : user.Surname;
            this.FirstName.Text = user.Name == "" ? "Имя" : user.Name;
            this.SecondName.Text = user.SecondName == "" ? "Отчество" : user.SecondName;
            this.Position.Text = user.Position == "" ? "Должность" : user.Position;
            this.Login.Text = user.Username == "" ? "Логин" : user.Username;
            this.Password.Text = user.Password == "" ? "Пароль" : "<Без изменений>";
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            record.Name = this.FirstName.Text;
            record.Surname = this.Surname.Text;
            record.SecondName = this.SecondName.Text;
            record.Position = this.Position.Text;
            record.Username = this.Login.Text;
            if (this.Password.Text != "<Без изменений>")
                record.Password = this.Password.Text;
            this.ApplyChanges(record);
        }

        public void IncorrectData(ErrorStruct errStruct)
        {
            if (errStruct.IncorrectFirstName)
                this.IncorrectFirstNameLabel.Visibility = Visibility.Visible;

            if (errStruct.IncorrectSecondName)
                this.IncorrectSecondNameLabel.Visibility = Visibility.Visible;

            if (errStruct.IncorrectSurname)
                this.IncorrectSurnameLabel.Visibility = Visibility.Visible;

            if (errStruct.IncorrectLogin)
                this.IncorrectLoginLabel.Visibility = Visibility.Visible;

            switch (errStruct.PWStrength)
            {
                case ErrorStruct.PassStrength.Low:
                    this.PWStrength.Value = 1/3;
                    break;
                case ErrorStruct.PassStrength.Medium:
                    this.PWStrength.Value = 2/3;
                    break;
                case ErrorStruct.PassStrength.High:
                    this.PWStrength.Value = 1;
                    break;
            }
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            record.Name = this.FirstName.Text;
            record.Surname = this.Surname.Text;
            record.SecondName = this.SecondName.Text;
            record.Position = this.Position.Text;
            record.Username = this.Login.Text;
            if (this.Password.Text != "<Без изменений>")
                record.Password = this.Password.Text;
            this.ApplyChanges(record);

            this.RecordModified(this.record);
        }
    }
}
