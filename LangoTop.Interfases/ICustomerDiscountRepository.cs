using System.Collections.Generic;
using _0_Framework.Domain;
using LangoTop.Application.Contract.CustomerDiscount;
using LangoTop.Domain;

namespace LangoTop.Interfaces
{
    public interface ICustomerDiscountRepository : IRepository<long, CustomerDiscount>
    {
        EditCustomerDiscount GetDetails(long id);
        List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel);
    }
}