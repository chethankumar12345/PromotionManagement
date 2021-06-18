using System;
using System.Collections.Generic;
using System.Text;
using PromotionManagement.Shared.Model;

namespace PromotionManagement.Business.Core
{
    public interface ICartContainer
    {
        Cart GetCartItems(List<Item> pItems, Predicate<Item> pCheckItem, Func<Item, int> pGetQuantity);
    }
}
