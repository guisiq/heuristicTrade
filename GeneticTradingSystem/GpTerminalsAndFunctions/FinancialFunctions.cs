using System;

namespace GeneticTradingSystem.GpTerminalsAndFunctions
{
    /// <summary>
    /// Provides placeholder methods that represent financial functions for Genetic Programming.
    /// These will be adapted or wrapped for use with HeuristicLab.
    /// Functions typically operate on one or more input values.
    /// </summary>
    public static class FinancialFunctions
    {
        #region Arithmetic Functions

        /// <summary>
        /// Adds two double values.
        /// </summary>
        /// <param name="a">The first value.</param>
        /// <param name="b">The second value.</param>
        /// <returns>The sum of a and b.</returns>
        public static double Add(double a, double b) => a + b;

        /// <summary>
        /// Subtracts the second double value from the first.
        /// </summary>
        /// <param name="a">The value to subtract from.</param>
        /// <param name="b">The value to subtract.</param>
        /// <returns>The result of a - b.</returns>
        public static double Subtract(double a, double b) => a - b;

        /// <summary>
        /// Multiplies two double values.
        /// </summary>
        /// <param name="a">The first value.</param>
        /// <param name="b">The second value.</param>
        /// <returns>The product of a and b.</returns>
        public static double Multiply(double a, double b) => a * b;

        /// <summary>
        /// Divides the first double value by the second. Implements safe division.
        /// </summary>
        /// <param name="a">The dividend.</param>
        /// <param name="b">The divisor.</param>
        /// <returns>The result of a / b. Returns 1 if b is 0 to prevent division by zero errors.</returns>
        public static double Divide(double a, double b) => (b == 0) ? 1.0 : a / b;

        #endregion

        #region Conditional Functions

        /// <summary>
        /// Returns one of two values based on a boolean condition.
        /// </summary>
        /// <param name="condition">The boolean condition to evaluate.</param>
        /// <param name="thenValue">The value to return if the condition is true.</param>
        /// <param name="elseValue">The value to return if the condition is false.</param>
        /// <returns>thenValue if condition is true, otherwise elseValue.</returns>
        public static double IfElse(bool condition, double thenValue, double elseValue) => condition ? thenValue : elseValue;

        #endregion

        #region Logical Functions
        // Note: For GP, logical operations often work with doubles,
        // where a convention (e.g., > 0 is true) is used.
        // The return type is bool for direct use in IfElse, or could be double (1.0 for true, 0.0 for false).

        /// <summary>
        /// Performs a logical AND operation. Considers positive values as true.
        /// </summary>
        /// <param name="a">The first value. Considered true if > 0.</param>
        /// <param name="b">The second value. Considered true if > 0.</param>
        /// <returns>True if both a and b are considered true, otherwise false.</returns>
        public static bool And(double a, double b) => (a > 0) && (b > 0);

        /// <summary>
        /// Performs a logical OR operation. Considers positive values as true.
        /// </summary>
        /// <param name="a">The first value. Considered true if > 0.</param>
        /// <param name="b">The second value. Considered true if > 0.</param>
        /// <returns>True if either a or b (or both) are considered true, otherwise false.</returns>
        public static bool Or(double a, double b) => (a > 0) || (b > 0);

        /// <summary>
        /// Performs a logical NOT operation. Considers positive values as true.
        /// </summary>
        /// <param name="a">The value to negate. Considered true if > 0.</param>
        /// <returns>True if a is considered false, otherwise false.</returns>
        public static bool Not(double a) => !(a > 0);

        /// <summary>
        /// Checks if the first value is greater than the second.
        /// </summary>
        /// <param name="a">The first value.</param>
        /// <param name="b">The second value.</param>
        /// <returns>True if a is greater than b, otherwise false.</returns>
        public static bool GreaterThan(double a, double b) => a > b;

        /// <summary>
        /// Checks if the first value is less than the second.
        /// </summary>
        /// <param name="a">The first value.</param>
        /// <param name="b">The second value.</param>
        /// <returns>True if a is less than b, otherwise false.</returns>
        public static bool LessThan(double a, double b) => a < b;

        /// <summary>
        /// Checks if the first value is equal to the second.
        /// Note: Direct comparison of doubles can be problematic due to precision.
        /// Consider using a tolerance if approximate equality is needed.
        /// </summary>
        /// <param name="a">The first value.</param>
        /// <param name="b">The second value.</param>
        /// <returns>True if a is equal to b, otherwise false.</returns>
        public static bool Equals(double a, double b) => a == b;

        #endregion
    }
}
