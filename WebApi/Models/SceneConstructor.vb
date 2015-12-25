Imports OrbitalMechanics
Imports OrbitalMechanics.CelestialObjects
Imports OrbitalMechanics.Classes

Namespace Models
    Public Class SceneConstructor

        Public Function SolSystem() As SolarSystem

            Dim objects = New List(Of ICelestialObject)

            objects.Add(New Star("Sol", 1, 5500, "sun.jpg", 20, New Orbit(0, 0, 0)))

            objects.Add(New Planet("Mercury", 1, "mercury.jpg", 2, New Orbit(40, 0.004, 0)))

            objects.Add(New Planet("Venus", 1, "venus.jpg", 4, New Orbit(80, 0.0036, 0)))

            Dim moons As New List(Of Moon)
            moons.Add(New Moon("Moon", 1, "moon.png", 1, New Orbit(8, 0.02, 0)))

            objects.Add(New Planet("Earth", 1, "earth.jpg", 5, New Orbit(120, 0.0034, 0), moons))

            objects.Add(New Planet("Mars", 1, "mars.jpg", 5, New Orbit(160, 0.003, 0)))

            objects.Add(New Planet("Jupiter", 1, "jupiter.jpg", 10, New Orbit(200, 0.0028, 0)))

            objects.Add(New Planet("Saturn", 1, "saturn.jpg", 7, New Orbit(240, 0.0025, 0)))

            objects.Add(New Planet("Uranus", 1, "uranus.jpg", 3, New Orbit(280, 0.002, 0)))

            objects.Add(New Planet("Neptune", 1, "neptune.jpg", 5, New Orbit(320, 0.0015, 0)))

            objects.Add(New Planet("Pluto", 1, "pluto.jpg", 4, New Orbit(360, 0.001, 0)))

            Return New SolarSystem(objects)
        End Function


    End Class
End Namespace