using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class AssetManagementSystem
{
    private const string connectionString = "Data Source=MSI\\SQLEXPRESS;Initial Catalog=AssetManagementSystem;Integrated Security=True";
    public static void InsertNewAsset(string assetName, decimal purchaseCost, decimal salvageValue, int usefulLife)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string sqlQuery = @"
            INSERT INTO ASSET (asset_name, purchase_cost, salvage_value, useful_life)
            VALUES (@AssetName, @PurchaseCost, @SalvageValue, @UsefulLife)";

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                command.Parameters.AddWithValue("@AssetName", assetName);
                command.Parameters.AddWithValue("@PurchaseCost", purchaseCost);
                command.Parameters.AddWithValue("@SalvageValue", salvageValue);
                command.Parameters.AddWithValue("@UsefulLife", usefulLife);

                command.ExecuteNonQuery();
            }
        }
    }
    public static void UpdateAssetMaintenanceRenewalDate(int assetID, DateTime newRenewalDate)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string sqlQuery = @"
            UPDATE Asset_Maintenance
            SET renewal_date = @NewRenewalDate
            WHERE asset_id = @AssetID";

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                command.Parameters.AddWithValue("@NewRenewalDate", newRenewalDate);
                command.Parameters.AddWithValue("@AssetID", assetID);

                command.ExecuteNonQuery();
            }
        }
    }
    public static void DeleteAsset(int assetID)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string sqlQuery = @"
            DELETE FROM ASSET
            WHERE asset_id = @AssetID";

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                command.Parameters.AddWithValue("@AssetID", assetID);

                command.ExecuteNonQuery();
            }
        }
    }
    public static void Main()
    {
        List<AssetIssuedToEmployee> issuedAssets = GetIssuedAssets();
        List<AssetDepreciation> depreciatedAssets = GetDepreciatedAssets();
        List<AssetIssuedToPeople> assetsIssuedToPeople = GetAssetsIssuedToPeople();
        List<AssetMaintenance> assetsWithMaintenance = GetAssetsWithMaintenance();

        Console.WriteLine("Assets Issued to Employees:");
        foreach (var asset in issuedAssets)
        {
            Console.WriteLine($"Asset ID: {asset.AssetID}, Name: {asset.AssetName}, Issued To: {asset.EmployeeName}, Issue Date: {asset.IssueDate}");
        }

        Console.WriteLine("\nDepreciation Values:");
        foreach (var asset in depreciatedAssets)
        {
            Console.WriteLine($"Asset ID: {asset.AssetID}, Name: {asset.AssetName}, Depreciation Value: {asset.DepreciationValue:C}");
        }

        Console.WriteLine("\nAssets Issued to People:");
        foreach (var asset in assetsIssuedToPeople)
        {
            Console.WriteLine($"Asset ID: {asset.AssetID}, Name: {asset.AssetName}, Issued To: {asset.EmployeeName}, Issue Date: {asset.IssueDate}");
        }

        Console.WriteLine("\nAssets with Maintenance Renewal Date:");
        foreach (var asset in assetsWithMaintenance)
        {
            Console.WriteLine($"Asset ID: {asset.AssetID}, Name: {asset.AssetName}, Maintenance Renewal Date: {asset.RenewalDate.ToShortDateString()}");
        }
    }

    public static List<AssetIssuedToEmployee> GetIssuedAssets()
    {
        List<AssetIssuedToEmployee> issuedAssets = new List<AssetIssuedToEmployee>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string sqlQuery = @"
                SELECT 
                    a.asset_id,
                    a.asset_name,
                    e.employee_name,
                    ia.issue_date
                FROM 
                    ASSET a
                    JOIN Issued_Assets ia ON a.asset_id = ia.asset_id
                    JOIN Employees e ON ia.employee_id = e.employee_id";

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                {
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);

                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        int assetID = Convert.ToInt32(row["asset_id"]);
                        string assetName = Convert.ToString(row["asset_name"]);
                        string employeeName = Convert.ToString(row["employee_name"]);
                        DateTime issueDate = Convert.ToDateTime(row["issue_date"]);

                        issuedAssets.Add(new AssetIssuedToEmployee(assetID, assetName, employeeName, issueDate));
                    }
                }
            }
        }

        return issuedAssets;
    }

    public static List<AssetDepreciation> GetDepreciatedAssets()
    {
        List<AssetDepreciation> depreciatedAssets = new List<AssetDepreciation>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string sqlQuery = @"
                SELECT 
                    asset_id,
                    asset_name,
                    (purchase_cost - salvage_value) / useful_life AS depreciation_value
                FROM 
                    ASSET";

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                {
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);

                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        int assetID = Convert.ToInt32(row["asset_id"]);
                        string assetName = Convert.ToString(row["asset_name"]);
                        decimal depreciationValue = 0;

                        if (row["depreciation_value"] != DBNull.Value)
                        {
                            depreciationValue = Convert.ToDecimal(row["depreciation_value"]);
                        }

                        depreciatedAssets.Add(new AssetDepreciation(assetID, assetName, depreciationValue));
                    }
                }
            }
        }

        return depreciatedAssets;
    }

    public static List<AssetIssuedToPeople> GetAssetsIssuedToPeople()
    {
        List<AssetIssuedToPeople> assetsIssuedToPeople = new List<AssetIssuedToPeople>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string sqlQuery = @"
                SELECT 
                    a.asset_id,
                    a.asset_name,
                    e.employee_name,
                    ia.issue_date
                FROM 
                    ASSET a
                    JOIN Issued_Assets ia ON a.asset_id = ia.asset_id
                    JOIN Employees e ON ia.employee_id = e.employee_id";

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                {
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);

                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        int assetID = Convert.ToInt32(row["asset_id"]);
                        string assetName = Convert.ToString(row["asset_name"]);
                        string employeeName = Convert.ToString(row["employee_name"]);
                        DateTime issueDate = Convert.ToDateTime(row["issue_date"]);

                        assetsIssuedToPeople.Add(new AssetIssuedToPeople(assetID, assetName, employeeName, issueDate));
                    }
                }
            }
        }

        return assetsIssuedToPeople;
    }

    public static List<AssetMaintenance> GetAssetsWithMaintenance()
    {
        List<AssetMaintenance> assetsWithMaintenance = new List<AssetMaintenance>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string sqlQuery = @"
                SELECT 
                    a.asset_id,
                    a.asset_name,
                    am.renewal_date
                FROM 
                    ASSET a
                    JOIN Asset_Maintenance am ON a.asset_id = am.asset_id";

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                {
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);

                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        int assetID = Convert.ToInt32(row["asset_id"]);
                        string assetName = Convert.ToString(row["asset_name"]);
                        DateTime renewalDate = Convert.ToDateTime(row["renewal_date"]);

                        assetsWithMaintenance.Add(new AssetMaintenance(assetID, assetName, renewalDate));
                    }
                }
            }
        }

        return assetsWithMaintenance;
    }
}

public class AssetIssuedToEmployee
{
    public int AssetID { get; }
    public string AssetName { get; }
    public string EmployeeName { get; }
    public DateTime IssueDate { get; }

    public AssetIssuedToEmployee(int assetID, string assetName, string employeeName, DateTime issueDate)
    {
        AssetID = assetID;
        AssetName = assetName;
        EmployeeName = employeeName;
        IssueDate = issueDate;
    }
}

public class AssetDepreciation
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

public class AssetIssuedToPeople : AssetIssuedToEmployee
{
    public AssetIssuedToPeople(int assetID, string assetName, string employeeName, DateTime issueDate)
        : base(assetID, assetName, employeeName, issueDate)
    {
    }
}

public class AssetMaintenance
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