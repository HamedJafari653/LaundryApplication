using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LaundryApplication.Models;
using LaundryApplication.Data;

namespace LaundryApplication.Services
{
    public class BookingService : IBookingService
    {
        private readonly MyDbContext _context; 

        public BookingService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<List<Booking>> GetSlotsForDateAsync(DateTime date)
        {
            // 1. Hämta de bokningar som redan finns i Azure för detta datum
            var existingBookings = await _context.Bookings
                .Where(b => b.StartTime.Date == date.Date)
                .ToListAsync();

            var allSlots = new List<Booking>();

            // 2. Generera fasta pass: 07:00, 09:00 ... till 20:00 (sista passet slutar 22:00)
            for (int hour = 7; hour <= 22; hour += 2)
            {
                var slotStartTime = new DateTime(date.Year, date.Month, date.Day, hour, 0, 0);

                // Kolla om detta pass redan är bokat i vår lista från databasen
                var bookedSlot = existingBookings.FirstOrDefault(b => b.StartTime == slotStartTime);

                if (bookedSlot != null)
                {
                    allSlots.Add(bookedSlot);
                }
                else
                {
                    // Om inte bokat, skapa ett "tomt" objekt för UI:t
                    allSlots.Add(new Booking
                    {
                        StartTime = slotStartTime,
                        IsReserved = false
                    });
                }
            }

            return allSlots;
        }

        public async Task<bool> ConfirmBookingAsync(Booking booking)
        {
            try
            {
                booking.IsReserved = true;
                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
