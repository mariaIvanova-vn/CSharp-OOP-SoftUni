using FrontDeskApp;
using NUnit.Framework;
using System;

namespace BookigApp.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void RoomCreatior()
        {
            Room room = new Room(3, 55);         

            Assert.AreEqual(room.PricePerNight, 55);
            Assert.AreEqual(room.BedCapacity, 3);
        }
        [Test]
        public void RoomCreatiorTrowWithNegativeCapacity()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Room room = new Room(-3, 55);
            });
        }
        [Test]
        public void RoomCreatiorTrowWithZeroCapacity()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Room room = new Room(0, 55);
            });
        }
        [Test]
        public void RoomCreatiorTrowWithNegativePrice()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Room room = new Room(3, -55);
            });
        }
        [Test]
        public void RoomCreatiorTrowWithZeroPrice()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Room room = new Room(3, 0);
            });
        }
        [Test]
        public void BookingCreatior()
        {
            Room room = new Room(3, 55);
            Booking booking = new Booking(1, room, 5);

            Assert.AreEqual(booking.BookingNumber, 1);
            Assert.AreEqual(booking.ResidenceDuration, 5);
            Assert.AreEqual(booking.Room, room);            
        }
        [Test]
        public void HotelCreatior()
        {
            Hotel hotel = new Hotel("Vanilla", 5);

            Assert.AreEqual(hotel.FullName, "Vanilla");
            Assert.AreEqual(hotel.Category, 5);
        }
        [Test]
        public void HotelThrowWithNullName()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Hotel hotel = new Hotel(null , 5);
            });
        }
        [Test]
        public void HotelThrowWithEmptyName()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Hotel hotel = new Hotel(" ", 5);
            });
        }
        [Test]
        public void HotelThrowWithCategory()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Hotel hotel = new Hotel("Vanilla", 0);
            });
            Assert.Throws<ArgumentException>(() =>
            {
                Hotel hotel = new Hotel("Vanilla", 6);
            });
        }
        [Test]
        public void AddRooms()
        {
            Room room = new Room(3, 55);
            Hotel hotel = new Hotel("Vanilla", 5);
            hotel.AddRoom(room);

            Assert.AreEqual(hotel.Rooms.Count, 1);
        }
        [Test]
        public void BookRooms()
        {
            Room room = new Room(3, 55);
            Hotel hotel = new Hotel("Vanilla", 5);
            hotel.AddRoom(room);
            hotel.BookRoom(2, 1, 3, 1000);

            Assert.AreEqual(hotel.Bookings.Count, 1);
            Assert.AreEqual(hotel.Turnover, 165);
           
        }
        [Test]
        public void BookRoomsThrow()
        {
            Room room = new Room(3, 55);
            Hotel hotel = new Hotel("Vanilla", 5);

            Assert.Throws<ArgumentException>(() =>
            {
                hotel.BookRoom(0, 1, 3, 100);
            });
            Assert.Throws<ArgumentException>(() =>
            {
                hotel.BookRoom(-2, 1, 3, 100);
            });
            Assert.Throws<ArgumentException>(() =>
            {
                hotel.BookRoom(2, -1, 3, 100);
            });
            Assert.Throws<ArgumentException>(() =>
            {
                hotel.BookRoom(2, 1, 0, 100);
            });
        }
    }
}