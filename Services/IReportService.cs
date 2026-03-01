using LaundryApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryApplication.Services
{
    internal interface IReportService
    {
        Task<bool> SubmitReportAsync(IssueReports report);
    }
}