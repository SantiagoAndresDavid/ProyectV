namespace Entity
{
    public class Driver
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Surname { get; set; }
        public int DocumentId { get; set; }
        public int IdLicenceDriver { get; set; }
        public string Photo { get; set; }

        public Driver(string firstName, string secondName, string surname, int documentId, int idLicenceDriver,
            string photo)
        {
            FirstName = firstName;
            SecondName = secondName;
            Surname = surname;
            DocumentId = documentId;
            IdLicenceDriver = idLicenceDriver;
            Photo = photo;
        }

        public Driver()
        {
        }
    }
}