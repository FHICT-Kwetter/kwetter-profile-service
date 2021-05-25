// <copyright file="BaseException.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace ProfileService.Domain.Exceptions.Base
{
    using System;

    /// <summary>
    /// Defines the base exception for all custom application exceptions to extend from.
    /// </summary>
    public abstract class BaseException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        protected BaseException(string message) : base(message)
        {
        }
    }
}