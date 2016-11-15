Imports Core.Classes
Imports Core.Enums
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

        Private Shared _samples As SampleTextures

        Private Shared ReadOnly Property Samples As SampleTextures
            Get
                If _samples Is Nothing Then
                    _samples = New SampleTextures(TextureType.Diffuse)
                End If
                Return _samples
            End Get
        End Property

        Private Shared ReadOnly All As New List(Of Texture) From {Samples.Low,
                                                                    Samples.Medium,
                                                                    Samples.High}

        Private Shared ReadOnly MediumHigh As New List(Of Texture) From {Samples.Medium,
                                                                            Samples.High}

        Private Shared ReadOnly MediumLow As New List(Of Texture) From {Samples.Medium,
                                                                            Samples.Low}

        Private Shared ReadOnly LowHigh As New List(Of Texture) From {Samples.High,
                                                                            Samples.Low}

        Private Shared ReadOnly OnlyLow As New List(Of Texture) From {Samples.Low}

        Private Shared ReadOnly None As New List(Of Texture)

        Private Shared ReadOnly AllWithLowValue As New List(Of Texture) From {Samples.Low,
                                                                                Samples.EmptyMedium,
                                                                                Samples.EmptyHigh}

        Private Shared ReadOnly AllWithHighalue As New List(Of Texture) From {Samples.EmptyLow,
                                                                                Samples.EmptyMedium,
                                                                                Samples.High}

        Public Shared Function GetHighestAvailableResolution_WhenGivenAList_ReturnsExpected_Data() As List(Of TestCaseData)
            Return New List(Of TestCaseData) From {
                New TestCaseData(All, Samples.Type, Samples.High.Path),
                New TestCaseData(MediumHigh, Samples.Type, Samples.High.Path),
                New TestCaseData(OnlyLow, Samples.Type, Samples.Low.Path),
                New TestCaseData(MediumLow, Samples.Type, Samples.Medium.Path),
                New TestCaseData(LowHigh, Samples.Type, Samples.High.Path),
                New TestCaseData(None, Samples.Type, ""),
                New TestCaseData(All, Samples.Type + 1, ""),
                New TestCaseData(AllWithLowValue, Samples.Type, Samples.Low.Path),
                New TestCaseData(AllWithHighalue, Samples.Type, Samples.High.Path)
            }
        End Function

        Public Shared Function GetLowestAvailableResolution_WhenGivenAList_ReturnsExpected_Data() As List(Of TestCaseData)
            Return New List(Of TestCaseData) From {
                New TestCaseData(All, Samples.Type, Samples.Low.Path),
                New TestCaseData(MediumHigh, Samples.Type, Samples.Medium.Path),
                New TestCaseData(OnlyLow, Samples.Type, Samples.Low.Path),
                New TestCaseData(MediumLow, Samples.Type, Samples.Low.Path),
                New TestCaseData(LowHigh, Samples.Type, Samples.Low.Path),
                New TestCaseData(None, Samples.Type, ""),
                New TestCaseData(All, Samples.Type + 1, ""),
                New TestCaseData(AllWithLowValue, Samples.Type, Samples.Low.Path),
                New TestCaseData(AllWithHighalue, Samples.Type, Samples.High.Path)
            }
        End Function
    End Class
End Namespace