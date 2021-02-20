using System.ComponentModel.DataAnnotations;

namespace Clocks.Shared.DtoModels.Account
{
    public class SignUpRequest
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [Compare(nameof(Password))]
        public string RepeatPassword { get; set; }
    }
}
