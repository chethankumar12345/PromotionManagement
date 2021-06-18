using System;
using System.Collections.Generic;
using System.Text;
using PromotionManagement.Shared.Model;

namespace PromotionManagement.Business.Core
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
