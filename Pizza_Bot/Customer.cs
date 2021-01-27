using System;
using System.Collections.Generic;
using System.Net.Mail;
using Pizza_Bot.Attributes;
using Pizza_Bot.Exceptions;

namespace Pizza_Bot
{
    public class Customer
    {
        public string Name { get; private set; }

        public string Email { get; private set; }

        [MinLenght(4)]
        public string Address { get; private set; }

        public void SetEmail()
        {
            Console.WriteLine("Please enter your email address to contact:");
            Email = Console.ReadLine();

            try
            {
                ValidateEmail();
            }
            catch (EmailException ex)
            {
                Console.WriteLine(ex.Message);
                SetEmail();
            }            
        }

        public void SetName()
        {
            Console.WriteLine("Please introduce yourself:");
            Name = Console.ReadLine();
        }

        public void SetAddress()
        {
            Console.WriteLine("Please enter your shipping address:");
            Address = Console.ReadLine();
        }

        private void ValidateEmail()
        {
            if (!IsValidEmail(Email))
            {
                throw new EmailException("Email is not valid.");
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                MailAddress mail = new MailAddress(email);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }        
    }
}
