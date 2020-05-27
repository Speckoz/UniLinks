using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using UniLinks.Dependencies.Data.VO;

namespace UniLinks.API.Filters
{
	public class ErrorResponseFilter : IExceptionFilter
	{
		public void OnException(ExceptionContext context) =>
			context.Result = new ObjectResult(ErrorResponseVO.From(context.Exception)) { StatusCode = 500 };
	}
}