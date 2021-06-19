using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PromotionManagement.Business.Core;
using PromotionManagement.Interfaces;
using PromotionManagement.Shared.Model;

namespace ProjectManagement.Business.Implementations
{
    public class CombinedPromotion : IPromotion
    {
        public double ApplyDiscount(Promotions pPromotion, List<PromotionCartItem> pItems)
        {
            double price = 0;
            var numberOfSetPromotionCanApply = 0;
            bool breakLoop = false;

            while (true)
            {
                foreach (var combination in pPromotion.CombinationList)
                {
                    var item = pItems.First(pX => pX.Item.ItemId.Equals(combination.ItemId));
                    if (item.AvailableQuantity < combination.Quantity * (numberOfSetPromotionCanApply + 1))
                    {
                        breakLoop = true;
                    }
                }

                if (breakLoop)
                {
                    break;
                }

                numberOfSetPromotionCanApply++;
            }

            if (numberOfSetPromotionCanApply > 0)
            {
                foreach (var combination in pPromotion.CombinationList)
                {
                    var item = pItems.First(pX => pX.Item.ItemId.Equals(combination.ItemId));
                    item.AvailableQuantity =
                        item.AvailableQuantity - combination.Quantity * numberOfSetPromotionCanApply;

                    item.PromotionAppliedQuantity =
                        item.PromotionAppliedQuantity + combination.Quantity * numberOfSetPromotionCanApply;
                    price = price +
                            (combination.Quantity * numberOfSetPromotionCanApply) * item.Item.Price;
                }

                price = price - numberOfSetPromotionCanApply * pPromotion.Discount;
            }

            return price;
        }
    }
}
