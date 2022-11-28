using Microsoft.Win32;
using System.Windows;
using WpfApp1.Mvvm;
using WpfApp1.Properties;

namespace WpfApp1.ViewModels;

public class AddFishViewModel : ViewModelBase
{
    private bool SendState;

    public AddFishViewModel()
    {
        AddFishButton_Click = new Command(_AddFishButton_Click);
        SendButton_Click = new Command(_SendButton_Click);
        BackButton_Click = new Command(_BackButton_Click);
    }

    public Command AddFishButton_Click { get; }
    public Command SendButton_Click { get; }
    public Command BackButton_Click { get; }
    public string LinkLabel { get; set; }
    private OpenFileDialog uploadedFile { get; set; }

    public string SpeciesTextBox { get; set; }

    private async void _AddFishButton_Click()
    {
        var dlg = new OpenFileDialog();

        dlg.DefaultExt = ".png";
        dlg.Filter =
            "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
        var result = dlg.ShowDialog();
        if (result == true)
        {
            var filename = dlg.FileName;
            LinkLabel = filename;
            OnPropertyChanged(nameof(LinkLabel));
            uploadedFile = dlg;
            SendState = true;
        }
    }

    private async void _SendButton_Click()
    {
        if (SendState)
        {
            var PersonId = Settings.Default.UserID;
            if (SpeciesTextBox != null && SpeciesTextBox.Length > 0)
            {
                var result = true;//await apiService.PostFish(SpeciesTextBox, PersonId, uploadedFile);
                if (result)
                {
                    MessageBox.Show("Ryba została dodana");
                    (Application.Current as App).viewModel.selectedViewModel = new ParticipantViewModels();
                }
                else
                {
                    MessageBox.Show("Problem podczas dodawania ryby");
                }
            }
            else
            {
                MessageBox.Show("Pole z nazwą ryby nie może być puste");
            }

            ;
        }
        else
        {
            MessageBox.Show("Pole z wyborem zdjęcia ryby nie może być puste");
        }

        ;
    }

    private async void _BackButton_Click()
    {
        (Application.Current as App).viewModel.selectedViewModel = new ParticipantViewModels();
    }
}