Imports System.Runtime.CompilerServices

Namespace Extensions
    Public Module EnumerableExtensions

        <Extension>
        Public Function HasAny(Of T)(this As IEnumerable(Of T)) As Boolean
            Return this IsNot Nothing AndAlso this.Any()
        End Function

    End Module
End Namespace