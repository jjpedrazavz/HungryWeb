using HungryWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungryWeb.Contratos
{
    interface IServiceFoodImages
    {

        Task<IEnumerable<FoodImages>> GetFoodImages();

        Task<FoodImages> GetOne(int id);

        Task<string> SaveImage(FoodImages image);

        Task<bool> UpdateImage(FoodImages image);

        Task<bool> DeleteImage(int id);

    }
}
