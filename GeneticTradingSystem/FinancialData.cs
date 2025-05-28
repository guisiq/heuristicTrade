using System;

namespace GeneticTradingSystem
{
    /// <summary>
    /// Represents a single Open, High, Low, Close (OHLC) data point for a financial instrument.
    /// </summary>
    public class OHLCDataPoint
    {
        /// <summary>
        /// Gets or sets the timestamp of the data point.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the opening price.
        /// </summary>
        public double Open { get; set; }

        /// <summary>
        /// Gets or sets the highest price during the period.
        /// </summary>
        public double High { get; set; }

        /// <summary>
        /// Gets or sets the lowest price during the period.
        /// </summary>
        public double Low { get; set; }

        /// <summary>
        /// Gets or sets the closing price.
        /// </summary>
        public double Close { get; set; }

        /// <summary>
        /// Gets or sets the trading volume.
        /// </summary>
        public double Volume { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OHLCDataPoint"/> class.
        /// </summary>
        /// <param name="date">The timestamp of the data point.</param>
        /// <param name="open">The opening price.</param>
        /// <param name="high">The highest price.</param>
        /// <param name="low">The lowest price.</param>
        /// <param name="close">The closing price.</param>
        /// <param name="volume">The trading volume.</param>
        public OHLCDataPoint(DateTime date, double open, double high, double low, double close, double volume)
        {
            Date = date;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Volume = volume;
        }
    }
}
