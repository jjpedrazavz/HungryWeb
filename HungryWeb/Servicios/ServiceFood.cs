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
        public void CreateItem(FoodViewModel orden)
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

                    var FoodList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Alimentos>>(data);

                    return FoodList;
                }

                return null;

            }
        }

        public async Task<FoodViewModel> GetDetailedFood(int id)
        {
            using (HttpClient _client = new HttpClient())
            {

                _client.BaseAddress = new Uri(string.Format(ApiConfig.GetAlimento,id));
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await _client.GetAsync(string.Format(ApiConfig.GetAlimento, id));

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    var FoodItem = Newtonsoft.Json.JsonConvert.DeserializeObject<FoodViewModel>(data);

                    return FoodItem;
                }

                return null;

            }
        }

        public void UpdateItem(int id, FoodViewModel orden)
        {
            throw new NotImplementedException();
        }
    }
}