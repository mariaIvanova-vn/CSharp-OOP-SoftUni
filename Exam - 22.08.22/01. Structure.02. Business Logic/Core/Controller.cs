using BookingApp.Core.Contracts;
using BookingApp.Models.Bookings;
using BookingApp.Models.Hotels;
using BookingApp.Models.Hotels.Contacts;
using BookingApp.Models.Rooms;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Repositories;
using BookingApp.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookingApp.Core
{
    public class Controller : IController
    {
        private HotelRepository hotels;

        public Controller()
        {
            hotels = new HotelRepository(); 
        }

        public string AddHotel(string hotelName, int category)
        {
            var existHotel = hotels.Select(hotelName);
            if (existHotel != null)
            {
                return string.Format(OutputMessages.HotelAlreadyRegistered, hotelName);
            }
            var hotel = new Hotel(hotelName, category);
            hotels.AddNew(hotel);
            return string.Format(OutputMessages.HotelSuccessfullyRegistered, category, hotelName);
        }
        public string UploadRoomTypes(string hotelName, string roomTypeName)
        {
            var existHotel = hotels.Select(hotelName);
            if (existHotel == null)
            {
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);
            }
            IHotel hotel = hotels.Select(hotelName);
            if (hotel.Rooms.Select(roomTypeName) != null)
            {
                return OutputMessages.RoomTypeAlreadyCreated;
            }

            IRoom room = null;
            if (roomTypeName == "Apartment")
            {
                room = new Apartment();
            }
            else if (roomTypeName == "DoubleBed")
            {
                room = new DoubleBed();
            }
            else if (roomTypeName == "Studio")
            {
                room = new Studio();
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.RoomTypeIncorrect);
            }

            existHotel.Rooms.AddNew(room);
            return string.Format(OutputMessages.RoomTypeAdded, roomTypeName, hotelName);
        }
        public string SetRoomPrices(string hotelName, string roomTypeName, double price)
        {
            var existHotel = hotels.Select(hotelName);
            if (existHotel == null)
            {
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);
            }

            if (roomTypeName != "DoubleBed" && roomTypeName != "Studio" && roomTypeName != "Apartment")
            {
                throw new ArgumentException(ExceptionMessages.RoomTypeIncorrect);
            }

            IRoom room = existHotel.Rooms.Select(roomTypeName);
            if (room == null)
            {
                return OutputMessages.RoomTypeNotCreated;
            }

            if (room.PricePerNight != 0)
            {
                throw new InvalidOperationException(ExceptionMessages.PriceAlreadySet);
            }

            room.SetPrice(price);
            return $"{string.Format(OutputMessages.PriceSetSuccessfully, roomTypeName, hotelName)}";
        }

        public string BookAvailableRoom(int adults, int children, int duration, int category)
        {
            var hotelsList = this.hotels.All().OrderBy(h => h.FullName);

            int guestsCount = adults + children;

            if (hotelsList.Any(h => h.Category == category))
            {
                foreach (var hotel in hotelsList)
                {
                    if (hotel.Category != category)
                    {
                        continue;
                    }

                    var avaliableRoomsList = hotel.Rooms.All()
                        .Where(r => r.PricePerNight > 0)
                        .OrderBy(r => r.BedCapacity);

                    if (avaliableRoomsList.Any(r => r.BedCapacity >= guestsCount))
                    {
                        foreach (var room in avaliableRoomsList)
                        {
                            if (room.BedCapacity >= guestsCount)
                            {
                                // Successful booking
                                int bookingNumber = hotel.Bookings.All().Count + 1;
                                Booking booking = new Booking(room, duration, adults, children, bookingNumber);
                                hotel.Bookings.AddNew(booking);
                                return $"{string.Format(OutputMessages.BookingSuccessful, bookingNumber, hotel.FullName)}";
                            }
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            else
            {
                return $"{string.Format(OutputMessages.CategoryInvalid, category)}";
            }

            return OutputMessages.RoomNotAppropriate;

        }

        public string HotelReport(string hotelName)
        {
            StringBuilder sb = new StringBuilder();

            if (hotels.Select(hotelName) == null)
            {
                return $"{string.Format(OutputMessages.HotelNameInvalid, hotelName)}";
            }

            IHotel hotel = hotels.Select(hotelName);

            sb.AppendLine($"Hotel name: {hotel.FullName}");
            sb.AppendLine($"--{hotel.Category} star hotel");
            sb.AppendLine($"--Turnover: {hotel.Turnover:f2} $");
            if (hotel.Bookings.All().Count == 0)
            {
                sb.AppendLine($"--Bookings:");
                sb.AppendLine();
                sb.AppendLine("none");
            }
            else
            {
                sb.AppendLine($"--Bookings:");
                sb.AppendLine();
                foreach (var booking in hotel.Bookings.All())
                {
                    sb.AppendLine(booking.BookingSummary());
                    sb.AppendLine();
                }
            }

            return sb.ToString().Trim();
        }        
    }
}
