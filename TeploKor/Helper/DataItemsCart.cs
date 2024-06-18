using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TeploKor.Helper
{
    public class DataItemsCart : INotifyPropertyChanged
    {
        public string dataItemsCartImageSource { get; set; }
        public string dataItemsCartName { get; set; }
        public decimal dataItemsCartPrice { get; set; }
        [ForeignKey("Radiator")]
        public int dataItemsRadiatorId { get; set; }
        [ForeignKey("Boiler")]
        public int dataItemsBoilerId { get; set; }
        [ForeignKey("Pipes")]
        public int dataItemsPipesId { get; set; }
        [ForeignKey("Substrate")]
        public int dataItemsSubstrateId { get; set; }
        [ForeignKey("WarmFloor")]
        public int dataItemsWarmFloorId { get; set; }
        public DataItemsCart() { }
        public DataItemsCart(string dataItemsCartImageSource, string dataItemsCartName, decimal dataItemsCartPrice, int dataItemsRadiatorId, int dataItemsBoilerId, int dataItemsPipesId, int dataItemsSubstrateId, int dataItemsWarmFloorId)
        {
            this.dataItemsCartImageSource = dataItemsCartImageSource;
            this.dataItemsCartName = dataItemsCartName;
            this.dataItemsCartPrice = dataItemsCartPrice;
            this.dataItemsRadiatorId = dataItemsRadiatorId;
            this.dataItemsBoilerId = dataItemsBoilerId;
            this.dataItemsPipesId = dataItemsPipesId;
            this.dataItemsSubstrateId = dataItemsSubstrateId;
            this.dataItemsWarmFloorId = dataItemsWarmFloorId;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
