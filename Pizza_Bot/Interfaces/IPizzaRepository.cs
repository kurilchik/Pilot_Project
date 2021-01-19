using System;
using System.Collections.Generic;
using System.Text;

namespace Pizza_Bot.Interfaces
{
    public interface IPizzaRepository<T>
    {
        public T GetPizzaByID(int id);

        public List<T> GetPizzas();

        public void CreatePizza(T pizza);

        public void UpdatePizza(T pizza);

        public void DeletePizza(int id);
    }
}
