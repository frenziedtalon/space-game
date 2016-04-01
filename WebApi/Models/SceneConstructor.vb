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

            Dim sun = New Star("Sol", 1, 5500, "sun.jpg", 20, _entityManager)

            Dim mercury = New Planet("Mercury", 1, "mercury.jpg", 2, _entityManager)
            Dim mercuryOrbit = New Orbit(40, TimeSpan.FromDays(88.02525), 0, _turnTracker)
            sun.AddSatellite(mercury, mercuryOrbit)

            Dim venus = New Planet("Venus", 1, "venus.jpg", 4, _entityManager)
            Dim venusOrbit = New Orbit(80, TimeSpan.FromDays(224.62875), 0, _turnTracker)
            sun.AddSatellite(venus, venusOrbit)

            Dim moon = New Moon("Moon", 1, "moon.png", 1, _entityManager)
            Dim moonOrbit = New Orbit(8, TimeSpan.FromDays(28), 0, _turnTracker)
            Dim earth = New Planet("Earth", 1, "earth.jpg", 5, _entityManager)
            earth.AddSatellite(moon, moonOrbit)
            Dim earthOrbit = New Orbit(120, TimeSpan.FromDays(365.25), 0, _turnTracker)
            sun.AddSatellite(earth, earthOrbit)

            Dim mars = New Planet("Mars", 1, "mars.jpg", 5, _entityManager)
            Dim marsOrbit = New Orbit(160, TimeSpan.FromDays(686.67), 0, _turnTracker)
            sun.AddSatellite(mars, marsOrbit)

            Dim jupiter = New Planet("Jupiter", 1, "jupiter.jpg", 10, _entityManager)
            Dim jupiterOrbit = New Orbit(200, TimeSpan.FromDays(4334.42175), 0, _turnTracker)
            sun.AddSatellite(jupiter, jupiterOrbit)

            Dim saturn = New Planet("Saturn", 1, "saturn.jpg", 7, _entityManager)
            Dim saturnOrbit = New Orbit(240, TimeSpan.FromDays(10760.63), 0, _turnTracker)
            sun.AddSatellite(saturn, saturnOrbit)

            Dim uranus = New Planet("Uranus", 1, "uranus.jpg", 3, _entityManager)
            Dim uranusOrbit = New Orbit(280, TimeSpan.FromDays(30691.9575), 0, _turnTracker)
            sun.AddSatellite(uranus, uranusOrbit)

            Dim neptune = New Planet("Neptune", 1, "neptune.jpg", 5, _entityManager)
            Dim neptuneOrbit = New Orbit(320, TimeSpan.FromDays(60198.67875), 0, _turnTracker)
            sun.AddSatellite(neptune, neptuneOrbit)

            Dim pluto = New Planet("Pluto", 1, "pluto.jpg", 4, _entityManager)
            Dim plutoOrbit = New Orbit(360, TimeSpan.FromDays(90602.81925), 0, _turnTracker)
            sun.AddSatellite(pluto, plutoOrbit)

            objects.Add(sun)

            Return objects
        End Function

    End Class
End Namespace