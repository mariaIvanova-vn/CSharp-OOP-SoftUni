using System;
using System.Collections.Generic;
using System.Text;

namespace NavalVessels.Models
{
    internal class Submarine : Vessel
    {
        private bool submergeMode = false;
        private const double InitialArmorThickness = 200;   //Has 200 initial armor thickness.

        public Submarine(string name, double mainWeaponCaliber, double speed)
            : base(name, mainWeaponCaliber, speed, InitialArmorThickness)
        {
        }

        public bool SubmergeMode => submergeMode;

        public void ToggleSubmergeMode()
        {
            if (SubmergeMode == false)
            {
                submergeMode = true;
                this.MainWeaponCaliber += 40;
                this.Speed -= 4;
            }
            if (SubmergeMode == true)
            {
                submergeMode = false;
                this.MainWeaponCaliber -= 40;
                this.Speed += 4;
            }
        }
        public override void RepairVessel()
        {
            this.ArmorThickness = InitialArmorThickness;
        }

        public override string ToString()   
        {
            StringBuilder sb = new StringBuilder();
            string sonar = submergeMode == true
                ? "ON"
                : "OFF";

            sb.AppendLine(base.ToString());
            sb.AppendLine($" *Submerge  mode: {sonar}");

            return sb.ToString().TrimEnd();
        }
    }
}
