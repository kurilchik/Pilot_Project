using System.Collections.Generic;
using Pizza_Bot.Extansions;

namespace Pizza_Bot
{
    public class Order
    {
        public List<Pizza> _pizzas = new List<Pizza>();
        public double OrderPrice { get; set; }
    }
}
