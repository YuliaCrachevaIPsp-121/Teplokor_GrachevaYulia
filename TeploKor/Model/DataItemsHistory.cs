using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TeploKor.Helper;

namespace TeploKor.Model
{
    public class DataItemsHistory : INotifyPropertyChanged
    {
        private static int _idCounter;
        [Key]
        public int dataItemsHistoryId { get; set; }
        public string dataItemsHistoryName { get; set; }
        public int clientId { get; set; }
        public string dataItemsHistoryPrice { get; set; }
        [ForeignKey("Boiler")]
        public int dataItemsHistoryBoilerId { get; set; }
        [ForeignKey("Radiator")]
        public int dataItemsHistoryRadiatorId {  get; set; }

        private static int GetNextId()
        {
            return ++_idCounter;
        }
        public DataItemsHistory() { }
        public DataItemsHistory(int dataItemsHistoryId, string dataItemsHistoryName, int clientId, string dataItemsHistoryDate, string dataItemsHistoryPrice)
        {
            this.dataItemsHistoryId = GetNextId();
            this.dataItemsHistoryName = dataItemsHistoryName;
            this.clientId = clientId;
            this.dataItemsHistoryPrice = dataItemsHistoryPrice;
        }
        public List<Order> GetOrdersInProgress()
        {
            using (var context = new MyDbContext())
            {
                return context.Order
                    .Where(o => o.orderStatus == "Выполняется")
                    .ToList();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
