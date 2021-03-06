using System.Linq;
using System.Collections.Generic;
using GovLib.Contracts;
using GovLib.ProPublica.Urls;
using GovLib.ProPublica.Util;
using GovLib.ProPublica.Util.MemberModels;
using GovLib.Util;
using GovLib.ProPublica.Util.ApiModels.Wrappers;
using Newtonsoft.Json;
using GovLib.ProPublica.Builders;

namespace GovLib.ProPublica.Modules
{
    /// <summary>
    /// Get information about members of congress.
    /// </summary>
    public class MembersApi
    {
        private Congress _congress { get; }
        private IMemberUrlBuilder _memberUrlBuilder { get; }

        internal MembersApi(Congress congress, IMemberUrlBuilder memberUrlBuilder)
        {
            _congress = congress;
            _memberUrlBuilder = memberUrlBuilder;
        }

        /// <summary>
        /// Fetch all senators from the current congress session.
        /// </summary>
        /// <returns><see cref="Senator" />array</returns>
        public IEnumerable<Senator> GetAllSenators()
        {
            return GetAllSenators(_congress.CurrentCongress);
        }

        /// <summary>
        /// Fetch all senators from given congress session.
        /// </summary>
        /// <param name="congressNum">Congress number</param>
        /// <returns><see cref="Senator" />array.</returns>
        public IEnumerable<Senator> GetAllSenators(int congressNum)
        {
            var url = _memberUrlBuilder.AllSenators(congressNum.ToString());
            var result = _congress.Client.Get(url, _congress.Headers);
            var json = JsonConvert.DeserializeObject<ResultsWrapper<MembersWrapper<ApiAllSenators>>>(result);
            var sens = json?.Results?[0].Members?.Select(s => ApiAllSenators.Convert(s));
            return sens;
        }

        /// <summary>
        /// Fetch all representatives from the current congress session.
        /// </summary>
        /// <returns><see cref="Representative" />array.</returns>
        public IEnumerable<Representative> GetAllRepresentatives()
        {
            return GetAllRepresentatives(_congress.CurrentCongress);
        }

        /// <summary>
        /// Fetch all representatives from given congress session.
        /// </summary>
        /// <param name="congressNum">Congress number</param>
        /// <returns><see cref="Representative" />array.</returns>
        public IEnumerable<Representative> GetAllRepresentatives(int congressNum)
        {
            var url = _memberUrlBuilder.AllRepresentatives(congressNum.ToString());
            var result = _congress.Client.Get(url, _congress.Headers);
            var json = JsonConvert.DeserializeObject<ResultsWrapper<MembersWrapper<ApiAllReps>>>(result);
            var reps = json?.Results?[0].Members?.Where(r => r.IsVotingMember()).Select(r => ApiAllReps.Convert(r));
            return reps;
        }

        /// <summary>
        /// Find congress member by BioGuide ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="Politician" /></returns>
        public Politician GetMemberByID(string id)
        {
            var url = _memberUrlBuilder.MemberByID(id);
            var result = _congress.Client.Get(url, _congress.Headers);
            var json = JsonConvert.DeserializeObject<ResultsWrapper<ApiMember>>(result);
            return json?.Results?.Where(r => r.IsVotingMember()).Select(p => ApiMember.Convert(p)).FirstOrDefault();
        }

        /// <summary>
        /// Get full Politician info from a contract.
        /// </summary>
        /// <param name="politician"><see cref="ICongressMember" /></param>
        /// <returns><see cref="Politician" /></returns>
        public Politician GetMemberByID(ICongressMember politician)
        {
            return GetMemberByID(politician.CongressID);
        }

        /// <summary>
        /// Fetch new congress members from given congress session.
        /// </summary>
        /// <returns><see cref="Politician" /></returns>
        public IEnumerable<PoliticianSummary> GetNewMembers()
        {
            var url = _memberUrlBuilder.NewMembers();
            var result = _congress.Client.Get(url, _congress.Headers);
            var json = JsonConvert.DeserializeObject<ResultsWrapper<NewMembersWrapper>>(result);
            var newMembers = json?.Results?[0].Members?.Where(r => r.IsVotingMember());
            return newMembers.Select(m => ApiNewMembers.Convert(m));
        }

        /// <summary>
        /// Fetch both current senators from the given state enum.
        /// </summary>
        /// <param name="state"><see cref="State" /></param>
        /// <returns><see cref="Senator" />array.</returns>
        public IEnumerable<SenatorSummary> GetSenatorsByState(State state)
        {
            return GetSenatorsByState(EnumConvert.StateEnumToCode(state));
        }

        /// <summary>
        /// Fetch both current senators from the given state.
        /// </summary>
        /// <param name="state">State code.</param>
        /// <returns><see cref="Senator" />array.</returns>
        public IEnumerable<SenatorSummary> GetSenatorsByState(string state)
        {
            var url = _memberUrlBuilder.SenatorsByState(state);
            var result = _congress.Client.Get(url, _congress.Headers);
            var json = JsonConvert.DeserializeObject<ResultsWrapper<ApiSenatorsByState>>(result);
            return json?.Results?.Select(s => ApiSenatorsByState.Convert(s, state));
        }

        /// <summary>
        /// Fetch all current representatives from the given state enum.
        /// </summary>
        /// <param name="state"><see cref="State" /></param>
        /// <returns><see cref="Representative" />array.</returns>
        public IEnumerable<RepresentativeSummary> GetRepresentativesByState(State state)
        {
            return GetRepresentaivesByState(EnumConvert.StateEnumToCode(state));
        }

        /// <summary>
        /// Fetch all current representatives from the given state.
        /// </summary>
        /// <param name="state">State code.</param>
        /// <returns><see cref="Representative" />array.</returns>
        public IEnumerable<RepresentativeSummary> GetRepresentaivesByState(string state)
        {
            var url = _memberUrlBuilder.RepresentativesByState(state);
            var result = _congress.Client.Get(url, _congress.Headers);
            var json = JsonConvert.DeserializeObject<ResultsWrapper<ApiRepresentativesByState>>(result);
            return json?.Results?.Select(r => ApiRepresentativesByState.Convert(r, state));
        }

        /// <summary>
        /// Fetch current representative from the given state enum and district.
        /// </summary>
        /// <param name="state"><see cref="State" /></param>
        /// <param name="district">District number.</param>
        /// <returns><see cref="Representative" /></returns>
        public RepresentativeSummary GetRepresentiveFromDistrict(State state, int district)
        {
            return GetRepresentiveFromDistrict(EnumConvert.StateEnumToCode(state), district);
        }

        /// <summary>
        /// Fetch current representative from the given state and district.
        /// </summary>
        /// <param name="state">State code.</param>
        /// <param name="district">District number.</param>
        /// <returns><see cref="Representative" /></returns>
        public RepresentativeSummary GetRepresentiveFromDistrict(string state, int district)
        {
            var url = _memberUrlBuilder.RepresentativeFromDistrict(state, district.ToString());
            var result = _congress.Client.Get(url, _congress.Headers);
            var json = JsonConvert.DeserializeObject<ResultsWrapper<ApiRepresentativeFromDistrict>>(result);
            return json?.Results?.Select(r => ApiRepresentativeFromDistrict.Convert(r, state, district)).FirstOrDefault();
        }

        /// <summary>
        /// Fetch all senators from the current congress session.
        /// </summary>
        /// <returns><see cref="Senator" />array.</returns>
        public IEnumerable<SenatorSummary> GetSenatorsLeavingOffice()
        {
            return GetSenatorsLeavingOffice(_congress.CurrentCongress);
        }

        /// <summary>
        /// Fetch senators that were leaving office during the given congress session.
        /// </summary>
        /// <param name="congressNum">Congress number</param>
        /// <returns><see cref="Senator" />array.</returns>
        public IEnumerable<SenatorSummary> GetSenatorsLeavingOffice(int congressNum)
        {
            var url = _memberUrlBuilder.SenatorsLeaving(congressNum.ToString());
            var result = _congress.Client.Get(url, _congress.Headers);
            var json = JsonConvert.DeserializeObject<ResultsWrapper<MembersWrapper<ApiSenatorsLeaving>>>(result);
            return json?.Results?[0].Members.Select(r => ApiSenatorsLeaving.Convert(r));
        }

        /// <summary>
        /// Fetch all senators from the current congress session.
        /// </summary>
        /// <returns><see cref="Representative" />array.</returns>
        public IEnumerable<RepresentativeSummary> GetRepresentativesLeavingOffice()
        {
            return GetRepresentativesLeavingOffice(_congress.CurrentCongress);
        }

        /// <summary>
        /// Fetch representatives that were leaving office during the given congress session.
        /// </summary>
        /// <param name="congressNum">Congress number</param>
        /// <returns><see cref="Representative" />array.</returns>
        public IEnumerable<RepresentativeSummary> GetRepresentativesLeavingOffice(int congressNum)
        {
            var url = _memberUrlBuilder.RepresentativesLeaving(congressNum.ToString());
            var result = _congress.Client.Get(url, _congress.Headers);
            var json = JsonConvert.DeserializeObject<ResultsWrapper<MembersWrapper<ApiRepsLeaving>>>(result);
            return json?.Results?[0].Members.Select(r => ApiRepsLeaving.Convert(r));
        }
    }
}
