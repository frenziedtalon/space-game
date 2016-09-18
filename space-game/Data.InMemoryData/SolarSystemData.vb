Imports Core.Classes
Imports Data.Classes

Public Class SolarSystemData

    Public Function SolSystem() As List(Of CelestialObjectData)
        Dim result As New List(Of CelestialObjectData)

        Dim sol = Sun

        sol.AddSatellite(Mercury)
        sol.AddSatellite(Venus)

        Dim e = Earth
        e.AddSatellite(Moon)
        sol.AddSatellite(e)

        sol.AddSatellite(Mars)
        sol.AddSatellite(Jupiter)
        sol.AddSatellite(Saturn)
        sol.AddSatellite(Uranus)
        sol.AddSatellite(Neptune)
        sol.AddSatellite(Pluto)

        result.Add(sol)

        Return result
    End Function

    Private ReadOnly Property Sun As CelestialObjectData
        Get
            Dim physicalData = New PhysicalData("Sol",
                                    Distance.FromKilometers(695700),
                                    Mass.FromSolarMasses(1),
                                    New Textures() With {.Low = "sun.jpg"},
                                    CelestialObjectType.Star)

            Return New CelestialObjectData(Nothing, physicalData)
        End Get
    End Property

    Private ReadOnly Property Mercury As CelestialObjectData
        Get
            Dim orbit = New OrbitData(longitudeOfAscendingNode:=Angle.FromDegrees(48.3313),
                                   inclination:=Angle.FromDegrees(7.0047),
                                   argumentOfPeriapsis:=Angle.FromDegrees(29.1241),
                                   semiMajorAxis:=Distance.FromAstronomicalUnits(0.38709893),
                                   eccentricity:=0.205635,
                                   meanAnomalyZero:=Angle.FromDegrees(168.6562))

            Dim physicalData = New PhysicalData("Mercury",
                                    Distance.FromKilometers(2440),
                                    Mass.FromEarthMasses(0.06),
                                    New Textures() With {.Low = "mercury.jpg"},
                                    CelestialObjectType.Planet)

            Return New CelestialObjectData(orbit, physicalData)
        End Get
    End Property

    Private ReadOnly Property Venus As CelestialObjectData
        Get
            Dim orbit = New OrbitData(longitudeOfAscendingNode:=Angle.FromDegrees(76.6799),
                                   inclination:=Angle.FromDegrees(3.3946),
                                   argumentOfPeriapsis:=Angle.FromDegrees(54.891),
                                   semiMajorAxis:=Distance.FromAstronomicalUnits(0.72333199),
                                   eccentricity:=0.006773,
                                   meanAnomalyZero:=Angle.FromDegrees(48.0052))

            Dim physicalData = New PhysicalData("Venus",
                                    Distance.FromKilometers(6052),
                                    Mass.FromEarthMasses(0.82),
                                    New Textures() With {.Low = "venus.jpg"},
                                    CelestialObjectType.Planet)

            Return New CelestialObjectData(orbit, physicalData)
        End Get
    End Property

    Private ReadOnly Property Earth As CelestialObjectData
        Get
            Dim orbit = New OrbitData(longitudeOfAscendingNode:=Angle.FromDegrees(0),
                                   inclination:=Angle.FromDegrees(0),
                                   argumentOfPeriapsis:=Angle.FromDegrees(282.9404),
                                   semiMajorAxis:=Distance.FromAstronomicalUnits(1.0),
                                   eccentricity:=0.016709,
                                   meanAnomalyZero:=Angle.FromDegrees(356.047))

            Dim physicalData = New PhysicalData("Earth",
                                    Distance.FromKilometers(6371),
                                    Mass.FromEarthMasses(1),
                                    New Textures() With {.Low = "earth.jpg"},
                                    CelestialObjectType.Planet)

            Return New CelestialObjectData(orbit, physicalData)
        End Get
    End Property

    Private ReadOnly Property Moon As CelestialObjectData
        Get
            Dim orbit = New OrbitData(longitudeOfAscendingNode:=Angle.FromDegrees(125.1228),
                                   inclination:=Angle.FromDegrees(5.1454),
                                   argumentOfPeriapsis:=Angle.FromDegrees(318.0634),
                                   semiMajorAxis:=Distance.FromAstronomicalUnits(0.00257188152),
                                   eccentricity:=0.0549,
                                   meanAnomalyZero:=Angle.FromDegrees(115.3654))

            Dim physicalData = New PhysicalData("Moon",
                                    Distance.FromKilometers(1737),
                                    Mass.FromEarthMasses(0.0123),
                                    New Textures() With {.Low = "moon.png"},
                                    CelestialObjectType.Moon)

            Return New CelestialObjectData(orbit, physicalData)
        End Get
    End Property

    Private ReadOnly Property Mars As CelestialObjectData
        Get
            Dim orbit = New OrbitData(longitudeOfAscendingNode:=Angle.FromDegrees(49.5574),
                                   inclination:=Angle.FromDegrees(1.8497),
                                   argumentOfPeriapsis:=Angle.FromDegrees(286.5016),
                                   semiMajorAxis:=Distance.FromAstronomicalUnits(1.52366231),
                                   eccentricity:=0.093405,
                                   meanAnomalyZero:=Angle.FromDegrees(18.6021))

            Dim physicalData = New PhysicalData("Mars",
                                    Distance.FromKilometers(3390),
                                    Mass.FromEarthMasses(0.11),
                                    New Textures() With {.Low = "mars.jpg"},
                                    CelestialObjectType.Planet)

            Return New CelestialObjectData(orbit, physicalData)
        End Get
    End Property

    Private ReadOnly Property Jupiter As CelestialObjectData
        Get
            Dim orbit = New OrbitData(longitudeOfAscendingNode:=Angle.FromDegrees(100.4542),
                                   inclination:=Angle.FromDegrees(1.303),
                                   argumentOfPeriapsis:=Angle.FromDegrees(273.8777),
                                   semiMajorAxis:=Distance.FromAstronomicalUnits(5.20336301),
                                   eccentricity:=0.048498,
                                   meanAnomalyZero:=Angle.FromDegrees(19.895))

            Dim physicalData = New PhysicalData("Jupiter",
                                    Distance.FromKilometers(69911),
                                    Mass.FromEarthMasses(317.8),
                                    New Textures() With {.Low = "jupiter.jpg"},
                                    CelestialObjectType.Planet)

            Return New CelestialObjectData(orbit, physicalData)
        End Get
    End Property

    Private ReadOnly Property Saturn As CelestialObjectData
        Get
            Dim orbit = New OrbitData(longitudeOfAscendingNode:=Angle.FromDegrees(113.6634),
                                   inclination:=Angle.FromDegrees(2.4886),
                                   argumentOfPeriapsis:=Angle.FromDegrees(339.3939),
                                   semiMajorAxis:=Distance.FromAstronomicalUnits(9.53707032),
                                   eccentricity:=0.055546,
                                   meanAnomalyZero:=Angle.FromDegrees(316.967))

            Dim physicalData = New PhysicalData("Saturn",
                                    Distance.FromKilometers(58232),
                                    Mass.FromEarthMasses(95.2),
                                    New Textures() With {.Low = "saturn.jpg"},
                                    CelestialObjectType.Planet)

            Return New CelestialObjectData(orbit, physicalData)
        End Get
    End Property

    Private ReadOnly Property Uranus As CelestialObjectData
        Get
            Dim orbit = New OrbitData(longitudeOfAscendingNode:=Angle.FromDegrees(74.0005),
                                   inclination:=Angle.FromDegrees(0.7733),
                                   argumentOfPeriapsis:=Angle.FromDegrees(96.6612),
                                   semiMajorAxis:=Distance.FromAstronomicalUnits(19.19126393),
                                   eccentricity:=0.047318,
                                   meanAnomalyZero:=Angle.FromDegrees(142.5905))

            Dim physicalData = New PhysicalData("Uranus",
                                    Distance.FromKilometers(25362),
                                    Mass.FromEarthMasses(14.6),
                                    New Textures() With {.Low = "uranus.jpg"},
                                    CelestialObjectType.Planet)

            Return New CelestialObjectData(orbit, physicalData)
        End Get
    End Property

    Private ReadOnly Property Neptune As CelestialObjectData
        Get
            Dim orbit = New OrbitData(longitudeOfAscendingNode:=Angle.FromDegrees(131.7806),
                                   inclination:=Angle.FromDegrees(1.77),
                                   argumentOfPeriapsis:=Angle.FromDegrees(272.8461),
                                   semiMajorAxis:=Distance.FromAstronomicalUnits(30.06896348),
                                   eccentricity:=0.008606,
                                   meanAnomalyZero:=Angle.FromDegrees(260.2471))

            Dim physicalData = New PhysicalData("Neptune",
                                    Distance.FromKilometers(24622),
                                    Mass.FromEarthMasses(17.2),
                                    New Textures() With {.Low = "neptune.jpg"},
                                    CelestialObjectType.Planet)

            Return New CelestialObjectData(orbit, physicalData)
        End Get
    End Property

    Private ReadOnly Property Pluto As CelestialObjectData
        Get
            Dim orbit = New OrbitData(longitudeOfAscendingNode:=Angle.FromDegrees(1.9250982),
                                   inclination:=Angle.FromDegrees(0.29914960832),
                                   argumentOfPeriapsis:=Angle.FromDegrees(1.98548656),
                                   semiMajorAxis:=Distance.FromAstronomicalUnits(39.48168677),
                                   eccentricity:=0.24883,
                                   meanAnomalyZero:=Angle.FromDegrees(0))

            Dim physicalData = New PhysicalData("Pluto",
                                    Distance.FromKilometers(1186),
                                    Mass.FromEarthMasses(0.0022),
                                    New Textures() With {.Low = "pluto.jpg"},
                                    CelestialObjectType.Planet)

            Return New CelestialObjectData(orbit, physicalData)
        End Get
    End Property
End Class