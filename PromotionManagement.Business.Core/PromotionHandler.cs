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
            IPromotion promotionService;
            if (_pLoadAvailablePromotions != null)
                foreach (var promotion in _pLoadAvailablePromotions?.Invoke())
                {
                    if (promotion.CombinationList.Count == 1)
                    {
                        promotionService = new SinglePromotion();
                    }
                    else
                    {
                        promotionService = new CombinedPromotion();
                      
                    }
                    totalPrice = totalPrice + promotionService.ApplyDiscount(promotion, _pPromotionCartItems);
                }

            foreach (var item in _pPromotionCartItems)
            {
                totalPrice = totalPrice + item.AvailableQuantity * item.Item.Price;
            }

            return totalPrice;
        }
    }
}
