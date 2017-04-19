using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using HungryWeb.Contratos;
using HungryWeb.Models;

namespace HungryWeb.Servicios
{
    public class ServiceFoodImages : IServiceFoodImages
    {
        public Task<bool> DeleteImage(FoodImages image)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<FoodImages>> GetFoodImages()
        {

            using (HttpClient client = new HttpClient())
            {

                client.BaseAddress = new Uri(ApiConfig.GetAllFoodImages);
                HttpResponseMessage response = await client.GetAsync(ApiConfig.GetAllFoodImages);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    var Collection = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<FoodImages>>(data);

                    return Collection;

                }

                return null;


            }
        }

        public async Task<FoodImages> GetOne(int id)
        {
            using (HttpClient client = new HttpClient())
            {

                client.BaseAddress = new Uri(string.Format(ApiConfig.GetImage, id));

                HttpResponseMessage response = await client.GetAsync(string.Format(ApiConfig.GetImage, id));

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var Image = Newtonsoft.Json.JsonConvert.DeserializeObject<FoodImages>(data);

                    return Image;

                }

                return null;


            }
        }


        public Task<bool> SaveImage(FoodImages image)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateImage(FoodImages image)
        {

            using (HttpClient client = new HttpClient())
            {

                HttpResponseMessage response = await client.PutAsync(ApiConfig.UpdateImage,
                    new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(image), Encoding.UTF8, "application/json"));


               return  response.StatusCode == System.Net.HttpStatusCode.NoContent ? true : false;

            }



        }

        Task<string> IServiceFoodImages.SaveImage(FoodImages image)
        {
            throw new NotImplementedException();
        }
    }
}