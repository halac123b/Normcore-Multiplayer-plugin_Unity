using Normal.Realtime;
// Namespace hỗ trợ đồng bộ (serial) data qua network
using Normal.Realtime.Serialization;

[RealtimeModel]
public partial class StopwatchModel {
    [RealtimeProperty(1, true)] private double _startTime;
}

