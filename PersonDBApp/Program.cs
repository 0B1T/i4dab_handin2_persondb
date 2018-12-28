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

            var per = AddPerson();
            AddAddress(ref per);
            AddAltAddress(ref per);
            AddEmail(ref per);
            AddPhone(ref per);
        }

        private static Person AddPerson()
        {
            _person = new Person();

            Console.WriteLine("Please fill in your personal info below:\n");

            Console.Write("First name: ");
            _person.Fornavn = Console.ReadLine();

            Console.Write("Middle name (this can remain blank): ");
            _person.Mellemnavn = Console.ReadLine();

            Console.Write("Last name: ");
            _person.Efternavn = Console.ReadLine();

            Console.WriteLine("Please write a short note about yourself: ");
            _person.Noter = Console.ReadLine();

            Console.WriteLine("\nThank you.");

            new AppCalls().CreatePer(_person); // Adding to DB.

            return _person; // Returning this person.
        }

        private static void AddAddress(ref Person per)
        {
            _adr = new Adresse();

            Console.WriteLine("\nNow please fill in your primary address:\n");

            Console.Write("Street name: ");
            _adr.Vejnavn = Console.ReadLine();

            Console.Write("Number: ");
            _adr.Nummer = Console.ReadLine();

            Console.Write("Postal code: ");
            _adr.Postnummer = Console.ReadLine();

            Console.Write("City: ");
            _adr.Bynavn = Console.ReadLine();

            _adr.PersonID = per.PersonID; // Linking to correct person id.

            new AppCalls().CreateAdr(_adr); // Adding to DB.
        }

        private static void AddAltAddress(ref Person per)
        {
            _altAdr = new AltAdresse();

            Console.WriteLine("\nNow please fill in your alternative address (If you have one, otherwise you can leave all blank):\n");

            Console.Write("Street name: ");
            _altAdr.Vejnavn = Console.ReadLine();

            Console.Write("Number: ");
            _altAdr.Nummer = Console.ReadLine();

            Console.Write("Postal code: ");
            _altAdr.Postnummer = Console.ReadLine();

            Console.Write("City: ");
            _altAdr.Bynavn = Console.ReadLine();

            Console.Write("Type of address (e.g. summerhouse, etc.): ");
            _altAdr.Type = Console.ReadLine();

            if (_altAdr.Vejnavn == "" || _altAdr.Vejnavn.Length < 2)
            {
                _altAdr.Vejnavn = "";
                _altAdr.Nummer = "";
                _altAdr.Postnummer = "";
                _altAdr.Bynavn = "";
                _altAdr.Type = "none";
            }

            _altAdr.PersonID = per.PersonID; // Linking to correct person id.
            new AppCalls().CreateAltAdr(_altAdr); // Adding to DB.
        }

        private static void AddEmail(ref Person per)
        {
            _email = new Email();

            Console.WriteLine("\nNow please fill in your email address:\n");

            Console.Write("Email Address: ");
            _email.EmailAdr = Console.ReadLine();

            Console.Write("Type of email (e.g. private, work, etc.): ");
            _email.Type = Console.ReadLine();

            _email.PersonID = per.PersonID; // Linking to correct person id.
            new AppCalls().CreateEmail(_email); // Adding to DB.
        }

        private static void AddPhone(ref Person per)
        {
            _tlf = new Telefon();

            Console.WriteLine("\nAnd last, but not least, please fill in your phone number:\n");

            Console.Write("Phone number: ");
            _tlf.Nummer = Console.ReadLine();

            Console.Write("Service provider: ");
            _tlf.Selskab = Console.ReadLine();

            Console.Write("Type of phone (e.g. private, work, etc.): ");
            _tlf.Type = Console.ReadLine();

            _tlf.PersonID = per.PersonID; // Linking to correct person id.
            new AppCalls().CreatePhone(_tlf); // Adding to DB.
        }
        #endregion


        private static void ShowAllEntriesCompact()
        {
            Console.Clear();

            Console.WriteLine("The following list (compact view) of people are currently stored in the database...");

            Console.WriteLine("\n*** START OF LIST ***\n");

            foreach (var p in new AppCalls().ReadAllPepsCompact())
            {
                Console.WriteLine($"{p.PersonID} {p.Fornavn} {p.Mellemnavn} {p.Efternavn}");
            }

            Console.WriteLine("\n*** END OF LIST ***\n\n");
        }

        private static void ShowAllEntriesExpanded()
        {
            Console.Clear();

            Console.WriteLine("The following list (expanded view) of people are currently stored in the database...");

            Console.WriteLine("\n*** START OF LIST ***\n");

            foreach (var p in new AppCalls().ReadAllPepsExpanded())
            {
                Console.WriteLine($"{p.PersonID} {p.Fornavn} {p.Mellemnavn} {p.Efternavn} {p.Adresse} {p.AltAdresse} {p.Email} {p.Telefon}");
            }

            Console.WriteLine("\n*** END OF LIST ***\n\n");
        }


        private static void UpdateExistingPerson()
        {
            Console.Clear();

            ShowAllEntriesCompact();
            Console.Write("Type in the ID of the person you wish to update: ");

            int.TryParse(Console.ReadLine(), out var pid);

            while (UpdateMenuChoice(RenderUpdateMenu(pid), pid)) { }
        }

        private static void DeleteAPerson()
        {
            Console.Clear();

            ShowAllEntriesCompact();
            Console.Write("Type in the ID of the person you wish to delete: ");

            int.TryParse(Console.ReadLine(), out var pid);

            foreach (var p in new AppCalls().ReadAllPepsCompact())
            {
                if (p.PersonID != pid) continue;

                new AppCalls().DeletePer(p);
            }
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
                    Console.WriteLine("1. Compact view.");
                    Console.WriteLine("2. Expanded view.");
                    Console.Write(": ");

                    switch (Console.ReadLine())
                    {
                        case "1":
                            ShowAllEntriesCompact();
                            break;

                        case "2":
                            ShowAllEntriesExpanded();
                            break;

                        default:
                            Console.WriteLine("\nError: no such choice.\n");
                            break;
                    }

                    break;

                case "3":
                    UpdateExistingPerson();
                    break;

                case "4":
                    DeleteAPerson();
                    break;

                case "5":
                    Console.Clear();
                    Console.WriteLine("EXITING...\n");
                    Console.Write("Press any key.");
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
            Console.WriteLine("1. Person-data (e.g. name and notes).");
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