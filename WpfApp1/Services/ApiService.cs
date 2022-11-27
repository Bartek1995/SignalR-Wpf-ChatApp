using Microsoft.Win32;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Services
{
    internal class ApiService : IApiService
    {
        public readonly HttpClient client;
        public readonly HttpClient fishImageAddClient;
        public readonly JsonSerializerOptions serialezerOptions;

        public ApiService()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri("https://fish-api-test.azurewebsites.net/")
            };


            serialezerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                IgnoreNullValues = true

            };
        }

        public async Task<IEnumerable<Person>> GetAllPeople()
        {

            var result = await client.GetAsync("api/people");
            if (!result.IsSuccessStatusCode) return new List<Person>();

            var content = await result.Content.ReadAsStringAsync();

            var list = JsonSerializer.Deserialize<List<Person>>(content, serialezerOptions);

            if (list is not null) return list;

            return new List<Person>();
        }

        public async Task<IEnumerable<Fish>> GetAllFish(bool WithoutRate = false, Nullable<int> PersonId = null)
        {
            var result = await client.GetAsync("api/fish");


            if (!result.IsSuccessStatusCode)
            {
                return new List<Fish>();
            }
            var content = await result.Content.ReadAsStringAsync();
            var list = JsonSerializer.Deserialize<List<Fish>>(content, serialezerOptions);

            if (WithoutRate)
            {
                foreach (Fish element in list.ToArray())
                {
                    if (element.Rate > 0) { list.Remove(element); }
                }
            }
            
            if (PersonId != null)
            {
                foreach (Fish element in list.ToArray())
                {
                    if (element.PersonId != PersonId) 
                    { list.Remove(element); }
                }

                list = list.OrderByDescending(Fish => Fish.Rate).ToList();
            }


            if (list is not null) { return list; }

            return new List<Fish>();
        }

        public async Task<Person> GetPerson(string id, string password)
        {

            var result = await client.GetAsync("api/people/" + id);
            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            var content = await result.Content.ReadAsStringAsync();
            var person = JsonSerializer.Deserialize<Person>(content, serialezerOptions);

            if (person.Password != password)
            {
                return null;
            }

            if (person is not null) { return person; }

            return null;

        }

        public async Task<bool> PostPerson(string FirstName, string LastName, string Password, string Role)
        {

            var person = new Person
            {
                FirstName = FirstName,
                LastName = LastName,
                Password = Password,
                Role = Role,
                //Id = null
            };

            var json = JsonSerializer.Serialize(person, serialezerOptions);

            var requestContent = new StringContent(json, Encoding.UTF8, "application/json");

            var result = await client.PostAsync("api/people", requestContent);

            if (result.IsSuccessStatusCode)
            {
                var location = result.Headers.Location;
                var resultString = Regex.Match(location.ToString(), @"\d+").Value;
                MessageBox.Show("Utworzono użytkownika - " + person.FirstName + " " + person.LastName + " o roli " + person.Role + " oraz ID = " + resultString);
                return true;

            }
            return false;
        }

        public async Task<bool> UpdateFish(Fish SelectedFish, int Rate)
        {
            SelectedFish.Rate = Rate;

            var json = JsonSerializer.Serialize(SelectedFish, serialezerOptions);

            var requestContent = new StringContent(json, Encoding.UTF8, "application/json");

            var result = await client.PutAsync("api/fish/" + SelectedFish.Id.ToString(), requestContent);

            if (result.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> PostFish(string FishName, int PersonId, OpenFileDialog dlg)
        {

            var FirstUriPart = "https://storageforfishapp.blob.core.windows.net/fishtank/";
            var LastUriPart = "?sp=racl&st=2022-05-31T20:48:14Z&se=2023-06-01T04:48:14Z&spr=https&sv=2020-08-04&sr=c&sig=4VnACOzLZCInxLwnhggplK4nsfpvCDgd%2BLbVGCSEnwM%3D";


            var BlobFileName = PersonId.ToString() + "_" + DateTime.Now.ToString("M_d_yyyy_h_mm_ss") + ".jpg";
            var FishImageLink = FirstUriPart + BlobFileName + LastUriPart;

            var filename = dlg.FileName;

            var fileStream = dlg.OpenFile();

            Azure.Storage.Blobs.BlobClient blobClient = new Azure.Storage.Blobs.BlobClient(
            connectionString: Properties.Settings.Default.BLOBconnectionString,
            blobContainerName: Properties.Settings.Default.BLOBcontainerName,
            blobName: BlobFileName);

            blobClient.Upload(fileStream);
            var fish = new Fish
            {
                Name = FishName.ToLower(),
                PersonId = PersonId,
                Rate = 0,
                FishImageLink = FishImageLink
            };

            var json = JsonSerializer.Serialize(fish, serialezerOptions);

            var requestContent = new StringContent(json, Encoding.UTF8, "application/json");

            var result = await client.PostAsync("api/fish", requestContent);
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
