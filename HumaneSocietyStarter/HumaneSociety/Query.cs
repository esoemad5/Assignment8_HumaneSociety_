using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HumaneSociety
{
    // Admin
    public static partial class Query
    {
        private static HumaneSocietyDataContext db = new HumaneSocietyDataContext();

        internal static void CreateEmployee(Employee employee)
        {
            db.Employees.InsertOnSubmit(employee);
            TryToSubmitChanges();
        }

        internal static void ReadEmployee(int? employeeId)
        {
            Employee employee = db.Employees.Where(e => e.EmployeeNumber == employeeId).Single();
            UserInterface.DisplayEmployee(employee);
            UserInterface.DisplayUserOptions("Press any key to continue.");
            Console.ReadKey();
        }
        
        internal static void UpdateEmployee(int employeeNumber)
        {
            Employee target = db.Employees.Where(e => e.EmployeeNumber == employeeNumber).Single();
            UserInterface.DisplayEmployee(target);
            UserInterface.DisplayUserOptions("Which field would you like to change?");
            UserInterface.UpdateEmployee_MenuSelection(target);
        }

        public static void UpdateEmployee_FirstName(string newFirstName, Employee employee)
        {
            employee.FirstName = newFirstName;
            TryToSubmitChanges();
        }
        public static void UpdateEmployee_LastName(string newLastName, Employee employee)
        {
            employee.LastName = newLastName;
            TryToSubmitChanges();
        }
        public static void UpdateEmployee_UserName(string newUserName, Employee employee)
        {
            employee.UserName = newUserName;
            TryToSubmitChanges();
        }
        public static void UpdateEmployee_Password(string newPassword, Employee employee)
        {
            employee.Password = newPassword;
            TryToSubmitChanges();
        }
        public static void UpdateEmployee_EmployeeNumber(int newEmployeeNumber, Employee employee)
        {
            employee.EmployeeNumber = newEmployeeNumber;
            TryToSubmitChanges();
        }
        public static void UpdateEmployee_Email(string newEmail, Employee employee)
        {
            employee.Email = newEmail;
            TryToSubmitChanges();
        }

        internal static void DeleteEmployee(Employee employee)
        {
            Employee target = db.Employees.Where(e => e.EmployeeNumber == employee.EmployeeNumber && e.LastName == employee.LastName).Single();
            Console.WriteLine("About to delete {0} {1}, EmployeeNumber: {2}. Are you sure?(y/n)", target.FirstName, target.LastName, target.EmployeeNumber);
            if (Console.ReadKey().KeyChar == 'y')
            {
                RemoveEmployeeFromAnimal(target);
                db.Employees.DeleteOnSubmit(target);
                TryToSubmitChanges();
                Console.WriteLine("Employee deleted.");
            }
            else
            {
                Console.WriteLine("Delete aborted.");
            }
            Console.WriteLine("Press any key to return to the previous menu.");
            Console.ReadKey(true);
        }

        private static void RemoveEmployeeFromAnimal(Employee employee)
        {
            List<Animal> animalsAssignedToEmployee = db.Animals.Where(a => a.EmployeeId == employee.EmployeeId).ToList();
            foreach(Animal animal in animalsAssignedToEmployee)
            {
                animal.EmployeeId = null;
            }

            TryToSubmitChanges();
        }

        internal static bool EmployeeNumberIsAlreadyInUse(int? employeeNumber)
        {
            try
            {
                Employee test = db.Employees.Where(e => e.EmployeeNumber == employeeNumber).Single();
                UserInterface.DisplayUserOptions("Employee Number " + employeeNumber + " is already in use");
                UserInterface.DisplayUserOptions("Press any key to return to previous menu.");
                Console.ReadKey(true);
                return true;
            }
            catch (Exception)
            {
                UserInterface.DisplayUserOptions("Valid number");
                return false;
            }
        }
    }

    // Customer
    public static partial class Query
    {
        internal static Client GetClient(string userName, string password)
        {
            Client outputClient = db.Clients.
                Where(u => u.UserName == userName && u.Password == password).
                Single();
            return outputClient;
        }

        internal static IQueryable<Adoption> GetUserAdoptionStatus(Client client)
        {
            return db.Adoptions.Where(a => a.ClientId == client.ClientId);
        }

        internal static Animal GetAnimalByID(int iD)
        {
            return db.Animals.Where(x => x.AnimalId == iD).Single();
        }

        internal static void Adopt(Animal animal, Client client)
        {
            animal.AdoptionStatus = "Pending";
            Adoption adoption = new Adoption();
            adoption.Client = client;
            adoption.Animal = animal;
            adoption.ApprovalStatus = "Pending";
            //These lines will need to be changed in the future if Adoption Fees will vary or a method for collecting payment actualizes
            adoption.AdoptionFee = 75;
            adoption.PaymentCollected = true;

            db.Adoptions.InsertOnSubmit(adoption);
            TryToSubmitChanges();

        }

        internal static IQueryable<USState> GetStates()
        {
            return db.USStates;
        }

        internal static void AddNewClient(string firstName, string lastName, string username, string password, string email, string streetAddress, int zipCode, int state)
        {
            Address address = AddAddress(streetAddress, zipCode, state);

            Client client = new Client();
            client.FirstName = firstName;
            client.LastName = lastName;
            client.UserName = username;
            client.Password = password;
            client.Email = email;
            client.AddressId = address.AddressId;
            db.Clients.InsertOnSubmit(client);

            TryToSubmitChanges();
        }
        private static Address AddAddress(string streetAddress, int zipCode, int state)
        {
            Address address = new Address();
            address.AddressLine1 = streetAddress;
            address.Zipcode = zipCode;
            address.USStateId = state;

            db.Addresses.InsertOnSubmit(address);
            TryToSubmitChanges();
            return address;
        }

        internal static void UpdateClient(Client client)
        {
            TryToSubmitChanges();
        }

        internal static IQueryable<Client> RetrieveClients()
        {
            return db.Clients;
        }
    }

    // Customer and Employee (Searching)
    public static partial class Query
    {
        internal static IQueryable<Animal> SearchForAnimalByMultipleTraits()
        {
            return db.Animals.Where(x => true);
        }

        public static IQueryable<Animal> SearchAnimalsByTrait(IQueryable<Animal> animals)
        {
            UserInterface.DisplayUserOptions("Whate Attribute would you like to search by?");
            string input = Console.ReadLine();
            switch (input)
            {
                case "Name":
                    return SearchByName(UserInterface.GetStringData("name", "the"), animals);
                case "Species":
                    return SearchBySpecies(UserInterface.GetStringData("Species", "the"), animals);
                case "Weight":
                    return SearchByWeight(UserInterface.GetIntegerData("Weight", "the"), animals);
                case "Age":
                    return SearchByAge(UserInterface.GetIntegerData("Age", "the"), animals);
                case "Diet Plan":
                    return SearchByDietPlan(UserInterface.GetStringData("Diet Plan", "the"), animals);
                case "Kid Friendly":
                    return SearchByKidFriendly(UserInterface.GetStringData("Kid Firendly status", "the"), animals);
                case "Pet Friendly":
                    return SearchByPetFriendly(UserInterface.GetStringData("Pet Friendly status", "the"), animals);
                case "Gender":
                    return SearchByGender(UserInterface.GetStringData("Gender", "the"), animals);
                default:
                    Console.WriteLine("Not a vaild criterea.");
                    return SearchAnimalsByTrait(animals);
            }
        }

        private static IQueryable<Animal> SearchByName(string name, IQueryable<Animal> animals)
        {
            return animals.Where(a => a.Name == name);
        }

        private static IQueryable<Animal> SearchBySpecies(string species, IQueryable<Animal> animals)
        {
            return animals.Where(a => a.Species.Name == species);
        }
        private static IQueryable<Animal> SearchByWeight(int weight, IQueryable<Animal> animals)
        {
            return animals.Where(a => a.Weight == weight);
        }
        private static IQueryable<Animal> SearchByAge(int age, IQueryable<Animal> animals)
        {
            return animals.Where(a => a.Age == age);
        }
        private static IQueryable<Animal> SearchByDietPlan(string dietPlan, IQueryable<Animal> animals)
        {
            return animals.Where(a => a.DietPlan.Name == dietPlan);
        }
        private static IQueryable<Animal> SearchByKidFriendly(string kidFriendly, IQueryable<Animal> animals)
        {
            return animals.Where(a => a.KidFriendly.ToString() == kidFriendly);
        }
        private static IQueryable<Animal> SearchByPetFriendly(string petFriendly, IQueryable<Animal> animals)
        {
            return animals.Where(a => a.PetFriendly.ToString() == petFriendly);
        }
        private static IQueryable<Animal> SearchByGender(string Gender, IQueryable<Animal> animals)
        {
            return animals.Where(a => a.Gender == Gender);
        }

    }

    //Employee
    public static partial class Query
    {
        internal static IQueryable<Adoption> GetPendingAdoptions()
        {
            return db.Adoptions.Where(a => a.ApprovalStatus == "Pending");
        }

        internal static void RemoveAnimal(Animal animal)
        {
            db.Animals.DeleteOnSubmit(animal);
            TryToSubmitChanges();
            // null checking
        }

        internal static void RemoveSpecieslessAnimals()
        {
            var badAnimals = db.Animals.Where(a => a.SpeciesId == 9);
            foreach(Animal a in badAnimals)
            {
                RemoveAnimal(a);
            }
        }

        internal static Species GetSpecies()
        {
            //Employee target = db.Employees.Where(e => e.EmployeeNumber == employeeNumber).Single();
            //Above is a better solution (it uses more LINQ which is what the instructors want
            List<Species> allSpecies = new List<Species>();
            foreach (Species s in db.Species)
            {
                allSpecies.Add(s);
            }
            Species newSpecies;
            DisplaySpeciesOptions();
            string input = UserInterface.GetStringData("menu number", "species");

            try
            {
                if (Int32.Parse(input) - 1 >= allSpecies.Count)
                {
                    return CreateSpecies();
                }
                else
                {
                    newSpecies = allSpecies[Int32.Parse(input) - 1];
                    return newSpecies;
                }
            }
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("Incorrect input please try again! \n");
                GetSpecies();
            }

            return null;
        }

        internal static void DisplaySpeciesOptions()
        {
            int counter = 1;
            Console.WriteLine("Please select the animal's species: \n");

            foreach (Species s in db.Species)
            {
                Console.WriteLine(counter + "- " + s.Name + "\n");
                counter++;
            }
            Console.WriteLine((counter) + "- Species Not Listed \n");
        }

        internal static Species CreateSpecies()
        {
            Species newSpecies = new Species();
            newSpecies.Name = UserInterface.GetStringData("the species", "the name of");
            db.Species.InsertOnSubmit(newSpecies);
            TryToSubmitChanges();
            return newSpecies;
        }

        internal static DietPlan GetDietPlan()
        {
            DisplayDietPlans();
            string dietName = UserInterface.GetStringData("name", "desired diet plan");
            return db.DietPlans.Where(d => d.Name == dietName).Single();
        }

        internal static void DisplayDietPlans()
        {
            UserInterface.DisplayUserOptions("Available Diet Plans: ");
            foreach (DietPlan d in db.DietPlans.Where(p => true))
            {
                UserInterface.DisplayUserOptions("- " + d.Name);
            }
        }

        internal static void AddAnimal(Animal animal)
        {
            db.Animals.InsertOnSubmit(animal);
            TryToSubmitChanges();
        }

        internal static Employee EmployeeLogin(string userName, string password)
        {
            var Employee = db.Employees.Where(x => x.UserName == userName && x.Password == password);
            return Employee.Single();
        }

        internal static Employee RetrieveEmployeeUser(string email, int? employeeNumber)
        {
            var Employee = db.Employees.Where(e => e.Email == email && e.EmployeeNumber == employeeNumber);
            return Employee.Single();
            // Null Check
        }

        internal static void UpdateAdoption(bool v, Adoption adoption)
        {
            if (v)
            {
                adoption.ApprovalStatus = "Approved";
            }
            else
            {
                adoption.ApprovalStatus = "Denied";
            }

            TryToSubmitChanges();
        }

        internal static Room GetRoom(int animalId)
        {
            return db.Rooms.Where(r => r.AnimalId == animalId).Single();
        }

        internal static void AddUsernameAndPassword(Employee employee)
        {
            RetrieveEmployeeUser(employee.Email, employee.EmployeeNumber).UserName = employee.UserName;
            RetrieveEmployeeUser(employee.Email, employee.EmployeeNumber).Password = employee.Password;
            TryToSubmitChanges();
        }

        internal static bool CheckEmployeeUserNameExist(string username)
        {
            // Null Check
            var nameExists = db.Employees.Where(e => e.UserName == username);
            if (nameExists.Count() > 0)
            {
                return true;
            }
            return false;
        }

        internal static IQueryable<AnimalShot> GetShots(Animal animal)
        {
            var shots = db.AnimalShots.Where(a => a.AnimalId == animal.AnimalId);
            return shots;
        }

        internal static void UpdateShot(string v, Animal animal)
        {
            AnimalShot newShot = new AnimalShot();
            //newShot.AnimalId = animal.AnimalId;
            if (CheckShot(v))
            {
                newShot.Animal = db.Animals.Where(a => a.AnimalId == animal.AnimalId).Single();
                newShot.DateReceived = DateTime.Now;
                newShot.Shot = db.Shots.Where(s => s.Name == v).Single();
                db.AnimalShots.InsertOnSubmit(newShot);
                TryToSubmitChanges();
                UserInterface.DisplayUserOptions("Shots Updated!");
            }
            else
            {
                UserInterface.DisplayUserOptions("Shot does not exist!");
            }
        }

        internal static bool CheckShot(string shotName)
        {
            bool shotExists = false;
            foreach (Shot s in db.Shots)
            {
                if (s.Name == shotName)
                {
                    shotExists = true;
                    return shotExists;
                }
            }
            return shotExists;
        }

        internal static void EnterUpdate(Animal animal, Dictionary<int, string> updates)
        {
            foreach (KeyValuePair<int, string> entry in updates)
            {
                // "1. Species", "2. Name", "3. Age", "4. Demeanor", "5. Kid friendly", "6. Pet friendly", "7. Weight", "8. Finished" };                
                switch (entry.Key)
                {
                    case 1:
                        animal.Species = db.Species.Where(s => s.Name == entry.Value).Single();
                        break;
                    case 2:
                        animal.Name = entry.Value;
                        break;
                    case 3:
                        animal.Age = Int32.Parse(entry.Value);
                        break;
                    case 4:
                        animal.Demeanor = entry.Value;
                        break;
                    case 5:
                        if (entry.Value.ToLower() == "true")
                        {
                            animal.KidFriendly = true;
                        }
                        else
                        {
                            animal.KidFriendly = false;
                        }
                        break;
                    case 6:
                        if (entry.Value.ToLower() == "true")
                        {
                            animal.PetFriendly = true;
                        }
                        else
                        {
                            animal.PetFriendly = false;
                        }
                        break;
                    case 7:
                        animal.Weight = Int32.Parse(entry.Value);
                        break;
                    default:
                        break;
                }
            }
            TryToSubmitChanges();
        }        

        internal static void ChangeAnimalRoom(Animal animal)
        {
            try
            {
                db.Rooms.Where(r => r.Animal == animal).Single().AnimalId = null;
            }
            catch (Exception)
            {

            }
            DisplayAvailableRooms();
            int newRoomNumber = UserInterface.GetIntegerData("room number", "the new");
            db.Rooms.Where(r => r.RoomNumber == newRoomNumber).Single().AnimalId = animal.AnimalId;
            TryToSubmitChanges();
        }

        internal static void ClearRoom(int roomNumber)
        {
            db.Rooms.Where(r => r.RoomNumber == roomNumber).Single().AnimalId = null;
            TryToSubmitChanges();
        }

        internal static bool RoomsAvailable()
        {
            bool roomsAvailable = false;
            var availableRooms = db.Rooms.Where(r => r.AnimalId == null);
            if (availableRooms.Count() > 0)
            {
                roomsAvailable = true;
            }

            return roomsAvailable;
        }

        internal static void DisplayAvailableRooms()
        {
            var rooms = db.Rooms.Where(r => true);
            UserInterface.DisplayUserOptions("Rooms Available: ");
            foreach (Room r in rooms)
            {
                if (r.AnimalId == 0 || r.AnimalId == null)
                {
                    UserInterface.DisplayUserOptions(r.RoomNumber + ", ");
                }
            }
            Console.WriteLine("\n");
        }
    }

    // CSV and db commit
    public static partial class Query
    {
        internal static void GetAnimalsByCSV(string filename)
        {
            // EXTRA FIELD IN CSV???
            // defaulting to going looking inside /bin/debug when just given file name with no path
            
            //string[] allLines = File.ReadAllLines(filename);
            //var query = from l in allLines
            //            let newFile = l.Split(',').Select(e => e.First);
            var filteredResults = File.ReadAllLines(filename)
            .Select(l => l.Split(',').Select(s => s.Trim()).ToArray())
            .Where(a => true);

            // Magic numbers?
            foreach (string[] x in filteredResults)
            {
                Animal newAnimal = new Animal();
                if (!RoomsAvailable())
                {
                    Console.Clear();
                    UserInterface.DisplayUserOptions("No rooms available for more animals.");                    
                    return;
                }

                newAnimal.Name = x[1];
                newAnimal.Weight = Int32.Parse(x[3]);
                newAnimal.Age = Int32.Parse(x[4]);
                newAnimal.Demeanor = x[7];
                newAnimal.KidFriendly = StringValueToBool(x[8]);
                newAnimal.PetFriendly = StringValueToBool(x[9]);
                newAnimal.AdoptionStatus = x[11];
                newAnimal.Species = db.Species.Where(s => s.Name == "Species Not Entered").Single();
                AddAnimal(newAnimal); // animal needs employee?
                ChangeAnimalRoom(newAnimal);
                newAnimal = null; // reset values IS this needed?                
            }
        }

        private static bool StringValueToBool(string numberString)
        {
            if (numberString == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static void TryToSubmitChanges()
        {
            try
            {
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
