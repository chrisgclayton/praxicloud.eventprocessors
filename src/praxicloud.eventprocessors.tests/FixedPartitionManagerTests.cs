// Copyright (c) Christopher Clayton. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace praxicloud.eventprocessors.tests
{
    using Microsoft.Azure.EventHubs;
    using Microsoft.Extensions.Logging;
    #region Using Clauses
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;
    #endregion

    /// <summary>
    /// Tests associated with the fixed partition manager
    /// </summary>
    [TestClass]
    public class FixedPartitionManagerTests
    {
        [TestMethod]
        public void SimpleManagerTest()
        {
            var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

            // Retrieve the partition information from the Event or IoT Hub
            var client = EventHubClient.CreateFromConnectionString(connectionString);
            client.RetryPolicy = new RetryExponential(TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(10), 10);
            var runtimeInformation = client.GetRuntimeInformationAsync().GetAwaiter().GetResult();
            var partitions = runtimeInformation.PartitionIds.OrderBy(item => item).ToArray();

            var logger = new LoggerFactory().CreateLogger("test");            
            var manager = new FixedPartitionManager(logger, connectionString, 0);

            manager.InitializeAsync(2).GetAwaiter().GetResult();

            var managerPartitions = manager.GetPartitions();
            var managerOwnedPartitions = manager.GetPartitions();
            var isOwned = manager.IsOwner("0");

            Assert.IsTrue(managerPartitions.Length == partitions.Length, "Partition counts should match");
            Assert.IsTrue(managerPartitions.Intersect(partitions).ToArray().Length == partitions.Length, "Intersecting partition should include all partitions");

            var partitionHalf = (int)(partitions.Length / 2) - 1;

            Assert.IsTrue(Math.Abs(managerOwnedPartitions.Length - partitionHalf) <= 1, "Owned partitions should be approximately half in case of odd numbers");
            Assert.IsTrue(Math.Abs(managerPartitions.Intersect(managerOwnedPartitions).ToArray().Length - partitionHalf) <= 1, "Intersecting owned partition should include all partitions");
            Assert.IsTrue(isOwned, "Ownership of partition 0 is expected based on the known algorithm. If the algorithm of modulo fails this needs adjusted");
        }
    }
}
