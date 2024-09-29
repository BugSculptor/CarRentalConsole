using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalConsole.Cars.Model
{
    internal abstract class Vehicle
    {
        public int Id { get; set; }
        public string Maker { get; set; }
        public string Model { get; set; }
        public string GasType { get; set; }
        public int Capacity { get; set; }

        public abstract void Refuel(int count); //zatankuj; abstract wymusza uzycie metody

        public void Borrow(string Borrower)
        {
            //Console.WriteLine("Metoda borrow w VEHICLE");
            //logika wypozyczenia
        }
    }
}
