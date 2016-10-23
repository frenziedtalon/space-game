Imports Data
Imports Data.Classes
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


    Private Function RecursiveCreateCelestialObject(o As CelestialObjectData) As ICelestialObject
        Dim primary As BaseCelestialObject = CreateCorrectCelestialObject(o)

        If o.Satellites.Any() Then
            For Each satellite In o.Satellites
                Dim s = RecursiveCreateCelestialObject(satellite)
                If s IsNot Nothing Then
                    Dim orbit = New Orbit(_turnTracker, satellite.Orbit, True)
                    primary.AddSatellite(DirectCast(s, OrbitingCelestialObjectBase), orbit)
                End If
            Next
        End If

        Return primary
    End Function

    Private Function CreateCorrectCelestialObject(o As CelestialObjectData) As BaseCelestialObject
        Select Case o.Physical.Type
            Case CelestialObjectType.Star
                Return New Star(5500, o.Physical, _entityManager)
            Case CelestialObjectType.Planet
                Return New Planet(o.Physical, _entityManager)
            Case CelestialObjectType.Moon
                Return New Moon(o.Physical, _entityManager)
            Case Else
                Return Nothing
        End Select
    End Function

    Public Function SolSystem() As List(Of ICelestialObject)
        Dim system = _dataProvider.SolarSystem
        Return (From o In system Select RecursiveCreateCelestialObject(o)).ToList()
    End Function

End Class