using System;
using GovLib.Models;
using System.Globalization;
using GovLib.Util;

namespace GovLib.ProPublica.Util.MemberModels
{
    internal class ApiRepresentativesByState
    {
        public string id { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string party { get; set; }
        public string district { get; set; }

        public static RepresentativeSummary Convert(ApiRepresentativesByState entity, string state)
        {
            var rep = new RepresentativeSummary();

            rep.ID = entity.id;
            rep.FirstName = entity.first_name;
            rep.LastName = entity.last_name;
            rep.Party = entity.party;
            rep.State = (State) EnumConvert.StateCodeToEnum(state);

            if (!string.IsNullOrEmpty(entity.middle_name))
                rep.MiddleName = entity.middle_name;

            if (entity.district == "At-Large")
            {
                rep.District = 1;
                rep.AtLargeDistrict = true;
            }
            else
            {
                rep.District = Int32.Parse(entity.district);
                rep.AtLargeDistrict = false;
            }

            return rep;
        }
    }
}