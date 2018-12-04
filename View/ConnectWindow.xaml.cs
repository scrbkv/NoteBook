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

namespace View
{
    /// <summary>
    /// Логика взаимодействия для ConnectWindow.xaml
    /// </summary>
    public delegate void ConnectHandler(string ip, string user, string dataBase, string password);

    public partial class ConnectWindow : Window
    {
        public event ConnectHandler Connect;

        public ConnectWindow()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            this.Connect(this.IP.Text, this.User.Text, this.DataBase.Text, this.Password.Text);
        }

        public void Error()
        {
            MessageBox.Show("не удалось подключиться к базе данных!");
        }
    }
}
