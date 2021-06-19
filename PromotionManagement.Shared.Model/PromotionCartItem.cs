namespace PromotionManagement.Shared.Model
{
    public class PromotionCartItem
    {
        public PromotionCartItem(Item pItem, int pAvailableQuantity)
        {
            Item = pItem;
            AvailableQuantity = pAvailableQuantity;
        }
        public Item Item { get; }
        public int AvailableQuantity { get; set; }
        public int PromotionAppliedQuantity { get; set; }
    }
}
