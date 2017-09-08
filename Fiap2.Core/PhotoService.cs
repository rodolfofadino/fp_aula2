using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Fiap2.Core
{
    public class PhotoService
    {
        public List<Photo> List()
        {
            var client = new HttpClient();

            var stringTask = client.GetStringAsync("https://jsonplaceholder.typicode.com/photos");
            var response = "";
            Task.Run(async () =>
            {
                response = await stringTask as string;
            }).Wait();
            var photoList = JsonConvert.DeserializeObject<List<Photo>>(response);

            return photoList.Take(20).ToList();
        }

    }

}
