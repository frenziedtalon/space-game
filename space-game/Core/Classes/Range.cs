using System;
using System.Collections.Generic;

namespace Core.Classes
{
    public class Range
    {

        private readonly List<double> _values = new List<double>();

        private bool _calculated;
        public Range(double defaultLowerBound, double defaultUpperBound)
        {
            if (defaultLowerBound > defaultUpperBound)
            {
                throw new ArgumentOutOfRangeException($"{nameof(defaultUpperBound)} should be greater than {nameof(defaultLowerBound)}");
            }
            _lowerBound = defaultLowerBound;
            _upperBound = defaultUpperBound;
            _calculated = true;
        }

        public void AddValue(double value)
        {
            _values.Add(value);
            _calculated = false;
        }

        private double _lowerBound;
        public double LowerBound => GetLowerBound();

        protected double GetLowerBound()
        {
            if (!_calculated)
            {
                CalculateValues();
            }
            return _lowerBound;
        }

        private double _upperBound;
        public double UpperBound => GetUpperBound();

        protected double GetUpperBound()
        {
            if (!_calculated)
            {
                CalculateValues();
            }
            return _upperBound;
        }

        private void CalculateValues()
        {
            _values.Sort();
            _lowerBound = _values[0];
            _upperBound = _values[_values.Count - 1];
            _calculated = true;
        }
    }
}
