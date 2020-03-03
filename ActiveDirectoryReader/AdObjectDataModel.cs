using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveDirectoryReader
{
    public class AdObjectDataModel
    {
        public string Name { get; set; }

        public string AdsPath { get; set; }

        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public Guid Guid { get; set; }
        public string DistinguishedName { get; set; }
        public string Category { get; set; }

        /// <summary>
        /// 不是每個物件都有此屬性。例如：Organizational-Unit和Container就沒有此屬性。但每個物件都有GUID，因此改用GUID比較好。
        /// </summary>
        public string Sid { get; set; }

        //public List<string> Class { get; set; }
    }
}
