using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Units
{
    internal static class UnitOfMeasureParser
    {
        internal static bool TryParseSimpleUnit<T>(string inputUnit, IEnumerable<T> units, out T unit) where T : UnitOfMeasure<T>
        {
            var input = inputUnit.ToLower();
            unit = null;

            var matchedUnit = units.FirstOrDefault(u => u.ShortName.ToLowerInvariant() == input || u.LongName.ToLowerInvariant() == input);

            if (matchedUnit == null)
                return false;

            unit = matchedUnit;
            return true;
        }

        private static readonly Regex SimpleUomParser = new Regex(@"^(?<value>[+-]?(([1-9][0-9]*)?[0-9](\.[0-9]*)?|\.[0-9]+))(\s*)(?<unit>\w+)$");

        internal static bool TryParseSimpleValue<T>(string input, IEnumerable<T> units, Func<decimal, T, T> factory, out T distance) where T : UnitOfMeasure<T>
        {
            distance = null;

            var decimalPart = SimpleUomParser.Match(input).Groups["value"];
            var unitPart = SimpleUomParser.Match(input).Groups["unit"];

            var valueStringMatched = decimal.TryParse(decimalPart.Value, out var value);
            var unitStringMatched = TryParseSimpleUnit<T>(unitPart.Value, units, out var unit);

            if (!valueStringMatched || !unitStringMatched)
                return false;

            distance = factory(value, unit);
            return true;
        }
    }
}