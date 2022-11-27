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


namespace WpfApp1.ViewModels
{
    public class AddFishViewModel : ViewModelBase
    {
        public Command AddFishButton_Click { get; }
        public Command SendButton_Click { get; }
        public Command BackButton_Click { get; }
        public string LinkLabel { get; set; }
        OpenFileDialog uploadedFile { get; set; }
        bool SendState;

        public string SpeciesTextBox { get; set; }

        public AddFishViewModel()
        {
            AddFishButton_Click = new Command(_AddFishButton_Click);
            SendButton_Click = new Command(_SendButton_Click);
            BackButton_Click = new Command(_BackButton_Click);
        }

        private async void _AddFishButton_Click()
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
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
                var PersonId = Properties.Settings.Default.UserID;
                ApiService apiService = new ApiService();
                if (SpeciesTextBox != null && SpeciesTextBox.Length > 0)
                {
                    var result = await apiService.PostFish(SpeciesTextBox, PersonId, uploadedFile);
                    if (result == true) 
                    {
                        MessageBox.Show("Ryba została dodana");
                        (Application.Current as App).viewModel.selectedViewModel = new ParticipantViewModels(); 
                    }
                    else { MessageBox.Show("Problem podczas dodawania ryby");}
                }
                else { MessageBox.Show("Pole z nazwą ryby nie może być puste"); };
                     
            }
            else { MessageBox.Show("Pole z wyborem zdjęcia ryby nie może być puste"); };
        }

        private async void _BackButton_Click()
        {
            (Application.Current as App).viewModel.selectedViewModel = new ParticipantViewModels();

        }
    }
}
