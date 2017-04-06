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
        Task<bool> CreateItem(FoodViewModel orden);

        Task<bool> UpdateItem(FoodViewModel orden);

        Task<bool> DeleteItem(int id);

        Task<IEnumerable<Alimentos>> GetAllFood();

        Task<FoodViewModel> GetDetailedFood(int id);

        Task<FoodViewModel> CreateItemGet();

    }
}
