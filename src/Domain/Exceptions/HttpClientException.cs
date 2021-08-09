using System;
using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    [Serializable]
    public class HttpClientException : ApplicationException
    {
        public HttpClientException()
            : base()
        {
        }

        public HttpClientException(Exception exception)
            : base(exception.Message, exception)
        {
        }

        public HttpClientException(string message)
            : base(message)
        {
        }

        public HttpClientException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected HttpClientException(
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