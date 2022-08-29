using BookingApp.Models.Bookings.Contracts;
using BookingApp.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookingApp.Repositories
{
    public class BookingRepository : IRepository<IBooking>
    {
        private readonly List<IBooking> models;

        public BookingRepository()
        {
            models = new List<IBooking>();
        }
        public void AddNew(IBooking model)
        {
            models.Add(model);
        }

        public IReadOnlyCollection<IBooking> All()
        {
            return models;
        }

        public IBooking Select(string criteria)
        {
            return models.FirstOrDefault(c => c.BookingNumber.ToString() == criteria);
        }
    }
}
