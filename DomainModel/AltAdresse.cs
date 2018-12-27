namespace DomainModel
{
    public class AltAdresse
    {
        public virtual int AltAdresseID { get; set; }

        public virtual string Vejnavn { get; set; }

        public virtual string Nummer { get; set; }

        public virtual string Postnummer { get; set; }

        public virtual string Bynavn { get; set; }

        public virtual string Type { get; set; }

        // Ref:
        public virtual long PersonID { get; set; }
    }
}