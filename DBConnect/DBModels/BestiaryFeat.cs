namespace DBConnect.DBModels
{
  public partial class BestiaryFeat
    {
        public int BestiaryFeatId { get; set; }
        public int BestiaryId { get; set; }
        public int FeatId { get; set; }
        public string Notes { get; set; }
    }
}
