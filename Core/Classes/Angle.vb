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

        Private Sub New(degrees As Double)
            _degrees = CorrectDegrees(degrees)
        End Sub

        Public ReadOnly Property Radians As Double
            Get
                Return ConvertDegreesToRadians(_degrees)
            End Get
        End Property

        Public ReadOnly Property Degrees As Double
            Get
                Return _degrees
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
    End Class
End Namespace