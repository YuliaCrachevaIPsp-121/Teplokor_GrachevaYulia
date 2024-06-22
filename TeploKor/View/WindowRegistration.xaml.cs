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
using TeploKor.Model;
using TeploKor.Helper;
using System.Text.RegularExpressions;

namespace TeploKor.View
{
    public partial class WindowRegistration : Window
    {
        public WindowRegistration()
        {
            InitializeComponent();
        }

        private void Back_Click (object sender, RoutedEventArgs e)
        {
            WindowEntrance windowEntrance = new WindowEntrance();
            windowEntrance.Show();
            this.Close();
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            // Проверка фамилии
            if (string.IsNullOrWhiteSpace(clientSurname.Text) || !IsValidName(clientSurname.Text))
            {
                MessageBox.Show("Фамилия должна быть заполнена корректно.");
                return;
            }

            // Проверка имени
            if (string.IsNullOrWhiteSpace(clientName.Text) || !IsValidName(clientName.Text))
            {
                MessageBox.Show("Имя должно быть заполнено корректно.");
                return;
            }

            // Проверка отчества
            if (string.IsNullOrWhiteSpace(clientPatronymic.Text) || !IsValidName(clientPatronymic.Text))
            {
                MessageBox.Show("Отчество должно быть заполнено корректно.");
                return;
            }

            // Проверка номера телефона
            if (!IsValidPhoneNumber(clientContactNumber.Text))
            {
                MessageBox.Show("Номер телефона должен быть заполнен корректно.");
                return;
            }

            // Проверка электронной почты
            if (string.IsNullOrWhiteSpace(clientEmail.Text) && string.IsNullOrWhiteSpace(clientContactNumber.Text))
            {
                MessageBox.Show("Для регистрации необходимо указать электронную почту или номер телефона.");
                return;
            }

            ObservableCollection<Client> newClients = new ObservableCollection<Client>
    {
        new Client
        {
            clientSurname = clientSurname.Text,
            clientName = clientName.Text,
            clientPatronymic = clientPatronymic.Text,
            clientContactNumber = clientContactNumber.Text,
            clientEmail = clientEmail.Text,
            clientPassword = clientPassword.Text
        }
    };

            MyDbContext db = new MyDbContext();
            try
            {
                db.SaveEntities<Client>(newClients);
                MessageBox.Show("Регистрация прошла успешно. Теперь вы можете войти в свой аккаунт.");
                WindowEntrance entranceWindow = new WindowEntrance();
                entranceWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при регистрации: " + ex.Message);
            }
        }

        private bool IsValidName(string name)
        {
            return !name.Any(char.IsDigit);
        }
        private bool IsValidPhoneNumber(string phoneNumber)
        { 
            string pattern = @"^\+?\d{1,3}?[- .]?\(?(?:\d{2,3})\)?[- .]?\d\d\d[- .]?\d\d\d\d$";
            return Regex.IsMatch(phoneNumber, pattern);
        }

    }
}
