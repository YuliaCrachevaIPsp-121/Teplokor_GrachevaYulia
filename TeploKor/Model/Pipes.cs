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
    public class Pipes : INotifyPropertyChanged
    {
        [Key]
        public int pipesId { get; set; }
        public string pipesMaterial { get; set; }
        public int pipesDiametr { get; set; }
        public decimal pipesPrice { get; set; }
        public Pipes() { }
        public Pipes(int pipesId, string pipesMaterial, int pipesDiametr, decimal pipesPrice)
        {
            this.pipesId = pipesId;
            this.pipesMaterial = pipesMaterial;
            this.pipesDiametr = pipesDiametr;
            this.pipesPrice = pipesPrice;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
