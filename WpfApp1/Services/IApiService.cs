using Microsoft.Win32;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Services
{


    public interface IApiService
    {
        Task<IEnumerable<Fish>> GetAllFish(bool WithoutRate = false, Nullable<int> PersonId = null);
        Task<IEnumerable<Person>> GetAllPeople();
        Task<bool> UpdateFish(Fish SelectedFish, int Rate);
        Task<Person> GetPerson(string id, string password);
        Task<bool> PostPerson(string FirstName, string LastName, string Password, string Role);
        Task<bool> PostFish(string FishName, int PersonId, OpenFileDialog dlg);
    }
}
