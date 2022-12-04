namespace Restaurant.Data.Models.MealModels
{
    public class MealGroupViewModel
    {
        public string GroupName { get; set; }
        public List<MealGroupItemViewModel> Meals { get; set; }
    }
}
