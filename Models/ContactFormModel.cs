using System.ComponentModel.DataAnnotations;

namespace QRScanner.Models
{
    public class ContactFormModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Contact Number is required")]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Enter a valid 10-digit contact number")]
        public string ContactNumber { get; set; }

        public string Message { get; set; }
    }
}
