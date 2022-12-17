using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Chat.Models;
using WpfApp1.Mvvm;

namespace WpfApp1.ViewModels;

public class RateViewModel : ViewModelBase
{
    // private Fish _SelectedFish;


    //public RateViewModel(IApiService _apiService)
    public RateViewModel()
    {
        //apiService = _apiService;
        ReloadButton_Click = new Command(_ReloadButton_Click);
        RateButton_Click = new Command(_RateButton_Click);
        LogoutButton_Click = new Command(_LogoutButton_Click);
    }

    public Command ReloadButton_Click { get; }
    public Command RateButton_Click { get; }
    public Command LogoutButton_Click { get; }


    public List<Message> Messages { get; set; }
    public ComboBoxItem SelectedRateCombo { get; set; }

    // public Fish SelectedFish
    // {
    //     get => _SelectedFish;
    //     set
    //     {
    //         _SelectedFish = value;
    //         if (_SelectedFish != null) FishImage = new BitmapImage(new Uri(SelectedFish.FishImageLink));
    //         OnPropertyChanged(nameof(FishImage));
    //     }
    // }

    public ImageSource FishImage { get; set; }


    public async Task LoadAllFish()
    {
        //załaduj ryby bez oceny
        //var apiService = new ApiService();
        //FishList = (await apiService.GetAllFish(true)).ToList();
    }

    private async void _ReloadButton_Click()
    {
        await LoadAllFish();
        // OnPropertyChanged(nameof(FishList));
    }

    private async void _RateButton_Click()
    {
        //var apiService = new ApiService();
        // FishImage = new BitmapImage(new Uri(SelectedFish.FishImageLink));
        OnPropertyChanged(nameof(FishImage));

        var numVal = Convert.ToInt32(SelectedRateCombo.Content);
        //var fishRateResult = await apiService.UpdateFish(SelectedFish, numVal);
        // MessageBox.Show("Wystawiono ocenę " + SelectedRateCombo.Content + " dla ryby " + SelectedFish.Name);

        await LoadAllFish();
        // OnPropertyChanged(nameof(FishList));
    }

    private async void _LogoutButton_Click()
    {
        (Application.Current as App).viewModel.selectedViewModel = new MainWindowViewModel();
    }
}