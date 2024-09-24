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

                ProcessProperties(parameter);
            }

            base.OnActionExecuting(context);
        }

        private void ProcessProperties(object parameter)
        {
            if (parameter == null) return;

            var properties = parameter.GetType().GetProperties();

            foreach (var property in properties)
            {
                if (property.IsDefined(typeof(HashPasswordAttribute), false))
                {
                    var passwordValue = property.GetValue(parameter) as string;
                    if (passwordValue != null)
                    {
                        var hashedPassword = HashPassword(passwordValue); // hash
                        property.SetValue(parameter, hashedPassword);
                    }
                }

                else if (property.PropertyType.IsClass && property.PropertyType != typeof(string)) // is it class  ? 
                {
                    var nestedProperty = property.GetValue(parameter);
                    if (nestedProperty != null)
                    {
                        ProcessProperties(nestedProperty); // for nested class recursive (models)
                    }
                }
            }
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
