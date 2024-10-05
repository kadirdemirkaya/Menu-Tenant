namespace Shared.Domain.Models.User
{
    public class UserLoginModel
    {
        public string Email { get; set; }
        public string? CompanyName { get; set; }
        public Token Token { get; set; }

        public UserLoginModel(string email, string? companyName, Token token)
        {
            Email = email;
            CompanyName = companyName;
            Token = token;
        }
    }
}
