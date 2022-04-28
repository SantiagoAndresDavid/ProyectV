namespace Entity
{
    public class AdminstrativeStaff
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Surname { get; set; }
        public int DocumentId { get; set; }
        public string Photos { get; set; }

        public AdminstrativeStaff(int documentId, int id, string firstName, string secondName, string surname,
            string photos)
        {
            Id = id;
            FirstName = firstName;
            SecondName = secondName;
            Surname = surname;
            DocumentId = documentId;
            Photos = photos;
        }

        public AdminstrativeStaff()
        {
        }
    }
}