﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Application.ZarinPal;
using _01_Query.Contracts;
using _01_Query.Contracts.Course;
using LangoTop.Application.Contract.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;

namespace ServiceHost.Pages
{
    [Authorize]
    public class CheckOutModel : PageModel
    {
        public const string CookieName = "cart-items";
        private readonly IAuthHelper _authHelper;
        private readonly ICartCalculateService _cartCalculateService;
        private readonly ICartService _cartService;
        private readonly IOrderApplication _orderApplication;
        private readonly ICourseQuery _courseQuery;
        private readonly IZarinPalFactory _zarinPalFactory;
        public OrderInfo OrderInfo;
        public Cart Cart;

        public CheckOutModel(ICartCalculateService cartCalculateService, ICartService cartService
            , IOrderApplication orderApplication, IZarinPalFactory zarinPalFactory,
            IAuthHelper authHelper, ICourseQuery courseQuery)
        {
            _cartCalculateService = cartCalculateService;
            _cartService = cartService;
            _orderApplication = orderApplication;
            _zarinPalFactory = zarinPalFactory;
            _authHelper = authHelper;
            _courseQuery = courseQuery;
            Cart = new Cart();
        }

        public void OnGet()
        {
            if (!string.IsNullOrWhiteSpace(Request.Cookies["cart-items"]))
            {
                var serializer = new JavaScriptSerializer();
                var value = Request.Cookies[CookieName];
                var cartItems = serializer.Deserialize<List<CartItem>>(value);
                if (cartItems != null)
                    foreach (var item in cartItems)
                        item.CalculateTotalItemPrice();

                Cart = _cartCalculateService.ComputeCart(cartItems, "");
                _cartService.Set(Cart);
            }
        }

        public IActionResult OnGetRemoveFromCartItem(long id)
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            Response.Cookies.Delete(CookieName);
            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            var itemToRemove = cartItems.FirstOrDefault(x => x.Id == id);
            cartItems.Remove(itemToRemove);
            var options = new CookieOptions {Expires = DateTime.Now.AddDays(1)};
            var serializeResult = serializer.Serialize(cartItems);
            Response.Cookies.Append("cart-items", serializeResult);
            return RedirectToPage("/Index");
        }

        public IActionResult OnPostPay()
        {
            var cart = _cartService.Get();
            var accountEmail = _authHelper.CurrentAccountInfo().Email;
            var accountMobile = _authHelper.CurrentAccountInfo().Mobile;
            var orderId = _orderApplication.PlaceOrder(cart);

            var paymentResponse = _zarinPalFactory.CreatePaymentRequest(cart.PayAmount.ToString(), accountMobile,
                accountEmail, "خرید دوره آموزش زبان انگلیسی از وبسایت لنگوتاپ", orderId);

            return Redirect($"https://{_zarinPalFactory.Prefix}.zarinpal.com/pg/StartPay/{paymentResponse.Authority}");
        }

        public IActionResult OnGetCallBack([FromQuery] long old, [FromQuery] string authority,
            [FromQuery] string status)
        {
            var orderAmount = _orderApplication.GetAmountBy(old);
            var verificationResponse =
                _zarinPalFactory.CreateVerificationRequest(authority,
                    orderAmount.ToString(CultureInfo.InvariantCulture));

            var result = new PaymentResult();
            if (status == "OK" && verificationResponse.Status == 100)
            {
                var issueTrackingNumber = _orderApplication.PaymentSucceeded(old, verificationResponse.RefID);
                Response.Cookies.Delete("cart-items");
                result = result.Succeeded("پرداخت با موفقیت انجام شد", issueTrackingNumber);
                return RedirectToPage("/PaymentResult", result);
            }

            result = result.Failed(
                "عملیات پرداخت با شکست مواجه شد، در صورت کسر از حساب شما مبلغ تا 48 ساعت آینده به حساب شما واریز خواهد شد.");
            return RedirectToPage("/PaymentResult", result);
        }
    }
}