using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace DevFreela.API.Filter
{
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var messages = context.ModelState
                    .SelectMany(ms => ms.Value.Errors)
                    .Select(er => er.ErrorMessage)
                    .ToList();

                context.Result = new BadRequestObjectResult(messages);
            }
        }
    }
}
