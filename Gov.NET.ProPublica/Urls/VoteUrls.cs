namespace Gov.NET.ProPublica.Urls
{
    internal static class VoteUrls
    {
        internal static string RollCall = "https://api.propublica.org/congress/v1/{0}/{1}/sessions/{2}/votes/{3}.json";
        internal static string VoteByType = "https://api.propublica.org/congress/v1/{0}/{1}/votes/{2}.json";
        internal static string VoteByDate = "https://api.propublica.org/congress/v1/{0}/votes/{1}/{2}.json";
        internal static string Nominations = "https://api.propublica.org/congress/v1/{0}/nominations.json";
    }
}
