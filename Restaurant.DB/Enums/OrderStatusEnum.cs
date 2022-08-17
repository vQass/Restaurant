namespace Restaurant.DB.Enums
{
    public enum OrderStatusEnum : byte
    {
        Pending,
        InProgress,
        Completed,
        Cancelled,
        Suspended
    }

    public static class OrderStatusDictionary
    {
        static OrderStatusDictionary()
        {
            OrderStatusesWithDescription = new Dictionary<OrderStatusEnum, string>()
            {
                { OrderStatusEnum.Pending, "W oczekiwaniu" },
                { OrderStatusEnum.InProgress, "W trakcie realizajci"},
                { OrderStatusEnum.Completed, "Zrealizowane"},
                { OrderStatusEnum.Cancelled, "Anulowane"},
                { OrderStatusEnum.Suspended, "Tymczasowo zawieszone"}
            };
        }

        public static Dictionary<OrderStatusEnum, string> OrderStatusesWithDescription { get; }
    }
}
