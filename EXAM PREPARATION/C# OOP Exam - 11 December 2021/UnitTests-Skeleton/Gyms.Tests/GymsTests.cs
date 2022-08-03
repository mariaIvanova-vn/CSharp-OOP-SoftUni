using NUnit.Framework;
using System;

namespace Gyms.Tests
{
    public class GymsTests
    {
        [Test]
        public void AthleteCreatior()
        {
            Athlete athlete = new Athlete("Maria");

            Assert.AreEqual(athlete.FullName, "Maria");
            Assert.AreEqual(false, athlete.IsInjured);
        }
        [Test]
        public void GymCreatior()
        {
            Gym gym = new Gym("Richie", 5);

            Assert.AreEqual("Richie", gym.Name);
            Assert.AreEqual(5, gym.Capacity);
            Assert.AreEqual(0, gym.Count);
        }
        [Test]
        public void GymThrowWithNullName()
        {
            Assert.Throws<ArgumentNullException>(() => 
            {
                Gym gym = new Gym(null, 5);
            });
        }
        [Test]
        public void GymThrowWithEmptyName()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Gym gym = new Gym("", 5);
            });
        }
        [Test]
        public void GymThrowWithNegativeCapacity()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Gym gym = new Gym("Richie", -5);
            });
        }
        [Test]
        public void GymAddAthlete()
        {
            Gym gym = new Gym("Richie", 5);
            Athlete athlete = new Athlete("Didi");
            gym.AddAthlete(athlete);
            Assert.AreEqual(1, gym.Count);
        }
        [Test]
        public void GymAddAthleteThrow()
        {
            Gym gym = new Gym("Richie", 1);
            Athlete athlete = new Athlete("Didi");
            Athlete athlete2 = new Athlete("Sashko");
            gym.AddAthlete(athlete);
            Assert.Throws<InvalidOperationException>(() => { gym.AddAthlete(athlete2); });           
        }
        [Test]
        public void GymRemoveAthlete()
        {
            Gym gym = new Gym("Richie", 5);
            Athlete athlete = new Athlete("Didi");
            gym.AddAthlete(athlete);
            gym.RemoveAthlete("Didi");
            Assert.AreEqual(0, gym.Count);
        }
        [Test]
        public void GymRemoveAthleteWithThrow()
        {
            Gym gym = new Gym("Richie", 5);
            Athlete athlete = new Athlete("Didi");
            gym.AddAthlete(athlete);
            Assert.Throws<InvalidOperationException>(() => { gym.RemoveAthlete("Sashko"); });                   
        }
        [Test]
        public void GymInjureAthleteTrue()
        {
            Gym gym = new Gym("Richie", 5);
            Athlete athlete = new Athlete("Didi");
            gym.AddAthlete(athlete);
           var returned = gym.InjureAthlete(athlete.FullName);
            Assert.AreEqual(true, athlete.IsInjured);
            Assert.AreSame(athlete, returned);
        }
        [Test]
        public void GymInjureAthleteThrow()
        {
            Gym gym = new Gym("Richie", 5);
            Athlete athlete = new Athlete("Didi");
            gym.AddAthlete(athlete);
            gym.InjureAthlete(athlete.FullName);
            Assert.Throws<InvalidOperationException>(() => { gym.InjureAthlete("Sashko"); });
        }
        [Test]
        public void GymReport()
        {
            Gym gym = new Gym("Richie", 5);
            Athlete athlete = new Athlete("Didi");
            gym.AddAthlete(athlete);

            Assert.AreEqual("Active athletes at Richie: Didi", gym.Report());
        }
        [Test]
        public void GymReportInjure()
        {
            Gym gym = new Gym("Richie", 5);
            Athlete athlete = new Athlete("Didi");
            gym.AddAthlete(athlete);
            gym.InjureAthlete(athlete.FullName);
            Assert.AreEqual("Active athletes at Richie: ", gym.Report());
        }
    }
}
