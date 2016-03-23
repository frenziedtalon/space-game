﻿Imports Entities
Imports OrbitalMechanics
Imports OrbitalMechanics.CelestialObjects
Imports OrbitalMechanics.Classes

Namespace Models
    Public Class SceneConstructor

        Public Function SolSystem(entityManager As IEntityManager) As SolarSystem

            Dim objects = New List(Of ICelestialObject)

            Dim planets As New List(Of OrbitingCelestialObjectBase)

            planets.Add(New Planet("Mercury", 1, "mercury.jpg", 2, New Orbit(40, 0.004, 0), entityManager))

            planets.Add(New Planet("Venus", 1, "venus.jpg", 4, New Orbit(80, 0.0036, 0), entityManager))

            Dim moon = New Moon("Moon", 1, "moon.png", 1, New Orbit(8, 0.02, 0), entityManager)

            Dim earth = New Planet("Earth", 1, "earth.jpg", 5, New Orbit(120, 0.0034, 0), entityManager)
            earth.AddSatellite(moon)

            planets.Add(earth)

            planets.Add(New Planet("Mars", 1, "mars.jpg", 5, New Orbit(160, 0.003, 0), entityManager))

            planets.Add(New Planet("Jupiter", 1, "jupiter.jpg", 10, New Orbit(200, 0.0028, 0), entityManager))

            planets.Add(New Planet("Saturn", 1, "saturn.jpg", 7, New Orbit(240, 0.0025, 0), entityManager))

            planets.Add(New Planet("Uranus", 1, "uranus.jpg", 3, New Orbit(280, 0.002, 0), entityManager))

            planets.Add(New Planet("Neptune", 1, "neptune.jpg", 5, New Orbit(320, 0.0015, 0), entityManager))

            planets.Add(New Planet("Pluto", 1, "pluto.jpg", 4, New Orbit(360, 0.001, 0), entityManager))

            Dim sun = New Star("Sol", 1, 5500, "sun.jpg", 20, New Orbit(0, 0, 0), entityManager)
            sun.AddSatellite(planets)

            objects.Add(sun)

            Return New SolarSystem(objects)
        End Function



    End Class
End Namespace