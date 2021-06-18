using System;
using System.Collections.Generic;

namespace PromotionManagement.Shared.Model
{
    public class Cart
    {
        public Cart(List<CartItem> pItems)
        {
            Items = pItems;
        }

        public List<CartItem> Items { get; set; }
    }
}
