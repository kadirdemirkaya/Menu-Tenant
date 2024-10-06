using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace Shared.Infrastructure.Filters
{
    public class PaginationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var pageValue = context.HttpContext.Request.Query["page"].ToString();
            var sizeValue = context.HttpContext.Request.Query["size"].ToString();

            if (!string.IsNullOrEmpty(pageValue))
                context.HttpContext.Items["page"] = pageValue;

            if (!string.IsNullOrEmpty(sizeValue))
                context.HttpContext.Items["size"] = sizeValue;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }
}
