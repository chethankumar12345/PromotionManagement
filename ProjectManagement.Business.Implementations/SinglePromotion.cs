using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PromotionManagement.Business.Core;
using PromotionManagement.Shared.Model;

namespace ProjectManagement.Business.Implementations
{
    public class SinglePromotion : IPromotion
    {
        public double ApplyDiscount(Promotions pPromotion, List<PromotionCartItem> pItems)
        {
            double price = 0;
            var promotionItem = pPromotion.CombinationList.First();
            var promotionCartItem =
                pItems.FirstOrDefault(pX => pX.Item.ItemId == promotionItem.ItemId);
            if (promotionCartItem != null)
            {
                var totalQuantityWithItem = promotionCartItem.AvailableQuantity;
                if (totalQuantityWithItem > promotionItem.Quantity)
                {
                    int promotionAppliedItemsCount = totalQuantityWithItem / promotionItem.Quantity;
                    price = price + promotionAppliedItemsCount * promotionItem.Quantity *
                        pItems.First(pX => pX.Item.ItemId == promotionItem.ItemId)
                            .Item.Price - promotionAppliedItemsCount * pPromotion.Discount;

                    var promotionAppliedQuantity = promotionAppliedItemsCount * promotionItem.Quantity;
                    promotionCartItem.AvailableQuantity =
                        promotionCartItem.AvailableQuantity - promotionAppliedQuantity;
                    promotionCartItem.PromotionAppliedQuantity =
                        promotionCartItem.PromotionAppliedQuantity + promotionAppliedQuantity;
                }
            }

            return price;
        }
    }
}
