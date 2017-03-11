using Newtonsoft.Json;

namespace OrbitalMechanics.Interfaces
{
    public interface I3DObject
    {
        [JsonIgnore]
        double Volume { get; }
    }
}
