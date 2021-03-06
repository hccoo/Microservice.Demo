﻿using System;

namespace Microservice.Demo.Infrastructure.Transactions
{
    public class EnableTransactionAttribute : Attribute
    {
    }

    public class CanNotGetAttributeException : Exception
    {
        public int ErrorCode { get; set; }

        public CanNotGetAttributeException(int errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
