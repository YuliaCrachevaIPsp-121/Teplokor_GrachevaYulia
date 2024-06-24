using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TeploKor.Helper;
using TeploKor.Model;
using TeploKor.View;
using System.Windows;

namespace TeploKor.ViewModel
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Employee> ListEmployee { get; set; }
        public static int EmployeeId;
        public int MaxId()
        {
            int max = 0;
            if (this.ListEmployee != null)
            {
                foreach (var cl in this.ListEmployee)
                {
                    if (max < cl.employeeId) max = cl.employeeId;
                }
            }
            return max;
        }

        public EmployeeViewModel()
        {
            // Строка подключения к SQLite
            string connectionString = "Data Source=D:\\TeploKor\\TeploKor\\BD\\TeploKor.db";

            List<Employee> employees = MyDbContext.GetEntities<Employee>(connectionString, "SELECT * FROM Employee");

            // Преобразование в ObservableCollection
            ListEmployee = new ObservableCollection<Employee>(employees);
        }
        private CurrentUser currentUser;
        private RelayCommand editEmployee;
        public RelayCommand EditEmployee
        {
            get
            {
                return editEmployee ??
                (editEmployee = new RelayCommand(obj =>
                {
                    WindowNewEmployee wnEmployee = new WindowNewEmployee(currentUser)
                    { Title = "Редактирование сотрудника" };

                    Employee employee = SelectedEmployee;
                    Employee tempEmployee = new Employee();

                    tempEmployee = employee.ShallowCopy();
                    wnEmployee.DataContext = tempEmployee;
                    if (wnEmployee.ShowDialog() == true)
                    {
                        employee.employeeId = tempEmployee.employeeId;
                        employee.employeeChildrenBirthCertificateNumber = tempEmployee.employeeChildrenBirthCertificateNumber;
                        employee.employeeContactNumber = tempEmployee.employeeContactNumber;
                        employee.employeeEducation = tempEmployee.employeeEducation;
                        employee.employeeNumberPassport = tempEmployee.employeeNumberPassport;
                        employee.employeeSurname = tempEmployee.employeeSurname;
                        employee.employeeName = tempEmployee.employeeName;
                        employee.employeePatronymic = tempEmployee.employeePatronymic;
                        employee.employeeEducationNumber = tempEmployee.employeeEducationNumber;
                        employee.employeeNumberWorkBook = tempEmployee.employeeNumberWorkBook;
                        employee.employeeNumberOfMilitaryId = tempEmployee.employeeNumberOfMilitaryId;
                        employee.employeeEducationSeries = tempEmployee.employeeEducationSeries;
                        employee.employeeEmail = tempEmployee.employeeEmail;
                        employee.employeeLogin = tempEmployee.employeeLogin;
                        employee.employeePassword = tempEmployee.employeePassword;
                        employee.employeeSeriesOfMilitaryId = tempEmployee.employeeSeriesOfMilitaryId;
                        employee.employeeSeriesWorkBook = tempEmployee.employeeSeriesWorkBook;
                        employee.IsAdmin = tempEmployee.IsAdmin;
                        employee.employeeSeriesPassport = tempEmployee.employeeSeriesPassport;

                        MyDbContext dbContext = new MyDbContext();
                        dbContext.UpdateEntity<Employee>(tempEmployee);
                    }
                }, (obj) => SelectedEmployee != null && ListEmployee.Count > 0));
            }
        }


        private Employee selectedEmployee;
        public Employee SelectedEmployee
        {
            get
            {
                return selectedEmployee;
            }
            set
            {
                selectedEmployee = value;
                OnPropertyChanged("SelectedEmployee");
                EditEmployee.CanExecute(true);
            }
        }

        private RelayCommand addEmployee;
        public RelayCommand AddEmployee
        {
            get
            {
                return addEmployee ??
                 (addEmployee = new RelayCommand(obj =>
                 {
                     WindowNewEmployee wnEmployee = new WindowNewEmployee(currentUser)
                     {
                         Title = "Новый сотрудник",
                     };
                     int maxIdEmployee = MaxId() + 1;
                     Employee employee = new Employee { employeeId = maxIdEmployee };
                     wnEmployee.DataContext = employee;
                     if (wnEmployee.ShowDialog() == true)
                     {
                         ListEmployee.Add(employee);
                         MyDbContext dbContext = new MyDbContext();
                         dbContext.SaveEntity<Employee>(dbContext, employee);
                     }
                     SelectedEmployee = employee;
                 }));
            }
        }

        private RelayCommand deleteEmployee;
        public RelayCommand DeleteEmployee
        {
            get
            {
                return deleteEmployee ??
                (deleteEmployee = new RelayCommand(obj =>
                {
                    Employee employee = SelectedEmployee;
                    MessageBoxResult result = MessageBox.Show("Удалить данные по сотруднику: " + employee.employeeSurname + " " + employee.employeeName + " " + employee.employeePatronymic, "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.OK)
                    {
                        ListEmployee.Remove(employee);
                        MyDbContext dbContext = new MyDbContext();
                        dbContext.DeleteEntityFromDatabase<Employee>(employee);
                    }
                }, (obj) => SelectedEmployee != null && ListEmployee.Count > 0));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
