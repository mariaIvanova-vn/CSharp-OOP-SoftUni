using NavalVessels.Core.Contracts;
using NavalVessels.Models;
using NavalVessels.Models.Contracts;
using NavalVessels.Repositories;
using NavalVessels.Utilities.Messages;
using System.Collections.Generic;
using System.Linq;

namespace NavalVessels.Core
{
    public class Controller : IController
    {
        private VesselRepository vessels;
        private readonly List<ICaptain> captains;

        public Controller()
        {
            vessels = new VesselRepository();
            captains = new List<ICaptain>();
        }

        public string HireCaptain(string fullName)
        {
            var existingCaptain = captains.FirstOrDefault(w => w.FullName == fullName);

            if (existingCaptain != null)
            {
                return string.Format(OutputMessages.CaptainIsAlreadyHired, fullName);
            }
            var captain = new Captain(fullName);
            captains.Add(captain);
            return string.Format(OutputMessages.SuccessfullyAddedCaptain, fullName);
        }
        public string ProduceVessel(string name, string vesselType, double mainWeaponCaliber, double speed)
        {
            IVessel vessel;

            if (vesselType == "Submarine")
            {
                vessel = new Submarine(name, mainWeaponCaliber, speed);
            }
            else if (vesselType == "Battleship")
            {
                vessel = new Battleship(name, mainWeaponCaliber, speed);
            }
            else
            {
                return string.Format(OutputMessages.InvalidVesselType);
            }

            if (vessels.FindByName(name) != null)
            {
                return string.Format(OutputMessages.VesselIsAlreadyManufactured, vesselType, name);
            }
            vessels.Add(vessel);
            return string.Format(OutputMessages.SuccessfullyCreateVessel, vesselType, name, mainWeaponCaliber, speed);
        }

        public string AssignCaptain(string selectedCaptainName, string selectedVesselName)
        {
            ICaptain captain = captains.FirstOrDefault(w => w.FullName == selectedCaptainName);
            if (captain == null)
            {
                return string.Format(OutputMessages.CaptainNotFound, selectedCaptainName);
            }
            IVessel vessel = vessels.FindByName(selectedVesselName);
            if (vessel == null)
            {
                return string.Format(OutputMessages.VesselNotFound, selectedVesselName);
            }
            if (vessel.Captain != null)
            {
                return string.Format(OutputMessages.VesselOccupied, selectedVesselName);
            }

            captains.Add(captain);
            vessel.Captain = captain;
            captain.Vessels.Add(vessel);

            return string.Format(OutputMessages.SuccessfullyAssignCaptain, selectedCaptainName, selectedVesselName);
        }
        public string CaptainReport(string captainFullName) //Searches for an assigned captain with a given name
                                                            //and returns the ICaptain.Report() method result.
        {
            var captain = captains.FirstOrDefault(w => w.FullName == captainFullName);
            return captain.Report();
        }
        public string VesselReport(string vesselName)
        {
            var vessel = vessels.FindByName(vesselName);
            return vessel.ToString();
        }

        public string ToggleSpecialMode(string vesselName)
        {
            var vessel = vessels.FindByName(vesselName);

            if (vessel == null)
            {
                return string.Format(OutputMessages.VesselNotFound, vesselName);
            }
            if (vessel.GetType().Name == nameof(Submarine))
            {
                Submarine submarine = (Submarine)vessel;
                submarine.ToggleSubmergeMode();

                return string.Format(OutputMessages.ToggleSubmarineSubmergeMode, vesselName);
            }
            else
            {
                var battleship = (Battleship)vessel;
                battleship.ToggleSonarMode();

                return string.Format(OutputMessages.ToggleBattleshipSonarMode, vesselName);
            }
        }

        public string ServiceVessel(string vesselName)
        {
            var vessel = vessels.FindByName(vesselName);
            vessel.RepairVessel();
            if (vessel == null)
            {
                return string.Format(OutputMessages.VesselNotFound, vesselName);
            }
            return string.Format(OutputMessages.SuccessfullyRepairVessel, vesselName);
        }

        public string AttackVessels(string attackingVesselName, string defendingVesselName)
        {
            var attackerVessel = vessels.FindByName(attackingVesselName);
            var defendingVessel = vessels.FindByName(defendingVesselName);

            if (attackerVessel == null || defendingVessel == null)
            {
                return string.Format(OutputMessages.VesselNotFound, attackingVesselName);
            }
            if (attackerVessel.ArmorThickness == 0 || defendingVessel.ArmorThickness == 0)
            {
                return string.Format(OutputMessages.AttackVesselArmorThicknessZero, attackingVesselName);
            }
            attackerVessel.Attack(defendingVessel);

            return string.Format(OutputMessages.SuccessfullyAttackVessel, defendingVesselName, attackingVesselName, defendingVessel);
        }
    }
}
