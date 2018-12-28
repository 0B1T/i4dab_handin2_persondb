using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using DomainModel;

namespace Infrastructure
{
    public class PersonDBUtil
    {
        private Person _currentPerson;

        #region SETUP:

        // CTOR:
        public PersonDBUtil()
        {
            _currentPerson = new Person()
            {
                PersonID = 0,
                Fornavn = "",
                Mellemnavn = "",
                Efternavn = "",
                Noter = "",

                // Ref:
                Adresse = null,
                AltAdresse = null,
                Email = null,
                Telefon = null
            };
        }

        // Opens SQL Connection:
        private SqlConnection OpenConnection
        {
            get
            {
                // Local Server:
                //var connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PersonDB;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True");

                // AU Server:
                var connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;User ID=E18I4DABau518762;Password=E18I4DABau518762;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

                connection.Open();

                return connection;
            }
        }

        #endregion


        #region BASIC CRUD OPERATIONS:

        // Create (Post) Person function:
        public void AddPersonToDB(ref Person per)
        {
            var insertStringParam = @"INSERT INTO [Person] (Fornavn, Mellemnavn, Efternavn, Noter)
                                                OUTPUT INSERTED.PersonID
                                                VALUES (@Fornavn, @Mellemnavn, @Efternavn, @Noter)";

            using (var cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@Fornavn", per.Fornavn);
                cmd.Parameters.AddWithValue("@Mellemnavn", per.Mellemnavn);
                cmd.Parameters.AddWithValue("@Efternavn", per.Efternavn);
                cmd.Parameters.AddWithValue("@Noter", per.Noter);

                per.PersonID = (int)cmd.ExecuteScalar(); //Returns the identity of the new tuple/record
            }
        }
    
        // Read (Get) Person function:
        public List<Person> ReadPeopleCompact()
        {
            var readStringParam = @"SELECT * FROM Person";

            using (var cmd = new SqlCommand(readStringParam, OpenConnection))
            {
                var rdr = cmd.ExecuteReader();

                var eachPerson = new List<Person>();

                while (rdr.Read())
                {
                    var per = new Person();

                    per.PersonID = (int)rdr["PersonID"];
                    per.Fornavn = (string)rdr["Fornavn"];
                    per.Mellemnavn = (string)rdr["Mellemnavn"];
                    per.Efternavn = (string)rdr["Efternavn"];
                    per.Noter = (string)rdr["Noter"];

                    eachPerson.Add(per);
                }

                return eachPerson;
            }
        }

        // Read (Get) all data function:
        public List<Person> ReadPeopleExpanded()
        {
            var readStringParam = @"SELECT	*
                                    FROM	Person INNER JOIN
		                                    Adresse ON Person.PersonID = Adresse.PersonID INNER JOIN
		                                    AltAdresse ON Person.PersonID = AltAdresse.PersonID INNER JOIN
                                            Email ON Person.PersonID = Email.PersonID INNER JOIN
		                                    Telefon ON Person.PersonID = Telefon.PersonID";

            using (var cmd = new SqlCommand(readStringParam, OpenConnection))
            {
                var rdr = cmd.ExecuteReader();

                var eachPerson = new List<Person>();

                while (rdr.Read())
                {
                    var per = new Person();
                    per.Adresse = new List<Adresse>();
                    per.AltAdresse = new List<AltAdresse>();
                    per.Email = new List<Email>();
                    per.Telefon = new List<Telefon>();

                    var adr = new Adresse();
                    var altAdr = new AltAdresse();
                    var email = new Email();
                    var tlf = new Telefon();

                    per.PersonID = (int)rdr["PersonID"];
                    per.Fornavn = (string)rdr["Fornavn"];
                    per.Mellemnavn = (string)rdr["Mellemnavn"];
                    per.Efternavn = (string)rdr["Efternavn"];
                    per.Noter = (string)rdr["Noter"];

                    adr.AdresseID = (int) rdr["AdresseID"];
                    adr.Vejnavn = (string) rdr["Vejnavn"];
                    adr.Nummer = (string) rdr["Nummer"];
                    adr.Postnummer = (string) rdr["Postnummer"];
                    adr.Bynavn = (string) rdr["Bynavn"];
                    adr.PersonID = (int) rdr["PersonID"];
                    per.Adresse.Add(adr);

                    altAdr.AltAdresseID = (int)rdr["AltAdresseID"];
                    altAdr.Vejnavn = (string)rdr["Vejnavn"];
                    altAdr.Nummer = (string)rdr["Nummer"];
                    altAdr.Postnummer = (string)rdr["Postnummer"];
                    altAdr.Bynavn = (string)rdr["Bynavn"];
                    altAdr.Type = (string)rdr["Type"];
                    altAdr.PersonID = (int)rdr["PersonID"];
                    per.AltAdresse.Add(altAdr);

                    email.EmailID = (int)rdr["EmailID"];
                    email.EmailAdr = (string)rdr["EmailAdr"];
                    email.Type = (string)rdr["Type"];
                    email.PersonID = (int)rdr["PersonID"];
                    per.Email.Add(email);

                    tlf.TelefonID = (int)rdr["TelefonID"];
                    tlf.Nummer = (string)rdr["Nummer"];
                    tlf.Selskab = (string)rdr["Selskab"];
                    tlf.Type = (string)rdr["Type"];
                    tlf.PersonID = (int)rdr["PersonID"];
                    per.Telefon.Add(tlf);

                    eachPerson.Add(per);
                }

                return eachPerson;
            }
        }

        // Update (Put) Person function:
        public void UpdatePeople(ref Person per, string newFN, string newMN, string newEN, string newNote)
        {
            var pers = new Person();
            pers = per;
            DeletePersonFromDB(ref per);

            pers.Fornavn = newFN;
            pers.Mellemnavn = newMN;
            pers.Efternavn = newEN;
            pers.Noter = newNote;

            AddPersonToDB(ref pers);
        }

        
        // Delete Person function:
        public void DeletePersonFromDB(ref Person per)
        {
            var allPeps = ReadPeopleExpanded();
            var adr = new Adresse();
            var altAdr = new AltAdresse();
            var email = new Email();
            var tlf = new Telefon();

            foreach (var p in allPeps)
            {
                if (p.PersonID == per.PersonID)
                {
                    adr = p.Adresse.First();
                    altAdr = p.AltAdresse.First();
                    email = p.Email.First();
                    tlf = p.Telefon.First();
                }
            }

            DeleteAdr(ref adr);
            DeleteAltAdr(ref altAdr);
            DeleteEmail(ref email);
            DeletePhone(ref tlf);

            var deleteStringParam = @"DELETE FROM [Person] WHERE (PersonID=@PersonID)";

            using (var cmd = new SqlCommand(deleteStringParam, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@PersonID", per.PersonID);

                cmd.ExecuteNonQuery();

                per = null;
            }
        }

        private void DeleteAdr(ref Adresse adr)
        {
            var deleteStringParam = @"DELETE FROM [Adresse] WHERE (AdresseID=@AdresseID)";

            using (var cmd = new SqlCommand(deleteStringParam, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@AdresseID", adr.AdresseID);

                cmd.ExecuteNonQuery();

                adr = null;
            }
        }

        private void DeleteAltAdr(ref AltAdresse altAdr)
        {
            var deleteStringParam = @"DELETE FROM [AltAdresse] WHERE (AltAdresseID=@AltAdresseID)";

            using (var cmd = new SqlCommand(deleteStringParam, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@AltAdresseID", altAdr.AltAdresseID);

                cmd.ExecuteNonQuery();

                altAdr = null;
            }
        }

        private void DeleteEmail(ref Email email)
        {
            var deleteStringParam = @"DELETE FROM [Email] WHERE (EmailID=@EmailID)";

            using (var cmd = new SqlCommand(deleteStringParam, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@EmailID", email.EmailID);

                cmd.ExecuteNonQuery();

                email = null;
            }
        }

        private void DeletePhone(ref Telefon tlf)
        {
            var deleteStringParam = @"DELETE FROM [Telefon] WHERE (TelefonID=@TelefonID)";

            using (var cmd = new SqlCommand(deleteStringParam, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@TelefonID", tlf.TelefonID);

                cmd.ExecuteNonQuery();

                tlf = null;
            }
        }

        #endregion


        #region ADRESSE:

        public void AddAddressToDB(ref Adresse adr)
        {
            var insertStringParam = @"INSERT INTO [Adresse] (Vejnavn, Nummer, Postnummer, Bynavn, PersonID)
                                        OUTPUT INSERTED.AdresseID
                                        VALUES (@Vejnavn, @Nummer, @Postnummer, @Bynavn, @PersonID)";

            using (var cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@Vejnavn", adr.Vejnavn);
                cmd.Parameters.AddWithValue("@Nummer", adr.Nummer);
                cmd.Parameters.AddWithValue("@Postnummer", adr.Postnummer);
                cmd.Parameters.AddWithValue("@Bynavn", adr.Bynavn);
                cmd.Parameters.AddWithValue("@PersonID", adr.PersonID);

                adr.AdresseID = (int)cmd.ExecuteScalar();
            }
        }

        #endregion

        #region ALTADRESSE:

        public void AddAltAddressToDB(ref AltAdresse adr)
        {
            var insertStringParam = @"INSERT INTO [AltAdresse] (Vejnavn, Nummer, Postnummer, Bynavn, Type, PersonID)
                                        OUTPUT INSERTED.AltAdresseID
                                        VALUES (@Vejnavn, @Nummer, @Postnummer, @Bynavn, @Type, @PersonID)";

            using (var cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@Vejnavn", adr.Vejnavn);
                cmd.Parameters.AddWithValue("@Nummer", adr.Nummer);
                cmd.Parameters.AddWithValue("@Postnummer", adr.Postnummer);
                cmd.Parameters.AddWithValue("@Bynavn", adr.Bynavn);
                cmd.Parameters.AddWithValue("@Type", adr.Type);
                cmd.Parameters.AddWithValue("@PersonID", adr.PersonID);

                adr.AltAdresseID = (int)cmd.ExecuteScalar();
            }
        }

        #endregion

        #region EMAIL:

        public void AddEmailToDB(ref Email email)
        {
            var insertStringParam = @"INSERT INTO [Email] (EmailAdr, Type, PersonID)
                                        OUTPUT INSERTED.EmailID
                                        VALUES (@EmailAdr, @Type, @PersonID)";

            using (var cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@EmailAdr", email.EmailAdr);
                cmd.Parameters.AddWithValue("@Type", email.Type);
                cmd.Parameters.AddWithValue("@PersonID", email.PersonID);

                email.EmailID = (int)cmd.ExecuteScalar();
            }
        }

        #endregion

        #region TELEFON:

        public void AddTelefonToDB(ref Telefon tlf)
        {
            var insertStringParam = @"INSERT INTO [Telefon] (Nummer, Selskab, Type, PersonID)
                                        OUTPUT INSERTED.TelefonID
                                        VALUES (@Nummer, @Selskab, @Type, @PersonID)";

            using (var cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@Nummer", tlf.Nummer);
                cmd.Parameters.AddWithValue("@Selskab", tlf.Selskab);
                cmd.Parameters.AddWithValue("@Type", tlf.Type);
                cmd.Parameters.AddWithValue("@PersonID", tlf.PersonID);

                tlf.TelefonID = (int)cmd.ExecuteScalar();
            }
        }

        #endregion
    }
}
