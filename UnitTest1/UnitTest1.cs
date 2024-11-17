using Xunit;
using MySite.Controllers;
using MySite.Models;
using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;
using MySite.Data;

namespace UnitTest1
{

public class ProductTests
    {
        [Fact]
        public void DiscountCalc()
        {
            var random = new Random();
            var price = random.Next(1, 1000);
            var discount = random.Next(1, 100); 

            var product = new Product
            {
                Price = price,
                DiscountPrecent = discount
            };


            var discountPrice = product.DiscountPrice;

            Assert.NotNull(discountPrice);
            Assert.Equal(price - (price * discount / 100m), discountPrice);
        }

        [Fact]
        public void NoDiscountCalc()
        {
            var random = new Random();
            var price = random.Next(1, 1000);

            var product = new Product
            {
                Price = price,
                DiscountPrecent = 0 
            };
            var discountPrice = product.DiscountPrice;
            Assert.Null(discountPrice);
        }

        [Fact]
        public void AddToCartTest()
        {
            // Arrange
            var productInStock = new Product
            {
                Id = 1,
                Name = "Товар у наявності",
                Stock = 10,
                Price = 100
            };

            var productOutOfStock = new Product
            {
                Id = 2,
                Name = "Товар відсутній",
                Stock = 0,
                Price = 200
            };

            var cart = new Cart
            {
                CartId = 1,
                UserId = "testUser",
                CartItems = new List<CartItem>()
            };

            // Act
            if (productInStock.Stock > 0)
            {
                cart.CartItems.Add(new CartItem
                {
                    ProductId = productInStock.Id,
                    CartId = cart.CartId,
                    Quantity = 3,
                    UnitPrice = productInStock.Price
                });
            }

            if (productOutOfStock.Stock > 0)
            {
                cart.CartItems.Add(new CartItem
                {
                    ProductId = productOutOfStock.Id,
                    CartId = cart.CartId,
                    Quantity = 1,
                    UnitPrice = productOutOfStock.Price
                });
            }

            // Assert
            Assert.Single(cart.CartItems); 
            Assert.Equal(productInStock.Id, cart.CartItems.First().ProductId); 
        }
    }

}

