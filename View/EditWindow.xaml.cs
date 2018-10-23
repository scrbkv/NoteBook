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
using NoteBook;

namespace View
{
    public delegate void ApplyChangesHandler(Record record);
    public delegate void RecordModifiedHandler(Record record);
    public partial class EditWindow : Window
    {
        private Record record = new Record();

        public event ApplyChangesHandler ApplyChanges;
        public event RecordModifiedHandler RecordModified;

        public EditWindow(Record user)
        {
            InitializeComponent();
            record = user;
            this.Surname.Text = user.Surname;
            this.FirstName.Text = user.Name;
            this.SecondName.Text = user.SecondName;
            this.Position.Text = user.Position;
            this.Login.Text = user.Login;
            this.Password.Text = user.Password;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            this.ApplyChanges(record);
            this.Close();
        }

        public void IncorrectData(ErrorStruct errStruct)
        {
            this.FirstNamePopUp.IsOpen = errStruct.IncorrectFirstName;
            this.SecondNamePopUp.IsOpen = errStruct.IncorrectSecondName;
            this.SurnamePopUp.IsOpen = errStruct.IncorrectSurname;
            this.LoginPopUp.IsOpen = errStruct.IncorrectLogin;

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
            record.Login = this.Login.Text;
            record.Password = this.Password.Text;

            try
            {
                this.RecordModified(this.record);
            }
            catch(Exception)
            {

            }
        }
    }
}
