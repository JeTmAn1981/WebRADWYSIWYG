'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated from a template.
'
'     Manual changes to this file may cause unexpected behavior in your application.
'     Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Collections.Generic
Imports System.Web.Script.Serialization

Partial Public Class ControlTypeDetailType
    Public Property ID As Integer
    Public Property Name As String
    Public Property Heading As String
    Public Property Category As Nullable(Of Integer)
    Public Property HTMLType As String
    Public Property CssClass As String
    Public Property Order As Nullable(Of Integer)
    Public Property ParentTypeID As Nullable(Of Integer)
    <ScriptIgnore()>
    Public Overridable Property ControlTypeDetailRequirements As ICollection(Of ControlTypeDetailRequirement) = New HashSet(Of ControlTypeDetailRequirement)
    <ScriptIgnore()>
    Public Overridable Property ControlTypeDetailTypeCategory As ControlTypeDetailTypeCategory
    <ScriptIgnore()>
    Public Overridable Property ControlTypeDetailTypeItems As ICollection(Of ControlTypeDetailTypeItem) = New HashSet(Of ControlTypeDetailTypeItem)
    <ScriptIgnore()>
    Public Overridable Property ControlTypeDetailTypes1 As ICollection(Of ControlTypeDetailType) = New HashSet(Of ControlTypeDetailType)
    <ScriptIgnore()>
    Public Overridable Property ControlTypeDetailType1 As ControlTypeDetailType
    <ScriptIgnore()>
    Public Overridable Property ControlTypeDetailValues As ICollection(Of ControlTypeDetailValue) = New HashSet(Of ControlTypeDetailValue)

End Class
