using System.Collections.Generic;
using DomainModel;
using Infrastructure;

namespace AppLogic
{
    public class AppCalls
    {
        // Create (Post) Utility Calls:
        #region CREATE:

        public void CreatePer(Person newPerson)
        {
            new PersonDBUtil().AddPersonToDB(ref newPerson);
        }

        public void CreateAdr(Adresse newAddress)
        {
            new PersonDBUtil().AddAddressToDB(ref newAddress);
        }

        public void CreateAltAdr(AltAdresse newAltAddress)
        {
            new PersonDBUtil().AddAltAddressToDB(ref newAltAddress);
        }

        public void CreateEmail(Email newEmail)
        {
            new PersonDBUtil().AddEmailToDB(ref newEmail);
        }

        public void CreatePhone(Telefon newPhone)
        {
            new PersonDBUtil().AddTelefonToDB(ref newPhone);
        }

        #endregion


        // Read (Get) Utility Calls:
        public List<Person> ReadPer()
        {
            return new PersonDBUtil().ReadPeople();
        }

        // Update (Put) Utility Calls:
        #region UPDATE

        public void UpdatePer(Person currentPerson)
        {
            new PersonDBUtil().UpdatePeople(ref currentPerson);
        }

        public void UpdateAdr(Person currentPerson)
        {
            new PersonDBUtil();
        }

        public void UpdateAltAdr(Person currentPerson)
        {
            new PersonDBUtil();
        }

        public void UpdateEmail(Person currentPerson)
        {
            new PersonDBUtil();
        }

        public void UpdatePhone(Person currentPerson)
        {
            new PersonDBUtil();
        }

        #endregion


        // Delete Utility Calls:
        public void DeletePer(Person currentPerson)
        {
            new PersonDBUtil().DeletePersonFromDB(ref currentPerson);
        }
    }
}
