namespace DraftTool.Models
{
    public class Card
    {
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public string Side { get; set; }
        public string Set { get; set; }
        public int MaxNumberOfUses { get; set; }
    }
}
