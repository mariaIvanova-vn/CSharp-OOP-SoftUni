﻿using Easter.Models.Bunnies.Contracts;
using Easter.Models.Eggs.Contracts;
using Easter.Models.Workshops.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Easter.Models.Workshops
{
    public class Workshop : IWorkshop
    {
        public Workshop()
        {
        }
        public void Color(IEgg egg, IBunny bunny)
        {
            // foreach (var currBunny in astronauts.Where(a => a.CanBreath == true))
            //var bunnyEnergy = bunny.Energy;
            //var bunnyDye = bunny.Dyes;

            //while (egg != null || bunnyEnergy > 0 || bunnyDye != null)
            //{
            //    bunny.Work();
            //    egg.GetColored();
            //}

            foreach (var dye in bunny.Dyes.Where(d => d.IsFinished() == false))
            {
                while (dye.IsFinished() == false)
                {
                    bunny.Work();
                    dye.Use();
                    egg.GetColored();

                    if (egg.IsDone())
                    {
                        break;
                    }
                }

                if (egg.IsDone())
                {
                    break;
                }
                if (bunny.Energy < 0)
                {
                    break;
                }

                if (bunny.Dyes.All(d => d.IsFinished() == true))
                {
                    break;
                }
            }
        }
    }
}
