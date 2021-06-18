using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionManagement.Shared.Model
{
    public class Promotions
    {
        public Promotions(HashSet<ItemWithQantity> pCombinationList, double pDiscount)
        {
            CombinationList = pCombinationList;
            Discount = pDiscount;
        }
        public HashSet<ItemWithQantity> CombinationList { get; }
        public double Discount { get; }
    }
}
