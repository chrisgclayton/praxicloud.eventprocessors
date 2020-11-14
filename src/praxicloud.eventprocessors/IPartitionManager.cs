// Copyright (c) Christopher Clayton. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace praxicloud.eventprocessors
{
    /// <summary>
    /// A class that handles management of partitions, including retrieving lists of available partitions
    /// </summary>
    public interface IPartitionManager : IOwnershipLookup
    {
        /// <summary>
        /// Returns the ids of all partitions owned by the current manager
        /// </summary>
        /// <returns>An array of partition ids that are owned by the manager</returns>
        string[] GetOwnedPartitions();

        /// <summary>
        /// Returns the ids of all partitions 
        /// </summary>
        /// <returns>An array of partition ids</returns>
        string[] GetPartitions();
    }
}
