using HungryWeb.Contratos;
using HungryWeb.Models3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HungryWeb.ViewModels;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace HungryWeb.Servicios
{
    public class ServiceFood : IServiceFood
    {
        public void CreateItem(AlimentoViewModel orden)
        {
            throw new NotImplementedException();
        }

        public void DeleteItem(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Alimentos>> GetAllFood()
        {
            using (HttpClient _client = new HttpClient())
            {

                _client.BaseAddress = new Uri(ApiConfig.GetAllFood);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await _client.GetAsync(ApiConfig.GetAllFood);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    var Food = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Alimentos>>(data);

                    return Food;
                }

                return null;

            }
        }

        public Task<Alimentos> GetDetailedFood(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(int id, AlimentoViewModel orden)
        {
            throw new NotImplementedException();
        }
    }
}