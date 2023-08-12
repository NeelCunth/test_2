using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementLibrary
{
    internal class AssetIssuedToEmployee
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
}
