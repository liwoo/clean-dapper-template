using System;

using Xunit;

using Domain.Exceptions;

namespace UnitTests
{
    public class RetryableExceptionTests
    {
        [Fact]
        public void TestRetryableExceptionInit1()
        {
            // Act
            var ex = new RetryableException();

            // Assert
            Assert.Equal(typeof(RetryableException), ex.GetType());
        }

        [Fact]
        public void TestRetryableExceptionInit2()
        {
            // Arrange
            var exception = new Exception("exception thrown");

            // Act
            var ex = new RetryableException(exception);

            // Assert
            Assert.Equal(typeof(RetryableException), ex.GetType());
            Assert.Equal(exception.Message, ex.Message);
        }

        [Fact]
        public void TestRetryableExceptionInit3()
        {
            // Arrange
            var message = "exception thrown";

            // Act
            var ex = new RetryableException(message);

            // Assert
            Assert.Equal(typeof(RetryableException), ex.GetType());
            Assert.Equal(message, ex.Message);
        }

        [Fact]
        public void TestRetryableExceptionInit4()
        {
            // Arrange
            var message = "exception thrown";
            var exception = new Exception("inner exception");

            // Act
            var ex = new RetryableException(message, exception);

            // Assert
            Assert.Equal(typeof(RetryableException), ex.GetType());
            Assert.Equal(message, ex.Message);
            Assert.Equal(exception.Message, ex.InnerException.Message);
        }
    }
}