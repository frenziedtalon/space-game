﻿Imports Core.Classes
Imports Newtonsoft.Json

Public Interface ISphere
    Inherits I3DObject

    ReadOnly Property Radius As Distance

    <JsonIgnore>
    ReadOnly Property AxialTilt As Integer

    <JsonIgnore>
    ReadOnly Property RotationSpeed As Integer

End Interface
