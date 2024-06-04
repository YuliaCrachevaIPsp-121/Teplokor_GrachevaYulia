using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeploKor.Model
{
    public class Client
    {
        [Key]
        public int clientId { get; set; }
        // опт или розница
        public string clientWholesaleOrRetail { get; set; }
        public string clientSurname { get; set; }
        public string clientName { get; set; }
        public string? clientPatronymic { get; set; }
        public string? clientContactNumber { get; set; }
        public string? clientEmail {  get; set; }
        public Client() { }
        public Client(int clientId, string clientWholesaleOrRetail, string clientSurname, string clientName, string? clientPatronymic, string? clientContactNumber, string? clientEmail)
        {
            this.clientId = clientId;
            this.clientWholesaleOrRetail = clientWholesaleOrRetail;
            this.clientSurname = clientSurname;
            this.clientName = clientName;
            this.clientPatronymic = clientPatronymic;
            this.clientContactNumber = clientContactNumber;
            this.clientEmail = clientEmail;
        }
    }
}
