﻿using System.Collections.Generic;
using GovLib.ProPublica;
using Xunit;

namespace GovLib.Tests.ProPublica.Congress.Bills
{
    [Collection("ProPublica Test Collection")]
    public class BillsBySubjectTests : IClassFixture<CongressFixture>
    {
        public IEnumerable<Bill> BillsBySubject { get; }

        public BillsBySubjectTests(CongressFixture fixture)
        {
            BillsBySubject = fixture.Congress.Bills.GetRecentBillsBySubject("meat");
        }

        [Fact]
        public void CollectionIsNotNull()
        {
            Assert.NotNull(BillsBySubject);
        }

        [Fact]
        public void CollectionIsNotEmpty()
        {
            Assert.NotEmpty(BillsBySubject);
        }

        [Fact]
        public void BillsAreNotNull()
        {
            foreach (var bill in BillsBySubject)
                Assert.NotNull(bill);
        }

        [Fact]
        public void BillsHaveAnID()
        {
            foreach (var bill in BillsBySubject)
                Assert.False(string.IsNullOrEmpty(bill.BillID));
        }

        [Fact]
        public void BillsHaveATitle()
        {
            foreach (var bill in BillsBySubject)
                Assert.False(string.IsNullOrEmpty(bill.Title));
        }

        [Fact]
        public void BillsHaveAChamber()
        {
            foreach (var bill in BillsBySubject)
                Assert.NotNull(bill.Chamber);
        }

        [Fact]
        public void BillsHaveAnIntroducedDate()
        {
            foreach (var bill in BillsBySubject)
                Assert.NotNull(bill.Introduced);
        }
    }
}