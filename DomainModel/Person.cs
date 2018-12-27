using System.Collections.Generic;

namespace DomainModel
{
    public class Person
    {
        public virtual int PersonID { get; set; }

        public virtual string Fornavn { get; set; }

        public virtual string Mellemnavn { get; set; }

        public virtual string Efternavn { get; set; }

        public virtual string Noter { get; set; }

        // Ref:
        public virtual ICollection<Adresse> Adresse { get; set; }
        public virtual ICollection<AltAdresse> AltAdresse { get; set; }
        public virtual ICollection<Email> Email { get; set; }
        public virtual ICollection<Telefon> Telefon { get; set; }
    }
}