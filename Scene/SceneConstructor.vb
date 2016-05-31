Imports Core.Classes
Imports Data
Imports Entities
Imports OrbitalMechanics.CelestialObjects
Imports OrbitalMechanics.Classes
Imports TurnTracker

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

        Dim sun = New Star("Sol", 1, 5500, "sun.jpg", Distance.FromKilometers(0.2), _entityManager)

        Dim mercury = New Planet("Mercury", 1, "mercury.jpg", Distance.FromKilometers(0.1), _entityManager)
        Dim mercuryOrbit = New Orbit(_turnTracker, _dataProvider.SolarSystem.Mercury().Orbit, True)
        sun.AddSatellite(mercury, mercuryOrbit)

        Dim venus = New Planet("Venus", 1, "venus.jpg", Distance.FromKilometers(0.1), _entityManager)
        Dim venusOrbit = New Orbit(_turnTracker, _dataProvider.SolarSystem.Venus().Orbit, True)
        sun.AddSatellite(venus, venusOrbit)

        Dim moon = New Moon("Moon", 1, "moon.png", Distance.FromKilometers(0.1), _entityManager)
        Dim moonOrbit = New Orbit(_turnTracker, _dataProvider.SolarSystem.Moon().Orbit, True)

        Dim earth = New Planet("Earth", 1, "earth.jpg", Distance.FromKilometers(0.1), _entityManager)
        earth.AddSatellite(moon, moonOrbit)

        Dim earthOrbit = New Orbit(_turnTracker, _dataProvider.SolarSystem.Earth().Orbit, True)
        sun.AddSatellite(earth, earthOrbit)

        Dim mars = New Planet("Mars", 1, "mars.jpg", Distance.FromKilometers(0.1), _entityManager)
        Dim marsOrbit = New Orbit(_turnTracker, _dataProvider.SolarSystem.Mars().Orbit, True)
        sun.AddSatellite(mars, marsOrbit)

        Dim jupiter = New Planet("Jupiter", 1, "jupiter.jpg", Distance.FromKilometers(0.15), _entityManager)
        Dim jupiterOrbit = New Orbit(_turnTracker, _dataProvider.SolarSystem.Jupiter().Orbit, True)
        sun.AddSatellite(jupiter, jupiterOrbit)

        Dim saturn = New Planet("Saturn", 1, "saturn.jpg", Distance.FromKilometers(0.12), _entityManager)
        Dim saturnOrbit = New Orbit(_turnTracker, _dataProvider.SolarSystem.Saturn().Orbit, True)
        sun.AddSatellite(saturn, saturnOrbit)

        Dim uranus = New Planet("Uranus", 1, "uranus.jpg", Distance.FromKilometers(0.1), _entityManager)
        Dim uranusOrbit = New Orbit(_turnTracker, _dataProvider.SolarSystem.Uranus().Orbit, True)
        sun.AddSatellite(uranus, uranusOrbit)

        Dim neptune = New Planet("Neptune", 1, "neptune.jpg", Distance.FromKilometers(0.1), _entityManager)
        Dim neptuneOrbit = New Orbit(_turnTracker, _dataProvider.SolarSystem.Neptune().Orbit, True)
        sun.AddSatellite(neptune, neptuneOrbit)

        Dim pluto = New Planet("Pluto", 1, "pluto.jpg", Distance.FromKilometers(0.1), _entityManager)
        Dim plutoOrbit = New Orbit(_turnTracker, _dataProvider.SolarSystem.Pluto().Orbit, True)
        sun.AddSatellite(pluto, plutoOrbit)

        objects.Add(sun)

        Return objects
    End Function

    Public Function CircularTestSystem() As List(Of ICelestialObject)

        Dim objects = New List(Of ICelestialObject)

        Dim sun = New Star("Sol", 1, 5500, "sun.jpg", Distance.FromKilometers(0.2), _entityManager)

        Dim planet = New Planet("Pluto", 1, "pluto.jpg", Distance.FromKilometers(0.1), _entityManager)

        Dim orbit = New Orbit(_turnTracker,
                              longitudeOfAscendingNode:=Angle.FromDegrees(48.3313),
                              inclination:=Angle.FromDegrees(7.0047),
                              argumentOfPeriapsis:=Angle.FromDegrees(29.1241),
                              semiMajorAxis:=Distance.FromAstronomicalUnits(0.38709893),
                              eccentricity:=0.0,
                              meanAnomalyZero:=Angle.FromDegrees(168.6562))

        sun.AddSatellite(planet, orbit)

        objects.Add(sun)
        Return objects
    End Function

    Public Function NearParabolicTestSystem() As List(Of ICelestialObject)

        Dim objects = New List(Of ICelestialObject)

        Dim sun = New Star("Sol", 1, 5500, "sun.jpg", Distance.FromKilometers(0.01), _entityManager)

        Dim planet = New Planet("Pluto", 1, "pluto.jpg", Distance.FromKilometers(0.01), _entityManager)

        Dim orbit = New Orbit(_turnTracker,
                              longitudeOfAscendingNode:=Angle.FromDegrees(48.3313),
                              inclination:=Angle.FromDegrees(7.0047),
                              argumentOfPeriapsis:=Angle.FromDegrees(29.1241),
                              semiMajorAxis:=Distance.FromAstronomicalUnits(0.38709893),
                              eccentricity:=0.79,
                              meanAnomalyZero:=Angle.FromDegrees(168.6562))

        sun.AddSatellite(planet, orbit)

        objects.Add(sun)
        Return objects
    End Function

End Class