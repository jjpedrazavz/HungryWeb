using HungryWeb.Models3;
using HungryWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungryWeb.Contratos
{
    public interface IServiceFood
    {
        void CreateItem(FoodViewModel orden);

        void UpdateItem(int id, FoodViewModel orden);

        void DeleteItem(int id);

        Task<IEnumerable<Alimentos>> GetAllFood();

        Task<FoodViewModel> GetDetailedFood(int id);
    }
}
