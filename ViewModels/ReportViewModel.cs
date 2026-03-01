using LaundryApplication.Services;
using LaundryApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LaundryApplication.ViewModels
{
    internal class ReportViewModel : BindableObject
    {
        private readonly IReportService _reportService;
        private string _title;
        private string _description;
        private string _apartmentNumber;
        private bool _isBusy;

        private string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }
        public string ApartmentNumber
        {
            get => _apartmentNumber;
            set
            {
                _apartmentNumber = value;
                OnPropertyChanged();
            }
        }
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }
        public ICommand SubmitReportCommand { get; }
        public ReportViewModel(IReportService reportService)
        {
            _reportService = reportService;
            SubmitReportCommand = new Command(async () => await SubmitReport(), () => !IsBusy);

        }
        private async Task SubmitReport()
        {
            if (string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace (Description))
            {
                await Application.Current.MainPage.DisplayAlert("Error, please fill both The Title and Description! ");
                return;
            }
            IsBusy = true;
            ((Command)SubmitReportCommand).ChangeCanExecute(); // Disable the button while processing

            var newReport = new IssueReports
            {
                Title = this.Title,
                Description = this.Description,
                ApartmentNumber = this.ApartmentNumber,
                DateReported = DateTime.Now,
                IsFixed = false,
            };
            var success = await _reportService.SubmitReportAsync(newReport);
            if (success)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "Report submitted successfully! " );
                Title = string.Empty;
                Description = string.Empty;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to submit report. Please try again later.");
            }
        }
    }
}
