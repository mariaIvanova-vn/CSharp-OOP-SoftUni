using System;
using System.Collections.Generic;
using System.Text;

namespace NavalVessels.Models
{
    public class Battleship : Vessel
    {
        private bool sonarMode = false;
        private const double InitialArmorThickness = 300;   //Has 300 initial armor thickness.

        public Battleship(string name, double mainWeaponCaliber, double speed) 
            : base(name, mainWeaponCaliber, speed, InitialArmorThickness)
        {
        }

        public bool SonarMode => sonarMode;

        public void ToggleSonarMode()        //Flips SonarMode (false -> true or true -> false). 
        {
            if (SonarMode == false)
            {
                sonarMode = true;
                this.MainWeaponCaliber += 40;
                this.Speed -= 5;
            }
            if (SonarMode == true)
            {
                sonarMode = false;
                this.MainWeaponCaliber -= 40;
                this.Speed += 5;
            }           
        }

        public override void RepairVessel()  //If the battleship was attacked (its initial armor thickness is less than 300),
                                             //set the battleship’s armor thickness back to the initial one.
        {
            this.ArmorThickness = InitialArmorThickness;
        }

        public override string ToString()   //Returns the same info as the Vessel class, but at the end depending on the
                                            //SonarMode mode writes the message on a new line:  " *Sonar mode: {ON/OFF}"
        {
            StringBuilder sb = new StringBuilder();
            string sonar = sonarMode == true
                ? "ON"
                : "OFF";

            sb.AppendLine(base.ToString());
            sb.AppendLine($" *Sonar mode: {sonar}");

            return sb.ToString().TrimEnd();
        }
    }
}
