using System;
using System.Collections.Generic;
using System.Text;

namespace Cars
{
    public interface IElectricCar
    {
        string Model { get; }
        string Color { get; }
        int Battery { get; }
    }
}
