using Core.Classes;
using Newtonsoft.Json;

namespace OrbitalMechanics.Interfaces
{
    public interface ISphere : I3DObject
    {
        Distance Radius { get; }
        [JsonIgnore]

        int AxialTilt { get; }
        [JsonIgnore]

        int RotationSpeed { get; }
    }
}
