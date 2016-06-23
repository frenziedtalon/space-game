﻿Namespace Classes
    Public Class Distance
        Private ReadOnly _kilometers As Double

        Const KilometersInAstronomicalUnit As Double = 149597870.691

        Private Sub New(kilometers As Double)
            _kilometers = kilometers
        End Sub

        Public ReadOnly Property AstronomicalUnits As Double
            Get
                Return _kilometers / KilometersInAstronomicalUnit
            End Get
        End Property

        Public Shared Function FromAstronomicalUnits(astronomicalUnits As Double) As Distance
            Return New Distance(astronomicalUnits * KilometersInAstronomicalUnit)
        End Function

        Public ReadOnly Property Kilometers As Double
            Get
                Return _kilometers
            End Get
        End Property

        Public Shared Function FromKilometers(kilometers As Double) As Distance
            Return New Distance(kilometers)
        End Function

    End Class
End Namespace