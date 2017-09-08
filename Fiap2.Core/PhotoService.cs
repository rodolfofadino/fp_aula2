using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace Fiap2.Core
{
    public class PhotoService : IPhotoService
    {
        private readonly IConfiguration _configuration;

        public PhotoService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public List<Photo> List()
        {
            var client = new HttpClient();
            var apiUrl = _configuration.GetSection("AppSettings").GetValue<string>("PhotoApiUrl");
            var stringTask = client.GetStringAsync(apiUrl);
            var response = string.Empty;

            Task.Run(async () =>
            {
                response = await stringTask as string;
            }).Wait();

            var photoList = JsonConvert.DeserializeObject<List<Photo>>(response);

            return photoList.Take(20).ToList();
        }

    }

}
