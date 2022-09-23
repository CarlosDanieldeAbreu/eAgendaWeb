using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace eAgenda.Webpi.Filters
{
    public class ValidarViewModelActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

            if (context.ModelState.IsValid == false)
            {
                var listaErros = context.ModelState.Values
                     .SelectMany(x => x.Errors)
                     .Select(x => x.ErrorMessage);

                context.Result = new BadRequestObjectResult(new
                {
                    sucesso = false,
                    erros = listaErros.ToList()
                });

                return;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
           
        }
    }
}
