using System;

namespace Entity
{
    public class TecnoMechanics
    {
        public DateTime Date { get; }
        public string BodyworkCondition { get; }
        public string BrakeCondition { get; }
        public string DamperCondition { get; }
        public string WeelsCondition { get; }
        public Car car { get; }


        public TecnoMechanics(DateTime date, string bodyworkCondition, string brakeCondition, string damperCondition,
            string weelsCondition, Car car)
        {
            Date = date;
            BodyworkCondition = bodyworkCondition;
            BrakeCondition = brakeCondition;
            DamperCondition = damperCondition;
            WeelsCondition = weelsCondition;
            this.car = car;
        }
    }
}