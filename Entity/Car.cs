namespace Entity
{
    public class Car
    {
        public string LicensePlate { get; set; }
        public string Model { get; set; }
        public string Colour { get; set; }
        public string Id { get; set; }   
        public int NumSeating { get; set; }

        public Car(string licensePlate, string model, string colour, string id, int numSeating)
        {
            LicensePlate = licensePlate;
            Model = model;
            Colour = colour;
            Id = id;
            NumSeating = numSeating;
        }

        public Car()
        {
        }
        
        
    }
}