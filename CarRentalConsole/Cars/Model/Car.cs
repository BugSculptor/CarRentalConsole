using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalConsole.Cars.Model
{
    internal class Car : Vehicle
    {
        public override void Refuel(int count)
        {
            throw new NotImplementedException();
        }

        public void Borrow(string Borrower)  //wywoła sie metoda z vehicle
        {
            Console.WriteLine("Metoda borrow w CAR");
        }
    }
}
