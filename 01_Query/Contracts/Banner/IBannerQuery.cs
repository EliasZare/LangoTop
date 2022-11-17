namespace _01_Query.Contracts.Banner
{
    public interface IBannerQuery
    {
        BannerQueryModel GetBannerBy(long type);
    }
}
