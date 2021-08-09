using System;
using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    [Serializable]
    public class RetryableException : ApplicationException
    {
        public RetryableException()
            : base()
        {
        }

        public RetryableException(Exception exception)
            : base(exception.Message, exception)
        {
        }

        public RetryableException(string message)
            : base(message)
        {
        }

        public RetryableException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected RetryableException(
            SerializationInfo info,
            StreamingContext context
        ) : base(info, context)
        { }

        public override void GetObjectData(
            SerializationInfo info,
            StreamingContext context
        )
        {
            base.GetObjectData(info, context);
        }
    }
}
