using System;
using System.Runtime.Serialization;

namespace DailyExpensesApp.Models
{
    [Serializable]
    internal class EmailNullPasswordNullException : Exception
    {
        public EmailNullPasswordNullException()
        {
        }

        public EmailNullPasswordNullException(string message) : base(message)
        {
        }

        public EmailNullPasswordNullException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EmailNullPasswordNullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}