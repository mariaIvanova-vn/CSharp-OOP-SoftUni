﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WildFarm.Models.Food
{
    public abstract class Food : IFood
    {
        public Food(int quantity)
        {
            this.Quantity = quantity;
        }

        public int Quantity { get; }
    }
}
