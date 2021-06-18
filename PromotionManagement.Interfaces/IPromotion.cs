using System;
using System.Collections.Generic;
using System.Text;
using PromotionManagement.Shared.Model;

namespace PromotionManagement.Business.Core
{
    public interface IPromotion
    {
        double ApplyDiscount(Promotions pPromotion, List<PromotionCartItem> pItems);
    }
}
