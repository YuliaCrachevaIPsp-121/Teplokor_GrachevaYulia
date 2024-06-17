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
        [Key]
        public int clientId { get; set; }
        public string clientSurname { get; set; }
        public string clientName { get; set; }
        public string? clientPatronymic { get; set; }
        public string? clientContactNumber { get; set; }
        public string? clientEmail {  get; set; }
        public string clientPassword { get; set; }
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
