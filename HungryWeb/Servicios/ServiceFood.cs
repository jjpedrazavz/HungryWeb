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
using Newtonsoft.Json;
using System.Text;

namespace HungryWeb.Servicios
{
    public class ServiceFood : IServiceFood
    {
        public async Task<bool> CreateItem(FoodViewModel orden)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiConfig.CrearAlimentoConfirm);
                StringContent content = new StringContent(JsonConvert.SerializeObject(orden).ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(ApiConfig.CrearAlimentoConfirm, content);
                return response.StatusCode == System.Net.HttpStatusCode.Created ? true : false;

            }
        }

        public async Task<FoodViewModel> CreateItemGet()
        {
            using (HttpClient _client = new HttpClient())
            {

                _client.BaseAddress = new Uri(ApiConfig.CreateAlimento);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await _client.GetAsync(ApiConfig.CreateAlimento);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    var elemento = Newtonsoft.Json.JsonConvert.DeserializeObject<FoodViewModel>(data);

                    return elemento;
                }

                return null;

            }
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