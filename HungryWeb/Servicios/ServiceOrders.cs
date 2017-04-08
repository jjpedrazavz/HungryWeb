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
using Newtonsoft.Json;
using System.Text;
using System.Net;

namespace HungryWeb.Servicios
{
    public class ServiceOrders : IServiceOrders
    {
        public async Task<bool> CreateItem(OrderViewModel orden)
        {

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiConfig.GetCreateOrdersForm);

                StringContent content = new StringContent(JsonConvert.SerializeObject(orden).ToString(), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(ApiConfig.GetCreateOrdersForm, content);

                return  response.StatusCode == HttpStatusCode.Created ? true : false;


            }



        }

        public async Task<bool> DeleteItem(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(string.Format(ApiConfig.DeleteOrder, id));

                HttpResponseMessage response = await client.DeleteAsync(string.Format(ApiConfig.DeleteOrder, id));

                return response.StatusCode == HttpStatusCode.NoContent ? true : false;


            }
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


        public async Task<bool> UpdateItem(DetailedOrderViewModel orden)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiConfig.UpdateOrder);

                StringContent content = new StringContent(JsonConvert.SerializeObject(orden).ToString(), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync(ApiConfig.UpdateOrder, content);

               return  response.StatusCode == HttpStatusCode.NoContent ? true : false;

            }
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

        public async Task<OrderViewModel> GetCreateOrderForm()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiConfig.GetCreateOrdersForm);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(ApiConfig.GetCreateOrdersForm);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var element = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderViewModel>(data);

                    return element;

                }

                return null;


            }
        }
    }
}