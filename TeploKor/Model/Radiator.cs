using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace TeploKor.Model
{
    public class Radiator : INotifyPropertyChanged
    {

        [Key]
        public int radiatorId { get; set; }
        public string radiatorName { get; set; }
        public decimal radiatorPrice {  get; set; }
        public string radiatorMaterial { get; set; }
        public int radiatorThickness { get; set; }
        public decimal radiatorLength { get; set; }
        public int radiatorHeight { get; set; }
        public string radiatorBrand { get; set; }
        public string radiatorImageSource {  get; set; }
        public Radiator() { }
        public Radiator(int radiatorId, string radiatorMaterial, int radiatorThickness, decimal radiatorLength, int radiatorHeight, string radiatorBrand, string radiatorName, decimal price, string radiatorImageSource)
        {
            this.radiatorId = radiatorId;
            this.radiatorMaterial = radiatorMaterial;
            this.radiatorThickness = radiatorThickness;
            this.radiatorLength = radiatorLength;
            this.radiatorHeight = radiatorHeight;
            this.radiatorBrand = radiatorBrand;
            this.radiatorName = radiatorName;
            this.radiatorPrice = price;
            this.radiatorImageSource = radiatorImageSource;
        }
        public Radiator ShallowCopy()
        {
            return (Radiator)this.MemberwiseClone();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
