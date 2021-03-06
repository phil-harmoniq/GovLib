using GovLib.Util;

namespace GovLib.ProPublica
{
    /// <summary>Full implementation for a house representative.</summary>
    public class Representative : Politician
    {
        #pragma warning disable CS1591

        public int District { get; internal set; }
        public bool AtLargeDistrict { get; internal set; }

        public override string ToString() =>
            $"Represenative {FullName} ({Party}) [{EnumConvert.StateEnumToCode(State)}-{District}]";
    }
}
