Namespace Classes
    Public Class Textures
        Implements IEquatable(Of Textures)

        Public High As String
        Public Medium As String
        Public Low As String

        Public Function GetHighestAvailableResolution() As String
            Return If(HasHighResolution(), High,
                    If(HasMediumResolution(), Medium,
                    If(HasLowResolution(), Low,
                    "")))
        End Function

        Private Function HasHighResolution() As Boolean
            Return Not String.IsNullOrWhiteSpace(High)
        End Function

        Private Function HasMediumResolution() As Boolean
            Return Not String.IsNullOrWhiteSpace(Medium)
        End Function

        Private Function HasLowResolution() As Boolean
            Return Not String.IsNullOrWhiteSpace(Low)
        End Function

        Public Shadows Function Equals(other As Textures) As Boolean Implements IEquatable(Of Textures).Equals
            If other IsNot Nothing Then
                Return High.Equals(other.High) AndAlso Medium.Equals(other.Medium) AndAlso Low.Equals(other.Low)
            End If
            Return False
        End Function
    End Class
End Namespace