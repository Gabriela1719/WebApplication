using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class VehicleModel
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public int MakeID {get;set;}
        public IEnumerable<VehicleMake> VehicleMakes { get; set; }
    }
}
