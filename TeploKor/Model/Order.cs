using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeploKor.Model
{
    public class Order
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
    }
}
