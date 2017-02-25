using Core.Classes;
using Data.Classes;
using System.Collections.Generic;

namespace Data
{
    public interface IPhysicalData
    {
        Mass Mass { get; set; }
        string Name { get; set; }
        Distance Radius { get; set; }
        List<Texture> Textures { get; set; }
        CelestialObjectType Type { get; set; }
    }
}