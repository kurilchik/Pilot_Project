using Pizza_Bot.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pizza_Bot
{
    class Pizza
    {
        public PizzaSize _size;
        public double _price;

        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
