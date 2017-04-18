using HungryWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungryWeb.Contratos
{
    public interface IServiceFoodImageMap
    {
        Task<IEnumerable<FoodImageMapping>> GetAllMappings(int imageID);
    }
}
