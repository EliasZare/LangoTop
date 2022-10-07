using System.Collections.Generic;
using _0_Framework.Domain;
using LangoTop.Application.Contract.CustomerDiscount;
using LangoTop.Domain.CustomerDiscountAgg;

namespace LangoTop.Interfaces.CustomerDiscountAgg
{
    public interface ICustomerDiscountRepository : IRepository<long, CustomerDiscount>
    {
        EditCustomerDiscount GetDetails(long id);
        List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel);
    }
}