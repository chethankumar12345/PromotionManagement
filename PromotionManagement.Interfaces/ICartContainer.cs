using System;
using System.Collections.Generic;
using PromotionManagement.Shared.Model;

namespace PromotionManagement.Interfaces
{
    public interface ICartContainer
    {
        Cart GetCartItems(List<Item> pItems, Predicate<Item> pCheckItem, Func<Item, int> pGetQuantity);
    }
}
