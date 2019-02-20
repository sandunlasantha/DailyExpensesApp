using System;
using System.Runtime.Serialization;

namespace DailyExpensesApp.ViewModels
{
    [Serializable]
    internal class PasswordNotMatchException : Exception
    {
        public PasswordNotMatchException()
        {
        }

        public PasswordNotMatchException(string message) : base(message)
        {
        }

        public PasswordNotMatchException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PasswordNotMatchException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}