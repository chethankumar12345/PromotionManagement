using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionManagement.Shared.Model
{
    public class CartItem
    {
        public CartItem(Item pItem, int pQuantity)
        {
            Item = pItem;
            Quantity = pQuantity;
        }
        public Item Item { get;  }
        public int Quantity { get;  }
    }
}
