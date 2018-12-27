using System.Collections.Generic;
using DomainModel;
using Infrastructure;

namespace AppLogic
{
    public class AppCalls
    {
        // Create (Post) Utility Call:
        public void CreatePer(Person newPerson)
        {
            new PersonDBUtil().AddPersonToDB(ref newPerson);
        }

        // Read (Get) Utility Call:
        public List<Person> ReadPer()
        {
            var util = new PersonDBUtil();

            return util.ReadPeople();
        }

        // Update (Put) Utility Call:
        public void UpdatePer(Person currentPerson)
        {
            var util = new PersonDBUtil();

            util.UpdatePeople(ref currentPerson);
        }
        
        // Delete Utility Call:
        public void DeletePer(Person currentPerson)
        {
            var util = new PersonDBUtil();

            util.DeletePersonFromDB(ref currentPerson);
        }


        // OTHER UTILITY FUNCTIONS:

    }
}
