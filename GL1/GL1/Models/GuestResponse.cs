using System.ComponentModel.DataAnnotations;

namespace GL1.Models
{
    public class GuestResponse
    {
        [Required(ErrorMessage ="Пожалуйста введите свое имя")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Пожалуйста введите свой адрес")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter а valid email address")] 
        public string Email { get; set; }

        [Required(ErrorMessage = "Пожалуйста укажите свой телефон")]
        public string Phone { get; set; }
        public bool? WillAttend { get; set; }
    }
}
