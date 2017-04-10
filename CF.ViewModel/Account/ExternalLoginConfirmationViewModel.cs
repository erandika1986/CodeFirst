using System.ComponentModel.DataAnnotations;

namespace CF.ViewModel.Account
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }
}
