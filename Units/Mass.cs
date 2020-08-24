using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Units
{
    public class Mass : UnitOfMeasure<Mass>
    {
        public static Mass Gram = new Mass(1m, 0.001m, "g", "Gram");
        public static Mass Kilogram = new Mass(1m, 1m, "kg", "Kilogram");

        public static readonly IEnumerable<Mass> AllUnits = new[]
        {
            Gram,
            Kilogram
        };

        public static Mass Create(decimal value, Mass unit) =>
            new Mass(value, unit.Ratio, unit.ShortName, unit.LongName, unit.Aliases);

        protected Mass(decimal value, decimal ratio, string shortName, string longName, params string[] aliases) :
            base(value, shortName, longName, value * ratio, aliases)
        {
            Ratio = ratio;
        }

        public override Mass ConvertTo(Mass target)
        {
            if (Unit.Equals(target.Unit))
                return this;

            var targetMass = ValueInBaseUnit / target.Ratio;

            return Create(targetMass, target);
        }

        public override Mass Unit => new Mass(1m, Ratio, ShortName, LongName);

        private decimal Ratio { get; }

        public static bool TryParseUnit(string input, out Mass unitMass) =>
            UnitOfMeasureParser.TryParseSimpleUnit(input, AllUnits, out unitMass);

        public static bool TryParse(string input, out Mass mass) =>
            UnitOfMeasureParser.TryParseSimpleValue(input, AllUnits, Create, out mass);
    }
}
