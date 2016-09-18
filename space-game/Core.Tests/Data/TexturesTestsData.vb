Imports Core.Classes
Imports NUnit.Framework

Namespace Data
    Public Class TexturesTestsData

        Public Shared Function Equals_WhenComparing_ReturnsExpected_Data() As List(Of TestCaseData)
            Dim nullTextures As Textures = Nothing
            Dim emptyTextures = New Textures() With {.High = "", .Medium = "", .Low = ""}
            Dim lowercaseTextures = New Textures() With {.High = "high", .Medium = "medium", .Low = "low"}
            Dim uppercaseTextures = New Textures() With {.High = "HIGH", .Medium = "MEDIUM", .Low = "LOW"}

            Dim highNotSame = New Textures() With {.High = "different", .Medium = "medium", .Low = "low"}
            Dim mediumNotSame = New Textures() With {.High = "high", .Medium = "different", .Low = "low"}
            Dim lowNotSame = New Textures() With {.High = "high", .Medium = "medium", .Low = "different"}

            Return New List(Of TestCaseData) From {
                    New TestCaseData(emptyTextures, nullTextures, False),
                    New TestCaseData(lowercaseTextures, nullTextures, False),
                    New TestCaseData(emptyTextures, emptyTextures, True),
                    New TestCaseData(emptyTextures, lowercaseTextures, False),
                    New TestCaseData(lowercaseTextures, uppercaseTextures, False),
                    New TestCaseData(lowercaseTextures, highNotSame, False),
                    New TestCaseData(lowercaseTextures, mediumNotSame, False),
                    New TestCaseData(lowercaseTextures, lowNotSame, False)
                }
        End Function

        Public Shared Function GetHighestAvailableResolution_WhenCalled_ReturnsExpected_Data() As List(Of TestCaseData)

            Dim empty = New Textures() With {.High = "", .Medium = "", .Low = ""}
            Dim allValues = New Textures() With {.High = "high", .Medium = "medium", .Low = "low"}
            Dim medium = New Textures() With {.High = "", .Medium = "medium", .Low = "low"}
            Dim low = New Textures() With {.High = "", .Medium = "", .Low = "low"}
            Dim whitespace = New Textures() With {.High = "  ", .Medium = "medium", .Low = "low"}

            Return New List(Of TestCaseData) From {
                New TestCaseData(empty, ""),
                New TestCaseData(allValues, "high"),
                New TestCaseData(medium, "medium"),
                New TestCaseData(low, "low"),
                New TestCaseData(whitespace, "medium")
            }
        End Function
    End Class
End Namespace