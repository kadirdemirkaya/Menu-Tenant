using Microsoft.AspNetCore.Mvc.Filters;
using Shared.Application.Filters.Attributes;
using System.Security.Cryptography;
using System.Text;

namespace Shared.Application.Filters
{
    public class HashPasswordActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var parameters = context.ActionArguments;

            foreach (var parameter in parameters.Values)
            {
                if (parameter == null) continue;

                var properties = parameter.GetType().GetProperties()
                    .Where(prop => prop.IsDefined(typeof(HashPasswordAttribute), false));

                foreach (var property in properties)
                {
                    var passwordValue = property.GetValue(parameter) as string;
                    if (passwordValue != null)
                    {
                        // Hash password
                        var hashedPassword = HashPassword(passwordValue);
                        property.SetValue(parameter, hashedPassword);
                    }
                }
            }
            base.OnActionExecuting(context);
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
