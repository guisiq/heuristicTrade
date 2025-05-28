using System;

namespace GeneticTradingSystem
{
    /// <summary>
    /// Defines the possible trading actions.
    /// </summary>
    public enum TradingAction
    {
        /// <summary>
        /// Represents a decision to buy.
        /// </summary>
        Buy,

        /// <summary>
        /// Represents a decision to sell.
        /// </summary>
        Sell,

        /// <summary>
        /// Represents a decision to hold (do nothing).
        /// </summary>
        Hold
    }

    /// <summary>
    /// Provides methods to evaluate the output of a Genetic Programming (GP) trading strategy.
    /// </summary>
    public static class StrategyEvaluator
    {
        /// <summary>
        /// Converts the raw output value from a GP program into a specific trading action.
        /// </summary>
        /// <param name="outputValue">The raw double value output by the GP-evolved trading rule.</param>
        /// <returns>A TradingAction (Buy, Sell, or Hold) based on the outputValue.</returns>
        /// <remarks>
        /// The interpretation is as follows:
        /// - If outputValue > 0.5, it signals a Buy action.
        /// - If outputValue < -0.5, it signals a Sell action.
        /// - Otherwise (if outputValue is between -0.5 and 0.5, inclusive), it signals a Hold action.
        /// These thresholds (0.5 and -0.5) can be adjusted based on strategy tuning.
        /// </remarks>
        public static TradingAction GetActionFromProgramOutput(double outputValue)
        {
            if (outputValue > 0.5)
            {
                return TradingAction.Buy;
            }
            else if (outputValue < -0.5)
            {
                return TradingAction.Sell;
            }
            else
            {
                return TradingAction.Hold;
            }
        }
    }
}
