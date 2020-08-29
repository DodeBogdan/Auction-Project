//-----------------------------------------------------------------------
// <copyright file="AppHelperTests.cs" company="Transilvania University of Brasov">
//     Copyright (c) Brassoi Silvia Maria. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Auction.Tests
{
    using System.Configuration;
    using AuctionLogic.Help;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>App helper tests class.</summary>
    [TestClass]
    public class AppHelperTests
    {
        /// <summary>Applications the helper default score value set properly.</summary>
        [TestMethod]
        public void AppHelper_DefaultScoreValue_SetProperly()
        {
            Assert.IsTrue(ApplicationHelp.DefaultScore.ToString() == ConfigurationManager.AppSettings["DefaultScore"]);
        }

        /// <summary>Applications the helper step value set properly.</summary>
        [TestMethod]
        public void AppHelper_StepValue_SetProperly()
        {
            Assert.IsTrue(ApplicationHelp.Step.ToString(string.Empty) == ConfigurationManager.AppSettings["Step"]);
        }

        /// <summary>Applications the helper banned days set properly.</summary>
        [TestMethod]
        public void AppHelper_BannedDays_SetProperly()
        {
            Assert.IsTrue(ApplicationHelp.BannedDays.ToString() == ConfigurationManager.AppSettings["BannedDays"]);
        }

        /// <summary>Applications the helper days to wait set properly.</summary>
        [TestMethod]
        public void AppHelper_DaysToWait_SetProperly()
        {
            Assert.IsTrue(ApplicationHelp.DaysToWait.ToString() == ConfigurationManager.AppSettings["DaysToWait"]);
        }

        /// <summary>Applications the helper last n scores set properly.</summary>
        [TestMethod]
        public void AppHelper_LastNScores_SetProperly()
        {
            Assert.IsTrue(ApplicationHelp.LastNScores.ToString() == ConfigurationManager.AppSettings["LastNScores"]);
        }

        /// <summary>Applications the helper minimum score set properly.</summary>
        [TestMethod]
        public void AppHelper_MinimumScore_SetProperly()
        {
            Assert.IsTrue(ApplicationHelp.MinimumScore.ToString() == ConfigurationManager.AppSettings["MinimumScore"]);
        }

        /// <summary>Applications the helper started and unfinished bids set properly.</summary>
        [TestMethod]
        public void AppHelper_StartedAndUnfinishedBids_SetProperly()
        {
            Assert.IsTrue(ApplicationHelp.StartedAndUnfinishedBids.ToString() == ConfigurationManager.AppSettings["StartedAndUnfinishedBids"]);
        }

        /// <summary>Applications the helper started and unfinished bids by category set properly.</summary>
        [TestMethod]
        public void AppHelper_StartedAndUnfinishedBidsByCategory_SetProperly()
        {
            Assert.IsTrue(ApplicationHelp.StartedAndUnfinishedBidsByCategory.ToString() == ConfigurationManager.AppSettings["StartedAndUnfinishedBidsByCategory"]);
        }
    }
}
