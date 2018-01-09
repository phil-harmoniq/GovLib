using GovLib.Util;

namespace GovLib.ProPublica
{
    /// <summary>Full implementation for a senator.</summary>
    public class Senator : Politician
    {
        #pragma warning disable CS1591

        public string Rank { get; set; }
        public int Class { get; set; }

        public override string ToString() =>
            $"Senator {FullName} ({Party}) [{EnumConvert.StateEnumToCode(State)}-{Class}]";
    }
}
