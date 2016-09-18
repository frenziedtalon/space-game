Namespace Classes
    Public Class Mass
        Implements IEquatable(Of Mass)

        Private ReadOnly _kilograms As Double

        Const KilogramsInSolarMass As Double = 1.98855 * 10 ^ 30
        Const KilogramsInEarthMass As Double = 5973.6 * 10 ^ 21

        Private Sub New(kilograms As Double)
            _kilograms = kilograms
        End Sub

        Public ReadOnly Property Kilograms() As Double
            Get
                Return _kilograms
            End Get
        End Property

        Public Shared Function FromKilograms(kilograms As Double) As Mass
            Return New Mass(kilograms)
        End Function

        Public ReadOnly Property SolarMasses() As Double
            Get
                Return _kilograms / KilogramsInSolarMass
            End Get
        End Property

        Public Shared Function FromSolarMasses(solarMasses As Double) As Mass
            Return New Mass(solarMasses * KilogramsInSolarMass)
        End Function

        Public ReadOnly Property EarthMasses() As Double
            Get
                Return _kilograms / KilogramsInEarthMass
            End Get
        End Property

        Public Shared Function FromEarthMasses(earthMasses As Double) As Mass
            Return New Mass(earthMasses * KilogramsInEarthMass)
        End Function

        Public Shadows Function Equals(other As Mass) As Boolean Implements IEquatable(Of Mass).Equals
            If other IsNot Nothing Then
                Return Kilograms.Equals(other.Kilograms)
            End If
            Return False
        End Function
    End Class
End Namespace