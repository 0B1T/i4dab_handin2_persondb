using System;
using AppLogic;
using DomainModel;

namespace PersonDBApp
{
    class Program
    {
        // OBJECTS:
        private static Person _p;
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


        private static void AddNewPerson()
        {
            Console.Clear();

            _p = new Person();

            Console.WriteLine("Hello, to add you to the database, please fill in the questions below...\n");

            Console.WriteLine("What is your first name?");
            _p.Fornavn = Console.ReadLine();

            Console.WriteLine("What is your middle name? (this can remain blank)");
            _p.Mellemnavn = Console.ReadLine();

            Console.WriteLine("What is you last name?");
            _p.Efternavn = Console.ReadLine();

            Console.WriteLine("Please write a short note about yourself.");
            _p.Noter = Console.ReadLine();

            Console.WriteLine($"\nThank you {_p.Fornavn} {_p.Mellemnavn} {_p.Efternavn}, that is all for now.");

            Console.WriteLine("Do you really want to be added to the database?");
            Console.WriteLine("Choose: Y/N:");

            if (Console.Read() == 'Y') new AppCalls().CreatePer(_p);
        }

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

            int.TryParse(Console.ReadLine(), out var choice);

            while (UpdateMenuChoice(RenderUpdateMenu(choice))) { }
        }


        #region MENU FUNCTIONS:

        // MAIN MENU:
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
                    //DeleteAPerson();
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

        // UPDATE MENU:
        private static string RenderUpdateMenu(int pid)
        {
            Console.WriteLine($"*** HELLO AND WELCOME TO THE UPDATE MENU ***");
            Console.WriteLine($"\nPlease choose what to update for person with ID: {pid}.");
            Console.WriteLine("1. Address.");
            Console.WriteLine("2. Address (alternative).");
            Console.WriteLine("3. Email.");
            Console.WriteLine("4. Phone.");
            Console.WriteLine("5. RETURN TO MAIN MENU!");

            Console.Write("\nPLEASE CHOOSE: ");

            return Console.ReadLine();
        }

        private static bool UpdateMenuChoice(string choice)
        {
            switch (choice)
            {
                case "1":
                    break;

                case "2":
                    break;

                case "3":
                    break;

                case "4":
                    break;

                case "5":
                    Console.Clear();
                    return false;

                default:
                    Console.Clear();
                    Console.WriteLine("WRONG INPUT! Please try again.\n");
                    break;
            }

            return true;
        }

        #endregion

    }
}



//} while (Console.ReadKey().Key != ConsoleKey.Escape);


//#region Creating People:

//var p1 = new Person()
//{
//    Fornavn = "John",
//    Mellemnavn = "Romby",
//    Efternavn = "Andersson",
//    Noter = "Mig selv.",
//};

//new Email()
//{
//    PersonID = p1.PersonID,
//    EmailAdr = "romby@outlook.com",
//    Type = "Privat",
//};

//new Telefon()
//{
//    PersonID = p1.PersonID,
//    Nummer = "30954607",
//    Type = "iPhone",
//    Selskab = "Telia",
//};

//new Adresse()
//{
//    PersonID = p1.PersonID,
//    Vejnavn = "Møllegade",
//    Nummer = "8",
//    Postnummer = "8000",
//    Bynavn = "Aarhus C",
//};

//var p2 = new Person()
//{
//    Fornavn = "Maya",
//    Mellemnavn = "Romby",
//    Efternavn = "Andersson",
//    Noter = "Min første datter.",
//};

//var p3 = new Person()
//{
//    Fornavn = "Gry",
//    Mellemnavn = "Romby",
//    Efternavn = "Andersson",
//    Noter = "Min anden datter.",
//};

//#endregion
