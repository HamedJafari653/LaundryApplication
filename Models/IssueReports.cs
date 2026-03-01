
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryApplication.Models
{
    internal class IssueReports
    {
        private int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateReported { get; set; } // Changed from private to public
        public string ApartmentNumber { get; set; }
        public string Description { get; set; }
        public bool IsFixed { get; set; } // Changed from private to public
    }
}
