using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Memory;

namespace Fiap2.Core
{
    public class PhotoService : IPhotoService
    {
        private readonly IConfiguration _configuration;
        private IMemoryCache _cache;


        public PhotoService(IConfiguration configuration, IMemoryCache cache)
        {
            _cache = cache;
            _configuration = configuration;
        }
        public List<Photo> List()
        {
            const string cacheKey = "photoList";
            List<Photo> photoList;
            if (!_cache.TryGetValue(cacheKey, out photoList))
            {
                var client = new HttpClient();
                var apiUrl = _configuration.GetSection("AppSettings").GetValue<string>("PhotoApiUrl");
                var stringTask = client.GetStringAsync(apiUrl);
                var response = string.Empty;

                Task.Run(async () =>
                {
                    response = await stringTask as string;
                }).Wait();

                photoList = JsonConvert.DeserializeObject<List<Photo>>(response);

                var cacheTime = _configuration.GetSection("AppSettings").GetValue<int>("CacheTime");
                _cache.Set(cacheKey, photoList, DateTimeOffset.Now.AddSeconds(cacheTime));
            }

            return photoList.Take(20).ToList();
        }

        public List<Photo> List(string category)
        {
            //filter category example
            return this.List();
        }
    }

}
