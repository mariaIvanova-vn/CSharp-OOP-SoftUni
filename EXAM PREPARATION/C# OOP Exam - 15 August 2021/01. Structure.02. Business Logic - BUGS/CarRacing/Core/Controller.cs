﻿using CarRacing.Core.Contracts;
using CarRacing.Models.Cars;
using CarRacing.Models.Cars.Contracts;
using CarRacing.Models.Maps;
using CarRacing.Models.Maps.Contracts;
using CarRacing.Models.Racers;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Repositories;
using CarRacing.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CarRacing.Core
{
    public class Controller : IController
    {
        private CarRepository cars;
        private RacerRepository racers;
        private IMap map;

        public Controller()
        {
            cars = new CarRepository();
            racers = new RacerRepository();
            map = new Map();
        }

        public string AddCar(string type, string make, string model, string VIN, int horsePower)
        {
            ICar car = null;
            if (type == "SuperCar")
            {
                car = new SuperCar(make, model, VIN, horsePower);
            }
            else if (type == "TunedCar")
            {
                car = new TunedCar(make, model, VIN, horsePower);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidCarType);
            }
            cars.Add(car);
            return string.Format(OutputMessages.SuccessfullyAddedCar,make, model, VIN);
        }

        public string AddRacer(string type, string username, string carVIN)
        {
            var car = cars.FindBy(carVIN);
            if (car == null)
            {
                throw new ArgumentException(ExceptionMessages.CarCannotBeFound);
            }
            Racer racer = null;
            if (type == "ProfessionalRacer")
            {
                racer = new ProfessionalRacer(username, car);
            }
            else if (type == "StreetRacer")
            {
                racer = new StreetRacer(username, car);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidRacerType);
            }
            racer.Car = car;
            racers.Add(racer);
            return string.Format(OutputMessages.SuccessfullyAddedRacer, racer.Username);
        }

        public string BeginRace(string racerOneUsername, string racerTwoUsername)
        {
            var racerOne = racers.FindBy(racerOneUsername);
            var racerTwo = racers.FindBy(racerTwoUsername);
            if (racerOne == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.RacerCannotBeFound, racerOneUsername));
            }
            if (racerTwo == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.RacerCannotBeFound, racerTwoUsername));
            }
            return map.StartRace(racerOne, racerTwo);
        }

        public string Report()
        {
            var orderRacer= racers.Models.OrderByDescending(r=>r.DrivingExperience).ThenBy(a=>a.Username);

            StringBuilder sb = new StringBuilder();
            foreach (var item in orderRacer)
            {
                sb.AppendLine(item.ToString());
            }
            return sb.ToString().TrimEnd();
        }
    }
}
