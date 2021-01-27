using System;

namespace Pizza_Bot.Exceptions
{
    public class AddressException : Exception
    {
        public AddressException(string message) : base(message)
        {

        }
    }
}
