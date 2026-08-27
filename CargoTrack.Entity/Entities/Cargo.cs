using CargoTrack.Entity.Entities.Common;
using CargoTrack.Entity.Entities.Enums;

namespace CargoTrack.Entity.Entities
{
    public class Cargo:BaseEntity
    {
        public string TrackCode { get; set; } 
        public DateTime ShipmentDate { get; set; }
        public DateTime EstinatedArrivalDate { get; set; }
        public CargoType cargoType { get; set; }
        public CargoStatus CargoStatus { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public Guid OriginBranchId { get; set; }
        public Guid DestinationBranchId { get; set; }

        //navigation properties
        public virtual AppUser Sender { get; set; }
        public virtual AppUser Receiver { get; set; }
        public virtual Branch OriginBranch { get; set; }
        public virtual Branch DestinationBranch { get; set; }


    }
}
