using System.Collections.Generic;
using Pizza_Bot.Extansions;

namespace Pizza_Bot
{
    public class Order
    {
        public Customer _customer;
        public List<Pizza> _pizzas;
        public double _orderPrice;

        public Order()
        {
            _customer = new Customer();
            _pizzas = new List<Pizza>();
        }
    }
}
