using System;
using GovLib.Contracts;
using System.Globalization;
using GovLib.Util;

namespace GovLib.ProPublica.Util.MemberModels
{
    internal class ApiSenatorsByState
    {
        public string id { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string party { get; set; }
    }
}
