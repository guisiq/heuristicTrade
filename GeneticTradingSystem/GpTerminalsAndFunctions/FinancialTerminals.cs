using System;

namespace GeneticTradingSystem.GpTerminalsAndFunctions
{
    /// <summary>
    /// Provides placeholder methods that represent financial terminals for Genetic Programming.
    /// These will be adapted or wrapped for use with HeuristicLab.
    /// Terminals typically return a value from the current data context (e.g., an OHLCDataPoint).
    /// </summary>
    public static class FinancialTerminals
    {
        /// <summary>
        /// Gets the closing price from the current bar.
        /// </summary>
        /// <param name="currentBar">The current OHLC data point.</param>
        /// <returns>The closing price.</returns>
        public static double GetClosePrice(OHLCDataPoint currentBar) => currentBar.Close;

        /// <summary>
        /// Gets the opening price from the current bar.
        /// </summary>
        /// <param name="currentBar">The current OHLC data point.</param>
        /// <returns>The opening price.</returns>
        public static double GetOpenPrice(OHLCDataPoint currentBar) => currentBar.Open;

        /// <summary>
        /// Gets the highest price from the current bar.
        /// </summary>
        /// <param name="currentBar">The current OHLC data point.</param>
        /// <returns>The highest price.</returns>
        public static double GetHighPrice(OHLCDataPoint currentBar) => currentBar.High;

        /// <summary>
        /// Gets the lowest price from the current bar.
        /// </summary>
        /// <param name="currentBar">The current OHLC data point.</param>
        /// <returns>The lowest price.</returns>
        public static double GetLowPrice(OHLCDataPoint currentBar) => currentBar.Low;

        /// <summary>
        /// Gets the volume from the current bar.
        /// </summary>
        /// <param name="currentBar">The current OHLC data point.</param>
        /// <returns>The trading volume.</returns>
        public static double GetVolume(OHLCDataPoint currentBar) => currentBar.Volume;
    }
}
