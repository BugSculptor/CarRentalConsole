using CarRentalConsole.Cars.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CarRentalConsole.Cars.Repositories
{
    internal class VehicleRepository
    {
        private static List<Vehicle> data = [];
        

        public VehicleRepository()
        {
            //TODO: Przerobic na pobiernaie danych z pliku, wrzucić do listy?
        }

        public List<Vehicle> GetAll()
        {

            string readJsonString = File.ReadAllText("VehicleList.json");
            Vehicle? vehicle = JsonSerializer.Deserialize<Vehicle>(readJsonString);
            Vehicle loadedVehicle = vehicle;

          
            if (data == null)
                data = [];
            return data;
        }
        public Vehicle GetVehicle(int id)
        {
            return data.FirstOrDefault(x => x.Id == id); //wyrażenie 'lambda'
        }

        public void Add(Vehicle vehicle)
        {
            int id = 0;
            if (data.Any())
                id = data.OrderByDescending(x => x.Id).First().Id;
            vehicle.Id = id + 1;
            data.Add(vehicle);

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
