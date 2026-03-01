using LaundryApplication.Services;
using LaundryApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace LaundryApplication.ViewModels
{
    internal class ReportViewModel : BindableObject
    {
        private readonly IReportService _reportService;
        private string _title;
        private string _description;
        private string _apartmentNumber;
        private bool _isBusy;

        public string Title
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
            if (string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(Description))
            {
                await DisplayErrorAlert("Validation Error", "Please fill both the Title and Description!");
                return;
            }
            IsBusy = true;
            ((Command)SubmitReportCommand).ChangeCanExecute();

            try
            {
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
                    await DisplaySuccessAlert("Success", "Report submitted successfully!");
                    Title = string.Empty;
                    Description = string.Empty;
                }
                else
                {
                    await DisplayErrorAlert("Error", "Failed to submit report. Please try again later.");
                }
            }
            finally
            {
                IsBusy = false;
                ((Command)SubmitReportCommand).ChangeCanExecute();
            }
        }

        private Task DisplaySuccessAlert(string title, string message)
        {
            return Application.Current.MainPage.DisplayAlert(title, message, "OK");
        }

        private Task DisplayErrorAlert(string title, string message)
        {
            return Application.Current.MainPage.DisplayAlert(title, message, "OK");
        }
    }
}
