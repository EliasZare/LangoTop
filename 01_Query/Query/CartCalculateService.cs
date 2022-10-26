using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_Query.Contracts;
using LangoTop.Application.Contract.Order;
using LangoTop.Infrastructure;

namespace _01_Query.Query
{
    public class CartCalculateService : ICartCalculateService
    {
        private readonly LangoTopContext _context;
        private readonly IAuthHelper _authHelper;

        public CartCalculateService(IAuthHelper authHelper, LangoTopContext context)
        {
            _authHelper = authHelper;
            _context = context;
        }


        public Cart ComputeCart(List<CartItem> cartItems, string code)
        {
            var cart = new Cart();

            var discountCode = _context.DiscountCodes
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new {x.DiscountRate, x.CourseId, x.Code}).FirstOrDefault(x => x.Code == code);

            var customerDiscounts = _context.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new {x.DiscountRate, x.CourseId}).ToList();


            foreach (var cartItem in cartItems)
            {
                if (discountCode != null)
                {
                    cartItem.DiscountRate = discountCode.DiscountRate;
                }
                else
                {
                    var customerDiscount = customerDiscounts.FirstOrDefault(x => x.CourseId == cartItem.Id);
                    if (customerDiscount != null)
                        cartItem.DiscountRate = customerDiscount.DiscountRate;
                }

                cartItem.DiscountAmount = cartItem.TotalItemPrice * cartItem.DiscountRate / 100;
                cartItem.ItemPayAmount = cartItem.TotalItemPrice - cartItem.DiscountAmount;
                cart.Add(cartItem);
            }

            return cart;
        }
    }
}