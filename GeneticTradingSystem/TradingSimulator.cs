using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticTradingSystem
{
    /// <summary>
    /// Represents performance metrics of a trading strategy.
    /// </summary>
    public class PortfolioMetrics
    {
        /// <summary>
        /// Gets the total return of the strategy as a percentage.
        /// </summary>
        public double TotalReturn { get; }

        /// <summary>
        /// Gets the Sharpe Ratio of the strategy.
        /// </summary>
        public double SharpeRatio { get; }

        /// <summary>
        /// Gets the total number of trades executed.
        /// </summary>
        public int TotalTrades { get; }

        /// <summary>
        /// Gets the history of portfolio values over the simulation period.
        /// </summary>
        public List<double> PortfolioValueOverTime { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PortfolioMetrics"/> class.
        /// </summary>
        /// <param name="totalReturn">The total return.</param>
        /// <param name="sharpeRatio">The Sharpe Ratio.</param>
        /// <param name="totalTrades">The total number of trades.</param>
        /// <param name="portfolioValueOverTime">The history of portfolio values.</param>
        public PortfolioMetrics(double totalReturn, double sharpeRatio, int totalTrades, List<double> portfolioValueOverTime)
        {
            TotalReturn = totalReturn;
            SharpeRatio = sharpeRatio;
            TotalTrades = totalTrades;
            PortfolioValueOverTime = portfolioValueOverTime ?? new List<double>();
        }
    }

    /// <summary>
    /// Simulates a trading strategy over historical financial data.
    /// </summary>
    public class TradingSimulator
    {
        /// <summary>
        /// Gets the historical OHLC data used for the simulation.
        /// </summary>
        public List<OHLCDataPoint> HistoricalData { get; private set; }

        /// <summary>
        /// Gets the initial capital for the simulation.
        /// </summary>
        public double InitialCapital { get; private set; }

        /// <summary>
        /// Gets the commission charged per trade.
        /// </summary>
        public double CommissionPerTrade { get; private set; }

        /// <summary>
        /// Gets the slippage percentage applied to trades.
        /// </summary>
        public double SlippagePercent { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TradingSimulator"/> class.
        /// </summary>
        /// <param name="historicalData">The list of historical OHLC data points.</param>
        /// <param name="initialCapital">The starting capital for the simulation.</param>
        /// <param name="commissionPerTrade">The commission fee for each trade. Defaults to 0.</param>
        /// <param name="slippagePercent">The slippage as a percentage of the trade price (e.g., 0.001 for 0.1%). Defaults to 0.</param>
        /// <exception cref="ArgumentNullException">Thrown if historicalData is null.</exception>
        /// <exception cref="ArgumentException">Thrown if historicalData is empty or initialCapital is not positive.</exception>
        public TradingSimulator(List<OHLCDataPoint> historicalData, double initialCapital, double commissionPerTrade = 0, double slippagePercent = 0)
        {
            if (historicalData == null)
                throw new ArgumentNullException(nameof(historicalData));
            if (!historicalData.Any())
                throw new ArgumentException("Historical data cannot be empty.", nameof(historicalData));
            if (initialCapital <= 0)
                throw new ArgumentException("Initial capital must be positive.", nameof(initialCapital));

            HistoricalData = historicalData;
            InitialCapital = initialCapital;
            CommissionPerTrade = commissionPerTrade;
            SlippagePercent = slippagePercent;
        }

        /// <summary>
        /// Evaluates a trading strategy over the historical data.
        /// </summary>
        /// <param name="strategyLogic">
        /// A function delegate representing the evolved trading strategy. 
        /// It takes the current OHLC bar, all historical close prices, and the current index, 
        /// and returns a TradingAction. This is a placeholder for HeuristicLab tree evaluation.
        /// </param>
        /// <param name="allClosePrices">A list of all historical closing prices, used by indicators within the strategy logic.</param>
        /// <returns>A <see cref="PortfolioMetrics"/> object containing the results of the simulation.</returns>
        public PortfolioMetrics EvaluateStrategy(Func<OHLCDataPoint, List<double>, int, TradingAction> strategyLogic, List<double> allClosePrices)
        {
            double currentCapital = InitialCapital;
            double sharesHeld = 0;
            var portfolioValueOverTime = new List<double> { InitialCapital };
            int totalTrades = 0;

            for (int currentIndex = 0; currentIndex < HistoricalData.Count; currentIndex++)
            {
                OHLCDataPoint currentBar = HistoricalData[currentIndex];
                TradingAction action = strategyLogic(currentBar, allClosePrices, currentIndex);

                if (action == TradingAction.Buy && sharesHeld == 0 && currentCapital > 0)
                {
                    double buyPrice = currentBar.Close * (1 + SlippagePercent);
                    if ((currentCapital - CommissionPerTrade) <= 0) // Cannot afford commission
                    {
                         // Update portfolio value even if no trade occurs
                        portfolioValueOverTime.Add(currentCapital + (sharesHeld * currentBar.Close));
                        continue;
                    }

                    double sharesToBuy = (currentCapital - CommissionPerTrade) / buyPrice;
                    if (sharesToBuy <= 0) // Cannot afford shares
                    {
                         // Update portfolio value even if no trade occurs
                        portfolioValueOverTime.Add(currentCapital + (sharesHeld * currentBar.Close));
                        continue;
                    }
                        
                    sharesHeld = sharesToBuy;
                    currentCapital -= (sharesToBuy * buyPrice) + CommissionPerTrade;
                    totalTrades++;
                }
                else if (action == TradingAction.Sell && sharesHeld > 0)
                {
                    double sellPrice = currentBar.Close * (1 - SlippagePercent);
                    currentCapital += (sharesHeld * sellPrice) - CommissionPerTrade;
                    sharesHeld = 0;
                    totalTrades++;
                }

                double currentPortfolioValue = currentCapital + (sharesHeld * currentBar.Close);
                portfolioValueOverTime.Add(currentPortfolioValue);
            }

            double finalPortfolioValue = portfolioValueOverTime.LastOrDefault();
            if (!portfolioValueOverTime.Any()) finalPortfolioValue = InitialCapital; // Should not happen if initialized

            double totalReturn = (InitialCapital == 0) ? 0 : (finalPortfolioValue / InitialCapital) - 1;

            double sharpeRatio = 0;
            if (portfolioValueOverTime.Count >= 2)
            {
                var dailyReturns = new List<double>();
                for (int i = 1; i < portfolioValueOverTime.Count; i++)
                {
                    if (portfolioValueOverTime[i - 1] == 0) dailyReturns.Add(0); // Avoid division by zero
                    else dailyReturns.Add((portfolioValueOverTime[i] / portfolioValueOverTime[i - 1]) - 1);
                }

                if (dailyReturns.Any())
                {
                    double averageDailyReturn = dailyReturns.Average();
                    double stdDevDailyReturn = CalculateStandardDeviation(dailyReturns);
                    sharpeRatio = (stdDevDailyReturn == 0) ? 0 : (averageDailyReturn / stdDevDailyReturn) * Math.Sqrt(252); // Assuming 252 trading days
                }
            }

            return new PortfolioMetrics(totalReturn, sharpeRatio, totalTrades, portfolioValueOverTime);
        }

        /// <summary>
        /// Calculates the standard deviation of a list of double values.
        /// </summary>
        /// <param name="values">The list of values.</param>
        /// <returns>The standard deviation. Returns 0 if the list has fewer than 2 elements.</returns>
        private static double CalculateStandardDeviation(List<double> values)
        {
            if (values == null || values.Count < 2)
            {
                return 0;
            }

            double mean = values.Average();
            double sumOfSquares = values.Sum(val => (val - mean) * (val - mean));
            return Math.Sqrt(sumOfSquares / (values.Count - 1));
        }
    }
}
