//-----------------------------------------------------------------------
// <copyright file="ApplicationHelp.cs" company="Transilvania University of Brasov">
//     Copyright (c) Brassoi Silvia Maria. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AuctionLogic.Help
{
    using System.Configuration;

    /// <summary>Helper class</summary>
    public class ApplicationHelp
    {
        /// <summary>Gets the default score.</summary>
        /// <value>The default score.</value>
        public static int DefaultScore { get; } = int.Parse(ConfigurationManager.AppSettings["DefaultScore"]);

        /// <summary>Gets the step.</summary>
        /// <value>The step.</value>
        public static double Step { get; } = double.Parse(ConfigurationManager.AppSettings["Step"]);

        /// <summary>Gets the started and unfinished bids.</summary>
        /// <value>The started and unfinished bids.</value>
        public static int StartedAndUnfinishedBids { get; } = int.Parse(ConfigurationManager.AppSettings["StartedAndUnfinishedBids"]);

        /// <summary>Gets the started and unfinished bids by category.</summary>
        /// <value>The started and unfinished bids by category.</value>
        public static int StartedAndUnfinishedBidsByCategory { get; } = int.Parse(ConfigurationManager.AppSettings["StartedAndUnfinishedBidsByCategory"]);

        /// <summary>Gets the last n scores.</summary>
        /// <value>The last n scores.</value>
        public static int LastNScores { get; } = int.Parse(ConfigurationManager.AppSettings["LastNScores"]);

        /// <summary>Gets the minimum score.</summary>
        /// <value>The minimum score.</value>
        public static int MinimumScore { get; } = int.Parse(ConfigurationManager.AppSettings["MinimumScore"]);

        /// <summary>Gets the banned days.</summary>
        /// <value>The banned days.</value>
        public static int BannedDays { get; } = int.Parse(ConfigurationManager.AppSettings["BannedDays"]);

        /// <summary>Gets the days to wait.</summary>
        /// <value>The days to wait.</value>
        public static int DaysToWait { get; } = int.Parse(ConfigurationManager.AppSettings["DaysToWait"]);
    }
}
