﻿using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes.Contracts;
using Easter.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easter.Models.Bunnies
{
    public abstract class Bunny : IBunny
    {
        private string name;
        private int energy;
        private ICollection<IDye> dyes;

        public Bunny(string name, int energy)
        {
            this.Name = name;   
            this.Energy = energy;   
            dyes = new List<IDye>();
        }
        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidBunnyName);
                }
                name = value;
            }
        }

        public int Energy
        {
            get => energy;
            protected set
            {
                energy = value;
                if (energy < 0)
                {
                    energy = 0;
                }               
            }
        }

        public ICollection<IDye> Dyes => dyes;

        public void AddDye(IDye dye)
        {
            dyes.Add(dye);
        }

        public virtual void Work()    // abstract??????????????
        {
            this.Energy -= 10;
            if (this.Energy<0)
            {
                this.Energy = 0;
            }
        }
    }
}
