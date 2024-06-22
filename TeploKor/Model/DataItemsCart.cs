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
    public class DataItemsCart : INotifyPropertyChanged
    {
        [Key]
        public int Id { get; set; }
        public string dataItemsCartName { get; set; }
        public string dataItemsCartImageSource { get; set; }
        public decimal dataItemsCartPrice { get; set; }
        [ForeignKey("Client")]
        public int clientId { get; set; }
        public DataItemsCart() { }
        private static int _idCounter;
        private static int GetNextId()
        {
            return ++_idCounter;
        }
        public DataItemsCart(string dataItemsCartName, string dataItemsCartImageSource, decimal dataItemsCartPrice, int clientId)
        {
            Id = GetNextId();
            this.dataItemsCartName = dataItemsCartName;
            this.dataItemsCartImageSource = dataItemsCartImageSource;
            this.dataItemsCartPrice = dataItemsCartPrice;
            this.clientId = clientId;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
