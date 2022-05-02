namespace Entity
{
    public class Trip
    {
        public Guid IdTrip { get; set; }
        public int NumberPassengers { get; set; }
        public Driver Driver { get; set; }
        public Car Car { get; set; }


        public Trip(Guid idTrip, int numberPassengers, Driver driver, Car car)
        {
            IdTrip = idTrip;
            NumberPassengers = numberPassengers;
            Driver = driver;
            Car = car;
        }

        public Trip()
        {
        }
    }
}