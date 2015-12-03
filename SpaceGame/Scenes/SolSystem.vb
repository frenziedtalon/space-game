
Imports System.Windows.Media.Media3D
Imports OrbitalMechanics
Imports OrbitalMechanics.CelestialObjects
Imports OrbitalMechanics.Classes

Namespace Scenes
    Public Class SceneConstructor

        Public Function SolSystem() As SolarSystem

            Dim objects = New List(Of ICelestialObject)

            objects.Add(New Star("Sol", 1, 5500, "sun.jpg", 20, New Orbit(0, 0, 0)))

            Dim moons As New List(Of Moon)

            moons.Add(New Moon("Moon", 1, "moon.jpg", 8, New Orbit(8, 0.002, 0)))

            Dim earth As New Planet("Earth", 1, "earth.jpg", 5, New Orbit(120, 0.0034, 0), moons)

            objects.Add(earth)

            Return New SolarSystem(objects)
        End Function


    End Class
End Namespace