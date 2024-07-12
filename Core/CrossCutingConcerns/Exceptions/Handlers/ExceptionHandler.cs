using Core.CrossCutingConcerns.Exceptions.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCutingConcerns.Exceptions.Handlers
{
    public abstract class ExceptionHandler
    {
        public Task HandleExceptionAsync(Exception exception) =>
            exception switch
            {
                BusinessException businessException => HandleException(businessException),
                ValidationCustomException validationException => HandleException(validationException),
                //_ => HandleException(exception)
            };

        protected abstract Task HandleException(BusinessException businessException);
        protected abstract Task HandleException(ValidationCustomException validationException);
        //protected abstract Task HandleException(Exception exception);

    }
}
