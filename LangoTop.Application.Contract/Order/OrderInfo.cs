using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;

namespace LangoTop.Application.Contract.Order
{
    public class OrderInfo
    {
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Address { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Mobile { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Postalcode { get; set; }
    }
}
