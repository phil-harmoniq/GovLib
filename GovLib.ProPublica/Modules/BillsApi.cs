using System.Net;
using Newtonsoft.Json.Linq;
using GovLib.ProPublica.Urls;
using GovLib.Contracts;
using GovLib.ProPublica.Util;
using GovLib.Util;
using GovLib.ProPublica.Util.ApiModels.BillModels;
using System.Linq;

namespace GovLib.ProPublica.Modules
{
    /// <summary> Get information about bills introduced to congress.</summary>
    public class BillsApi
    {
        private Congress _parent { get; }

        internal BillsApi(Congress parent)
        {
            _parent = parent;
        }

        /// <summary> Get recent bills by status.</summary>
        public Bill[] GetRecentBills(Chamber chamber, int congress, BillStatus status)
        {
            using (var client = new HttpClient())
            {
                var chamberString = EnumConvert.ChamberEnumToString(chamber);
                var statusString = EnumConvert.BillStatusEnumToString(status);
                var url = string.Format(BillUrls.RecentBills, congress, chamberString, statusString);
                var result = client.Get<ResultWrapper<BillsWrapper<ApiBill>>>(url, _parent.Headers);
                return result?.results?[0].bills.Select(b => ApiBill.Convert(b)).ToArray();
            }
        }

        /// <summary> Get recent bills by member.</summary>
        public Bill[] GetRecentBillsByMember(ICongressMember politician)
        {
            return GetRecentBillsByMember(politician.CongressID);
        }

        /// <summary> Get recent bills by member id.</summary>
        public Bill[] GetRecentBillsByMember(string id)
        {
            using (var client = new HttpClient())
            {
                var url = string.Format(BillUrls.MemberBills, id, "introduced");
                var result = client.Get<ResultWrapper<BillsWrapper<ApiBill>>>(url, _parent.Headers);
                return result?.results?[0].bills.Select(b => ApiBill.Convert(b)).ToArray();
            }
        }
    }
}