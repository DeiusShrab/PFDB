namespace DBConnect.DBModels
{
  public partial class Skill
  {
    public int SkillId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool TrainedOnly { get; set; }
  }
}
