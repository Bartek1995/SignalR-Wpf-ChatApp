using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Mvvm;
using WpfApp1.Properties;

namespace WpfApp1.ViewModels;

public class ParticipantViewModels : ViewModelBase
{
    // private IApiService apiService;


    public ParticipantViewModels()
    {
        AddFishButton = new Command(_AddFishButton);
        LogoutButton = new Command(_LogoutButton);

        RankLabel = "1";
        //uzupełnić z bazy danych

        LoadAllFish();
    }

    public Command AddFishButton { get; }
    public Command LogoutButton { get; }
    public string RankLabel { get; set; }

    public List<Fish> FishList { get; set; }

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
        //var apiService = new ApiService();
        //FishList = (await apiService.GetAllFish(false, Settings.Default.UserID)).ToList();
        OnPropertyChanged(nameof(FishList));
    }
}