namespace DomainModel
{
    public class Adresse
    {
        public virtual int AdresseID { get; set; }

        public virtual string Vejnavn { get; set; }

        public virtual string Nummer { get; set; }

        public virtual string Postnummer { get; set; }

        public virtual string Bynavn { get; set; }

        // Ref:
        public virtual long PersonID { get; set; }
    }
}