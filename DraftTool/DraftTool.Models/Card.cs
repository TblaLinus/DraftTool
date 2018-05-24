using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftTool.Models
{
    public class Card
    {
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public string CardSide { get; set; }
        public string CardSet { get; set; }
        public int MaxNumberOfUses { get; set; }
    }
}
