namespace LangoTop.Application.Contract.Order
{
    public interface ICartService
    {
        void Set(Cart cart);
        Cart Get();
    }
}
