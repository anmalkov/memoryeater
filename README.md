# MemoryEater

This is a demo app created in C# that eats X Megabytes of memory each Y seconds.  It could be used as an example in Kubernetes monitoring and troubleshooting.

# How to use

To configure this tool you can specify two environment variable:
- `MEMORYEATER_EATMEGABYTES` - Numeric value in Megabytes to specufy how many Megabytes tool will eat on each interval (default 1).
- `MEMORYEATER_DELAYINSECONDS` - Numeric value in seconds to specify an interval (default 1).

This tool is also available as a container image in Docker Hub [Memoryeater on Docker Hub](https://hub.docker.com/repository/docker/anmalkov/memoryeater).
Please look at Examples section below to see how you can use a container image. 

### Note
By default dotnet will kill your application when it comsumed about a half of available mmemory. To prevent this behavior you can specify additional environment variables:   

#### Heap limit
Specifies the maximum commit size, in bytes, for the GC heap and GC bookkeeping.

Environment variables:
- `COMPlus_GCHeapHardLimit` - for .NET Core 3.0, 3.1 and .NET 5
- `DOTNET_GCHeapHardLimit` - for .NET 6

The value for these variables is in bytes and have to be provided in hexadecimal format: 
- 200 Megabytes - 200 * 1024 * 1024 = 209715200 bytes or C800000 in hexadecimal format - DOTNET_GCHeapHardLimit=C800000
- 500 Megabytes - 500 * 1024 * 1024 = 524288000 bytes or 1F400000 in hexadecimal format - DOTNET_GCHeapHardLimit=1F400000
- 1 Gigabytes (1024 Megabytes) - 1024 * 1024 * 1024 = 1073741824 bytes or 40000000 in hexadecimal format - DOTNET_GCHeapHardLimit=40000000
- 2 Gigabytes (2048 Megabytes) - 2048 * 1024 * 1024 = 2147483648 bytes or 80000000 in hexadecimal format - DOTNET_GCHeapHardLimit=80000000

#### Heap limit percent
Specifies the allowable GC heap usage as a percentage of the total physical memory.

Environment variables:
- `COMPlus_GCHeapHardLimitPercent` - for .NET Core 3.0, 3.1 and .NET 5
- `DOTNET_GCHeapHardLimitPercent` - for .NET 6

The value for these variables is in percents and have to be provided in hexadecimal format: 
- 50% or 32 in hexadecimal format - DOTNET_GCHeapHardLimitPercent=32
- 75% or 4B in hexadecimal format - DOTNET_GCHeapHardLimitPercent=4B
- 100% or 64 in hexadecimal format - DOTNET_GCHeapHardLimitPercent=64

# Examples

Eat 100 Megabytes each 2 seconds:

```console
docker run -e MEMORYEATER_EATMEGABYTES=100 -e MEMORYEATER_DELAYINSECONDS=2 anmalkov/memoryeater
```

Eat 1 Gigabyte per second:

```console
docker run -e MEMORYEATER_EATMEGABYTES=1024 anmalkov/memoryeater
```

Set memory limit to a container to 1 Gigabyte, eat 100 Megabytes each second and remove .NET memory consumption limits:

```console
docker run -m 1g --memory-swap 1g -e MEMORYEATER_EATMEGABYTES=100 -e DOTNET_GCHeapHardLimit=80000000 -e DOTNET_GCHeapHardLimitPercent=64 anmalkov/memoryeater
```

