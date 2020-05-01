using System;
using System.Collections.Generic;
using System.Text;

namespace UniLink.Dependencies.Data.VO
{
    public class ErrorResponseVO
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public ErrorResponseVO InnerError { get; set; }
        public List<string> Details { get; set; }

        public static ErrorResponseVO From(Exception e)
        {
            if (e == null)
                return null;

            return new ErrorResponseVO
            {
                Code = e.HResult,
                Message = e.Message,
                InnerError = From(e.InnerException)
            };
        }
    }
}
