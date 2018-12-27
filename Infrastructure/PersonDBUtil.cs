using System.Collections.Generic;
using System.Data.SqlClient;
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
                var connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PersonDB;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True");

                // AU Server:
                //var connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;User ID=E18I4DABau518762;Password=E18I4DABau518762;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

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
        public List<Person> ReadPeople()
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

        // Update (Put) Person function:
        public void UpdatePeople(ref Person per)
        {
            var updateStringParam = @"UPDATE [Person]
                                        SET Fornavn=@Fornavn, Mellemnavn=@Mellemnavn, Efternavn=@Efternavn, Noter=@Noter 
                                        WHERE PersonID=@PersonID";

            using (var cmd = new SqlCommand(updateStringParam, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@PersonID", per.PersonID);
                cmd.Parameters.AddWithValue("@Fornavn", per.Fornavn);
                cmd.Parameters.AddWithValue("@Mellemnavn", per.Mellemnavn);
                cmd.Parameters.AddWithValue("@Efternavn", per.Efternavn);
                cmd.Parameters.AddWithValue("@Noter", per.Noter);

                cmd.ExecuteNonQuery();
            }
        }

        // Delete Person function:
        public void DeletePersonFromDB(ref Person per)
        {
            var deleteStringParam = @"DELETE FROM [Person] WHERE (PersonID=@PersonID)";

            using (var cmd = new SqlCommand(deleteStringParam, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@PersonID", per.PersonID);

                cmd.ExecuteNonQuery();

                per = null;
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
