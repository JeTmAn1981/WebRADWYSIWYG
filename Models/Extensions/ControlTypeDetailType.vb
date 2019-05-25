Imports System.ComponentModel.DataAnnotations
Imports System.Web.Script.Serialization

Friend NotInheritable Class ControlTypeDetailTypeMetaData
    <ScriptIgnore()>
    Public Property ControlTypeDetailTypeItems As ICollection(Of ControlTypeDetailTypeItem) = New HashSet(Of ControlTypeDetailTypeItem)
    <ScriptIgnore()>
    Public Property ControlTypeDetailTypes1 As ICollection(Of ControlTypeDetailType) = New HashSet(Of ControlTypeDetailType)


End Class

<MetadataType(GetType(ControlTypeDetailTypeMetaData))>
Partial Public Class ControlTypeDetailType
End Class
