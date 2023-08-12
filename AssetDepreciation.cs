using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementLibrary
{
    internal class AssetDepreciation
    {
        public int AssetID { get; }
        public string AssetName { get; }
        public decimal DepreciationValue { get; }

        public AssetDepreciation(int assetID, string assetName, decimal depreciationValue)
        {
            AssetID = assetID;
            AssetName = assetName;
            DepreciationValue = depreciationValue;
        }
    }
}
