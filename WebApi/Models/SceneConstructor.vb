Imports Entities
Imports OrbitalMechanics.CelestialObjects
Imports OrbitalMechanics.Classes
Imports TurnTracker

Namespace Models
    Public Class SceneConstructor

        Private ReadOnly _entityManager As IEntityManager
        Private ReadOnly _turnTracker As ITurnTracker

        Public Sub New(entityManager As IEntityManager,
                        turnTracker As ITurnTracker)
            _entityManager = entityManager
            _turnTracker = turnTracker
        End Sub

        Public Function SolSystem() As List(Of ICelestialObject)

            Dim objects = New List(Of ICelestialObject)

            Dim planets As New List(Of OrbitingCelestialObjectBase)

            planets.Add(New Planet("Mercury", 1, "mercury.jpg", 2, New Orbit(40, TimeSpan.FromDays(88.02525), 0, _turnTracker), _entityManager))

            planets.Add(New Planet("Venus", 1, "venus.jpg", 4, New Orbit(80, TimeSpan.FromDays(224.62875), 0, _turnTracker), _entityManager))

            Dim moon = New Moon("Moon", 1, "moon.png", 1, New Orbit(8, TimeSpan.FromDays(28), 0, _turnTracker), _entityManager)

            Dim earth = New Planet("Earth", 1, "earth.jpg", 5, New Orbit(120, TimeSpan.FromDays(365.25), 0, _turnTracker), _entityManager)
            earth.AddSatellite(moon)

            planets.Add(earth)

            planets.Add(New Planet("Mars", 1, "mars.jpg", 5, New Orbit(160, TimeSpan.FromDays(686.67), 0, _turnTracker), _entityManager))

            planets.Add(New Planet("Jupiter", 1, "jupiter.jpg", 10, New Orbit(200, TimeSpan.FromDays(4334.42175), 0, _turnTracker), _entityManager))

            planets.Add(New Planet("Saturn", 1, "saturn.jpg", 7, New Orbit(240, TimeSpan.FromDays(10760.63), 0, _turnTracker), _entityManager))

            planets.Add(New Planet("Uranus", 1, "uranus.jpg", 3, New Orbit(280, TimeSpan.FromDays(30691.9575), 0, _turnTracker), _entityManager))

            planets.Add(New Planet("Neptune", 1, "neptune.jpg", 5, New Orbit(320, TimeSpan.FromDays(60198.67875), 0, _turnTracker), _entityManager))

            planets.Add(New Planet("Pluto", 1, "pluto.jpg", 4, New Orbit(360, TimeSpan.FromDays(90602.81925), 0, _turnTracker), _entityManager))

            Dim sun = New Star("Sol", 1, 5500, "sun.jpg", 20, Nothing, _entityManager)
            sun.AddSatellite(planets)

            objects.Add(sun)

            Return objects
        End Function

    End Class
End Namespace