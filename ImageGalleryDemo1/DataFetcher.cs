using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.IO;

namespace ImageGalleryDemo1
{
    class DataFetcher
    {
        //method uses the HttpClient class to fetch the JSON data from the server. The method GetStringAsync() is used to return response as a string in a asynchronous operation.
       
        async Task<string> GetDatafromService(string searchstring)
        {
            string readText = null;
            try
            {
                string url = @"https://imagefetcher20200529182038.azurewebsites.net/api/fetch_images?query=" + searchstring + "&max_count=5";
                using (HttpClient c = new HttpClient())
                {
                    readText = await c.GetStringAsync(url);
                }
            }
            catch
            {
                readText = File.ReadAllText(@"Data/sampleData.json");
            }
            return readText;
        }

        // Method to use the JsonConvert.DeserializeObject() method of Newtonsoft.Json to parse the json data into an instance of ImageItem.
        
        public async Task<List<ImageItem>> GetImageData(string search)
        {
            string data = await GetDatafromService(search);
            return JsonConvert.DeserializeObject<List<ImageItem>>(data);
        }

    }
}
