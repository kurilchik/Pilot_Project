using System;
using System.Collections.Generic;
using Pizza_Bot.Enums;
using Pizza_Bot.Repositories;
using Pizza_Bot.Extansions;
using Pizza_Bot.Drawing;

namespace Pizza_Bot
{
    public class Bot
    {
        private readonly FilePizzaRepository _filePizza = new FilePizzaRepository();
        private Order _order = new Order();

        public void Start()
        {
            AsciiArt.Logo();

            Greeting();
            Introduce();
            SetEmailAddress();
        }

        public void OrderPizza()
        {
            Console.Clear();

            PrintMenu();
            ChoicePizza();
            PrintOrder();

            Console.WriteLine("Do you want to order more pizza?");
            if (YesOrNo())
                OrderPizza();
        }

        public void Delivery()
        {
            OrderComplete();
            OrderDeliver();
            OrderPaid();
        }

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
        }

        private void Introduce()
        {
            Console.WriteLine("Please introduce yourself:");
            _order._customer.Name = Console.ReadLine();
        }

        private void SetEmailAddress()
        {
            Console.WriteLine("Please enter your email address for communication:");
            _order._customer.Email = Console.ReadLine();
        }

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
                _order._orderPrice += pizza.BasePrice.TotalPrice(pizza.Size);
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
            Console.WriteLine($"Total price = {_order._orderPrice} BYN\n");
        }

        private void OrderComplete()
        {
            AsciiArt.Timer();
            Console.WriteLine("Order is completed.");
        }

        private void OrderDeliver()
        {
            AsciiArt.Timer();
            Console.WriteLine("Order delivered by courier.");
        }

        private void OrderPaid()
        {
            AsciiArt.Timer();
            Console.WriteLine("Order has been paid.");
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
    }
}
