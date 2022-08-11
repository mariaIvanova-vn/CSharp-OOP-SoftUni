using NavalVessels.Models.Contracts;
using NavalVessels.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavalVessels.Models
{
    public class Captain : ICaptain
    {
        private string fullName;
        private int combatExperience;
        private List<IVessel> vesselList;


        public Captain(string fullName)
        {
            this.FullName = fullName;
            vesselList = new List<IVessel>();
        }
        public string FullName
        {
            get => fullName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(string.Format(ExceptionMessages.InvalidCaptainName, value));
                }
                fullName = value;
            }
        }

        public int CombatExperience
        {
            get => combatExperience;
            private set
            {
                if (value>10)
                {
                    combatExperience = 10;
                }
                combatExperience = 0;
            }
        }

        public ICollection<IVessel> Vessels => vesselList;

        public void AddVessel(IVessel vessel)
        {
            if (vessel == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.InvalidVesselForCaptain, vessel));
            }
            vesselList.Add(vessel); 
        }

        public void IncreaseCombatExperience()
        {
            this.combatExperience += 10;
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{FullName} has {CombatExperience} combat experience and commands {vesselList.Count} vessels.");

            if (this.vesselList.Count>0)
            {
                foreach (var item in vesselList)
                {
                    sb.AppendLine(base.ToString());
                }
            }

            return sb.ToString().Trim();
        }
    }
}
