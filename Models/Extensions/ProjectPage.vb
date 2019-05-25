Imports System.ComponentModel.DataAnnotations
Imports System.Web.Script.Serialization

Friend NotInheritable Class ProjectPageMetadata
    <ScriptIgnore()>
    Public Property ProjectAdditionalOperations As ICollection(Of ProjectAdditionalOperation) = New HashSet(Of ProjectAdditionalOperation)
    <ScriptIgnore()>
    Public Property ProjectBuildPages As ICollection(Of ProjectBuildPage) = New HashSet(Of ProjectBuildPage)
    <ScriptIgnore()>
    Public Property Project As Project
End Class

<MetadataType(GetType(ProjectPageMetadata))>
Partial Public Class ProjectPage
End Class
