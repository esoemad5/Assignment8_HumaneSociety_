using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public static class Query
    {
        private static HumaneSocietyDataContext db = new HumaneSocietyDataContext();

        internal static void RunEmployeeQueries(Employee employee, string v)
        {
            throw new NotImplementedException();
        }

        internal static Client GetClient(string userName, string password)
        {
            Client outputClient = db.Clients.
                Where(u => u.UserName == userName && u.Password == password).
                /*Could put .AsEnumerable() here. Doesn't seem neccesary. Might not be the case elsewhere*/
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
            Animal adoptee = db.Animals.Where(a => a.AnimalId == animal.AnimalId).Single();
            adoptee.AdoptionStatus = "Pending";
            Client adoptor = db.Clients.Where(c => c.ClientId == client.ClientId).Single();
            Adoption adoption = new Adoption();
            adoption.Client = adoptor;
            adoption.Animal = adoptee;
            adoption.ApprovalStatus = "Pending";

            //These lines will need to be changed in the future
            adoption.AdoptionFee = 75;
            adoption.PaymentCollected = true;

            db.Adoptions.InsertOnSubmit(adoption);
            try
            {
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        internal static IQueryable<Animal> SearchForAnimalByMultipleTraits() // Simply lists animals right now
        {
            return db.Animals.Where(x => true);
        }

        internal static IQueryable<Client> RetrieveClients()
        {
            return db.Clients;
        }

        internal static IQueryable<USState> GetStates()
        {
            return db.USStates;
        }

        internal static void AddNewClient(string firstName, string lastName, string username, string password, string email, string streetAddress, int zipCode, int state)
        {
            Console.WriteLine("In AddNewClient");
            // TODO make this into 2 methods
            Address address = new Address();
            address.AddressLine1 = streetAddress;
            address.Zipcode = zipCode;
            address.USStateId = state;
            db.Addresses.InsertOnSubmit(address);
            try
            {
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Client client = new Client();
            client.FirstName = firstName;
            client.LastName = lastName;
            client.UserName = username;
            client.Password = password;
            client.Email = email;
            client.AddressId = address.AddressId;
            db.Clients.InsertOnSubmit(client);
            try
            {
                db.SubmitChanges();
                Console.WriteLine(client);
                Console.WriteLine(db.Clients.Select(x => x.FirstName == client.FirstName));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        internal static void updateClient(Client client) // Why not updateIncome/NumberOfKids/HomeSquareFootage????
        {
            throw new NotImplementedException();
        }

        internal static void UpdateUsername(Client client)
        {
            db.Clients.Where(c => c.ClientId == client.ClientId).Single().UserName = client.UserName;
            try
            {
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        internal static void UpdateEmail(Client client)
        {
            db.Clients.Where(c => c.ClientId == client.ClientId).Single().Email = client.Email;
            try
            {
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        internal static void UpdateAddress(Client client)
        {
            throw new NotImplementedException();
        }

        internal static IQueryable<Adoption> GetPendingAdoptions()
        {
            throw new NotImplementedException();
        }

        internal static void UpdateFirstName(Client client)
        {
            db.Clients.Where(c => c.ClientId == client.ClientId).Single().FirstName = client.FirstName;
            try
            {
                db.SubmitChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        internal static void UpdateLastName(Client client)
        {
            db.Clients.Where(c => c.ClientId == client.ClientId).Single().LastName = client.LastName;
            try
            {
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        internal static void RemoveAnimal(Animal animal)
        {
            db.Animals.DeleteOnSubmit(animal);
            db.SubmitChanges();
            // null checking
        }

        internal static Species GetSpecies()
        {
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
                if (Int32.Parse(input) >= allSpecies.Count)
                {
                    return CreateSpecies();
                }
                else
                {
                    newSpecies = allSpecies[Int32.Parse(input)];
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
            db.SubmitChanges();
            return newSpecies;            
        }

        internal static DietPlan GetDietPlan()
        {
            throw new NotImplementedException();
        }

        internal static void AddAnimal(Animal animal)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        internal static Room GetRoom(int animalId)
        {
            return db.Rooms.Where(r => r.AnimalId == animalId).Single();
        }

        internal static void AddUsernameAndPassword(Employee employee)
        {
            RetrieveEmployeeUser(employee.Email, employee.EmployeeNumber).UserName = employee.UserName;
            RetrieveEmployeeUser(employee.Email, employee.EmployeeNumber).Password = employee.Password;
            // Null Check
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
            throw new NotImplementedException();
        }

        internal static void EnterUpdate(Animal animal, Dictionary<int, string> updates)
        {
            throw new NotImplementedException();
        }
    }
}
