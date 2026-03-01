
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryApplication.Models
{
    internal class IssueReports
    {
        int Id { get; set; }
        public string Title { get; set; }
        DateTime DateReported { get; set; } 
        public string ApartmentNumber { get; set; }
        public string Description { get; set; } // Mer detaljerad beskrivning av problemet
        bool IsFixed { get; set; } = false;
    }
}
