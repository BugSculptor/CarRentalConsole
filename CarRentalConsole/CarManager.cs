//using ComarchBootcamp1.App.Cars;



using CarRentalConsole.Cars.Model;
using CarRentalConsole.Cars.Repositories;
using ConsoleTables;
using System.Diagnostics;
using System.Drawing;
using System.Xml.Linq;

using System.Text.Json;
using System;
using System.Globalization;

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
                        case 5:
                            EditCar();
                            break;

                        default:
                            break;
                    }
            }
            while (choice != 0);
        }

        private static void EditCar()
        {

            //1. podaj właściwości
            Console.WriteLine("Wprowadż właściwości pojazu oddzielając je średnikiem: \nprzykład: Fiat;Tipo;benzyna;1");
            string line = Console.ReadLine();
            string[] vehicleToSearch = line.Split(";");
            if(vehicleToSearch.Length != 4)
            {
                Console.WriteLine("Podano nieprawidlowe wartości!");
            }
            else
            {
                                                        //2. wyszukaj ID
                int idVehicle = SearcherVehicleId("VehicleList.txt", vehicleToSearch);
                Console.WriteLine($"ID pasującego rekordu: {idVehicle}");

                //3. edytuj wg ID
                DeleteCar("VehicleList.txt", string.Concat(idVehicle.ToString(),";", line));
                OverrideVehicle("VehicleList.txt", idVehicle);
            }
            
           

        }

        private static void OverrideVehicle(string file, int id_deletedVehicle)
        {
            AddNewCar(id_deletedVehicle);

           


            //dodanie na końcu i sortowanie?

            throw new NotImplementedException();
        }

        private static void BorrowCar() //wypozyczenie
        {
            throw new NotImplementedException();
        }

        private static void DeleteCar(string file = "", string value = "") //usunięcie
        {
            string fileName = file; // nazwa pliku
            string textToRemove = value; // tekst do usunięcia

            // Odczytanie wszystkich linii z pliku
            string[] lines = File.ReadAllLines(fileName);
            // Filtrowanie linii, które nie zawierają tekstu do usunięcia
            var updatedLines = lines.Where(line => !line.Contains(textToRemove)).ToArray();
            // Zapisanie zaktualizowanych linii do pliku (nadpisanie pliku)
            File.WriteAllLines(fileName, updatedLines);
            Console.WriteLine($"Linia zawierająca tekst '{textToRemove}' została usunięta.");
        }

        private static void AddNewCar(int iD = 0) //dodawanie
        {
            Console.WriteLine("Dostępne typy pojazdu: ");
            Console.WriteLine("Podaj typ: ");
            foreach (var item in Enum.GetValues(typeof(CarTypes)))
                Console.WriteLine($"\t[{(short)item}] {item}");

            int carTypeUser = int.Parse(Console.ReadLine());
            
            CarTypes carType = (CarTypes)carTypeUser;
            
            //Console.WriteLine($"Wybrałes: {carType}");

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

            string fileName = "VehicleList.txt"; //nazwa pliku
            string filePath = fileName; // Ścieżka do pliku

            var repository = new VehicleRepository();
            repository.Add(vehicle, filePath, iD);


            


            if (!IsVehicleExists(filePath, vehicle))
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine($"{vehicle.Id};{vehicle.Maker};{vehicle.Model};{vehicle.GasType};{vehicle.Capacity}");
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Pojazd został dodany.");
                Console.ForegroundColor = default;
                Console.ReadKey();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Pojazd już istnieje w bazie.");
                Console.ForegroundColor = default;
                Console.ReadKey();
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
                    string[] linesVehicle = File.ReadAllLines(filePath);
                    foreach (string l in linesVehicle)
                    {
                        string[] vehicleData = l.Split(';');
                        if (vehicleData.Length == 5
                            //&& int.Parse(vehicleData[0]) == vehicle.Id
                            && vehicleData[1] == vehicle.Maker
                            && vehicleData[2] == vehicle.Model
                            && vehicleData[3] == vehicle.GasType
                            && int.Parse(vehicleData[4]) == vehicle.Capacity
                            )
                            return true;
                    }
                    return false;
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Błąd " + ex.Message);
                }
            }
            return false;
        }

        private static int SearcherVehicleId(string filePath, string[] data)
        {
            string[] lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue; //pomijam puste

                try
                {
                    string[] linesVehicle = File.ReadAllLines(filePath);
                    foreach (string l in linesVehicle)
                    {
                        string[] vehicleData = l.Split(';');
                        if (vehicleData.Length == 5
                            && vehicleData[1] == data[0]
                            && vehicleData[2] == data[1]
                            && vehicleData[3] == data[2]
                            && vehicleData[4] == data[3]
                            )
                        {
                            return int.Parse(vehicleData[0]);
                        }
                    }
                    return 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Błąd - SearcherVehicleId: " + ex.Message);
                }
                
            }
            return 0;
        }

        private static void ShowCars() //pokaż
        {
            VehicleRepository repository = new VehicleRepository(); //zapis TESTOWY!

            //testy
            





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
                "[5] Edytuj pojazd",
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
