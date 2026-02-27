using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryApplication.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime => StartTime.AddHours(2);
        public bool IsReserved { get; set; }
        public string ReservedByUserName { get; set; }
        public string ApartmentNumber { get; set; }
        public string IssueReports { get; set; }
    }
}
