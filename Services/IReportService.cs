using LaundryApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryApplication.Services
{
    internal class IReportService
    {
        Task<bool> SubmitReportAsync(IssueReport report);
    }
}
