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
    internal class UserViewModel : BindableObject
    {
        private readonly IUserService _user;
        private User _currentUser;
        private bool _IsBusy;

        public User CurrentUser
        {
            get => _currentUser;
            set { _currentUser = value; OnPropertyChanged();}
        }
        public bool IsBusy
        {
            get => _IsBusy;
            set { _IsBusy = value; OnPropertyChanged(); }
        }
        public ICommand UpdateProfileCommand { get; }
        public ICommand LogoutCommand { get; }

        public UserViewModel(IUserService userService)
        {
            _userService = userService;

            LoadUserData();
            UpdateProfileCommand = new Command(async () => await OnUpdateProfile());
        }
    }
}
