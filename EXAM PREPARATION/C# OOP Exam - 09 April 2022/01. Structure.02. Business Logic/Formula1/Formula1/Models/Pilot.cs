using Formula1.Models.Contracts;
using Formula1.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formula1.Models
{
    public class Pilot : IPilot
    {
        private string fullName;
        private bool canRace = false;
        private IFormulaOneCar car;

        public Pilot(string fullName)
        {
            this.FullName = fullName;   
        }
        public string FullName
        {
            get => fullName;
            private set
            {
                if ((string.IsNullOrWhiteSpace(value)) || value.Length < 5)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidPilot, value));
                }
                fullName = value;
            }
        }

        public IFormulaOneCar Car
        {
            get => car;
            private set
            {
                if (value == null)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidCarForPilot));
                }
                car = value;
            }
        }

        public int NumberOfWins { get; private set; }

        public bool CanRace
        {
            get => canRace;
            private set
            {
                canRace = value;
            }
        }
        

        public void AddCar(IFormulaOneCar car)
        {
            //Sets a car to the pilot, and set CanRace to true.
            this.Car = car;
            this.CanRace = true;
        }

        public void WinRace()
        {
            this.NumberOfWins++;
        }


      // string ToString()
       //Returns a string with information about the number of wins for the pilot.The returned string must be in the following format:
       //"Pilot { full name } has { number of wins } wins."

        public override string ToString()
        {
            return $"Pilot {this.FullName} has {this.NumberOfWins} wins.";
        }
    }
}
