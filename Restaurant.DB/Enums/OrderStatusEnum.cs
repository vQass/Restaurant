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
            OrderStatusesWithDescription = new Dictionary<byte, string>()
            {
                { (byte)OrderStatusEnum.Pending, "Oczekujące" },
                { (byte)OrderStatusEnum.InProgress, "W trakcie realizacji" },
                { (byte)OrderStatusEnum.Completed, "Zrealizowane" },
                { (byte)OrderStatusEnum.Cancelled, "Anulowane" },
                { (byte)OrderStatusEnum.Suspended, "Tymczasowo zawieszone" }
            };
        }

        public static Dictionary<byte, string> OrderStatusesWithDescription { get; }
    }
}
