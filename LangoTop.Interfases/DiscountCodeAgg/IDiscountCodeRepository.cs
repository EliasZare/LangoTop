using System.Collections.Generic;
using _0_Framework.Domain;
using LangoTop.Application.Contract.DiscountCode;
using LangoTop.Domain.DiscountCodeAgg;

namespace LangoTop.Interfaces.DiscountCodeAgg
{
    public interface IDiscountCodeRepository : IRepository<long, DiscountCode>
    {
        EditDiscountCode GetDetails(long id);
        List<DiscountCodeViewModel> Search(DiscountCodeSearchModel searchModel);
    }
}