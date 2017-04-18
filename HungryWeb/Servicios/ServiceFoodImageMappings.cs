using HungryWeb.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HungryWeb.Models;
using System.Threading.Tasks;
using System.Net.Http;

namespace HungryWeb.Servicios
{
    public class ServiceFoodImageMappings : IServiceFoodImageMap
    {
        public async Task<IEnumerable<FoodImageMapping>> GetAllMappings(int imageID)
        {

            using (HttpClient client = new HttpClient())
            {

                client.BaseAddress = new Uri(string.Format(ApiConfig.GetImageMappings,imageID));

                HttpResponseMessage response = await client.GetAsync(string.Format(ApiConfig.GetImageMappings, imageID));

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    var Collection = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<FoodImageMapping>>(data);

                    return Collection;

                }

                return null;


            }
        }
    }
}