using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Mvvm;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Services;
using Microsoft.Win32;
using Models;

namespace WpfApp1.ViewModels
{
    public class ParticipantViewModels: ViewModelBase
    {
        public Command AddFishButton { get; }
        public Command LogoutButton { get; }
        public string RankLabel { get; set; }

        private IApiService apiService;

        public List<Fish> FishList { get; set; }
        

        public ParticipantViewModels()
        {

            AddFishButton = new Command(_AddFishButton);
            LogoutButton = new Command(_LogoutButton);

            RankLabel = "1";
            //uzupełnić z bazy danych

            LoadAllFish();
        }

        public async void _AddFishButton()
        {
            (Application.Current as App).viewModel.selectedViewModel = new AddFishViewModel();
        }

        public async void _LogoutButton()
        {
            (Application.Current as App).viewModel.selectedViewModel = new MainWindowViewModel();
        }

        public async Task LoadAllFish()
        {
            //załaduj ryby bez oceny zamienić na ryby uzytkownika
            var apiService = new ApiService();
            FishList = (await apiService.GetAllFish(false, Properties.Settings.Default.UserID)).ToList();
            OnPropertyChanged(nameof(FishList));

        }
    }
}
