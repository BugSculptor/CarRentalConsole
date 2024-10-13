using CarRentalConsole.Cars.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalConsole.Cars.Repositories
{
    internal class VehicleRepository
    {
        private static List<Vehicle> data = [];
        private string nameOfFile = "VehicleList.txt"; //nazwa pliku

        public VehicleRepository()
        {
            //TODO: Przerobic na pobiernaie danych z pliku, wrzucić do listy?
            //1.Sprawdzic czy istnieje
            Console.WriteLine($"Czy plik istnieje: { File.Exists(nameOfFile)}");
            //File.Exists(nameOfFile);



        }

        public List<Vehicle> GetAll()
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            string[] lines = File.ReadAllLines(nameOfFile);

            foreach (string line in lines)
            {
                // Rozdzielenie danych w linii na podstawie średnika
                string[] vehicleData = line.Split(';');

                // Upewnienie się, że dane są poprawne (powinny być 4 elementy)
                if (vehicleData.Length == 5)
                {
                    Vehicle vehicle = new Car //TODO: pojazd jest klasa abstrakcyjną?
                    {
                        Id = int.Parse(vehicleData[0]),
                        Maker = vehicleData[1],
                        Model = vehicleData[2],
                        GasType = vehicleData[3],
                        Capacity = int.Parse(vehicleData[4]) // Konwersja pojemności na liczbę całkowitą
                    };

                    vehicles.Add(vehicle); // Dodanie pojazdu do listy
                }
            }
            return vehicles; //lista wszytskich pojazdów
        }
        public Vehicle GetVehicle(int id)
        {
            return data.FirstOrDefault(x => x.Id == id); //wyrażenie 'lambda'
        }

        public void Add(Vehicle vehicle, string nameOfFile, int iD = 0)
        {
            int id = iD;
            string[] lines = File.ReadAllLines(nameOfFile);
            if (lines.Length > 0)
            {
                string[] lastLine = lines[lines.Length - 1].Split(';'); // Pobranie ostatniej linii
                id = int.Parse(lastLine[0]);
            }

            //id = data.OrderByDescending(x => x.Id).First().Id;
            vehicle.Id = iD == 0 ? iD : id + 1; //bo chce nadać przy akt

            data.Add(vehicle);

            Console.WriteLine("nadano kolejno ID: "+ vehicle.Id);

        }

        public void Update(Vehicle vehicle)
        {
            //TODO
        }

        public void Remove(int id)
        {
            Vehicle vehicle = GetVehicle(id); //musze pobrac zeby moc usunac;
            data.Remove(vehicle);
        }


    }
}
