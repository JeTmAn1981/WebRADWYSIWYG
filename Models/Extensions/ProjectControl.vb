Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports System.Data.SqlClient
Imports System.Data
Imports System.Web.UI
Imports WhitTools.DataWarehouse
Imports WhitTools.eCommerce
Imports WhitTools.Getter
Imports WhitTools.Setter
Imports WhitTools.File
Imports WhitTools.Filler
Imports WhitTools.BillingInformation
Imports WhitTools.Utilities
Imports WhitTools.DataTables
Imports WhitTools.Converter
Imports WhitTools.Validator
Imports WhitTools.Formatter
Imports WhitTools.GlobalEnum
Imports WhitTools.Repeaters
Imports WhitTools.ErrorHandler
Imports WhitTools.Email
Imports WhitTools.WebTeam
Imports WhitTools.SQL
Imports WhitTools.Encryption
Imports WhitTools.RulesAssignments
Imports WhitTools.Workflow
Imports WebRADWYSIWYG.Utilities
Imports System.Web.Script.Serialization
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports React
Imports System.Web.Services
Imports Common.General.DataSources
Imports Common.Webpages.Main
Imports System.Data.Common

<Serializable()>
Partial Public Class ProjectControl
    Private db As New WebRADEntities()

    Public Function GetDataItems() As List(Of ListItem)
        If SelectionItems = "2" Then
            Return GetDataSourceItems()
        ElseIf SelectionItems = "4" Then
            Return GetDataMethodItems()
        Else
            Return New List(Of ListItem)(New ListItem() {New ListItem("No data items found")})
        End If
    End Function

    Private Function GetDataSourceItems() As List(Of ListItem)
        Dim items As List(Of ListItem) = New List(Of ListItem)

        If IncludePleaseSelect Then
            items.Add(New ListItem(""))
        End If

        Try
            Dim sqlStatement As String = GetDataSource(ProjectDataSource.ID)
            Dim databaseName As String = Common.SQL.Main.GetSQLDatabaseName(db.Projects.First(Function(project) project.ID = ProjectID).SQLDatabase)
            Dim data As New DataTable
            Try
                data = GetDataTable(sqlStatement, New SqlConnection("server=web3;database=" & databaseName & ";Trusted_Connection=True;"))
        Catch ex As Exception

            End Try



            'Dim cnx = CreateSQLConnection("StudentSystems")
            'Dim cmd As New SqlCommand(sqlStatement)
            'cmd.Connection = cnx

            'Dim daTemp As New SqlDataAdapter()

            'daTemp.SelectCommand = cmd
            'daTemp.Fill(data)

            'data = GetDataTable(sqlStatement, cnx)

            For Each currentRow As DataRow In data.Rows
                items.Add(New ListItem() With {.Text = currentRow.Item(ProjectDataSource.TextField), .Value = currentRow.Item(ProjectDataSource.ValueField)})
            Next

        Catch ex As Exception
            items.Add(New ListItem(ex.ToString))
        End Try


        Return items
    End Function

    Private Function GetDataMethodItems() As List(Of ListItem)
        Dim items As List(Of ListItem) = New List(Of ListItem)

        Dim blah = Name

        Select Case DataMethod
            Case "1"
                items = GetStatesList()
            Case "3"
                items = GetNumbersList(MinimumValue, MaximumValue)
            Case "4"
                items.Add(New ListItem("Other Data Method Specified - Not Available in Design View"))
            Case "5"
                items = GetDepartmentSelectList()
            Case "6"
                items = GetProspectMajorsList()
            Case "7"
                items = GetTimesItems(GetTime("8:00 AM", MinimumValue), GetTime("7:45 AM", MaximumValue))
            Case "8"
                items = GetMonthItems()
            Case "9"
                items = GetResidenceHallsItems()
        End Select

        If IncludePleaseSelect Then
            items.Insert(0, New ListItem(""))
        End If

        Return items
    End Function


End Class
