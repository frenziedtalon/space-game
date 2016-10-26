Imports NUnit.Framework

Namespace Data
    Public Class EnumerableExtensionsTestsData
        Public Shared Function HasAny_WhenGivenAList_ReturnsExpected_Data() As List(Of TestCaseData)
            Dim nullList As List(Of String) = Nothing
            Dim emptyList As New List(Of String)
            Dim listWithMembers = New List(Of String) From {"a", "b"}

            Return New List(Of TestCaseData) From {
                New TestCaseData(nullList, False),
                New TestCaseData(emptyList, False),
                New TestCaseData(listWithMembers, True)
            }
        End Function
    End Class
End Namespace