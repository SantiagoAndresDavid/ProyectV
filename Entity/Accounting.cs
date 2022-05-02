using System;

namespace Entity
{
    public class Accounting
    {
        public DateTime Date { get; set; }
        public AdminstrativeStaff AdminstrativeStaff { get; set; }
        public Car car { get; set; }
        public Driver driver { get; set; }
        public int earnedMoney { get; set; }

        public Trip trip { get; set; }


        public Accounting(DateTime date, AdminstrativeStaff adminstrativeStaff, Car car, Driver driver, int earnedMoney, Trip trip)
        {
            Date = date;
            AdminstrativeStaff = adminstrativeStaff;
            this.car = car;
            this.driver = driver;
            this.earnedMoney = earnedMoney;
            this.trip = trip;
        }

        public Accounting()
        {
        }
    }
}

