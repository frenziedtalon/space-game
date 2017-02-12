Imports Core.Extensions
Imports Data.Classes
Imports Data.SqlDataProvider.Extensions
Imports Mapster

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
                    result.AddRange(From co In system Where Not co.PrimaryId.HasValue Select RecursiveCreateObject(co))
                End If
                Return result
            End Get
        End Property

        Private Function RecursiveCreateObject(co As CelestialObject) As CelestialObjectData
            Dim orbit As OrbitData = If(co.PrimaryId.HasValue, co.Adapt(Of OrbitData)(), Nothing)
            Dim physical As PhysicalData = co.Adapt(Of PhysicalData)()

            Dim primary = New CelestialObjectData(orbit, physical)

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

        Private Function GetSolarSystem() As List(Of CelestialObject)
            Return GetSolarSystemByName("Solar System")
        End Function

        Private Function GetSolarSystemByName(name As String) As List(Of CelestialObject)
            If Not String.IsNullOrWhiteSpace(name) Then
                Using con As New SolarSystemEntities
                    con.Configuration.LazyLoadingEnabled = False
                    con.Configuration.ProxyCreationEnabled = False

                    Dim result = con.CelestialObjects.
                                Where(Function(o) o.SolarSystem.Name = name).
                                IncludeAllTables().
                                OrderBy(Function(p) p.PrimaryId).
                                ToList()

                    Return result
                End Using
            End If
            Return Nothing
        End Function

    End Class
End Namespace