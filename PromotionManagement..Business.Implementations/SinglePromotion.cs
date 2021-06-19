using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PromotionManagement.Business.Core;
using PromotionManagement.Interfaces;
using PromotionManagement.Shared.Model;

namespace ProjectManagement.Business.Implementations
{
    public class SinglePromotion : IPromotion
    {
        public double ApplyDiscount(Promotions pPromotion, List<PromotionCartItem> pItems)
        {
            double price = 0;
            var promotion = pPromotion.CombinationList.First();
            var cartItem =
                pItems.FirstOrDefault(pX => pX.Item.ItemId == promotion.ItemId);
            if (cartItem != null)
            {
                var totalQuantityWithItem = cartItem.AvailableQuantity;
                if (totalQuantityWithItem > promotion.Quantity)
                {
                    int promotionAppliedItemsCount = totalQuantityWithItem / promotion.Quantity;
                    price = price + promotionAppliedItemsCount * promotion.Quantity *
                        pItems.First(pX => pX.Item.ItemId == promotion.ItemId)
                            .Item.Price - promotionAppliedItemsCount * pPromotion.Discount;

                    var promotionAppliedQuantity = promotionAppliedItemsCount * promotion.Quantity;
                    cartItem.AvailableQuantity =
                        cartItem.AvailableQuantity - promotionAppliedQuantity;
                    cartItem.PromotionAppliedQuantity =
                        cartItem.PromotionAppliedQuantity + promotionAppliedQuantity;
                }
            }

            return price;
        }
    }
}
