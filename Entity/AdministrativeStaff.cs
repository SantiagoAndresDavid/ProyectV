namespace Entity
{
    public class AdminstrativeStaff
    {
        public int Id { get; }
        public string FirstName { get; }
        public string SecondName { get; }
        public string surname { get; }
        public int DocumentId { get; }
        public string Photos { get; }
        
        public AdminstrativeStaff(int id, string firstName, string secondName, string surname, int documentId, string photos)
        {
            Id = id;
            FirstName = firstName;
            SecondName = secondName;
            this.surname = surname;
            DocumentId = documentId;
            Photos = photos;
        }
    }
}