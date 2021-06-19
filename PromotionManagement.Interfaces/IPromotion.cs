using System.Collections.Generic;
using PromotionManagement.Shared.Model;

namespace PromotionManagement.Interfaces
{
    public interface IPromotion
    {
        double ApplyDiscount(Promotions pPromotion, List<PromotionCartItem> pItems);
    }
}
