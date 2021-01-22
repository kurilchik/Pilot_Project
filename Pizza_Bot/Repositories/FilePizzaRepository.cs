using Newtonsoft.Json;
using Pizza_Bot.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace Pizza_Bot.Repositories
{
    class FilePizzaRepository : IPizzaRepository<Pizza>
    {
        private readonly string _savePath = @"..\..\..\Pizzas\";


        public FilePizzaRepository()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(_savePath);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
        }


        public void CreatePizza(Pizza pizza)
        {
            string path = $"{_savePath}ID_{pizza.Id}.json";

            File.WriteAllText(path, JsonConvert.SerializeObject(pizza));
        }

        public void DeletePizza(int id)
        {
            string path = $"{_savePath}ID_{id}.json";

            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Exists)

            {
                fileInfo.Delete();
            }
            else
                Console.WriteLine("File not found!");
        }

        public Pizza GetPizzaByID(int id)
        {
            string path = $"{_savePath}ID_{id}.json";

            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                return JsonConvert.DeserializeObject<Pizza>(File.ReadAllText(path));
            }
            else
            {
                Console.WriteLine("File not found!");
                return new Pizza();
            }
        }

        public List<Pizza> GetPizzas()
        {
            List<Pizza> pizzas = new List<Pizza>();

            DirectoryInfo directoryInfo = new DirectoryInfo(_savePath);
            FileInfo[] filesInfo = directoryInfo.GetFiles();

            for (int i = 0; i < filesInfo.Length; i++)
            {
                if (filesInfo[i].Extension.Equals(".json", StringComparison.OrdinalIgnoreCase))
                    pizzas.Add(JsonConvert.DeserializeObject<Pizza>(File.ReadAllText(filesInfo[i].FullName)));
            }

            if (pizzas.Count.Equals(0))
            {
                Console.WriteLine("Files not found!");
            }

            return pizzas;
        }

        public void UpdatePizza(Pizza pizza)
        {
            string path = $"{_savePath}ID_{pizza.Id}.json";

            FileInfo fileInfo = new FileInfo(path);
            if (!fileInfo.Exists)
            {
                Console.WriteLine("File not found!");
            }
            else
                CreatePizza(pizza);
        }
    }
}
