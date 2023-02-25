using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DemoAPI.Models;
using Newtonsoft.Json.Converters;

namespace DemoAPI.Services
{

    public class TargetAssetService : ITargetAssetService
    {
        private readonly HttpClient _httpClient;

        public TargetAssetService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<List<TargetAsset>> GetTargetAssets()
        {

            var response = await _httpClient.GetAsync("https://06ba2c18-ac5b-4e14-988c-94f400643ebf.mock.pstmn.io/targetAsset");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var targetAssets = JsonConvert.DeserializeObject<List<TargetAsset>>(content);
            //Console.WriteLine("Test");
            //Console.WriteLine(targetAssets.Count);

            foreach (var item in targetAssets)
            {
                try
                {
                        item.isStartable = IsStartable(item);
                        item.parentTargetAssetCount = GetParentTargetAssetCount(item, targetAssets);
                }

                catch (NullReferenceException) 
                { 
                    Console.WriteLine("Error in Null Value");
                    
                }

                //System.Diagnostics.Debug.WriteLine(item.name);

            }

            return targetAssets;
        }

        private bool IsStartable(TargetAsset item)
        {
                       
            if (DateTime.Today.Day == 3 && item.status == "Running")
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }


        private int GetParentTargetAssetCount(TargetAsset item, List<TargetAsset> targetAssets)
        {
            int count = 0;
     
            while (item.parentId != null)
            {
                var parent = targetAssets.FirstOrDefault(t => t.id == item.parentId);
                Console.WriteLine(parent.parentId);
                if (parent.parentId == null)
                {
                    // Handle scenario where parent is not found in list
                    return count;
                }

                count++;
                item = parent;
            }
            

            return count;
        }
    }
}

