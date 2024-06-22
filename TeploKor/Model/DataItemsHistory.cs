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
    class DataItemsHistory : INotifyPropertyChanged
    {
        private static int _idCounter;
        [Key]
        public int dataItemsHistoryId { get; set; }
        public string dataItemsHistoryName { get; set; }
        public int clientId { get; set; }
        public string dataItemsHistoryDate { get; set; }
        public string dataItemsHistoryPrice { get; set; }
        [ForeignKey("Boiler")]
        public int dataItemsHistoryBoilerId { get; set; }
        [ForeignKey("Radiator")]
        public int dataItemsHistoryRadiatorId {  get; set; }
        [ForeignKey("WaemFloor")]
        public int dataItemsHistoryWaemFloorId { get; set; }

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
            this.dataItemsHistoryDate = dataItemsHistoryDate;
            this.dataItemsHistoryPrice = dataItemsHistoryPrice;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
