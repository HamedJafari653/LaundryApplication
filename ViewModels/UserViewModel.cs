using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LaundryApplication.Models;
using LaundryApplication.Services;

namespace LaundryApplication.ViewModels
{
    public class UserViewModel : BindableObject
    {
        private readonly IUserService _userService;
        private User _currentUser;
        private bool _isBusy;

        // Objektet som håller all användardata
        public User CurrentUser
        {
            get => _currentUser;
            set { _currentUser = value; OnPropertyChanged(); }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set { _isBusy = value; OnPropertyChanged(); }
        }

        public ICommand UpdateProfileCommand { get; }
        public ICommand LogoutCommand { get; }

        public UserViewModel(IUserService userService)
        {
            _userService = userService;

            // Hämta användaren när vi startar
            LoadUserData();

            UpdateProfileCommand = new Command(async () => await OnUpdateProfile());
            LogoutCommand = new Command(async () => await OnLogout());
        }

        private async void LoadUserData()
        {
            IsBusy = true;
            // Här simulerar vi att vi hämtar den inloggade användarens ID (t.ex. ID 1)
            CurrentUser = await _userService.GetUserByIdAsync(1);
            IsBusy = false;
        }

        private async Task OnUpdateProfile()
        {
            if (CurrentUser == null) return;

            IsBusy = true;
            bool success = await _userService.UpdateUserAsync(CurrentUser);
            IsBusy = false;

            if (success)
                await Application.Current.MainPage.DisplayAlert("Succé", "Profilen uppdaterad!", "OK");
            else
                await Application.Current.MainPage.DisplayAlert("Fel", "Kunde inte spara ändringar", "OK");
        }

        private async Task OnLogout()
        {
            // Logik för att rensa sessionen och skicka användaren till inloggningssidan
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
