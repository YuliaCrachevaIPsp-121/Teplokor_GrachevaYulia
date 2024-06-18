using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TeploKor.Model
{
    public class Boiler : INotifyPropertyChanged
    {
        private string _brand;
        private string _name;
        private string _description;
        private string _image;
        private decimal _price;
        private int _square;
        private string _electric;
        private int _kilowatt;
        public string _install;
        private string _turbocharged;
        private string _type;
        [Key]
        public int boilerId { get; set; }
        public string boilerBrand
        {
            get { return _brand; }
            set
            {
                _brand = value;
                OnPropertyChanged("boilerBrand");
            }
        }
        public string boilerName
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("boilerName");
            }
        }
        public string boilerDescription
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("boilerDescription");
            }
        }
        public decimal boilerPrice
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged("boilerPrice");
            }
        }
        public int boilerSquare
        {
            get { return _square; }
            set
            {
                _square = value;
                OnPropertyChanged("boilerSquare");
            }
        }
        public string boilerElectricOrNot
        {
            get { return _electric; }
            set
            {
                _electric = value;
                OnPropertyChanged("boilerElectricOrNot");
            }
        }
        public int boilerKikowatt
        {
            get { return _kilowatt; }
            set
            {
                _kilowatt = value;
                OnPropertyChanged("boilerKikowatt");
            }
        }
        public string boilerInstallationLocation
        {
            get { return _install; }
            set
            {
                _install = value;
                OnPropertyChanged("boilerInstallationLocation");
            }
        }
        public string boilerTurbochargedOrNot
        {
            get { return _turbocharged; }
            set
            {
                _turbocharged = value;
                OnPropertyChanged("boilerTurbochargedOrNot");
            }
        }
        public string boilerType
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged("boilerType");
            }
        }
        public string boilerImageSource
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged("boilerImageSource");
            }
        }
        public Boiler() { }
        public Boiler(int boilerId, string boilerBrand, string boilerName, string boilerDescription, decimal boilerPrice, int boilerSquare, string boilerElectricOrNot, int boilerKikowatt, string boilerInstallationLocation, string boilerTurbochargedOrNot, string boilerType, string boilerPhoto)
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
            this.boilerImageSource = boilerPhoto;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
