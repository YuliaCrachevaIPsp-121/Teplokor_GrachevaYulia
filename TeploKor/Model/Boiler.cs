using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeploKor.Model
{
    public class Boiler
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
        public string boilerType { get; set; }
        public string boilerTurbochargedOrNot { get; set; }
        public Boiler() { }
    }
}
