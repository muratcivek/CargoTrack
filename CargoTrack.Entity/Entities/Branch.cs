using CargoTrack.Entity.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrack.Entity.Entities
{
    public class Branch : BaseEntity //şube entitysi
    {
        public string Name { get; set; }

        public Guid CityId { get; set; }

        // Navigation properties
        public City City { get; set; }
        public IList<Cargo> OriginCargos { get; set; }
        public IList<Cargo> DestinationCargos { get; set; }


    }
}
