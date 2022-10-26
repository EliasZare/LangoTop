using System.Collections.Generic;
using LangoTop.Application.Contract.Order;

namespace _01_Query.Contracts
{
    public interface ICartCalculateService
    {
        Cart ComputeCart(List<CartItem> cartItems, string discountCode);
    }
}
