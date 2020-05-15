using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using UniLink.Dependencies.Data.VO;

namespace UniLink.API.Filters
{
	public class ErrorResponseFilter : IExceptionFilter
	{
		public void OnException(ExceptionContext context) =>
			context.Result = new ObjectResult(ErrorResponseVO.From(context.Exception)) { StatusCode = 500 };
	}
}