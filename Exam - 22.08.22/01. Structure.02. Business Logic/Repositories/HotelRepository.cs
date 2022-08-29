using BookingApp.Models.Hotels.Contacts;
using BookingApp.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookingApp.Repositories
{
    public class HotelRepository : IRepository<IHotel>
    {
        private readonly List<IHotel> models;

        public HotelRepository()
        {
            models = new List<IHotel>();
        }
        public void AddNew(IHotel model)
        {
            models.Add(model);
        }

        public IReadOnlyCollection<IHotel> All()
        {
            return models;
        }

        public IHotel Select(string criteria)
        {
            return models.FirstOrDefault(c => c.FullName == criteria);
        }
    }
}
