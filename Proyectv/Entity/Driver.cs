namespace Entity
{
    public class Driver
    {
        public string FirstName { get; }
        public string SecondName { get; }
        public string Surname { get; }
        public int DocumentId { get; }
        public int IdLicenceDriver { get; }
        public string Photo { get; }

        public Driver(string firstName, string secondName, string surname, int documentId, int idLicenceDriver, string photo)
        {
            FirstName = firstName;
            SecondName = secondName;
            Surname = surname;
            DocumentId = documentId;
            IdLicenceDriver = idLicenceDriver;
            Photo = photo;
        }
    }
}