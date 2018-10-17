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
            throw new NotImplementedException();
        }

        internal static Animal GetAnimalByID(int iD)
        {
            throw new NotImplementedException();
        }

        internal static void Adopt(Animal animal, Client client)
        {
            throw new NotImplementedException();
        }

        internal static IQueryable<Animal> SearchForAnimalByMultipleTraits()
        {
            throw new NotImplementedException();
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
            int counter = 1;
            Console.WriteLine("Please select the animal's species: \n");

            foreach (Species s in db.Species)
            {
                Console.WriteLine(counter + "- " + s.Name + "\n");
                counter++;
            }
            Console.WriteLine((counter+1) + "- Species Not Listed");
            string input = UserInterface.GetStringData("menu number", "species");

            // create species if this option is selected
            if (Int32.Parse(input) == counter+1)
            {
                return CreateSpecies();
            }
            
            foreach (Species s in db.Species)
            {
                if (input == s.Name)
                {
                    return s;
                }
            }
            return null;
        }

        internal static Species CreateSpecies()
        {
            Species newSpecies = new Species();
            return newSpecies;

            // CHANGE
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
