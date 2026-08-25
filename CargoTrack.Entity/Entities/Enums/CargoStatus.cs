using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrack.Entity.Entities.Enums
{
    public enum CargoStatus
    {
        Received = 1,// kargo alındı
        InTransferCenter = 2, // kargo transfer merkezinde
        DispatchedFromTransferCenter = 3, // kargo transfer merkezinden gönderildi
        ArrivedAtDeliveryBranch = 4, // kargo teslimat şubesine ulaştı
        OutForDelivery = 5, // kargo teslimat için yola çıktı
        Delivered = 6// kargo teslim edildi
    }
}
