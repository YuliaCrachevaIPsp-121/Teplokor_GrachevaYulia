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
    public class Boiler : INotifyPropertyChanged
    {
        [Key]
        public int boilerId { get; set; }
        public string boilerBrand { get; set; }
        public string boilerName { get; set;}
        public string boilerDescription { get; set;}
        public decimal boilerPrice { get; set;}
        public int boilerSquare {  get; set;}
        public string boilerElectricOrNot { get; set; }
        public int boilerKikowatt { get; set; }
        public string boilerInstallationLocation { get; set; }
        public string boilerTurbochargedOrNot { get; set; }
        public string boilerType { get; set; }
        public byte[] boilerPhoto { get; set; }
        public Boiler() { }
        public Boiler(int boilerId, string boilerBrand, string boilerName, string boilerDescription, decimal boilerPrice, int boilerSquare, string boilerElectricOrNot, int boilerKikowatt, string boilerInstallationLocation, string boilerTurbochargedOrNot, string boilerType, byte[] boilerPhoto)
        {
            this.boilerId = boilerId;
            this.boilerBrand = boilerBrand;
            this.boilerName = boilerName;
            this.boilerDescription = boilerDescription;
            this.boilerPrice = boilerPrice;
            this.boilerSquare = boilerSquare;
            this.boilerElectricOrNot = boilerElectricOrNot;
            this.boilerKikowatt = boilerKikowatt;
            this.boilerInstallationLocation = boilerInstallationLocation;
            this.boilerTurbochargedOrNot = boilerTurbochargedOrNot;
            this.boilerType = boilerType;
            this.boilerPhoto = boilerPhoto;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
