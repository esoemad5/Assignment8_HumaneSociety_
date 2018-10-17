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
                Where(u => u.UserName == userName && u.Password == password)
                /*Could put .AsEnumerable() here. Doesn't seem neccesary. Might not be the case elsewhere*/
                .Single();
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
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        internal static void updateClient(Client client)
        {
            throw new NotImplementedException();
        }

        internal static void UpdateUsername(Client client)
        {
            throw new NotImplementedException();
        }

        internal static void UpdateEmail(Client client)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        internal static void UpdateLastName(Client client)
        {
            throw new NotImplementedException();
        }

        internal static void RemoveAnimal(Animal animal)
        {
            throw new NotImplementedException();
        }

        internal static Species GetSpecies()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        internal static Employee RetrieveEmployeeUser(string email, int employeeNumber)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        internal static bool CheckEmployeeUserNameExist(string username)
        {
            throw new NotImplementedException();
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
