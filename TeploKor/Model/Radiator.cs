using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace TeploKor.Model
{
    public class Radiator
    {
        [Key]
        public int radiatorId { get; set; }
        public string radiatorMaterial { get; set; }
        public int radiatorThickness { get; set; }
        public decimal radiatorLength { get; set; }
        public int radiatorHeight { get; set; }
        public string radiatorBrand { get; set; }
        public Radiator() { }
        public Radiator(int radiatorId, string radiatorMaterial, int radiatorThickness, decimal radiatorLength, int radiatorHeight, string radiatorBrand)
        {
            this.radiatorId = radiatorId;
            this.radiatorMaterial = radiatorMaterial;
            this.radiatorThickness = radiatorThickness;
            this.radiatorLength = radiatorLength;
            this.radiatorHeight = radiatorHeight;
            this.radiatorBrand = radiatorBrand;
        }
    }
}
