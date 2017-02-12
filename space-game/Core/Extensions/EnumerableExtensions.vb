Imports System.Runtime.CompilerServices
Imports Core.Classes
Imports Core.Enums

Namespace Extensions
    Public Module EnumerableExtensions

        <Extension>
        Public Function HasAny(Of T)(this As IEnumerable(Of T)) As Boolean
            Return this IsNot Nothing AndAlso this.Any()
        End Function

        <Extension>
        Public Function GetHighestAvailableResolution(this As IEnumerable(Of Texture), type As TextureType) As String
            Dim result = ""

            Dim r = GetTexture(this, type).
                    OrderByDescending(Function(t) t.QualityEnum).
                    FirstOrDefault()

            If r IsNot Nothing Then
                result = r.Path
            End If

            Return result
        End Function

        <Extension>
        Public Function GetHighestAvailableResolutionForEachType(this As IEnumerable(Of Texture)) As List(Of Texture)
            Dim result As New Dictionary(Of TextureType, Texture)

            Dim textures = this.
                            OrderByDescending(Function(t) t.QualityEnum).
                            ToList()

            For i = 0 To textures.Count - 1 Step 1
                Dim t = textures(i)
                If Not String.IsNullOrWhiteSpace(t.Path) AndAlso Not result.ContainsKey(t.TypeEnum) Then
                    result.Add(t.TypeEnum, t)
                End If
            Next

            Return result.Values.ToList()
        End Function

        <Extension>
        Public Function GetLowestAvailableResolution(this As IEnumerable(Of Texture), type As TextureType) As String
            Dim result = ""

            Dim r = GetTexture(this, type).
                    OrderBy(Function(t) t.QualityEnum).
                    FirstOrDefault()

            If r IsNot Nothing Then
                result = r.Path
            End If

            Return result
        End Function

        Private Function GetTexture(textures As IEnumerable(Of Texture), type As TextureType) As IEnumerable(Of Texture)
            Return textures.Where(Function(t) t.TypeEnum.Equals(type) AndAlso Not String.IsNullOrWhiteSpace(t.Path))
        End Function

        <Extension>
        Public Function IsEquivalent(this As IEnumerable(Of Texture), other As IEnumerable(Of Texture)) As Boolean
            If (this Is Nothing And other Is Nothing) OrElse (this IsNot Nothing And other IsNot Nothing) Then
                If this.Count.Equals(other.Count) Then

                    Dim sortedThis = this.OrderBy(Function(l) l.Path).
                                                ThenBy(Function(l) l.Quality).
                                                ThenBy(Function(l) l.Type)

                    Dim sortedOther = other.OrderBy(Function(l) l.Path).
                                            ThenBy(Function(l) l.Quality).
                                            ThenBy(Function(l) l.Type)

                    Return sortedThis.SequenceEqual(sortedOther)
                End If
            End If

            Return False
        End Function
    End Module
End Namespace