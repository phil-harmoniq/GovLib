using System;
using Gov.NET.Models;
using Gov.NET.Common.Models.Cards;
using System.Globalization;

namespace Gov.NET.ProPublica.ApiModels
{
    public class ApiRepsLeaving
    {
        public string id { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string party { get; set; }
        public string state { get; set; }
        public string district { get; set; }

        public static RepresentativeCard Convert(ApiRepsLeaving entity)
        {
            var rep = new RepresentativeCard();

            rep.ID = entity.id;
            rep.FirstName = entity.first_name;
            rep.LastName = entity.last_name;
            rep.Party = entity.party;
            rep.State = entity.state;

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