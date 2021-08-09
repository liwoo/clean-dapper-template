using System;

using Xunit;

using Domain.Exceptions;

namespace UnitTests
{
    public class HttpClientExceptionTests
    {
        [Fact]
        public void TestHttpClientExceptionInit1()
        {
            // Act
            var ex = new HttpClientException();

            // Assert
            Assert.Equal(typeof(HttpClientException), ex.GetType());
        }

        [Fact]
        public void TestHttpClientExceptionInit2()
        {
            // Arrange
            var exception = new Exception("exception thrown");

            // Act
            var ex = new HttpClientException(exception);

            // Assert
            Assert.Equal(typeof(HttpClientException), ex.GetType());
            Assert.Equal(exception.Message, ex.Message);
        }

        [Fact]
        public void TestHttpClientExceptionInit3()
        {
            // Arrange
            var message = "exception thrown";

            // Act
            var ex = new HttpClientException(message);

            // Assert
            Assert.Equal(typeof(HttpClientException), ex.GetType());
            Assert.Equal(message, ex.Message);
        }

        [Fact]
        public void TestHttpClientExceptionInit4()
        {
            // Arrange
            var message = "exception thrown";
            var exception = new Exception("inner exception");

            // Act
            var ex = new HttpClientException(message, exception);

            // Assert
            Assert.Equal(typeof(HttpClientException), ex.GetType());
            Assert.Equal(message, ex.Message);
            Assert.Equal(exception.Message, ex.InnerException.Message);
        }
    }
}