using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeploKor.Helper;
using TeploKor.Model;

namespace TeploKor.ViewModel
{

    public class BoilerViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Boiler> ListBoiler { get; set; }
        public static int BoilerId;
        public int MaxId()
        {
            int max = 0;
            if (this.ListBoiler != null)
            {
                foreach (var cl in this.ListBoiler)
                {
                    if (max < cl.boilerId) max = cl.boilerId;
                }
            }
            return max;
        }

        public ObservableCollection<Radiator> ListRadiator { get; set; }
        public static int RadiatorId;
        public int MaxId()
        {
            int max = 0;
            if (this.ListBoiler != null)
            {
                foreach (var cl in this.ListRadiator)
                {
                    if (max < cl.radiatorId) max = cl.boilerId;
                }
            }
            return max;
        }

        public BoilerViewModel()
        {
            // Строка подключения к SQLite
            string connectionString = "Data Source=D:\\TeploKor\\TeploKor\\BD\\TeploKor.db";

            List<Boiler> boiler = MyDbContext.GetEntities<Boiler>(connectionString, "SELECT * FROM Boiler");

            // Преобразование в ObservableCollection
            ListBoiler = new ObservableCollection<Boiler>(boiler);

            List<Radiator> radiator = MyDbContext.GetEntities<Radiator>(connectionString, "SELECT * FROM Radiator");

            // Преобразование в ObservableCollection
            ListRadiator = new ObservableCollection<Radiator>(radiator);
        }

        private RelayCommand editBoiler;
        public RelayCommand EditBoiler
        {
            get
            {
                return editBoiler ??
                (editBoiler = new RelayCommand(obj =>
                {
                    WindowNewBoiler wnBoiler = new WindowNewBoiler
                    { Title = "Редактирование котла" };

                    Boiler boiler = SelectedBoiler;
                    Boiler tempBoiler = new Employee();

                    tempEmployee = employee.ShallowCopy();
                    wnBoiler.DataContext = tempBoiler;
                    if (wnBoiler.ShowDialog() == true)
                    {
                        boiler.boilerId = 
                        boiler.boilerBrand = tempBoiler.employeeChildrenBirthCertificateNumber;
                        boiler.boilerName = tempBoiler.employeeContactNumber;
                        boiler.boilerDescription = tempBoiler.employeeEducation;
                        boiler.boilerPrice = tempBoiler.employeeNumberPassport;
                        boiler.boilerSquare = tempBoiler.employeeSurname;
                        boiler.boilerElectricOrNot = tempBoiler.employeeName;
                        boiler.boilerKikowatt = tempBoiler.employeePatronymic;
                        boiler.boilerInstallationLocation = tempBoiler.employeeEducationNumber;
                        boiler.boilerTurbochargedOrNot = tempBoiler.employeeNumberWorkBook;
                        boiler.boilerType = tempBoiler.employeeNumberOfMilitaryId;
                        boiler.boilerImageSource = tempBoiler.employeeEducationSeries;

                        MyDbContext dbContext = new MyDbContext();
                        dbContext.UpdateEntity<boiler>(tempEmployee);
                    }
                }, (obj) => SelectedBoiler != null && ListBoiler.Count > 0));
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
                     WindowNewEmployee wnEmployee = new WindowNewEmployee
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