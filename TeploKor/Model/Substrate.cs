using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeploKor.Model
{
    public class Substrate
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
    }
}
