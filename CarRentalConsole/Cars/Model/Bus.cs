using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalConsole.Cars.Model
{
    internal class Bus : Vehicle
    {
        public int SeatsCount { get; set; }
        public override void Refuel(int count)
        {
            throw new NotImplementedException();
        }
    }
}
