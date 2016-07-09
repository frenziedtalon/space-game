Imports System.Runtime.CompilerServices

Namespace Extensions
    Public Module GuidExtensions

        <Extension>
        Public Function IsEmpty(this As Guid) As Boolean
            Return this.Equals(Guid.Empty)
        End Function
    End Module
End Namespace