using System;
using System.Collections.Generic;
using System.Linq;
using GovLib.ProPublica.Util.ApiModels.BillModels;
using GovLib.Util;
using Newtonsoft.Json;

namespace GovLib.ProPublica.Util.ApiModels.VoteModels
{
    internal class ApiRollCallVote
    {
        [JsonProperty("congress")]
        internal int Congress { get; set; }
        
        [JsonProperty("session")]
        internal int Session { get; set; }
        
        [JsonProperty("chamber")]
        internal string Chamber { get; set; }
        
        [JsonProperty("roll_call")]
        internal int RollCall { get; set; }
        
        [JsonProperty("question")]
        internal string Question { get; set; }
        
        [JsonProperty("description")]
        internal string Description { get; set; }
        
        [JsonProperty("vote_type")]
        internal string VoteType { get; set; }
        
        [JsonProperty("date")]
        internal string Date { get; set; }
        
        [JsonProperty("time")]
        internal string Time { get; set; }
        
        [JsonProperty("result")]
        internal string Result { get; set; }
        
        [JsonProperty("title")]
        internal string Title { get; set; }
        
        [JsonProperty("document_title")]
        internal string DocumentTitle { get; set; }
        
        [JsonProperty("bill")]
        internal ApiVoteBill Bill { get; set; }
        
        [JsonProperty("amendment")]
        internal ApiVoteAmendment Amendment { get; set; }
        
        [JsonProperty("nomination")]
        internal ApiVoteNomination Nomination { get; set; }
        
        [JsonProperty("republican")]
        internal ApiPartyVote RepublicanVotes { get; set; }
        
        [JsonProperty("democratic")]
        internal ApiPartyVote DemocraticVotes { get; set; }
        
        [JsonProperty("independent")]
        internal ApiPartyVote IndependentVotes { get; set; }
        
        [JsonProperty("total")]
        internal ApiPartyVote TotalVotes { get; set; }
        
        [JsonProperty("positions")]
        internal ApiVoteRollCallPosition[] Positions { get; set; }

        internal static VoteRollCall Convert(ApiRollCallVote entity)
        {
            if (entity == null)
                return null;
            
            var democraticVotes = new VoteResult
            {
                Yes = entity.DemocraticVotes.Yes,
                No = entity.DemocraticVotes.No,
                NotVoting = entity.DemocraticVotes.NotVoting,
                Present = entity.DemocraticVotes.Present,
            };
            
            var republicanVotes = new VoteResult
            {
                Yes = entity.RepublicanVotes.Yes,
                No = entity.RepublicanVotes.No,
                NotVoting = entity.RepublicanVotes.NotVoting,
                Present = entity.RepublicanVotes.Present,
            };
            
            var independentVotes = new VoteResult
            {
                Yes = entity.IndependentVotes.Yes,
                No = entity.IndependentVotes.No,
                NotVoting = entity.IndependentVotes.NotVoting,
                Present = entity.IndependentVotes.Present,
            };
            
            var totalVotes = new VoteResult
            {
                Yes = entity.TotalVotes.Yes,
                No = entity.TotalVotes.No,
                NotVoting = entity.TotalVotes.NotVoting,
                Present = entity.TotalVotes.Present,
            };

            Chamber chamber;

            if (entity.Chamber.Equals("senate", StringComparison.InvariantCultureIgnoreCase))
                chamber = GovLib.Chamber.Senate;
            else
                chamber = GovLib.Chamber.House;

            return new VoteRollCall
            {
                Chamber = chamber,
                Congress = entity.Congress,
                Session = entity.Session,
                RollCall = entity.RollCall,
                Question = entity.Question,
                Title = entity.Title ?? entity.DocumentTitle,
                Description = entity.Description,
                VoteType = entity.VoteType,
                TimeStamp = DateTime.Parse($"{entity.Date} {entity.Time}"),
                Passed = entity.Result.Equals("Passed", StringComparison.InvariantCultureIgnoreCase),
                DemocraticVotes = democraticVotes,
                RepublicanVotes = republicanVotes,
                IndependentVotes = independentVotes,
                TotalVotes = totalVotes,
                Positions = entity.Positions.Select(p => ApiVoteRollCallPosition.Convert(p)).ToArray()
            };
        }
    }
}