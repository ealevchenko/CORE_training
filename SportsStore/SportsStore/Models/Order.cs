using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static SportsStore.Models.Cart;

namespace SportsStore.Models
{
    public class Order
    {
        [BindNever]
        public int OrderID { get; set;}

        [BindNever]
        public ICollection<CartLine> Lines { get; set; }

        [Required(ErrorMessage = "Please enter а name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter the first address line")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string LineЗ { get; set; }
        [Required(ErrorMessage = "Please enter а city name")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please enter а state name")]
        public string State { get; set; }
        public string Zip { get; set; }
        [Required(ErrorMessage = "Please enter а country name")]
        public string Country { get; set; }
        public bool GiftWrap { get; set; }
    }
}
