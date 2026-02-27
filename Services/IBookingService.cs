using LaundryApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryApplication.Services
{
    public interface IBookingService
    {
        // Hämtar tiderna för en dag (blandar fasta tider med bokningar från DB)
        Task<List<Booking>> GetSlotsForDateAsync(DateTime date);

        // Sparar bokningen i Azure
        Task<bool> ConfirmBookingAsync(Booking booking);
    }
}
