using System.Threading;
using Gov.NET.Models;
using Gov.NET.Common.Models.Cards;
using Gov.NET.ProPublica;

namespace Gov.NET.Tests.ProPublicaTests.CongressTests.MembersTests
{
    public class RepresentativesByStateFixture : CongressFixture
    {
        public RepresentativeCard[] RepresentativeCards { get; }

        public RepresentativesByStateFixture()
        {
            // Sleep before making api call to limit request spam.
            Thread.Sleep(60);
            RepresentativeCards = Congress.Members.GetRepresentaivesByState("CO");
        }
    }
}