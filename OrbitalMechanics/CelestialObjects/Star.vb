﻿
Imports Core
Imports Entities
Imports Newtonsoft.Json
Imports OrbitalMechanics.Classes

Namespace CelestialObjects
    Public Class Star
        Inherits OrbitingCelestialObjectBase
        Implements ISphere

        Private ReadOnly _surfaceTemperature As Integer

        Public Sub New(name As String,
                       mass As Integer,
                       surfaceTemperature As Integer,
                       texture As String,
                       radius As Integer,
                       orbit As IOrbit,
                       entityManager As IEntityManager)

            MyBase.New(name, mass, texture, orbit, entityManager)
            _surfaceTemperature = surfaceTemperature
            _radius = radius
        End Sub

        Public Overrides Sub Update()
            Throw New NotImplementedException()
        End Sub


        Private _classification As StarClassification


        Public ReadOnly Property Classification() As StarClassification
            Get
                Select Case SurfaceTemperature
                    Case >= 33000
                        Return StarClassification.O
                    Case >= 10500
                        Return StarClassification.B
                    Case >= 7500
                        Return StarClassification.A
                    Case >= 6000
                        Return StarClassification.F
                    Case >= 5500
                        Return StarClassification.G
                    Case >= 4000
                        Return StarClassification.K
                    Case Else
                        Return StarClassification.M
                End Select
            End Get
        End Property

        ''' <summary>
        ''' Surface temperature of the star. Unit is Kelvins.
        ''' </summary>
        Public ReadOnly Property SurfaceTemperature() As Integer
            Get
                Return _surfaceTemperature
            End Get
        End Property

        Private ReadOnly _radius As Integer
        Public ReadOnly Property Radius As Integer Implements ISphere.Radius
            Get
                Return _radius
            End Get
        End Property

        Public ReadOnly Property AxialTilt As Integer Implements ISphere.AxialTilt
            Get
                Throw New NotImplementedException()
            End Get
        End Property

        Public ReadOnly Property RotationSpeed As Integer Implements ISphere.RotationSpeed
            Get
                Throw New NotImplementedException()
            End Get
        End Property

        <JsonIgnore>
        Public ReadOnly Property LightIntensity() As Integer
            Get
                Throw New NotImplementedException
            End Get
        End Property

        ''' <summary>
        ''' Star light intensity fades gradually until becoming zero at this range
        ''' </summary>
        <JsonIgnore()>
        Public ReadOnly Property LightRange() As Integer
            Get
                Throw New NotImplementedException
            End Get
        End Property

        Private _volume As Double = 0

        Public ReadOnly Property Volume As Double Implements I3DObject.Volume
            Get
                If Double.Equals(_volume, 0.0) AndAlso Radius > 0 Then
                    _volume = Helpers.Shapes.ShapeHelper.VolumeOfASphere(Radius)
                End If
                Return _volume
            End Get
        End Property

    End Class


    Public Enum StarClassification
        O
        B
        A
        F
        G
        K
        M
    End Enum
End Namespace