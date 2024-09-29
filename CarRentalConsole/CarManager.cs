//using ComarchBootcamp1.App.Cars;



using CarRentalConsole.Cars.Model;
using CarRentalConsole.Cars.Repositories;
using ConsoleTables;
using System.Diagnostics;
using System.Drawing;
using System.Xml.Linq;

using System.Text.Json;
using System;

namespace CarRentalConsole
{
    internal class CarManager
    {
        public static void Main(string[] args)
        {
            int choice;
            do
            {
                Console.Clear();
                GenerateBanner();
                ShowMenu();

                Console.Write("Wybierz jedną z opcji: ");

                if (int.TryParse(Console.ReadLine(), out choice))
                    switch (choice)
                    {
                        case 1:
                            ShowCars();
                            break;
                        case 2:
                            AddNewCar();
                            break;
                        case 3:
                            DeleteCar();
                            break;
                        case 4:
                            BorrowCar();
                            break;
                        default:
                            break;
                    }


            }
            while (choice != 0);

            



        }

        private static void BorrowCar() //wypozyczenie
        {
            throw new NotImplementedException();
        }

        private static void DeleteCar() //usunięcie
        {
            throw new NotImplementedException();
        }

        private static void AddNewCar() //dodawanie
        {
            Console.WriteLine("Dostępne typy pojazdu: ");
            Console.WriteLine("Podaj typ: ");
            foreach (var item in Enum.GetValues(typeof(CarTypes)))
                Console.WriteLine($"\t[{(short)item}] {item}");

            int carTypeUser = int.Parse(Console.ReadLine());
            
            CarTypes carType = (CarTypes)carTypeUser;
            
            Console.WriteLine($"Wybrałes: {carType}");

            Vehicle? vehicle = null; //bo jest dafaul i ona nie zwraca...
            switch (carType)
            {
                case CarTypes.Car:
                    vehicle = new Car();
                    break;
                case CarTypes.Bus:
                    vehicle = new Bus();
                    break;
                case CarTypes.Truck:
                    vehicle = new Truck();
                    break;
                default:
                    Console.WriteLine("Nieprawidłowa opcja!");
                    break;
            }

            if (vehicle == null) return; //zabezpieczenie bo jest jeszcze default i opcja null

            Console.Write("Podaj markę: ");
            vehicle.Maker = Console.ReadLine();

            Console.Write("Podaj model: ");
            vehicle.Model = Console.ReadLine();

            Console.Write("Podaj rodzaj paliwa: ");
            vehicle.GasType = Console.ReadLine();

            Console.Write("Podaj pojemność silnika: ");
            vehicle.Capacity = int.Parse(Console.ReadLine());

            var repository = new VehicleRepository();
            //repository.Add(vehicle);


            string fileName = "VehicleList.json"; //nazwa pliku
            string filePath = fileName; // Ścieżka do pliku


            if (!IsVehicleExists(filePath, vehicle))
            {
                
                string jsonString = JsonSerializer.Serialize(vehicle); // Serializacja do JSON
                File.AppendAllText(filePath, jsonString + Environment.NewLine); // Dopisywanie do pliku
                Console.WriteLine("Dane dodane do pliku.");
            }
            else
            {
                Console.WriteLine("zapis już istnieje w pliku.");
            }


        }

        private static bool IsVehicleExists(string filePath, Vehicle vehicle)
        {
            if (!File.Exists(filePath))
                return false;

            string[] lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue; //pomijam puste

                try
                {
                    //var existingVehicle = JsonSerializer.Deserialize<Vehicle>(line);
                    var existingVehicle = JsonSerializer.Deserialize<Vehicle>(line);

                    if (existingVehicle != null &&
                        existingVehicle.Maker == vehicle.Maker &&
                        existingVehicle.Model == vehicle.Model &&
                        existingVehicle.GasType == vehicle.GasType &&
                        existingVehicle.Capacity == vehicle.Capacity)
                        return true;
                }
                catch (JsonException)
                {
                    Console.WriteLine("Błąd deserializacji linii: " + line);
                }
            }
            return false;
        }

        private static void ShowCars() //pokaż
        {
            VehicleRepository repository = new VehicleRepository(); //zapis TESTOWY!
            var carList = repository.GetAll();

            ConsoleTable
                .From(carList)
                .Write(Format.Default);
            Console.ReadKey();


        }

        private static void ShowMenu()
        {
            List<string> options = new List<string> {
                "[1] Lista pojazdów",
                "[2] Dodaj pojazd",
                "[3] Usuń pojazd",
                "[4] Wypożycz",
                "[0] Zakończ"};

            foreach (string op in options)
                Console.WriteLine(string.Concat("\t", op));
            Console.WriteLine(sepa);

        }

        public static string sepa = "==================================================";
        private static void GenerateBanner()
        {
            Console.ForegroundColor = ConsoleColor.Green; // Ustaw kolor konsoli

            string[] carDrawing = new string[]
            {
            sepa,
            "                                       CAR SHARING",
            "     *********",
            "    **   |    \\",
            "   ******************",
            "   **(  )******(  )**",
            "                                              v1.0",
            sepa
            };

            foreach (string car in carDrawing)
                Console.WriteLine(car);
            Console.ForegroundColor = ConsoleColor.White;
        
        }
    }
}
