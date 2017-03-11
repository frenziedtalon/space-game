using Core.Classes;
using System.Collections.Generic;

namespace OrbitalMechanics.Interfaces
{
    public interface ICelestialObject
    {
        Mass Mass { get; }
        string Name { get; }
        List<Texture> Textures { get; }
    }
}
