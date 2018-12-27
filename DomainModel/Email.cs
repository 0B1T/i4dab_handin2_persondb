namespace DomainModel
{
    public class Email
    {
        public virtual int EmailID { get; set; }

        public virtual string EmailAdr { get; set; }

        public virtual string Type { get; set; }

        public virtual long PersonID { get; set; }
    }
}