Imports Newtonsoft.Json

Namespace Classes
    ''' <summary>
    ''' Represents the concept of an angle and allows easily switching between the different unit types.
    ''' </summary>
    ''' <remarks>
    ''' Any angle less than zero or greater than (or equal to) 360 (or 2*Pi) will be corrected until it is within these limits
    ''' Using degrees as the internal value. Found that this results in fewer rounding errors than operating with Pi repeatedly.
    ''' </remarks>
    Public Class Angle
        Private ReadOnly _degrees As Double
        Private ReadOnly _decimalPlaces As Integer

        Const DefaultDecimalPlaces As Integer = 14
        Const RoundingMethod As MidpointRounding = MidpointRounding.AwayFromZero

        Private Sub New(degrees As Double, Optional decimalPlaces As Integer = DefaultDecimalPlaces)
            _degrees = CorrectDegrees(degrees)

            If decimalPlaces > DefaultDecimalPlaces Then
                decimalPlaces = DefaultDecimalPlaces
            End If

            _decimalPlaces = decimalPlaces
        End Sub

        Public ReadOnly Property Radians As Double
            Get
                Return Math.Round(ConvertDegreesToRadians(_degrees), _decimalPlaces, RoundingMethod)
            End Get
        End Property

        <JsonIgnore()>
        Public ReadOnly Property Degrees As Double
            Get
                Return Math.Round(_degrees, _decimalPlaces, RoundingMethod)
            End Get
        End Property

        Public Shared Function FromRadians(radians As Double) As Angle
            Return New Angle(ConvertRadiansToDegrees(radians))
        End Function

        Public Shared Function FromDegrees(degrees As Double) As Angle
            Return New Angle(degrees)
        End Function

        Private Function CorrectDegrees(angle As Double) As Double
            If angle < 0 Then
                Do Until angle > 0
                    angle += 360
                Loop
            ElseIf angle >= 360 Then
                Do Until angle < 360
                    angle -= 360
                Loop
            End If

            Return angle
        End Function

        Private Shared Function ConvertRadiansToDegrees(radians As Double) As Double
            Return radians / (Math.PI / 180)
        End Function

        Private Shared Function ConvertDegreesToRadians(degrees As Double) As Double
            Return degrees * (Math.PI / 180)
        End Function

        ''' <summary>
        ''' The number of decimal places to which all results will be rounded
        ''' </summary>
        <JsonIgnore>
        Public ReadOnly Property DecimalPlaces As Integer
            Get
                Return _decimalPlaces
            End Get
        End Property
    End Class
End Namespace