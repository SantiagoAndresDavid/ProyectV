using System;

namespace Entity
{
    public class TechnoMechanics
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string BodyworkCondition { get; set; }
        public string BrakeCondition { get; set; }
        public string DamperCondition { get; set; }
        public string WeelsCondition { get; set; }
        public Car Car { get; set; }

        public TechnoMechanics()
        {
        }

        public TechnoMechanics(Guid id, DateTime date, string bodyworkCondition, string brakeCondition, string damperCondition, string weelsCondition, Car car)
        {
            Id = id;
            Date = date;
            BodyworkCondition = bodyworkCondition;
            BrakeCondition = brakeCondition;
            DamperCondition = damperCondition;
            WeelsCondition = weelsCondition;
            Car = car;
        }
    }
}