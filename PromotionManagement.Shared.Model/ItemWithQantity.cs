using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionManagement.Shared.Model
{
    public class ItemWithQantity
    {
        public ItemWithQantity(Guid pItemId, int pQuantity)
        {
            ItemId = pItemId;
            Quantity = pQuantity;
        }
        public Guid ItemId { get;  }
        public int Quantity { get; set; }

        public override bool Equals(object obj)
        {
            var item = obj as ItemWithQantity;

            if (item == null)
            {
                return false;
            }

            return this.ItemId.Equals(item.ItemId);
        }

        public override int GetHashCode()
        {
            return this.ItemId.GetHashCode();
        }
    }
}
