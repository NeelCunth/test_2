using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementLibrary
{
    internal class AssetIssuedToPeople : AssetIssuedToEmployee
    {
        public AssetIssuedToPeople(int assetID, string assetName, string employeeName, DateTime issueDate)
            : base(assetID, assetName, employeeName, issueDate)
        {
        }
    }
}
