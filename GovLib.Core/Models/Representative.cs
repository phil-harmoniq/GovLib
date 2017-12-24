namespace GovLib.Models
{
    /// <summary>Full implementation for a house representative.</summary>
    public class Representative : Politician, IRepresentative
    {
        #pragma warning disable CS1591

        public int District { get; set; }
        public bool AtLargeDistrict { get; set; }
        
        #pragma warning restore CS1591
    }
}