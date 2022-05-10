using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace PudelkoLibrary {
    public sealed class Pudelko : IFormattable, IEquatable<Pudelko>, IEnumerable<double>, IComparable<Pudelko> {
        private double a;
        private double b;
        private double c;
        public UnitOfMeasure Unit { get; set; } = UnitOfMeasure.meter;
        public double A
        {
            get => Convert.ToDouble(a.ToString("0.000"));
            private set { this.a = value; }
        }
        public double B
        {
            get => Convert.ToDouble(b.ToString("0.000"));
            private set { b = value; }
        }

        public double C
        {
            get => Convert.ToDouble(c.ToString("0.000"));
            private set { c = value; }
        }

        public double Objetosc { get => Math.Round(A * B * C, 9); }

        public double Pole { get => Math.Round(2 * (A * C + A * B + B * C), 6); }

        private double ConvertToMeters(double a) {
            return Math.Floor((a / (double)Unit) * 1000.0) / 1000.0; 
        }

        public Pudelko(double? a = null, double? b = null, double? c = null, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            A = (double)(a != null ? (rNumber((double)a / (ushort)unit)) : 0.1);
            B = (double)(b != null ? (rNumber((double)b / (ushort)unit)) : 0.1);
            C = (double)(c != null ? (rNumber((double)c / (ushort)unit)) : 0.1);

            if (A <= 0 | A > 10 | B <= 0 | B > 10 | C <= 0 | C > 10)
            {
                throw new ArgumentOutOfRangeException();
            }      
        }

        private double rNumber(double number)
        {
            return Math.Floor(number * Math.Pow(10, 3)) / Math.Pow(10, 3);
        }


        public override string ToString() {
            return ToString("m");
        }

        public string ToString(string format) {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider) {
            if(String.IsNullOrEmpty(format)) format = "m";
            if(formatProvider == null) formatProvider = CultureInfo.CurrentCulture;
            format = format.ToLowerInvariant();
            UnitOfMeasure unit = UnitOfMeasureMethods.GetUnitOfMeasure(format);
            double multiply = (double)unit;
            string round = unit == UnitOfMeasure.centimeter ? "F1" : (unit == UnitOfMeasure.milimeter ? "F0" : "F3");
            return $"{(A * multiply).ToString(round, formatProvider)} {format} × {(B * multiply).ToString(round, formatProvider)} {format} × {(C*multiply).ToString(round, formatProvider)} {format}";
        }

        public bool Equals(Pudelko other) {
            return A + B + C == other.A + other.B + other.C && Objetosc == other.Objetosc && Pole == other.Pole;
        }

        public override bool Equals(object obj) {
            return obj is Pudelko ? Equals((Pudelko)obj) : base.Equals(obj);
        }
       

        public override int GetHashCode() {
            return A.GetHashCode() + B.GetHashCode() + C.GetHashCode() + Unit.GetHashCode();
        }

        public int CompareTo(Pudelko pudelko)
        {
            double objetoscP1 = Objetosc;
            double objetoscP2 = pudelko.Objetosc;

            if (objetoscP1 == objetoscP2)
            {
                double poleP1 = Pole;
                double poleP2 = pudelko.Pole;

                if (poleP1 == poleP2)
                {
                    double sumABCP1 = A + B +C;
                    double sumABCP2 = pudelko.A + pudelko.B + pudelko.C;

                    if (sumABCP1 == sumABCP2)
                        return 0;

                    return sumABCP1 < sumABCP2 ? 1 : -1;
                }

                return poleP1 < poleP2 ? 1 : -1;
            }

            return (objetoscP1 < objetoscP2) ? 1 : -1;
        }

        public IEnumerator<double> GetEnumerator() {
            return new PudelkoEnum(this);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public static bool operator == (Pudelko p1, Pudelko p2) => p1.Equals(p2);
        public static bool operator != (Pudelko p1, Pudelko p2) => !p1.Equals(p2);
        public static explicit operator double[](Pudelko p) => new double[] {p.A, p.B, p.C};
        public static implicit operator Pudelko(ValueTuple<double, double, double> pudelko) =>
            new Pudelko(pudelko.Item1, pudelko.Item2, pudelko.Item3, UnitOfMeasure.milimeter);
        public static Pudelko operator + (Pudelko p1, Pudelko p2) {
            double[] p1Lengths = (double[])p1, p2Lengths = (double[])p2;
            Array.Sort(p1Lengths);
            Array.Sort(p2Lengths);
            return new Pudelko(
                p1Lengths[0] + p2Lengths[0],
                p1Lengths[1] + p2Lengths[1],
                p1Lengths[2] + p2Lengths[2]
            );
        }

        public double this[int index] {
            get {
                if(index < 3) {
                    return ((double[])this)[index];
                } else {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        public static Pudelko Parse(string pudelkoString) {
            string[] parts = pudelkoString.Split('×');
            double[] lengths = new double[3];
            string unit = "m";
            for(int i = 0; i < parts.Length; ++i) {
                string[] length = parts[i].Split(' ');
                lengths[i] = Double.Parse(length[0]);
                unit = length[1];
            }
            return new Pudelko(lengths[0], lengths[1], lengths[2], UnitOfMeasureMethods.GetUnitOfMeasure(unit));
        }
    }
}
