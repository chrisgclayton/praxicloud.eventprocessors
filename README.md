# PraxiCloud Event Processors
PraxiCloud Libraries are a set of common utilities and tools for general software development that simplify common development efforts for software development. The event processors library contains common components and elements that are applicable across the various event processor host supporting libraries in the tools.



# Installing via NuGet

Install-Package PraxiCloud-EventProcessors



# Partition Management



## Key Types and Interfaces

|Class| Description | Notes |
| ------------- | ------------- | ------------- |
|**FixedPartitionManager**|The fixed partition manager is used to determine ownership of the partitions of an Event Hub or IoT Hub. This builds on the string index manager types in the toolset. <br />***InitializeAsync*** prepares the callback and connects to the hub to retrieve the partition information.<br />***IsOwner*** can be used to determine whether a specific partition is the owner of a partition.<br />***IsOwnerAsync*** can be used to determine whether a specific partitions is the owner of a partition.<br />***GetOwnedPartitions*** returns a list of all partitions owned by the manager.<br />***GetPartitions*** returns a complete list of all partitions on the hub.<br />***UpdateManagerQuantityAsync*** changes the number of managers that share the responsibility of managing the partitions.| This must be called before the library will begin tracking ownership |

## Sample Usage

### Create Manager and Check Ownership

```csharp
var manager = new FixedPartitionManager(logger, connectionString, 0);

manager.InitializeAsync(2).GetAwaiter().GetResult();

var managerPartitions = manager.GetPartitions();
var managerOwnedPartitions = manager.GetPartitions();
var isOwned = manager.IsOwner("0");
```

## Additional Information

For additional information the Visual Studio generated documentation found [here](./documents/praxicloud.eventprocessors/praxicloud.eventprocessors.xml), can be viewed using your favorite documentation viewer.




