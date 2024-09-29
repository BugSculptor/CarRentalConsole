using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalConsole.Cars.Model
{
    internal class Truck:Vehicle
    {
        public int LoadCapacity { get; set; }
        public override void Refuel(int count)
        {
            throw new NotImplementedException();
        }
    }
}
