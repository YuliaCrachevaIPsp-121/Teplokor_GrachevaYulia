using Microsoft.EntityFrameworkCore;
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
using TeploKor.Helper;
using TeploKor.Model;

namespace TeploKor.View
{
    public partial class WindowEntrance : Window
    {
        public int? ClientId { get; private set; }
        public int? EmployeeId { get; private set; }
        public CurrentUser CurrentUser { get; private set; } = new CurrentUser();

        public WindowEntrance()
        {
            InitializeComponent();
        }

        private void Registration_Click (object sender, RoutedEventArgs e)
        {
            WindowRegistration windowRegistration = new WindowRegistration();
            windowRegistration.Show();
            this.Close();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text.Trim();
            string password = PasswordTextBox.Text.Trim();

            using (MyDbContext db = new MyDbContext())
            {
                    var client = db.Client.FromSqlRaw(
                        $"SELECT * FROM Client WHERE clientEmail = '{login}' OR clientContactNumber = '{login}' AND clientPassword = '{password}'")
                        .FirstOrDefault();

                    if (client != null)
                    {
                        CurrentUser.IsClient = true;
                        CurrentUser.UserId = client.clientId;
                        MessageBox.Show($"Добро пожаловать, {client.clientSurname} {client.clientName}!");
                        WindowClient windowClient = new WindowClient(CurrentUser);
                        windowClient.Show();
                        this.Close();
                        return;
                    }
                // Проверяем, существует ли пользователь в таблице Employee
                var employee = db.Employee.FromSqlRaw(
    $"SELECT * FROM Employee WHERE employeeLogin = '{login}' AND employeePassword = '{password}'")
    .FirstOrDefault();
                if (employee != null)
                {
                    CurrentUser.UserId = employee.employeeId; // extract the employeeId property from the Employee object
                    if (employee.IsAdmin)
                    {
                        MessageBox.Show($"Добро пожаловать, администратор {employee.employeeSurname} {employee.employeeName}!");
                        WindowClient windowClient = new WindowClient(CurrentUser);
                        windowClient.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show($"Добро пожаловать, сотрудник {employee.employeeSurname} {employee.employeeName}!");
                        WindowClient windowClient = new WindowClient(CurrentUser);
                        windowClient.Show();
                        this.Close();
                    }
                    return;
                }
                else
                {
                    MessageBox.Show("Неправильный логин или пароль.");
                    LoginTextBox.Clear();
                    PasswordTextBox.Clear();
                }
            }
        }
    }
}
