Imports Data
Imports Entities
Imports OrbitalMechanics.CelestialObjects
Imports OrbitalMechanics.Classes
Imports TurnTracker

Namespace Models
    Public Class SceneConstructor

        Private ReadOnly _entityManager As IEntityManager
        Private ReadOnly _turnTracker As ITurnTracker
        Private ReadOnly _dataProvider As IDataProvider


        Public Sub New(entityManager As IEntityManager,
                        turnTracker As ITurnTracker,
                       dataProvider As IDataProvider)
            _entityManager = entityManager
            _turnTracker = turnTracker
            _dataProvider = dataProvider
        End Sub

        Public Function SolSystem() As List(Of ICelestialObject)

            Dim objects = New List(Of ICelestialObject)

            Dim sun = New Star("Sol", 1, 5500, "sun.jpg", 0.2, _entityManager)

            Dim mercury = New Planet("Mercury", 1, "mercury.jpg", 0.1, _entityManager)
            Dim mercuryOrbit = New Orbit(_turnTracker, _dataProvider.SolarSystem.Mercury().Orbit)
            sun.AddSatellite(mercury, mercuryOrbit)

            Dim venus = New Planet("Venus", 1, "venus.jpg", 0.1, _entityManager)
            Dim venusOrbit = New Orbit(_turnTracker, _dataProvider.SolarSystem.Venus().Orbit)
            sun.AddSatellite(venus, venusOrbit)

            Dim moon = New Moon("Moon", 1, "moon.png", 0.1, _entityManager)
            Dim moonOrbit = New Orbit(_turnTracker, _dataProvider.SolarSystem.Moon().Orbit)

            Dim earth = New Planet("Earth", 1, "earth.jpg", 0.1, _entityManager)
            earth.AddSatellite(moon, moonOrbit)

            Dim earthOrbit = New Orbit(_turnTracker, _dataProvider.SolarSystem.Earth().Orbit)
            sun.AddSatellite(earth, earthOrbit)

            Dim mars = New Planet("Mars", 1, "mars.jpg", 0.1, _entityManager)
            Dim marsOrbit = New Orbit(_turnTracker, _dataProvider.SolarSystem.Mars().Orbit)
            sun.AddSatellite(mars, marsOrbit)

            Dim jupiter = New Planet("Jupiter", 1, "jupiter.jpg", 0.15, _entityManager)
            Dim jupiterOrbit = New Orbit(_turnTracker, _dataProvider.SolarSystem.Jupiter().Orbit)
            sun.AddSatellite(jupiter, jupiterOrbit)

            Dim saturn = New Planet("Saturn", 1, "saturn.jpg", 0.12, _entityManager)
            Dim saturnOrbit = New Orbit(_turnTracker, _dataProvider.SolarSystem.Saturn().Orbit)
            sun.AddSatellite(saturn, saturnOrbit)

            Dim uranus = New Planet("Uranus", 1, "uranus.jpg", 0.1, _entityManager)
            Dim uranusOrbit = New Orbit(_turnTracker, _dataProvider.SolarSystem.Uranus().Orbit)
            sun.AddSatellite(uranus, uranusOrbit)

            Dim neptune = New Planet("Neptune", 1, "neptune.jpg", 0.1, _entityManager)
            Dim neptuneOrbit = New Orbit(_turnTracker, _dataProvider.SolarSystem.Neptune().Orbit)
            sun.AddSatellite(neptune, neptuneOrbit)

            Dim pluto = New Planet("Pluto", 1, "pluto.jpg", 0.1, _entityManager)
            Dim plutoOrbit = New Orbit(_turnTracker, _dataProvider.SolarSystem.Pluto().Orbit)
            sun.AddSatellite(pluto, plutoOrbit)

            objects.Add(sun)

            Return objects
        End Function

    End Class
End Namespace