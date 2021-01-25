using Pizza_Bot.Enums;

namespace Pizza_Bot.Extansions
{
    public static class DoubleExtansions
    {
        public static double TotalPrice(this double num, PizzaSize pizzaSize)
        {
            if (pizzaSize.Equals(PizzaSize.Medium))
                return num * 1.5;
            else if (pizzaSize.Equals(PizzaSize.Large))
                return num * 2;
            return num;
        }
    }
}
