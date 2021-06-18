using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PromotionManagement.Shared.Model;

namespace PromotionManagement.Business.Core
{
    public class CartContainer : ICartContainer
    {
        public Cart GetCartItems(List<Item> pItems, Predicate<Item> pCheckItem, Func<Item,int> pGetQuantity)
        {
            var cartItems = new List<CartItem>();
            for (int i = 0; i < 3; i++)
            {
                var items = pItems.Where(pCheckItem.Invoke);
                foreach (var item in items)
                {
                    if (item != null)
                    {
                        cartItems.Add(new CartItem(item, pGetQuantity.Invoke(item)));
                    }
                }
            }

            return new Cart(cartItems);
        }
    }
}
