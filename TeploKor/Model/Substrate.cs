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
    public class Substrate : INotifyPropertyChanged
    {
        [Key]
        public int substrateId { get; set; }
        public decimal substratePrice { get; set; }
        public Substrate() { }
        public Substrate(int substrateId, decimal substratePrice)
        {
            this.substrateId = substrateId;
            this.substratePrice = substratePrice;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
