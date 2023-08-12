using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementLibrary
{
    internal class AssetMaintenance
    {
        public int AssetID { get; }
        public string AssetName { get; }
        public DateTime RenewalDate { get; }

        public AssetMaintenance(int assetID, string assetName, DateTime renewalDate)
        {
            AssetID = assetID;
            AssetName = assetName;
            RenewalDate = renewalDate;
        }
    }
}
