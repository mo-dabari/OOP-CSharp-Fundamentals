namespace Polymorphism.Examples.Basic
{
    // ════════════════════════════════════════════════════════════
    // 3. OPERATOR OVERLOADING
    // ════════════════════════════════════════════════════════════

    /// <summary>
    /// الكسر - مثال على Operator Overloading
    /// </summary>
    public class Fraction
    {
        public int Numerator { get; set; }
        public int Denominator { get; set; }

        public Fraction(int numerator, int denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }

        // تعريف +
        public static Fraction operator +(Fraction a, Fraction b)
        {
            int num = a.Numerator * b.Denominator + b.Numerator * a.Denominator;
            int den = a.Denominator * b.Denominator;
            return new Fraction(num, den);
        }

        // تعريف -
        public static Fraction operator -(Fraction a, Fraction b)
        {
            int num = a.Numerator * b.Denominator - b.Numerator * a.Denominator;
            int den = a.Denominator * b.Denominator;
            return new Fraction(num, den);
        }

        // تعريف *
        public static Fraction operator *(Fraction a, Fraction b)
        {
            return new Fraction(
                a.Numerator * b.Numerator,
                a.Denominator * b.Denominator);
        }

        // تعريف ==
        public static bool operator ==(Fraction a, Fraction b)
        {
            return a.Numerator * b.Denominator ==
                   b.Numerator * a.Denominator;
        }

        // تعريف !=
        public static bool operator !=(Fraction a, Fraction b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return $"{Numerator}/{Denominator}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Fraction f)
                return this == f;
            return false;
        }

        public override int GetHashCode()
        {
            return (Numerator, Denominator).GetHashCode();
        }
    }
}
