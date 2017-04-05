using HungryWeb.Contratos;
using HungryWeb.Models3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HungryWeb.ViewModels;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace HungryWeb.Servicios
{
    public class ServiceOrders : IServiceOrders
    {
        public void CreateItem(OrdenesViewModel orden)
        {
            throw new NotImplementedException();
        }

        public void DeleteItem(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SlimOrdersViewModel>> GetAllSlimOrders()
        {
            using (HttpClient _client = new HttpClient())
            {
                _client.BaseAddress = new Uri(ApiConfig.GetSlimOrders);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await _client.GetAsync(ApiConfig.GetSlimOrders);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var Lista = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SlimOrdersViewModel>>(data);

                    return Lista;

                }

                return null;

            }
        }


        public void UpdateItem(int id, OrdenesViewModel orden)
        {
            throw new NotImplementedException();
        }

        public async Task<DetailedOrderViewModel> GetDetailedOrder(int id)
        {
            using (HttpClient _client = new HttpClient())
            {

                _client.BaseAddress = new Uri(string.Format(ApiConfig.GetDetailedOrder, id));
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await _client.GetAsync(string.Format(ApiConfig.GetDetailedOrder, id));

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    var Order = Newtonsoft.Json.JsonConvert.DeserializeObject<DetailedOrderViewModel>(data);

                    return Order;
                }

                return null;


            }
        }
    }
}