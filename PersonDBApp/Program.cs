using System;
using AppLogic;
using DomainModel;

namespace PersonDBApp
{
    class Program
    {
        // OBJECTS:
        private static Person _person;
        private static Adresse _adr;
        private static AltAdresse _altAdr;
        private static Email _email;
        private static Telefon _tlf;

        // MAIN:
        public static void Main(string[] args)
        {
            while (MainMenuChoice(RenderMainMenu())) { }

            Console.ReadKey();
        }

        // NEW PERSON:
        #region NEW PERSON CREATION:
        private static void AddNewPerson()
        {
            Console.Clear();

            var pid = AddPerson();
            AddAddress(pid);
            AddAltAddress(pid);
            AddEmail(pid);
            AddPhone(pid);
        }

        private static int AddPerson()
        {
            _person = new Person();

            Console.WriteLine("Please fill in all the questions below:\n");

            Console.WriteLine("What is your first name?");
            _person.Fornavn = Console.ReadLine();

            Console.WriteLine("What is your middle name? (this can remain blank)");
            _person.Mellemnavn = Console.ReadLine();

            Console.WriteLine("What is you last name?");
            _person.Efternavn = Console.ReadLine();

            Console.WriteLine("Please write a short note about yourself.");
            _person.Noter = Console.ReadLine();

            Console.WriteLine("\nThank you.");

            new AppCalls().CreatePer(_person); // Adding to DB.

            return _person.PersonID; // Returning id of this person.
        }

        private static void AddAddress(int pid)
        {
            _adr = new Adresse();

            Console.WriteLine("\nNow please fill in your primary address:\n");

            Console.WriteLine("Street name: ");
            _adr.Vejnavn = Console.Read().ToString();

            Console.WriteLine("Number: ");
            _adr.Nummer = Console.Read().ToString();

            Console.WriteLine("Postal code: ");
            _adr.Postnummer = Console.Read().ToString();

            Console.WriteLine("City: ");
            _adr.Bynavn = Console.Read().ToString();

            _adr.PersonID = pid; // Linking to correct person id.

            new AppCalls().CreateAdr(_adr); // Adding to DB.
        }

        private static void AddAltAddress(int pid)
        {
            _altAdr = new AltAdresse();

            Console.WriteLine("Now please fill in your alternative address (If you have one otherwise leave all blank):");

            Console.WriteLine("Street name: ");
            _altAdr.Vejnavn = Console.Read().ToString();

            Console.WriteLine("Number: ");
            _altAdr.Nummer = Console.Read().ToString();

            Console.WriteLine("Postal code: ");
            _altAdr.Postnummer = Console.Read().ToString();

            Console.WriteLine("City: ");
            _altAdr.Bynavn = Console.Read().ToString();

            Console.WriteLine("Type of address (e.g. summerhouse, etc.)");
            _altAdr.Type = Console.Read().ToString();

            _altAdr.PersonID = pid; // Linking to correct person id.

            new AppCalls().CreateAltAdr(_altAdr); // Adding to DB.
        }

        private static void AddEmail(int pid)
        {
            _email = new Email();

            Console.WriteLine("Now please fill in your email address:");

            Console.WriteLine("Email Address: ");
            _email.EmailAdr = Console.Read().ToString();

            Console.WriteLine("Type of email (e.g. private, work, etc.): ");
            _email.Type = Console.Read().ToString();

            _email.PersonID = pid; // Linking to correct person id.

            new AppCalls().CreateEmail(_email); // Adding to DB.
        }

        private static void AddPhone(int pid)
        {
            _tlf = new Telefon();

            Console.WriteLine("And last, but not least, please fill in your phone number:");

            Console.WriteLine("Phone number: ");
            _tlf.Nummer = Console.Read().ToString();

            Console.WriteLine("Service provider: ");
            _tlf.Selskab = Console.Read().ToString();

            Console.WriteLine("Type of phone (e.g. private, work, etc.): ");
            _tlf.Type = Console.Read().ToString();

            _tlf.PersonID = pid; // Linking to correct person id.

            new AppCalls().CreatePhone(_tlf); // Adding to DB.
        }
        #endregion


        private static void ShowAllEntries()
        {
            Console.Clear();

            Console.WriteLine("The following list of people are currently stored in the database...");

            Console.WriteLine("\n*** START OF LIST ***\n");

            foreach (var p in new AppCalls().ReadPer())
            {
                Console.WriteLine($"{p.PersonID} {p.Fornavn} {p.Mellemnavn} {p.Efternavn}");
            }

            Console.WriteLine("\n*** END OF LIST ***\n\n");
        }

        private static void UpdateExistingPerson()
        {
            Console.Clear();

            ShowAllEntries();
            Console.Write("Type in the ID of the person you wish to update: ");

            int.TryParse(Console.ReadLine(), out var pid);

            while (UpdateMenuChoice(RenderUpdateMenu(pid), pid)) { }
        }

        private static void DeleteAPerson()
        {

        }


        // MAIN MENU:
        #region MAIN MENU:
        private static string RenderMainMenu()
        {
            Console.WriteLine("*** HELLO AND WELCOME TO PERSON DATABASE MAIN MENU ***");
            Console.WriteLine("1. Add new person to the database.");
            Console.WriteLine("2. Show list of all people in the database.");
            Console.WriteLine("3. Update existing person in the database.");
            Console.WriteLine("4. Delete a person in the database.");
            Console.WriteLine("5. EXIT!");

            Console.Write("\nPLEASE CHOOSE: ");

            return Console.ReadLine();
        }

        private static bool MainMenuChoice(string choice)
        {
            switch (choice)
            {
                case "1":
                    AddNewPerson();
                    break;

                case "2":
                    ShowAllEntries();
                    break;

                case "3":
                    UpdateExistingPerson();
                    break;

                case "4":
                    DeleteAPerson();
                    break;

                case "5":
                    Console.Clear();
                    Console.WriteLine("EXITING...");
                    return false;

                default:
                    Console.Clear();
                    Console.WriteLine("WRONG INPUT! Please try again.\n");
                    break;
            }

            return true;
        }



        #endregion


        // UPDATE MENU:
        #region UPDATE MENU:
        private static string RenderUpdateMenu(int pid)
        {
            Console.WriteLine("*** HELLO AND WELCOME TO THE UPDATE MENU ***");
            Console.WriteLine($"\nPlease choose what to update for person with ID: {pid}.");
            Console.WriteLine("1. Person-data (Ex. name and notes).");
            Console.WriteLine("2. Address.");
            Console.WriteLine("3. Alternative address.");
            Console.WriteLine("4. Email.");
            Console.WriteLine("5. Phone.");
            Console.WriteLine("6. RETURN TO MAIN MENU!");

            Console.Write("\nPLEASE CHOOSE WHAT TO UPDATE: ");

            return Console.ReadLine();
        }

        private static bool UpdateMenuChoice(string choice, int pid)
        {
            switch (choice)
            {
                case "1":
                    UpdPerson(pid);
                    break;

                case "2":
                    UpdAddress(pid);
                    break;

                case "3":
                    UpdAltAddress(pid);
                    break;

                case "4":
                    UpdEmail(pid);
                    break;

                case "5":
                    UpdPhone(pid);
                    break;

                case "6":
                    Console.Clear();
                    return false;

                default:
                    Console.Clear();
                    Console.WriteLine("WRONG INPUT! Please try again.\n");
                    break;
            }

            return true;
        }

        private static void UpdPerson(int pid)
        {
            
        }

        private static void UpdAddress(int pid)
        {
            
        }

        private static void UpdAltAddress(int pid)
        {

        }

        private static void UpdEmail(int pid)
        {
            
        }

        private static void UpdPhone(int pid)
        {
            
        }
        #endregion
    }
}