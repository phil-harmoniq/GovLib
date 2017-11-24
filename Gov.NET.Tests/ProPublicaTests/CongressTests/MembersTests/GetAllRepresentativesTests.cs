using Xunit;

namespace Gov.NET.Tests.ProPublicaTests.CongressTests.MembersTests
{
    public class GetAllRepresentativesTests : IClassFixture<AllReprentativesFixture>
    {
        public AllReprentativesFixture Fixture { get; }

        public GetAllRepresentativesTests(AllReprentativesFixture fixture)
        {
            this.Fixture = fixture;
        }

        [Fact]
        public void CollectionIsNotNull()
        {
            Assert.NotNull(Fixture.AllRepresentatives);
        }

        [Fact]
        public void CollectionIsNotEmpty()
        {
            Assert.NotEmpty(Fixture.AllRepresentatives);
        }

        [Fact]
        public void AllRepresentativesArentNull()
        {
            foreach (var representative in Fixture.AllRepresentatives)
                Assert.NotNull(representative);
        }

        [Fact]
        public void AllRepresentativesHaveFirstName()
        {
            foreach (var representative in Fixture.AllRepresentatives)
                Assert.False(string.IsNullOrEmpty(representative.FirstName));
        }

        [Fact]
        public void AllRepresentativesHaveLastName()
        {
            foreach (var representative in Fixture.AllRepresentatives)
                Assert.False(string.IsNullOrEmpty(representative.LastName));
        }

        [Fact]
        public void AllRepresentativesHaveFullName()
        {
            foreach (var representative in Fixture.AllRepresentatives)
                Assert.False(string.IsNullOrEmpty(representative.FullName));
        }

        [Fact]
        public void AllRepresentativesHaveAnID()
        {
            foreach (var representative in Fixture.AllRepresentatives)
                Assert.False(string.IsNullOrEmpty(representative.ID));
        }

        [Fact]
        public void AllRepresentativesHaveAGender()
        {
            foreach (var representative in Fixture.AllRepresentatives)
                Assert.NotNull(representative.Gender);
        }

        [Fact]
        public void AllRepresentativesHaveAHomeState()
        {
            foreach (var representative in Fixture.AllRepresentatives)
                Assert.False(string.IsNullOrEmpty(representative.State));
        }

        [Fact]
        public void AllRepresentativesHaveABirthDate()
        {
            foreach (var representative in Fixture.AllRepresentatives)
                Assert.False(string.IsNullOrEmpty(representative.BirthDate.ToString()));
        }

        [Fact]
        public void AllRepresentativesInOfficeHaveADistrict()
        {
            foreach (var representative in Fixture.AllRepresentatives)
                Assert.NotNull(representative.District);
        }

        [Fact]
        public void AllRepresentativesInOfficeHaveAnAtLargeBool()
        {
            foreach (var representative in Fixture.AllRepresentatives)
                Assert.NotNull(representative.AtLargeDistrict);
        }

        [Fact]
        public void AllRepresentativesHavePartyLoyaltyRatio()
        {
            foreach (var representative in Fixture.AllRepresentatives)
                Assert.NotNull(representative.PartyLoyaltyRatio);
        }

        [Fact]
        public void AllRepresentativesHaveMissedVotesRatio()
        {
            foreach (var representative in Fixture.AllRepresentatives)
                Assert.NotNull(representative.MissedVotesRatio);
        }

        [Fact]
        public void AllRepresentativesHaveTotalVotesCast()
        {
            foreach (var representative in Fixture.AllRepresentatives)
                Assert.NotNull(representative.VotesCast);
        }

        [Fact]
        public void AllRepresentativesHaveTotalVotesMissed()
        {
            foreach (var representative in Fixture.AllRepresentatives)
                Assert.NotNull(representative.VotesMissed);
        }

        [Fact]
        public void AllRepresentativesHaveTotalVotesPresent()
        {
            foreach (var representative in Fixture.AllRepresentatives)
                Assert.NotNull(representative.VotesPresent);
        }
    }
}