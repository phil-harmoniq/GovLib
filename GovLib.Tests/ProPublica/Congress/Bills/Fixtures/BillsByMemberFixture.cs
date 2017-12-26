using System.Threading;
using GovLib.Contracts;
using GovLib.ProPublica;

namespace GovLib.Tests.ProPublica.Congress.Bills
{
    public class BillsByMemberFixture : CongressFixture
    {
        public Bill[] BillsByMember { get; }

        public BillsByMemberFixture()
        {
            BillsByMember = Congress.Bills.GetRecentBillsByMember("L000287");
        }
    }
}