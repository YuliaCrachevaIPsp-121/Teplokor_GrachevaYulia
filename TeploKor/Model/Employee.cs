using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TeploKor.Model
{
    public class Employee :INotifyPropertyChanged
    {
        private string _name;
        private string _surname;
        private string? _patronymic;
        private string _email;
        private string _phone;
        private int _pSeries;
        private int _pNumber;
        private string _education;
        private string? _childrenBirthCertificateNumber;
        private string _numberWorkBook;
        private string _seriesWorkBook;
        private string _educationNumber;
        private string _educationSeries;
        public string? _numberOfMilitary;
        public string? _seriesOfMilitary;
        public string _login;
        public string _password;

        [Key]
        public int employeeId { get; set; }
        public string employeeSurname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                OnPropertyChanged("employeeSurname");
            }
        }
        public string employeeName
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("employeeName");
            }
        }
        public string? employeePatronymic
        {
            get { return _patronymic; }
            set
            {
                _patronymic = value;
                OnPropertyChanged("employeePatronymic");
            }
        }
        public string employeeContactNumber
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPropertyChanged("employeeContactNumber");
            }
        }
        public string employeeEmail
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged("employeeEmail");
            }
        }
        public int employeeSeriesPassport
        {
            get { return _pSeries; }
            set
            {
                _pSeries = value;
                OnPropertyChanged("employeeSeriesPassport");
            }
        }
        public int employeeNumberPassport
        {
            get { return _pNumber; }
            set
            {
                _pNumber = value;
                OnPropertyChanged("employeeNumberPassport");
            }
        }
        public string employeeEducation
        {
            get { return _education; }
            set
            {
                _education = value;
                OnPropertyChanged("employeeEducation");
            }
        }
        public string? employeeChildrenBirthCertificateNumber
        {
            get { return _childrenBirthCertificateNumber; }
            set
            {
                _childrenBirthCertificateNumber = value;
                OnPropertyChanged("employeeChildrenBirthCertificateNumber");
            }
        }
        public string employeeNumberWorkBook
        {
            get { return _numberWorkBook; }
            set
            {
                _numberWorkBook = value;
                OnPropertyChanged("employeeNumberWorkBook");
            }
        }
        public string employeeSeriesWorkBook
        {
            get { return _seriesWorkBook; }
            set
            {
                _seriesWorkBook = value;
                OnPropertyChanged("employeeSeriesWorkBook");
            }
        }
        public string employeeEducationNumber
        {
            get { return _educationNumber; }
            set
            {
                _educationNumber = value;
                OnPropertyChanged("employeeEducationNumber");
            }
        }
        public string employeeEducationSeries
        {
            get { return _educationSeries; }
            set
            {
                _educationSeries = value;
                OnPropertyChanged("employeeEducationSeries");
            }
        }
        public string? employeeNumberOfMilitaryId
        {
            get { return _numberOfMilitary; }
            set
            {
                _numberOfMilitary = value;
                OnPropertyChanged("employeeNumberOfMilitaryId");
            }
        }
        public string? employeeSeriesOfMilitaryId
        {
            get { return _seriesOfMilitary; }
            set
            {
                _seriesOfMilitary = value;
                OnPropertyChanged("employeeSeriesOfMilitaryId");
            }
        }
        public bool IsAdmin {  get; set; }
        public string employeeLogin
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged("employeeLogin");
            }
        }
        public string employeePassword
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("employeePassword");
            }
        }
        public Employee() { }
        public Employee(int employeeId, string employeeSurname, string employeeName, string? employeePatronymic, int employeeSeriesPassport, int employeeNumberPassport, string employeeEducation, string? employeeChildrenBirthCertificateNumber, string employeeNumberWorkBook, string employeeSeriesWorkBook, string employeeEducationNumber, string employeeEducationSeries, string? employeeNumberOfMilitaryId, string? employeeSeriesOfMilitaryId, string employeeLogin, string employeePassword, string employeeContactNumber, bool isAdmin, string email)
        {
            this.employeeId = employeeId;
            this.employeeSurname = employeeSurname;
            this.employeeName = employeeName;
            this.employeePatronymic = employeePatronymic;
            this.employeeSeriesPassport = employeeSeriesPassport;
            this.employeeNumberPassport = employeeNumberPassport;
            this.employeeEducation = employeeEducation;
            this.employeeChildrenBirthCertificateNumber = employeeChildrenBirthCertificateNumber;
            this.employeeNumberWorkBook = employeeNumberWorkBook;
            this.employeeSeriesWorkBook = employeeSeriesWorkBook;
            this.employeeEducationNumber = employeeEducationNumber;
            this.employeeEducationSeries = employeeEducationSeries;
            this.employeeNumberOfMilitaryId = employeeNumberOfMilitaryId;
            this.employeeSeriesOfMilitaryId = employeeSeriesOfMilitaryId;
            this.employeeLogin = employeeLogin;
            this.employeePassword = employeePassword;
            this.employeeContactNumber = employeeContactNumber;
            this.IsAdmin = isAdmin;
            this.employeeEmail = employeeEmail;
        }
        public Employee ShallowCopy()
        {
            return (Employee)this.MemberwiseClone();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
