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

Partial Public Class ControlActionType
    Public Property ID As Integer
    Public Property Type As String
    Public Property UseJavascript As Nullable(Of Integer)
    <ScriptIgnore()>
    Public Overridable Property ControlTypeActions As ICollection(Of ControlTypeAction) = New HashSet(Of ControlTypeAction)
    <ScriptIgnore()>
    Public Overridable Property ProjectControlPostbackActions As ICollection(Of ProjectControlPostbackAction) = New HashSet(Of ProjectControlPostbackAction)

End Class
