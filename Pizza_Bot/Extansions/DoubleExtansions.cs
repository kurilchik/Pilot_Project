using Pizza_Bot.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pizza_Bot.Extansions
{
    public static class DoubleExtansions
    {
        public static double TotalPrice(this double num, PizzaSize pizzaSize)
        {
            if (pizzaSize.Equals(PizzaSize.Medium))
                return num * 1.2;
            else if (pizzaSize.Equals(PizzaSize.Large))
                return num * 1.4;
            else if (pizzaSize.Equals(PizzaSize.ExtraLarge))
                return num * 1.6;
            return num;
        }
    }
}
