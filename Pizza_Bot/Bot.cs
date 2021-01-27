using System;
using System.Collections.Generic;
using Pizza_Bot.Enums;
using Pizza_Bot.Repositories;
using Pizza_Bot.Extansions;
using Pizza_Bot.Drawing;
using Pizza_Bot.Attributes;
using System.Reflection;
using Pizza_Bot.Exceptions;

namespace Pizza_Bot
{
    public class Bot
    {
        private readonly Customer _customer = new Customer();
        private readonly Order _order = new Order();
        private readonly FilePizzaRepository _filePizza = new FilePizzaRepository();     

        public void Start()
        {            
            AsciiArt.Logo();

            Greeting();
            _customer.SetName();
            _customer.SetEmail();
        }

        public void OrderPizza()
        {
            Console.Clear();

            PrintMenu();
            ChoicePizza();
            PrintOrder();
            MorePizza(); 
        }

        public void Delivery()
        {
            SetShippingAddress();
            OrderComplete();
            OrderDeliver();
            OrderPaid();
        }

        #region MethodStart
        private void Greeting()
        {
            TimeSpan now = DateTime.Now.TimeOfDay;

            if (now.Hours >= 9 && now.Hours < 12)
            {
                Console.Write("Good morning! ");
            }
            else if (now.Hours >= 12 && now.Hours < 18)
            {
                Console.Write("Good day! ");
            }
            else if (now.Hours >= 18 && now.Hours < 23)
            {
                Console.Write("Good evening! ");
            }
            else
                Console.Write("Good night! ");
            Console.WriteLine("Welcome to PIZZA BOT.");
        }
        #endregion

        #region MethodOrderPizza
        private void PrintMenu()
        {
            List<Pizza> _pizzasList = _filePizza.GetPizzas();

            Console.WriteLine("What kind of pizza do you want?\n");
            foreach (var piza in _pizzasList)
            {
                Console.WriteLine($"{piza.Id}. PIZZA \"{piza.Title}\" Ingredients: {piza.Body}");
                Console.WriteLine($"Small - {piza.BasePrice.TotalPrice(PizzaSize.Small)} BYN, Medium - {piza.BasePrice.TotalPrice(PizzaSize.Medium)} BYN, Large - {piza.BasePrice.TotalPrice(PizzaSize.Large)} BYN\n");
            }
        }

        private void ChoicePizza()
        {
            Console.WriteLine("Please, enter the number of the pizza:");

            int userChoice;
            if (int.TryParse(Console.ReadLine(), out userChoice))
            {
                Pizza pizza = _filePizza.GetPizzaByID(userChoice);
                pizza.Size = ChoicePizzaSize();
                _order.OrderPrice += pizza.BasePrice.TotalPrice(pizza.Size);
                _order._pizzas.Add(pizza);
            }
            else
            {
                Console.WriteLine("Something wrong!");                
            }            
        }

        private PizzaSize ChoicePizzaSize()
        {
            Console.WriteLine("Please select a pizza size:");
            int counter = 1;
            foreach (var item in Enum.GetNames(typeof(PizzaSize)))
            {
                Console.WriteLine($"{counter}. {item}");
                counter += 1;
            }

            int userChoice;
            if (int.TryParse(Console.ReadLine(), out userChoice))
            {
                if (Enum.IsDefined(typeof(PizzaSize), userChoice))
                    return (PizzaSize)userChoice;
                else
                {
                    Console.WriteLine("Something wrong!");
                    return ChoicePizzaSize();
                }
            }
            else
            {
                Console.WriteLine("Something wrong!");
                return ChoicePizzaSize();
            }
        }

        private void PrintOrder()
        {
            Console.Clear();
            Console.WriteLine("Your order:");
            int counter = 1;
            foreach (var item in _order._pizzas)
            {
                Console.WriteLine($"{counter}. \"{item.Title}\" : {item.BasePrice.TotalPrice(item.Size)} BYN - {item.Size} Size");
                counter++;
            }
            Console.WriteLine($"Total price = {_order.OrderPrice} BYN\n");
        }

        private void MorePizza()
        {
            Console.WriteLine("Do you want to order more pizza?");
            if (YesOrNo())
                OrderPizza();
        }

        private bool YesOrNo()
        {
            Console.WriteLine("Enter <Y> or <N>:");
            string userChoice = Console.ReadLine();

            if (userChoice.Equals("Y", StringComparison.OrdinalIgnoreCase))
                return true;
            else if (userChoice.Equals("N", StringComparison.OrdinalIgnoreCase))
                return false;
            return YesOrNo();
        }
        #endregion

        #region MethodDelivery
        private void SetShippingAddress()
        {
            _customer.SetAddress();

            try
            {
                ValidateAddress();
            }
            catch (AddressException ex)
            {
                Console.WriteLine(ex.Message);
                SetShippingAddress();
            }
        }

        private void ValidateAddress()
        {
            Type type = typeof(Customer);
            foreach (PropertyInfo property in type.GetProperties())
            {
                foreach (Attribute attribute in property.GetCustomAttributes())
                {
                    MinLenghtAttribute minLenghtAttribute = attribute as MinLenghtAttribute;

                    if (minLenghtAttribute == null)
                        continue;

                    if (_customer.Address.Length < minLenghtAttribute.MinLenght)
                    {
                        throw new AddressException("Address is not valid.");
                    }
                }
            }
        }

        private void OrderComplete()
        {
            string subject = "Order is completed.";
            string message = $"{_customer.Name}, your order is completed.\nTo pay {_order.OrderPrice} BYN.";

            AsciiArt.Timer();

            Console.WriteLine(subject);
            MailSend(subject, message);
        }

        private void OrderDeliver()
        {
            string subject = "Order delivered by courier.";
            string message = $"{_customer.Name}, your order delivered by courier.";

            AsciiArt.Timer();

            Console.WriteLine(subject);
            MailSend(subject, message);
        }

        private void OrderPaid()
        {
            string subject = "Order has been paid.";
            string message = $"{_customer.Name}, your order in the amount of {_order.OrderPrice} BYN has been paid.\nBon Appetit!";

            AsciiArt.Timer();

            Console.WriteLine(subject);
            MailSend(subject, message);
        }

        private void MailSend(string messageSubject, string messageBody)
        {
            Mail mail = new Mail(_customer.Email, messageSubject, messageBody);
            mail.Send();
        }
        #endregion
    }
}
