Imports Messaging
Imports SpaceGame.Classes.BaseEntities

Namespace Classes.CelestialObjects
    Public Class Star
        Inherits BaseCelestialObject

        Private ReadOnly _surfaceTemperature As Integer

        Public Sub New(name As String,
                       mass As Integer,
                       surfaceTemperature As Integer)

            MyBase.New(name, mass)
            _surfaceTemperature = surfaceTemperature
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