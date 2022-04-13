namespace Entity
{
    public class Trip
    {
        int NumberPassengers { get; }
        Driver driver { get; }
        Car car { get; }

        public Trip(int numberPassengers, Driver driver, Car car)
        {
            NumberPassengers = numberPassengers;
            this.driver = driver;
            this.car = car;
        }
    }
}