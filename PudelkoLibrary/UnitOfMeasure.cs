using System;
using System.Collections.Generic;
using System.Text;

namespace PudelkoLibrary {
    public enum UnitOfMeasure {
        milimeter = 1000, 
        centimeter = 100, 
        meter = 1
    }

    public class UnitOfMeasureMethods {
        public static UnitOfMeasure GetUnitOfMeasure(string unit) {
            switch(unit) {
                case "mm":
                    return UnitOfMeasure.milimeter;
                case "cm":
                    return UnitOfMeasure.centimeter;
                case "m":
                    return UnitOfMeasure.meter;
                default:
                    throw new FormatException();
            }
        }
    }
}
