﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public static class UserInterface
    {
        public static void DisplayUserOptions(List<string> options)
        {
            foreach(string option in options)
            {
                Console.WriteLine(option);
            }
        }
        public static void DisplayUserOptions(string options)
        {
            Console.WriteLine(options);
        }

        public static void DisplayEmployee(Employee employee)
        {
            DisplayUserOptions("Current values:");
            DisplayUserOptions("Name: " + employee.FirstName + " " + employee.LastName);
            DisplayUserOptions("Username: " + employee.UserName);
            DisplayUserOptions("Password: " + employee.Password);
            DisplayUserOptions("Email: " + employee.Email);
            DisplayUserOptions("Employee Number: " + employee.EmployeeNumber);
            DisplayUserOptions("Assigned Animals: ");
            foreach (Animal a in employee.Animals)
            {
                DisplayUserOptions(a.Name);
            }
        }

        public static string GetUserInput()
        {
            string input = Console.ReadLine();
            switch (input.ToLower())
            {
                case "reset":
                    PointOfEntry.Run();
                    Environment.Exit(1);
                    break;
                case "exit":
                    Environment.Exit(1);
                    break;
                default:
                    break;
            }

            return input;
        }
        public static string GetStringData(string parameter, string target)
        {
            string data;
            DisplayUserOptions($"What is {target} {parameter}?");
            data = GetUserInput();
            return data;
        }

        internal static void UpdateEmployee_MenuSelection(Employee target)
        {
            string input = GetStringData("selection", "your");
            switch (input)
            {
                case "FirstName":
                    Query.UpdateEmployee_FirstName(GetStringData(input + "'s", "new value"), target);
                    break;
                case "LastName":
                    Query.UpdateEmployee_LastName(GetStringData(input + "'s", "new value"), target);
                    break;
                case "UserName":
                    Query.UpdateEmployee_UserName(GetStringData(input + "'s", "new value"), target);
                    break;
                case "Password":
                    Query.UpdateEmployee_Password(GetStringData(input + "'s", "new value"), target); ;
                    break;
                case "EmployeeNumber":
                    Query.UpdateEmployee_EmployeeNumber(GetIntegerData(input + "'s", "new value"), target);
                    break;
                case "Email":
                    Query.UpdateEmployee_Email(GetStringData(input + "'s", "new value"), target);
                    break;
                case "AssignedAnimals":
                    break;
                default:
                    DisplayUserOptions("Invalid Selection.");
                    UpdateEmployee_MenuSelection(target);
                    return;
            }
        }

        internal static bool? GetBitData(List<string> options)
        {
            DisplayUserOptions(options);
            string input = GetUserInput();
            if (input.ToLower() == "yes" || input.ToLower() == "y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool? GetBitData()
        {
            string input = GetUserInput();
            if (input.ToLower() == "yes" || input.ToLower() == "y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        internal static bool? GetBitData(string target, string parameter)
        {
            DisplayUserOptions($"Is {target} {parameter}?");
            string input = GetUserInput();
            if (input.ToLower() == "yes" || input.ToLower() == "y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal static void DisplayAnimals(List<Animal> animals)
        {
            foreach(Animal animal in animals)
            {
                Console.WriteLine("--------------------");
                Console.WriteLine("ID: {0}", animal.AnimalId);
                Console.WriteLine("Name: {0}", animal.Name);
                Console.WriteLine("Species: {0}", animal.Species.Name);
                Console.WriteLine("Weight: {0}", animal.Weight);
                Console.WriteLine("Age: {0}", animal.Age);
                Console.WriteLine("DietPlan: {0}", animal.DietPlan.Name);
                Console.WriteLine("Demeanor: {0}", animal.Demeanor);
                Console.WriteLine("Kid Friendly: {0}", animal.KidFriendly);
                Console.WriteLine("Pet Friendly: {0}", animal.PetFriendly);
                Console.WriteLine("Gender: {0}", animal.Gender);
                Console.WriteLine("Adoption Status: {0}", animal.AdoptionStatus);
            }
        }

        internal static int GetIntegerData()
        {
            try
            {
                int data = int.Parse(GetUserInput());
                return data;
            }
            catch
            {
                Console.Clear();
                DisplayUserOptions("Incorrect input please enter an integer number.");
                return GetIntegerData();
            }
        }

        public static int GetIntegerData(string parameter, string target)
        {
            try
            {
                int data = int.Parse(GetStringData(parameter, target));
                return data;
            }
            catch
            {
                Console.Clear();
                DisplayUserOptions("Incorrect input please enter an integer number.");
                return GetIntegerData(parameter, target);
            }
        }

        internal static void DisplayClientInfo(Client client)
        {
            List<string> info = new List<string>() { client.FirstName, client.LastName, client.Email, "Number of kids: " + client.NumberOfKids.ToString(), "Home size: " + client.HomeSquareFootage.ToString(), "Income: " + client.Income.ToString(), client.Address.USState.Name };
            DisplayUserOptions(info);
            Console.ReadLine();        }

        public static void DisplayAnimalInfo(Animal animal)
        {
            Room animalRoom = Query.GetRoom(animal.AnimalId);
            List<string> info = new List<string>() {"ID: " + animal.AnimalId, "Name: " + animal.Name,"Age: "+ animal.Age + " years", "Demeanour: " + animal.Demeanor, "Kid friendly: " + BoolToYesNo(animal.KidFriendly), "pet friendly: " + BoolToYesNo(animal.PetFriendly), $"Location: " + animalRoom.RoomId, "Weight: " + animal.Weight.ToString(),  "Food amoumnt in cups:" + animal.DietPlan.FoodAmountInCups};
            DisplayUserOptions(info);
            Console.ReadLine();

        }

        private static string BoolToYesNo(bool? input)
        {
            if (input == true)
            {
                return "yes";
            }
            else
            {
                return "no";
            }
        }

        public static bool GetBitData(string option)
        {
            DisplayUserOptions(option);
            string input = GetUserInput();
            if (input.ToLower() == "yes" || input.ToLower() == "y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static Dictionary<int, string> GetAnimalCriteria()
        {
            Dictionary<int, string> searchParameters = new Dictionary<int, string>();
            bool isSearching = true;
            while (isSearching)
            {
                Console.Clear();
                List<string> options = new List<string>() { "Select Search Criteia: (Enter number and choose finished when finished)", "1. Species", "2. Name", "3. Age", "4. Demeanor", "5. Kid friendly", "6. Pet friendly", "7. Weight", "8. ID", "9. Finished" };
                DisplayUserOptions(options);
                string input = GetUserInput();
                if (input.ToLower() == "10" || input.ToLower() == "finished")
                {
                    isSearching = false;
                    continue;
                }
                else
                {
                    searchParameters = EnterSearchCriteria(searchParameters, input);
                }
            }
            return searchParameters;
        }
        public static Dictionary<int, string> EnterSearchCriteria(Dictionary<int, string> searchParameters, string input)
        {
            Console.Clear();
            switch (input)
            {
                case "1":
                    searchParameters.Add(1, UserInterface.GetStringData("species", "the animal's"));
                    return searchParameters;
                case "2":
                    searchParameters.Add(2, UserInterface.GetStringData("name", "the animal's"));
                    return searchParameters;
                case "3":
                    searchParameters.Add(3, UserInterface.GetIntegerData("age", "the animal's").ToString());
                    return searchParameters;
                case "4":
                    searchParameters.Add(4, UserInterface.GetStringData("demeanor", "the animal's"));
                    return searchParameters;
                case "5":
                    searchParameters.Add(5, UserInterface.GetBitData("the animal", "kid friendly").ToString());
                    return searchParameters;
                case "6":
                    searchParameters.Add(6, UserInterface.GetBitData("the animal", "pet friendly").ToString());
                    return searchParameters;
                case "7":
                    searchParameters.Add(7, UserInterface.GetIntegerData("weight", "the animal's").ToString());
                    return searchParameters;
                case "8":
                    searchParameters.Add(8, UserInterface.GetIntegerData("ID", "the animal's").ToString());
                    return searchParameters;
                default:
                    UserInterface.DisplayUserOptions("Input not recognized please try agian");
                    return searchParameters;
            }
        }
    }
}
