using System;

namespace Pizza_Bot.Exceptions
{
    public class EmailException : Exception
    {
        public EmailException(string message) : base(message)
        {

        }
    }
}
