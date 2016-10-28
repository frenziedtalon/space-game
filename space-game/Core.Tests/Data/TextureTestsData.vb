Imports Core.Classes
Imports Core.Enums
Imports NUnit.Framework

Namespace Data
    Public Class TextureTestsData

        Public Shared Function Equals_WhenComparing_ReturnsExpected_Data() As List(Of TestCaseData)
            Dim standard = New Texture() With {.Type = TextureType.Bump,
                                            .Path = "path\to\texture",
                                            .Quality = TextureQuality.High
                                        }

            Dim copyOfStandard = standard

            Dim capitalisedPath = New Texture() With {.Type = TextureType.Bump,
                                            .Path = "PATH\TO\TEXTURE",
                                            .Quality = TextureQuality.High
                                        }

            Dim emptyPath = New Texture() With {.Type = TextureType.Bump,
                                            .Path = "",
                                            .Quality = TextureQuality.High
                                        }

            Dim differentType = New Texture() With {.Type = TextureType.Diffuse,
                                            .Path = "path\to\texture",
                                            .Quality = TextureQuality.High
                                        }

            Dim differentQuality = New Texture() With {.Type = TextureType.Bump,
                                            .Path = "path\to\texture",
                                            .Quality = TextureQuality.Low
                                        }

            Return New List(Of TestCaseData) From {
                    New TestCaseData(standard, copyOfStandard, True),
                    New TestCaseData(standard, capitalisedPath, True),
                    New TestCaseData(standard, emptyPath, False),
                    New TestCaseData(standard, differentType, False),
                    New TestCaseData(standard, differentQuality, False)
                }
        End Function
    End Class
End Namespace