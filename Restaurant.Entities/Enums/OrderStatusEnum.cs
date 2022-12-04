namespace Restaurant.Entities.Enums
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

            // DO NOT CHANGE TAGS. THEY ARE BEEING USED AS CSS CLASSES IN FRONT
            OrderStatusesTags = new Dictionary<byte, string>()
            {
                { (byte)OrderStatusEnum.Pending, "Pending" },
                { (byte)OrderStatusEnum.InProgress, "InProgress" },
                { (byte)OrderStatusEnum.Completed, "Completed" },
                { (byte)OrderStatusEnum.Cancelled, "Cancelled" },
                { (byte)OrderStatusEnum.Suspended, "Suspended" }
            };
        }

        public static Dictionary<byte, string> OrderStatusesWithDescription { get; }
        public static Dictionary<byte, string> OrderStatusesTags { get; }
    }
}
