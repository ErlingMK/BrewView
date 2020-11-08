using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Android.Content;
using Newtonsoft.Json;

namespace BrewView.Services.Mocks
{
    internal class JsonReader
    {
        public static async Task<T> ReadFromJson<T>(string jsonFile)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var manifestResourceStream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.Services.Mocks.{jsonFile}");

            using var reader = new StreamReader(manifestResourceStream, Encoding.GetEncoding("iso-8859-1")); // Encoding required to properly read 'æ', 'ø', 'å' etc.
            var data = await reader.ReadToEndAsync();

            await Task.Delay(new Random().Next(20, 1500));

            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
