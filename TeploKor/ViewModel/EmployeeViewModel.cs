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
            string connectionString = "Data Source=D:\\Interpol\\interpolApp\\interpolApp\\BD\\interpolBD.db";

            List<Employee> employees = MyDbContext.GetEntities<Employee>(connectionString, "SELECT * FROM Employee");

            // Преобразование в ObservableCollection
            ListEmployee = new ObservableCollection<Employee>(employees);
        }

        private RelayCommand editEmployee;
        public RelayCommand EditEmployee
        {
            get
            {
                return editEmployee ??
                (editEmployee = new RelayCommand(obj =>
                {
                    WindowNewGroup wnGroup = new WindowNewGroup
                    { Title = "Редактирование группировка" };

                    CriminalGroup criminalGroup = SelectedCriminalGroup;
                    CriminalGroup tempCriminalGroup = new CriminalGroup();

                    tempCriminalGroup = criminalGroup.ShallowCopy();
                    wnGroup.DataContext = tempCriminalGroup;
                    if (wnGroup.ShowDialog() == true)
                    {
                        criminalGroup.CriminalGroupId = tempCriminalGroup.CriminalGroupId;
                        criminalGroup.CriminalGroupName = tempCriminalGroup.CriminalGroupName;
                        MyDbContext dbContext = new MyDbContext();
                        dbContext.UpdateEntity<CriminalGroup>(tempCriminalGroup);
                    }
                }, (obj) => SelectedCriminalGroup != null && ListCriminalGroup.Count > 0));
            }
        }


        private CriminalGroup selectedCriminalGroup;
        public CriminalGroup SelectedCriminalGroup
        {
            get
            {
                return selectedCriminalGroup;
            }
            set
            {
                selectedCriminalGroup = value;
                OnPropertyChanged("SelectedCriminalGroup");
                EditCriminalGroup.CanExecute(true);
            }
        }

        private RelayCommand addCriminalGroup;
        public RelayCommand AddCriminalGroup
        {
            get
            {
                return addCriminalGroup ??
                 (addCriminalGroup = new RelayCommand(obj =>
                 {
                     WindowNewGroup wnGroup = new WindowNewGroup
                     {
                         Title = "Новая группировка",
                     };
                     int maxIdCriminalGroup = MaxId() + 1;
                     CriminalGroup criminalGroup = new CriminalGroup { CriminalGroupId = maxIdCriminalGroup };
                     wnGroup.DataContext = criminalGroup;
                     if (wnGroup.ShowDialog() == true)
                     {
                         ListCriminalGroup.Add(criminalGroup);
                         MyDbContext dbContext = new MyDbContext();
                         dbContext.SaveEntity<CriminalGroup>(dbContext, criminalGroup);
                     }
                     SelectedCriminalGroup = criminalGroup;
                 }));
            }
        }

        private RelayCommand deleteCriminalGroup;
        public RelayCommand DeleteCriminalGroup
        {
            get
            {
                return deleteCriminalGroup ??
                (deleteCriminalGroup = new RelayCommand(obj =>
                {
                    CriminalGroup criminalGroup = SelectedCriminalGroup;
                    MessageBoxResult result = MessageBox.Show("Удалить данные по группировке: " + criminalGroup.CriminalGroupName, "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.OK)
                    {
                        ListCriminalGroup.Remove(criminalGroup);
                        MyDbContext dbContext = new MyDbContext();
                        dbContext.DeleteEntityFromDatabase<CriminalGroup>(criminalGroup);
                    }
                }, (obj) => SelectedCriminalGroup != null && ListCriminalGroup.Count > 0));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
