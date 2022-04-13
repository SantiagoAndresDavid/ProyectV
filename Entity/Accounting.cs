using System;

namespace Entity
{
    public class Accounting
    {
        public DateTime Date { get; }
        public AdminstrativeStaff AdminstrativeStaff { get; }
        public Car car { get; }
        public Driver driver { get; }
        public int earnedMoney { get; }

        public Accounting(DateTime date, AdminstrativeStaff adminstrativeStaff, Car car, Driver driver, int earnedMoney)
        {
            Date = date;
            AdminstrativeStaff = adminstrativeStaff;
            this.car = car;
            this.driver = driver;
            this.earnedMoney = earnedMoney;
        }
    }
}