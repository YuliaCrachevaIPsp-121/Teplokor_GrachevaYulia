using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TeploKor.Model
{
    public class Order : INotifyPropertyChanged
    {
        [Key]
        public int orderId { get; set; }
        public string orderDeliveryMethod { get; set; }
        public string orderPaymentMethod { get; set; }
        public string? orderDeliveryAddressCountry { get; set; }
        public string? orderDeliveryAddressStreet { get; set; }
        public string? orderDeliveryAddressHome { get; set; }
        public string? orderDeliveryAddressNumberApartament { get; set; }
        public decimal orderCost { get; set; }
        [ForeignKey("Client")]
        public int clientId { get; set; }
        [ForeignKey("Employee")]
        public int employeeId { get; set; }
        public Order() { }

        public Order(int orderId, string orderDeliveryMethod, string orderPaymentMethod, string? orderDeliveryAddressCountry, string? orderDeliveryAddressStreet, string? orderDeliveryAddressHome, string? orderDeliveryAddressNumberApartament, decimal orderCost, int clientId, int employeeId)
        {
            this.orderId = orderId;
            this.orderDeliveryMethod = orderDeliveryMethod;
            this.orderPaymentMethod = orderPaymentMethod;
            this.orderDeliveryAddressCountry = orderDeliveryAddressCountry;
            this.orderDeliveryAddressStreet = orderDeliveryAddressStreet;
            this.orderDeliveryAddressHome = orderDeliveryAddressHome;
            this.orderDeliveryAddressNumberApartament = orderDeliveryAddressNumberApartament;
            this.orderCost = orderCost;
            this.clientId = clientId;
            this.employeeId = employeeId;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
