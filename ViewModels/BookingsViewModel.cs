using LaundryApplication.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using LaundryApplication.Models;
using System.Windows.Input;

namespace LaundryApplication.ViewModels
{
    internal class BookingsViewModel : BindableObject
    {
        private readonly IBookingService _bookingService;
        private bool _isRefreshing;

        public ObservableCollection<Booking> Bookings { get; set; }

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = value;
                OnPropertyChanged();
            }
        }
        public ICommand RefreshCommand { get; }
        public ICommand BookTimeCommand { get; }

        public BookingsViewModel(IBookingService bookingService)
        {
            _bookingService = bookingService;

            RefreshCommand = new Command(async () => await LoadBookings());
            BookTimeCommand = new Command(async (booking) => await ExecuteBookTime(booking));

            Task.Run(async () => await LoadBookings());
        }
        private async Task LoadBookings()
        {
            IsRefreshing = true;

            try
            {
                var  result = await _bookingService.GetBookingsAsync();
                Bookings.Clear();
                foreach (var b in result)
                {
                    Bookings.Add(b);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Failed to load bookings: {ex.Message}", "OK");
            }
            finally
            {
                IsRefreshing = false;
            }
        }
        private async Task ExecuteBookTime(Booking selectedBooking)
        {
            if (selectedBooking == null) return;

            bool success = await _bookingService.ConfirmBookingAsync(selectedBooking);
            if (success)
            {
                await LoadBookings();
            }
        }
    }
}
