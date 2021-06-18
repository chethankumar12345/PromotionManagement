using System;
using System.Collections.Generic;
using System.Linq;
using ProjectManagement.Business.Implementations;
using PromotionManagement.Shared.Model;

namespace PromotionManagement.Business.Core
{
    public class PromotionHandler
    {
        private readonly List<PromotionCartItem> _pPromotionCartItems;
        private readonly Func<List<Promotions>> _pLoadAvailablePromotions;

        public PromotionHandler(Cart pCart,Func<List<Promotions>> pLoadAvailablePromotions)
        {
            _pPromotionCartItems = pCart.Items.Select(pX=> new PromotionCartItem(pX.Item, pX.Quantity)).ToList();
            _pLoadAvailablePromotions = pLoadAvailablePromotions;
        }

        public double ApplyPromotionGetTotal()
        {
            double totalPrice = 0;
           
            if (_pLoadAvailablePromotions != null)
                foreach (var promotion in _pLoadAvailablePromotions?.Invoke())
                {
                    if (promotion.CombinationList.Count == 1)
                    {
                     
                        var promotionItem = promotion.CombinationList.First();
                        var promotionCartItem =
                            _pPromotionCartItems.FirstOrDefault(pX => pX.Item.ItemId == promotionItem.ItemId);
                        if (promotionCartItem != null)
                        {
                            var totalQuantityWithItem = promotionCartItem.AvailableQuantity;
                            if (totalQuantityWithItem > promotionItem.Quantity)
                            {
                                int promotionAppliedItemsCount = totalQuantityWithItem / promotionItem.Quantity;
                                totalPrice = totalPrice + promotionAppliedItemsCount * promotionItem.Quantity *
                                    _pPromotionCartItems.First(pX => pX.Item.ItemId == promotionItem.ItemId)
                                        .Item.Price - promotionAppliedItemsCount * promotion.Discount;

                                var promotionAppliedQuantity = promotionAppliedItemsCount * promotionItem.Quantity;
                                promotionCartItem.AvailableQuantity =
                                    promotionCartItem.AvailableQuantity - promotionAppliedQuantity;
                                promotionCartItem.PromotionAppliedQuantity =
                                    promotionCartItem.PromotionAppliedQuantity + promotionAppliedQuantity;
                            }
                        }
                    }
                    else
                    {
                        var numberOfSetPromotionCanApply = 0;
                        bool breakLoop = false;
                        while (true)
                        {
                            foreach (var combination in promotion.CombinationList)
                            {
                                var item = _pPromotionCartItems.First(pX => pX.Item.ItemId.Equals(combination.ItemId));
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
                            foreach (var combination in promotion.CombinationList)
                            {
                                var item = _pPromotionCartItems.First(pX => pX.Item.ItemId.Equals(combination.ItemId));
                                item.AvailableQuantity =
                                    item.AvailableQuantity - combination.Quantity * numberOfSetPromotionCanApply;

                                item.PromotionAppliedQuantity =
                                    item.PromotionAppliedQuantity + combination.Quantity * numberOfSetPromotionCanApply;
                                totalPrice = totalPrice +
                                             (combination.Quantity * numberOfSetPromotionCanApply) * item.Item.Price;
                            }

                            totalPrice = totalPrice - numberOfSetPromotionCanApply * promotion.Discount;
                        }
                    }
                }

            foreach (var item in _pPromotionCartItems)
            {
                totalPrice = totalPrice + item.AvailableQuantity * item.Item.Price;
            }

            return totalPrice;
        }
    }
}
