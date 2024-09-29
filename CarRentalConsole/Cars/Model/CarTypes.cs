using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalConsole.Cars.Model;

internal enum CarTypes : short
{
    [Description("Samochody - opis")]
    Car = 1,
    [Description("Autobusy - opis")]
    Bus,
    [Description("Ciężarówki - opis")]
    Truck,
}