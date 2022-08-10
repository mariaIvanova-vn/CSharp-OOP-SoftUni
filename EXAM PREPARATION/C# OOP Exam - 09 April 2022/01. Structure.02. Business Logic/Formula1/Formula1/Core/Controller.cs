using Formula1.Core.Contracts;
using Formula1.Models;
using Formula1.Models.Contracts;
using Formula1.Repositories;
using Formula1.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Formula1.Core
{
    public class Controller : IController
    {
        private PilotRepository pilotRepository;
        private RaceRepository raceRepository;
        private FormulaOneCarRepository carRepository;

        public Controller()
        {
            pilotRepository = new PilotRepository();
            raceRepository = new RaceRepository();  
            carRepository = new FormulaOneCarRepository();  
        }
        public string CreatePilot(string fullName)
        {           
            var existingPilot = pilotRepository.Models.FirstOrDefault(w => w.FullName == fullName);

            if (existingPilot != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotExistErrorMessage, fullName));
            }
            IPilot pilot = new Pilot(fullName);
            pilotRepository.Add(pilot);
            return string.Format(OutputMessages.SuccessfullyCreatePilot, fullName);
        }
        public string CreateCar(string type, string model, int horsepower, double engineDisplacement)
        {
            IFormulaOneCar car;

            if (type == "Ferrari")
            {
                car = new Ferrari(model,horsepower,engineDisplacement);
            }
            else if (type == "Williams")
            {
                car = new Williams(model, horsepower, engineDisplacement);
            }
            else
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidTypeCar, type));
            }

            if (carRepository.FindByName(model) != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.CarExistErrorMessage, model));
            }
            carRepository.Add(car);
            return string.Format(OutputMessages.SuccessfullyCreateCar, type, model);
        }

        public string CreateRace(string raceName, int numberOfLaps)
        {
            var existingRace = raceRepository.Models.FirstOrDefault(w => w.RaceName == raceName);

            if (existingRace != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceExistErrorMessage, raceName));
            }

            IRace race = new Race(raceName, numberOfLaps);
            raceRepository.Add(race);
            return string.Format(OutputMessages.SuccessfullyCreateRace, raceName);
        }

        public string AddCarToPilot(string pilotName, string carModel)
        {
            IPilot pilot = pilotRepository.Models.FirstOrDefault(w => w.FullName == pilotName);
            if (pilot == null || pilot.Car != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotDoesNotExistOrHasCarErrorMessage, pilotName));
            }
            IFormulaOneCar car = carRepository.FindByName(carModel);
            if (car == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.CarDoesNotExistErrorMessage, carModel));
            }

            pilot.AddCar(car);
            carRepository.Remove(car);

            return string.Format(OutputMessages.SuccessfullyPilotToCar, pilotName, car.GetType().Name, carModel);  
        }

        public string AddPilotToRace(string raceName, string pilotFullName)
        {
            IRace race= raceRepository.FindByName(raceName);
            if (race == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));
            }
            IPilot pilot = pilotRepository.FindByName(pilotFullName);
            if (pilot == null || pilot.CanRace == false || race.Pilots.Any(p=>p.FullName == pilotFullName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotDoesNotExistErrorMessage, pilotFullName));
            }
            race.Pilots.Add(pilot);
            return string.Format(OutputMessages.SuccessfullyAddPilotToRace,pilotFullName, raceName);
        }
        public string StartRace(string raceName)
        {
            var race = raceRepository.FindByName(raceName);
            if (race == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));
            }
            if (race.Pilots.Count < 3)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidRaceParticipants, raceName));
            }
            if (race.TookPlace == true)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceTookPlaceErrorMessage, raceName));
            }
            race.TookPlace = true;
            var pilot = race.Pilots.OrderByDescending(r=>r.Car.RaceScoreCalculator(race.NumberOfLaps)).Take(3).ToList();
            IPilot winner = pilot[0];
            winner.WinRace();
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Pilot {winner.FullName} wins the {raceName} race.");
            sb.AppendLine($"Pilot {pilot[1].FullName} is second in the {raceName} race.");
            sb.AppendLine($"Pilot {pilot[2].FullName} is third in the {raceName} race.");

            return sb.ToString().TrimEnd();

        }
        public string PilotReport()
        {
            var result = pilotRepository.Models.OrderByDescending(p => p.NumberOfWins);
            StringBuilder sb = new StringBuilder();
            foreach (var item in result)
            {
                sb.AppendLine(item.ToString());
            }
            return sb.ToString().TrimEnd();
        }

        public string RaceReport()
        {
            StringBuilder sb = new StringBuilder();
            var result = raceRepository.Models.Where(r => r.TookPlace == true);
            foreach (var item in result)
            {
                sb.AppendLine(item.RaceInfo());
            }
            return sb.ToString().TrimEnd();
        }

        
    }
}
