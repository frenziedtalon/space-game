﻿
Imports System.Windows.Media.Media3D
Imports OrbitalMechanics.Classes

Namespace CelestialObjects
    Public Class Asteroid
        Inherits BaseCelestialObject

        Public Sub New(name As String,
                       mass As Integer,
                       texture As String,
                       orbit As Orbit)

            MyBase.New(name, mass, texture)
            _orbit = orbit
        End Sub

        Public Overrides Sub Update()
            Throw New NotImplementedException()
            Orbit.Update()
        End Sub

        Private ReadOnly _orbit As Orbit
        Public ReadOnly Property Orbit As Orbit
            Get
                Return _orbit
            End Get
        End Property

    End Class
End Namespace