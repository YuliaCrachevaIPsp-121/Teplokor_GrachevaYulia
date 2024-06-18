using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TeploKor.Model
{
    public class Client : INotifyPropertyChanged
    {
        private string _surname;
        private string _email;
        private string _password;
        private string _name;
        private string? _patronymic;
        private string? _phone;

        [Key]
        public int clientId { get; set; }
        public string clientSurname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                OnPropertyChanged("clientSurname");
            }
        }
        public string clientName
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("clientName");
            }
        }
        public string? clientPatronymic
        {
            get { return _patronymic; }
            set
            {
                _patronymic = value;
                OnPropertyChanged("clientPatronymic");
            }
        }
        public string? clientContactNumber
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPropertyChanged("clientContactNumber");
            }
        }
        public string? clientEmail
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged("clientEmail");
            }
        }
        public string clientPassword
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("clientPassword");
            }
        }
        public Client() { }
        public Client(int clientId, string clientSurname, string clientName, string? clientPatronymic, string? clientContactNumber, string? clientEmail, string clientPassword)
        {
            this.clientId = clientId;
            this.clientSurname = clientSurname;
            this.clientName = clientName;
            this.clientPatronymic = clientPatronymic;
            this.clientContactNumber = clientContactNumber;
            this.clientEmail = clientEmail;
            this.clientPassword = clientPassword;
        }
        public Client ShallowCopy()
        {
            return (Client)this.MemberwiseClone();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
