﻿
Imports Entities

Namespace CelestialObjects

    Public MustInherit Class BaseCelestialObject
        Inherits BaseGameEntity
        Implements ICelestialObject

        Protected Sub New(name As String,
                       mass As Integer)

            _name = name
            _mass = mass

        End Sub

        Private ReadOnly _mass As Integer
        Public ReadOnly Property Mass As Integer Implements ICelestialObject.Mass
            Get
                Return _mass
            End Get
        End Property

        Private ReadOnly _name As String
        Public ReadOnly Property Name As String Implements ICelestialObject.Name
            Get
                Return _name
            End Get
        End Property
    End Class
End Namespace