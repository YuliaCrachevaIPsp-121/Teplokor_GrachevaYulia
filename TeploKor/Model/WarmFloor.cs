using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeploKor.Model
{
    public class WarmFloor
    {
        [Key]
        public int warmFloorId { get; set; }
        public string warmFloorType { get; set; }
        public int warmFloorLayer {  get; set; }
        public decimal warmFloorPricePerMeter { get; set; }
        [ForeignKey("Substrate")]
        public int? substrateId { get; set; }
        public WarmFloor() { }
        public WarmFloor(int warmFloorId, string warmFloorType, int warmFloorLayer, decimal warmFloorPricePerMeter, int? substrateId)
        {
            this.warmFloorId = warmFloorId;
            this.warmFloorType = warmFloorType;
            this.warmFloorLayer = warmFloorLayer;
            this.warmFloorPricePerMeter = warmFloorPricePerMeter;
            this.substrateId = substrateId;
        }
    }
}
