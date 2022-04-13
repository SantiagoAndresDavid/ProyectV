namespace Entity
{
    public class Car
    {
        public string LicensePlate { get; }
        public string Model { get; }
        public string Colour { get; }
        public string Id { get; }
        public int NumSeating { get; }

        public Car(string licensePlate, string model, string colour, string id, int numSeating)
        {
            LicensePlate = licensePlate;
            Model = model;
            Colour = colour;
            Id = id;
            NumSeating = numSeating;
        }
    }
}