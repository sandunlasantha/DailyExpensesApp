using System;
using System.Runtime.Serialization;

namespace DailyExpensesApp.Models
{
    [Serializable]
    internal class EmailTruePasswordFalseException : Exception
    {
        public EmailTruePasswordFalseException()
        {
        }

        public EmailTruePasswordFalseException(string message) : base(message)
        {
        }

        public EmailTruePasswordFalseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EmailTruePasswordFalseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}