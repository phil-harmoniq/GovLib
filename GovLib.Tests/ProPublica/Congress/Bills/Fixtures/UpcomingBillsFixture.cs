﻿using GovLib.ProPublica;

namespace GovLib.Tests.ProPublica.Congress.Bills
{
    public class UpcomingBillsFixture : CongressFixture
    {
        public BillSummary[] UpcomingBills { get; }

        public UpcomingBillsFixture()
        {
            UpcomingBills = Congress.Bills.GetUpcomingBills(Chamber.Senate);
            System.Console.WriteLine();
        }
    }
}