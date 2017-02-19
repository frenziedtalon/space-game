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

        Public Shared Function GetHighestAvailableResolutionForEachType_High() As List(Of TestCaseData)
            Dim diffuseSamples = New SampleTextures(TextureType.Diffuse)
            Dim bumpSamples = New SampleTextures(TextureType.Bump)
            Dim emissiveSamples = New SampleTextures(TextureType.Emissive)

            Dim textures As New List(Of Texture)
            textures.AddRange({diffuseSamples.Low, diffuseSamples.Medium, diffuseSamples.High})
            textures.AddRange({bumpSamples.Low, bumpSamples.Medium, bumpSamples.High})
            textures.AddRange({emissiveSamples.Low, emissiveSamples.Medium, emissiveSamples.High})

            Dim expected As New List(Of Texture) From {
                    diffuseSamples.High,
                    bumpSamples.High,
                    emissiveSamples.High
                }

            Return New List(Of TestCaseData) From {
                    New TestCaseData(textures, expected)
                }
        End Function

        Public Shared Function GetHighestAvailableResolutionForEachType_Medium() As List(Of TestCaseData)
            Dim diffuseSamples = New SampleTextures(TextureType.Diffuse)
            Dim bumpSamples = New SampleTextures(TextureType.Bump)
            Dim emissiveSamples = New SampleTextures(TextureType.Emissive)

            Dim textures As New List(Of Texture)
            textures.AddRange({diffuseSamples.Low, diffuseSamples.Medium})
            textures.AddRange({bumpSamples.Low, bumpSamples.Medium})
            textures.AddRange({emissiveSamples.Low, emissiveSamples.Medium})

            Dim expected As New List(Of Texture) From {
                    diffuseSamples.Medium,
                    bumpSamples.Medium,
                    emissiveSamples.Medium
                }

            Return New List(Of TestCaseData) From {
                    New TestCaseData(textures, expected)
                }
        End Function

        Public Shared Function GetHighestAvailableResolutionForEachType_Low() As List(Of TestCaseData)
            Dim diffuseSamples = New SampleTextures(TextureType.Diffuse)
            Dim bumpSamples = New SampleTextures(TextureType.Bump)
            Dim emissiveSamples = New SampleTextures(TextureType.Emissive)

            Dim textures As New List(Of Texture)
            textures.AddRange({diffuseSamples.Low})
            textures.AddRange({bumpSamples.Low})
            textures.AddRange({emissiveSamples.Low})

            Dim expected As New List(Of Texture) From {
                    diffuseSamples.Low,
                    bumpSamples.Low,
                    emissiveSamples.Low
                }

            Return New List(Of TestCaseData) From {
                    New TestCaseData(textures, expected)
                }
        End Function

        Public Shared Function GetHighestAvailableResolutionForEachType_Mix() As List(Of TestCaseData)
            Dim diffuseSamples = New SampleTextures(TextureType.Diffuse)
            Dim bumpSamples = New SampleTextures(TextureType.Bump)
            Dim emissiveSamples = New SampleTextures(TextureType.Emissive)

            Dim textures As New List(Of Texture)
            textures.AddRange({diffuseSamples.Low, diffuseSamples.Medium, diffuseSamples.High})
            textures.AddRange({bumpSamples.Low, bumpSamples.Medium})
            textures.AddRange({emissiveSamples.Low})

            Dim expected As New List(Of Texture) From {
                    diffuseSamples.High,
                    bumpSamples.Medium,
                    emissiveSamples.Low
                }

            Return New List(Of TestCaseData) From {
                    New TestCaseData(textures, expected)
                }
        End Function

        Public Shared Function GetHighestAvailableResolutionForEachType_Empty() As List(Of TestCaseData)
            Dim diffuseSamples = New SampleTextures(TextureType.Diffuse)
            Dim bumpSamples = New SampleTextures(TextureType.Bump)
            Dim emissiveSamples = New SampleTextures(TextureType.Emissive)

            Dim textures As New List(Of Texture)
            diffuseSamples.High.Path = ""
            bumpSamples.Medium.Path = ""
            emissiveSamples.Low.Path = ""

            textures.AddRange({diffuseSamples.Low, diffuseSamples.Medium, diffuseSamples.High})
            textures.AddRange({bumpSamples.Low, bumpSamples.Medium})
            textures.AddRange({emissiveSamples.Low})

            Dim expected As New List(Of Texture) From {
                    diffuseSamples.Medium,
                    bumpSamples.Low
                }

            Return New List(Of TestCaseData) From {
                    New TestCaseData(textures, expected)
                }
        End Function

        Public Shared Function IsEquivalent_WhenGivenAList_ReturnsExpected_NullAndEmpty() As List(Of TestCaseData)
            Return New List(Of TestCaseData) From {
                    New TestCaseData(New List(Of Texture), Nothing, False)
                  }
        End Function

        Public Shared Function IsEquivalent_WhenGivenAList_ReturnsExpected_Contents() As List(Of TestCaseData)

            Dim allCopy = New List(Of Texture)(All)
            Dim allWithExtra = New List(Of Texture)(All)

            allWithExtra.Add(New Texture() With {.QualityEnum = TextureQuality.Medium,
                                            .TypeEnum = TextureType.Opacity,
                                            .Path = "extra texture"})

            All.Reverse()

            Return New List(Of TestCaseData) From {
                    New TestCaseData(All, All, True),
                    New TestCaseData(allCopy, allCopy, True),
                    New TestCaseData(allCopy, All, True),
                    New TestCaseData(All, OnlyLow, False),
                    New TestCaseData(All, allWithExtra, False)
                  }
        End Function
    End Class
End Namespace