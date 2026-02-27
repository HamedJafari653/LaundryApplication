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
        public string Title { get; set; }   // Kort beskrivning av problemet
        DateTime DateReported { get; set; } // Datum och tid när problemet rapporterades
        public string ApartmentNumber { get; set; }
        bool IsFixed { get; set; } = false;
    }
}
