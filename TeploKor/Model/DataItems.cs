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
    public class DataItems : INotifyPropertyChanged
    {
        private string _imageSource;
        private string _name;
        [Key]
        public int dataItemsId { get; set; }
        public string dataItemsImageSource 
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                OnPropertyChanged("dataItemsImageSource");
            }
        }
        public string dataItemsName
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("dataItemsName");
            }
        }
        [ForeignKey("Radiator")]
        public int? dataItemsRadiatorId { get; set; }
        [ForeignKey("Boiler")]
        public int? dataItemsBoilerId { get; set; }
        [ForeignKey("Pipes")]
        public int? dataItemsPipesId { get; set; }
        [ForeignKey("Substrate")]
        public int? dataItemsSubstrateId { get; set; }
        [ForeignKey("WarmFloor")]
        public int? dataItemsWarmFloorId { get; set; }

        public DataItems() { }
        public DataItems(string dataItemsImageSource, string dataItemsName, int? dataItemsRadiatorId, int? dataItemsBoilerId, int? dataItemsPipesId, int? dataItemsSubstrateId, int? dataItemsWarmFloorId)
        {
            this.dataItemsImageSource = dataItemsImageSource;
            this.dataItemsName = dataItemsName;
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
