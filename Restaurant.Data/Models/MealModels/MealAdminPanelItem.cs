namespace Restaurant.Data.Models.MealModels
{
    public class MealAdminPanelItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool Available { get; set; } = false;
        public short MealCategoryId { get; set; }
        public string MealCategoryName { get; set; }
    }
}
