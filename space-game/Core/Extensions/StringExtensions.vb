Imports System.Runtime.CompilerServices

Namespace Extensions
    Public Module StringExtensions

        <Extension>
        Public Function ToEnum(Of TEnum As Structure)(value As String) As TEnum?
            If Not String.IsNullOrWhiteSpace(value) Then
                Dim e As TEnum = Nothing
                If [Enum].TryParse(value, True, e) Then
                    Return New TEnum?(e)
                End If
            End If
            Return Nothing
        End Function

    End Module
End Namespace