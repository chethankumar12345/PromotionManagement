using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionManagement.Shared.Model
{
    public class Item
    {
        public Item(string pName, double pPrice)
        {
            ItemId = Guid.NewGuid();
            Name = pName;
            Price = pPrice;
        }

        public Guid ItemId { get;  }
        public string Name { get;  }
        public double Price { get;  }
    }
}
