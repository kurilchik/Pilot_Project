using Pizza_Bot.Enums;

namespace Pizza_Bot
{
    public class Pizza
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public double BasePrice { get; set; }
        public PizzaSize Size { get; set; }
    }
}
