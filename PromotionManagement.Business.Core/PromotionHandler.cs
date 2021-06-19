using System;
using System.Collections.Generic;
using System.Linq;
using ProjectManagement.Business.Implementations;
using PromotionManagement.Interfaces;
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
                    IPromotion promotionService;
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

            totalPrice = totalPrice + CalculateTotalForAvailableQuantities(_pPromotionCartItems);

            return totalPrice;
        }

        private double CalculateTotalForAvailableQuantities(List<PromotionCartItem> pItems)
        {
            double total = 0;
            foreach (var item in _pPromotionCartItems)
            {
                total = total + item.AvailableQuantity * item.Item.Price;
            }

            return total;
        }
        
    }
}
