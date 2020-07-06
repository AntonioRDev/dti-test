using ProvaDTI.Models.Enums;

namespace ProvaDTI.Models
{
    public class Food
    {
        public string Name { get; set; }
        public FoodGroup FoodGroup { get; set; }
        public double CaloricAmount { get; set; }
        public string Portion { get; set; }
    }
}
