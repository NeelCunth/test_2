using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementLibrary
{
    public class AssetManagementSystem
    {
        private const string connectionString = "Data Source=MSI\\SQLEXPRESS;Initial Catalog=AssetManagementSystem;Integrated Security=True";

        // ... InsertNewAsset method ...
        public static void InsertNewAsset(string assetName, decimal purchaseCost, decimal salvageValue, int usefulLife)
        {
            // ... Existing code for InsertNewAsset ...
        }

        // ... UpdateAssetMaintenanceRenewalDate method ...
        public static void UpdateAssetMaintenanceRenewalDate(int assetID, DateTime newRenewalDate)
        {
            // ... Existing code for UpdateAssetMaintenanceRenewalDate ...
        }

        // ... DeleteAsset method ...
        public static void DeleteAsset(int assetID)
        {
            // ... Existing code for DeleteAsset ...
        }

        // ... Main method ...
        public static void Main()
        {
            // ... Existing code for Main ...
        }

        // ... GetIssuedAssets method ...
        public static List<AssetIssuedToEmployee> GetIssuedAssets()
        {
            // ... Existing code for GetIssuedAssets ...
        }

        // ... GetDepreciatedAssets method ...
        public static List<AssetDepreciation> GetDepreciatedAssets()
        {
            // ... Existing code for GetDepreciatedAssets ...
        }

        // ... GetAssetsIssuedToPeople method ...
        public static List<AssetIssuedToPeople> GetAssetsIssuedToPeople()
        {
            // ... Existing code for GetAssetsIssuedToPeople ...
        }

        // ... GetAssetsWithMaintenance method ...
        public static List<AssetMaintenance> GetAssetsWithMaintenance()
        {
            // ... Existing code for GetAssetsWithMaintenance ...
        }
    }

}
