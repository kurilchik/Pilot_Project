namespace Pizza_Bot
{
    class Program
    {
        static void Main(string[] args)
        {
            Bot bot = new Bot();
            bot.Start();
            bot.OrderPizza();
            bot.Delivery();
        }
    }
}
