using System;

namespace ProfileService.Domain.Exceptions.Base
{
    public abstract class BaseException : Exception
    {
        protected BaseException(string message) : base(message)
        {
        }
    }
}