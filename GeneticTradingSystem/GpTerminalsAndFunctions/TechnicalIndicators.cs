using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticTradingSystem.GpTerminalsAndFunctions
{
    /// <summary>
    /// Provides placeholder methods for calculating common financial technical indicators.
    /// These methods will be used by the Genetic Programming system, potentially wrapped as HeuristicLab functions.
    /// </summary>
    public static class TechnicalIndicators
    {
        /// <summary>
        /// Calculates the Simple Moving Average (SMA).
        /// </summary>
        /// <param name="prices">A list of price values (e.g., closing prices).</param>
        /// <param name="currentIndex">The current index in the price list for which to calculate the SMA.</param>
        /// <param name="period">The number of periods to consider for the SMA.</param>
        /// <returns>The SMA value. If there are not enough data points for the given period, 
        /// it returns the current price or 0 if the price list is empty or current index is out of bounds.</returns>
        public static double SMA(List<double> prices, int currentIndex, int period)
        {
            if (prices == null || !prices.Any() || period <= 0 || currentIndex < 0 || currentIndex >= prices.Count)
            {
                return 0; // Or handle error appropriately
            }

            if (currentIndex < period - 1)
            {
                // Not enough data for the period, return current price as a fallback
                return prices[currentIndex];
            }

            double sum = 0;
            for (int i = 0; i < period; i++)
            {
                sum += prices[currentIndex - i];
            }
            return sum / period;
        }

        /// <summary>
        /// Calculates the Relative Strength Index (RSI).
        /// </summary>
        /// <param name="prices">A list of price values (e.g., closing prices).</param>
        /// <param name="currentIndex">The current index in the price list for which to calculate the RSI.</param>
        /// <param name="period">The number of periods to consider for the RSI (typically 14).</param>
        /// <returns>The RSI value (between 0 and 100). 
        /// Returns 50.0 (neutral) if there are not enough data points for the given period.</returns>
        public static double RSI(List<double> prices, int currentIndex, int period)
        {
            if (prices == null || period <= 0 || currentIndex < period || currentIndex >= prices.Count)
            {
                return 50.0; // Neutral RSI value if not enough data
            }

            var changes = new List<double>();
            for (int i = 1; i <= period; i++)
            {
                changes.Add(prices[currentIndex - period + i] - prices[currentIndex - period + i - 1]);
            }

            double averageGain = 0;
            double averageLoss = 0;
            int gainCount = 0;
            int lossCount = 0;

            foreach (var change in changes)
            {
                if (change > 0)
                {
                    averageGain += change;
                    gainCount++;
                }
                else
                {
                    averageLoss += Math.Abs(change); // Loss is positive
                    lossCount++;
                }
            }
            
            // Calculate initial average gain and loss
            // If there are no gains or no losses in the initial period, subsequent calculations might be skewed or result in NaN.
            // A common approach is to use simple average for the first period.
            if (gainCount > 0) averageGain /= period; // gainCount;
            else averageGain = 0;

            if (lossCount > 0) averageLoss /= period; // lossCount;
            else averageLoss = 0;


            // Smooth RSI for subsequent periods (not implemented here as we calculate for a single currentIndex)
            // For a single point RSI based on the initial period:
            if (averageLoss == 0)
            {
                return 100.0; // Max RSI if no losses
            }

            double rs = averageGain / averageLoss;
            return 100.0 - (100.0 / (1.0 + rs));
        }
    }
}
