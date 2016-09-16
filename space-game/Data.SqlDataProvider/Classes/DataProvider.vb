Imports System.Data.Entity
Imports Core.Classes
Imports Core.Extensions
Imports Data.Classes

Namespace Classes

    Public Class DataProvider
        Implements IDataProvider

        Public Sub New()
        End Sub

        Public ReadOnly Property SolarSystem As List(Of CelestialObjectData) Implements IDataProvider.SolarSystem
            Get
                Dim result As New List(Of CelestialObjectData)

                Dim system = GetSolarSystem()

                If system.HasAny() Then
                    result.AddRange(From co In system Select RecursiveCreateObject(co))
                End If

                Return result
            End Get
        End Property

        Private Function RecursiveCreateObject(co As CelestialObject) As CelestialObjectData

            Dim orbit As OrbitData = If(co.CelestialObject2 IsNot Nothing, CreateOrbitData(co), Nothing)

            Dim primary = New CelestialObjectData(orbit, CreatePhysicalData(co))

            If co.CelestialObject1.HasAny() Then
                For Each satellite In co.CelestialObject1
                    Dim s = RecursiveCreateObject(satellite)

                    If s IsNot Nothing Then
                        primary.AddSatellite(s)
                    End If
                Next
            End If

            Return primary
        End Function

        Private Function CreatePhysicalData(co As CelestialObject) As PhysicalData
            If co IsNot Nothing Then
                Dim t = co.CelestialObjectType.Name.ToEnum(Of Global.Data.Classes.CelestialObjectType)

                Return New PhysicalData(co.Name,
                                        Distance.FromKilometers(co.Radius),
                                        Mass.FromKilograms(co.Mass),
                                        co.Texture,
                                        t.Value)
            End If
            Return Nothing
        End Function

        Private Function CreateOrbitData(co As CelestialObject) As OrbitData
            If co IsNot Nothing Then
                Return New OrbitData(Distance.FromKilometers(co.SemiMajorAxis.Value),
                                    co.Eccentricity.Value,
                                    Angle.FromDegrees(co.Inclination.Value),
                                    Angle.FromDegrees(co.ArgumentOfPeriapsis.Value),
                                    Angle.FromDegrees(co.LongitudeOfAscendingNode.Value),
                                    Angle.FromDegrees(co.MeanAnomalyZero.Value))
            End If
            Return Nothing
        End Function

        Private Function GetSolarSystem() As List(Of CelestialObject)
            Return GetSolarSystemByName("Solar System")
        End Function

        Private Function GetSolarSystemByName(name As String) As List(Of CelestialObject)
            If Not String.IsNullOrWhiteSpace(name) Then
                Using con As New SolarSystemEntities
                    Return con.CelestialObjects.
                                Where(Function(o) o.SolarSystem.Name = name AndAlso o.PrimaryId Is Nothing).
                                Include(Function(s) s.CelestialObjectType). ' top level, stars, free floating objects
                                Include(Function(s) s.CelestialObject1). ' planets
                                Include(Function(s) s.CelestialObject1.Select(Function(t) t.CelestialObjectType)).
                                Include(Function(s) s.CelestialObject1.Select(Function(t) t.CelestialObject1)). ' moons of planets
                                Include(Function(s) s.CelestialObject1.Select(Function(t) t.CelestialObject1.Select(Function(u) u.CelestialObjectType))).
                                Include(Function(s) s.CelestialObject1.Select(Function(t) t.CelestialObject1.Select(Function(u) u.CelestialObject1))). ' anything orbiting a moon of a planet
                                ToList()

                    ' This will need refactoring if we ever get as far as moons having orbiting objects.
                End Using
            End If

            Return Nothing
        End Function

    End Class
End Namespace