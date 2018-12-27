namespace DomainModel
{
    public class Telefon
    {
        public virtual int TelefonID { get; set; }

        public virtual string Nummer { get; set; }

        public virtual string Selskab { get; set; }

        public virtual string Type { get; set; }

        public virtual long PersonID { get; set; }
    }
}