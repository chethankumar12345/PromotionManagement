using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using PromotionManagement.Business.Core;
using PromotionManagement.Shared.Model;
using Moq;
using PromotionManagement.Interfaces;

namespace PromotionMangement.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void FirstSenario()
        {

            var mockRepo = new Mock<ICartContainer>();
            var promotions = new List<Promotions>();

            var allItems = GetAvailableItems();
            var availablePromotions = GeneratePromotions(allItems);

            var itemWithQuantities = new HashSet<ItemWithQantity>();
            var firstItemId = allItems.First(pX => pX.Name.ToLower() == SammpleItems.A.ToLower()).ItemId;
            itemWithQuantities.Add(new ItemWithQantity(firstItemId, 2));
            promotions.Add(new Promotions(itemWithQuantities, 20));

            mockRepo.Setup(x => x.GetCartItems(allItems.ToList(), IsItemAddToCart, GetItemQuatityForCart)).Returns(GetFirstSenarioItems(allItems));
            var cart = mockRepo.Object.GetCartItems(allItems.ToList(), IsItemAddToCart, GetItemQuatityForCart);
            cart.Items = GetSumOfItemsWithQuantity(cart.Items);

            var promotionHandler = new PromotionHandler(cart, () => availablePromotions.ToList());
            var total = promotionHandler.ApplyPromotionGetTotal();
            Assert.AreEqual(total, 320);


        }

        [TestMethod]
        public void SecondSenario()
        {

            var mockRepo = new Mock<ICartContainer>();
            var promotions = new List<Promotions>();

            var allItems = GetAvailableItems();
            var availablePromotions = GeneratePromotions(allItems);

            var itemWithQuantities = new HashSet<ItemWithQantity>();
            var firstItemId = allItems.First(pX => pX.Name.ToLower() == SammpleItems.A.ToLower()).ItemId;
            itemWithQuantities.Add(new ItemWithQantity(firstItemId, 2));
            promotions.Add(new Promotions(itemWithQuantities, 20));

            mockRepo.Setup(x => x.GetCartItems(allItems.ToList(), IsItemAddToCart, GetItemQuatityForCart)).Returns(GetSecondSenarioItems(allItems));
            var cart = mockRepo.Object.GetCartItems(allItems.ToList(), IsItemAddToCart, GetItemQuatityForCart);
            cart.Items = GetSumOfItemsWithQuantity(cart.Items);

            var promotionHandler = new PromotionHandler(cart, () => availablePromotions.ToList());
            var total = promotionHandler.ApplyPromotionGetTotal();
            Assert.AreEqual(total, 320);


        }

        private Cart GetFirstSenarioItems(List<Item> pItems)
        {
            var cartItems = new List<CartItem>();
            cartItems.Add(new CartItem(pItems.First(pX => pX.Name.Equals(SammpleItems.A)), 1));
            cartItems.Add(new CartItem(pItems.First(pX => pX.Name.Equals(SammpleItems.B)), 1));
            cartItems.Add(new CartItem(pItems.First(pX => pX.Name.Equals(SammpleItems.C)), 1));

            return new Cart(cartItems);
        }

        private Cart GetSecondSenarioItems(List<Item> pItems)
        {
            var cartItems = new List<CartItem>();
            cartItems.Add(new CartItem(pItems.First(pX => pX.Name.Equals(SammpleItems.A)), 5));
            cartItems.Add(new CartItem(pItems.First(pX => pX.Name.Equals(SammpleItems.B)), 5));
            cartItems.Add(new CartItem(pItems.First(pX => pX.Name.Equals(SammpleItems.C)), 1));

            return new Cart(cartItems);
        }
        private List<Item> GetAvailableItems()
        {
            var allItems = new List<Item>();
            allItems.Add(new Item(SammpleItems.A, 50));
            allItems.Add(new Item(SammpleItems.B, 30));
            allItems.Add(new Item(SammpleItems.C, 20));
            allItems.Add(new Item(SammpleItems.D, 15));
            return allItems;
        }
     
        private static bool IsItemAddToCart(Item pItem)
        {
            return pItem.Name.ToLower().Equals(SammpleItems.A.ToLower()) ||
                   pItem.Name.ToLower().Equals(SammpleItems.B.ToLower());
        }

        private static int GetItemQuatityForCart(Item pItem)
        {
            if (pItem.Name.ToLower() == SammpleItems.A.ToLower())
            {
                return 2;
            }

            return 1;
        }

        private static List<CartItem> GetSumOfItemsWithQuantity(List<CartItem> pItems)
        {
            return pItems.GroupBy(pX => pX.Item)
                .Select(pX => new CartItem(pX.First().Item, pX.Sum(pY => pY.Quantity)))
                .ToList();
        }

        private static IEnumerable<Promotions> GeneratePromotions(List<Item> pItems)
        {
            var promotions = new List<Promotions>();
            var allItems = pItems.ToList();

            //Single Promotion Items

            //First Promotion Item - A* 2 , 20 discount
            var itemWithQuantities = new HashSet<ItemWithQantity>();
            var firstItemId = allItems.First(pX => pX.Name.ToLower() == SammpleItems.A.ToLower()).ItemId;
            itemWithQuantities.Add(new ItemWithQantity(firstItemId, 2));
            promotions.Add(new Promotions(itemWithQuantities, 20));


            //Second Promotion Item- B* 2 , 10 discount
            itemWithQuantities = new HashSet<ItemWithQantity>();
            var secondItemId = allItems.First(pX => pX.Name.ToLower() == SammpleItems.B.ToLower()).ItemId;
            itemWithQuantities.Add(new ItemWithQantity(secondItemId, 2));
            promotions.Add(new Promotions(itemWithQuantities, 15));


            //Combined Promotion Item A* 2 + B * 2, 30 discount
            itemWithQuantities = new HashSet<ItemWithQantity>();
            var firstCombineItemId = allItems.First(pX => pX.Name.ToLower() == SammpleItems.C.ToLower()).ItemId;
            var secondCombineItemId = allItems.First(pX => pX.Name.ToLower() == SammpleItems.D.ToLower()).ItemId;
            itemWithQuantities.Add(new ItemWithQantity(firstCombineItemId, 1));
            itemWithQuantities.Add(new ItemWithQantity(secondCombineItemId, 1));
            promotions.Add(new Promotions(itemWithQuantities, 5));

            return promotions;
        }
    }

    public static class SammpleItems
    {
        internal const string A = "A";
        internal const string B = "B";
        internal const string C = "C";
        internal const string D = "D";
    }
}

