Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports System.Data.SqlClient
Imports System.Reflection
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
Imports Common.EnclosureOpenings
Imports System.Web.Services
Imports Common.General.DataSources
Imports Common.Webpages.Main

Public Class HomeController
    Inherits System.Web.Mvc.Controller

    Shared cnx As SqlConnection = CreateSQLConnection("WebRAD")
    Shared sqlcnx As SqlConnection
    Shared ProjectDT As New DataTable
    Shared AncillaryProjectDT As New DataTable
    Shared ControlsDT As New DataTable
    Shared nPageNumber As Integer = -1
    Shared bFrontend As Boolean = True
    Shared bPrintable As Boolean = False
    Shared bSearch As Boolean = False
    Shared bInsert As Boolean = False
    Shared bMVC As Boolean = False
    Shared bBackendIndex As Boolean = False
    Shared bHTML As Boolean = False
    Shared bResponsive As Boolean = False
    Shared sProjectLocation As String = ""
    Shared sArchiveRef As String = ""
    Shared sSQLDatabaseName, sSQLServerName As String
    Shared sClassListing As String
    Shared sStorageProperties, sConceptualProperties, sScalarProperties, sModelProperties As String
    Shared sCSSClass As String = ""

    Private Shared _displayOptions As String
    Private Shared _dataOptions As String
    Private Shared _generalOptions As String
    Private Shared _actionOptions As String
    Private Shared _PlacedControls As String
    Private Shared _CurrentProject As String
    Private Shared _Projects As String
    Private Shared _ProjectPages As String
    Private Shared _ControlTypeDetails As String
    Private Shared _ControlTypeActions As String
    Private Shared _ControlActionTypes As String
    Private Shared _ControlTypes As String
    Private Shared _ControlTypeItems As String
    Private Shared _ControlDataTypes As String
    Private Shared _ControlTypeDetailRequirements As String
    Private Shared _ControlTypeDetailTypes As String
    Private Shared _ControlTypeDetailValues As String
    Private Shared _ControlTypeDetailTypeItems As String
    Public Shared projectID As Integer
    Public Shared currentProject As Project

    'private static  IList<Project> _projects;
    Private db As New WebRADEntities()
    Dim projectControls As New List(Of ProjectControl)

    Sub New()

    End Sub

    Private Sub SetupControlData(ByVal reloadCache As Boolean)
        If _displayOptions Is Nothing Or reloadCache Then
            db.Configuration.ProxyCreationEnabled = False

            SetProjectControls()

            GetControlOptions()
            SetControlTypeDataTypes()
            SetProjectControlProperties()

            Dim jss As New JavaScriptSerializer()

            Dim currentProject = db.Projects.FirstOrDefault(Function(project) project.ID = projectID)

            _Projects = jss.Serialize(db.Projects.ToList().Select(Function(project) New SelectListItem With {.Text = project.PageTitle, .Value = project.ID}))
            'Uncommenting this will cause a circular reference error
            '  _CurrentProject = jss.Serialize(db.Projects.FirstOrDefault(Function(project) project.ID = projectID))
            _ProjectPages = jss.Serialize(db.ProjectPages.Where(Function(page) page.ProjectID = projectID))
            _PlacedControls = jss.Serialize(projectControls)
            _ControlTypes = jss.Serialize(db.ControlTypes.OrderBy(Function(ct) ct.Type).ToList())
            _ControlTypeItems = jss.Serialize(db.ControlTypeItems.ToList())
            _ControlDataTypes = jss.Serialize(db.ControlDataTypes.OrderBy(Function(dt) dt.Description).ToList())
            _ControlTypeDetails = jss.Serialize(db.ControlTypeDetails.ToList())
            _ControlTypeActions = jss.Serialize(db.ControlTypeActions.ToList())
            _ControlActionTypes = jss.Serialize(db.ControlActionTypes.ToList())
            _ControlTypeDetailRequirements = jss.Serialize(db.ControlTypeDetailRequirements.ToList())
            _ControlTypeDetailTypes = jss.Serialize(db.ControlTypeDetailTypes.ToList())
            _ControlTypeDetailValues = jss.Serialize(db.ControlTypeDetailValues.ToList())
            _ControlTypeDetailTypeItems = jss.Serialize(db.ControlTypeDetailTypeItems.ToList())
        End If

        ViewBag.ProjectID = projectID
        ViewBag.CurrentPageID = If(currentProject.CurrentPageID, currentProject.ProjectPages.OrderByDescending(Function(page) page.ID).FirstOrDefault.ID)
        ViewBag.ProjectPages = _ProjectPages
        ViewBag.DisplayOptions = _displayOptions
        ViewBag.DataOptions = _dataOptions
        ViewBag.GeneralOptions = _generalOptions
        ViewBag.ActionOptions = _actionOptions
        ViewBag.Projects = db.Projects.Where(Function(project) project.ID > 0).OrderBy(Function(project) project.PageTitle).ToList().Select(Function(project) New SelectListItem With {.Text = project.PageTitle, .Value = project.ID, .Selected = (project.ID = projectID)}).ToList
        ViewBag.CurrentProject = _CurrentProject
        ViewBag.PlacedControls = _PlacedControls
        ViewBag.ControlTypes = _ControlTypes
        ViewBag.ControlTypeItems = _ControlTypeItems
        ViewBag.ControlDataTypes = _ControlDataTypes
        ViewBag.ControlTypeActions = _ControlTypeActions
        ViewBag.ControlActionTypes = _ControlActionTypes
        ViewBag.ControlTypeDetails = _ControlTypeDetails
        ViewBag.ControlTypeDetailRequirements = _ControlTypeDetailRequirements
        ViewBag.ControlTypeDetailTypes = _ControlTypeDetailTypes
        ViewBag.ControlTypeDetailValues = _ControlTypeDetailValues
        ViewBag.ControlTypeDetailTypeItems = _ControlTypeDetailTypeItems
    End Sub

    Private Sub SetProjectControls()
        projectControls = db.ProjectControls.Where(Function(control) If(control.ControlType, 0) > 0 And control.ProjectID = projectID).OrderBy(Function(pc) pc.PageID).ThenBy(Function(control) control.Position).ToList()

        DeleteControlsWithoutControlType()

        projectControls = projectControls.Where(Function(pc) If(pc.ControlType, 0) > 0).ToList()
    End Sub

    Private Sub DeleteControlsWithoutControlType()
        projectControls.ForEach(Sub(pc)
                                    If If(pc.ControlType, 0) <= 0 Then
                                        db.Entry(pc).State = Entity.EntityState.Deleted
                                    End If
                                End Sub)

        db.SaveChanges()
    End Sub

    Public Function UpdatePosition(ByVal positionUpdates As List(Of ProjectControl)) As String
        For Each control As ProjectControl In positionUpdates
            db.ProjectControls.First(Function(pc) pc.ID = control.ID).Position = control.Position
        Next

        db.SaveChanges()


        Return "Successfully updated positions"
    End Function

    Public Function UpdateParent(ByVal controlID As Integer, ByVal parentControlID As String) As String
        Dim control = db.ProjectControls.FirstOrDefault(Function(pc) pc.ID = controlID)

        If control IsNot Nothing Then
            If parentControlID = "PlacedControls" Then
                control.ParentControlID = Nothing
                db.SaveChanges()

                Return "Successfully set parent control for control #" & controlID & " to no parent."
            Else
                Dim parentControlIDInteger As Integer

                If Integer.TryParse(parentControlID, parentControlIDInteger) Then
                    If db.ProjectControls.FirstOrDefault(Function(pc) pc.ID = parentControlIDInteger) IsNot Nothing Then
                        control.ParentControlID = parentControlIDInteger
                        db.SaveChanges()

                        Return "Successfully set parent control for control #" & controlID & " to control #" & parentControlID & "."
                    Else
                        Return "Parent Control Not Found"
                    End If
                Else
                    Return "Parent Control ID is not a valid Integer"
                End If
            End If
        Else
            Return "Control not found"
        End If


    End Function

    Public Class PositionUpdate
        Public ID As String
        Public Position As Integer
    End Class

    <WebMethod>
    <HttpPost>
    Public Function DeleteControl(ControlID As Integer) As String
        Try
            db.ProjectControls.Remove(db.ProjectControls.FirstOrDefault(Function(pc) pc.ID = ControlID))
            db.SaveChanges()
        Catch ex As Exception
            Return ex.ToString
        End Try

        Return "Control #" & ControlID & " Deleted"
    End Function

    <WebMethod>
    Public Function GetDataItems(control As ProjectControl) As JsonResult
        Dim dataItems As List(Of ListItem) = New List(Of ListItem)()

        Dim dummyData As List(Of ListItem) = New List(Of ListItem)()
        dummyData.Add(New ListItem() With {.Text = "1", .Value = "2"})

        Try
            dataItems = control.GetDataItems()
        Catch ex As Exception
            dummyData.Add(New ListItem() With {.Text = ex.ToString, .Value = ex.ToString()})

            Return Json(dummyData, JsonRequestBehavior.AllowGet)

            'dataItems.Add(New ListItem(ex.ToString))
        End Try


        Try
            Return Json(dataItems, JsonRequestBehavior.AllowGet)
        Catch ex As Exception
            dummyData.Add(New ListItem() With {.Text = ex.ToString, .Value = ex.ToString()})
            Return Json(dummyData, JsonRequestBehavior.AllowGet)
        End Try
    End Function

    <WebMethod>
    <HttpPost>
    <ValidateInput(False)>
    Public Function AddControl(control As ProjectControl) As JsonResult
        Dim controls As New List(Of ProjectControl)

        If db.CompositeControls.Any(Function(cc) cc.CompositeControlType = control.ControlType) Then
            Dim returnedcontrols = AddCompositeControls(projectID, control.PageID, control.ControlType, If(control.Position, 0), False)

            Return Json(returnedcontrols.Select(Function(returnedcontrol)
                                                    returnedcontrol.ProjectID = projectID
                                                    returnedcontrol.PageID = control.PageID
                                                    returnedcontrol.ControlType1 = Nothing
                                                    'returnedcontrol.Heading = If(control.Heading, "")
                                                    Return returnedcontrol
                                                End Function), JsonRequestBehavior.AllowGet)
        Else
            control.ProjectID = projectID
            control.Heading = If(control.Heading IsNot Nothing, control.Heading, "")

            db.ProjectControls.Add(control)
            db.SaveChanges()

            controls.Add(control)
        End If

        Return Json(controls, JsonRequestBehavior.AllowGet)
    End Function

    ''' <summary>
    ''' This is copied from the main WebRAD project.  For convenience's sake i'm using it like this
    ''' currently but longterm this code needs to be shared, probably hosted as a web service
    ''' by the main project.
    ''' </summary>
    ''' <param name="projectID"></param>
    ''' <param name="pageID"></param>
    ''' <param name="nControlType"></param>
    ''' <param name="nInsertPosition"></param>
    ''' <param name="controlID"></param>
    ''' <param name="deleteOriginalControl"></param>
    ''' <returns></returns>
    Public Function AddCompositeControls(ByVal projectID As Integer, ByVal pageID As Integer, ByVal nControlType As Integer, ByVal nInsertPosition As Integer, Optional ByVal controlID As Integer = -1, Optional deleteOriginalControl As Boolean = True) As List(Of ProjectControl)
        Dim individualControls = db.CompositeControls.Where(Function(cc) cc.CompositeControlType = nControlType).OrderBy(Function(cc) cc.Position).ToList()
        Dim controlsAdded As New List(Of ProjectControl)

        Dim columnExclusions = db.ControlTypeDetailColumnExclusions.ToList()

        Dim projectControlProperties = GetType(ProjectControl).GetProperties()
        Dim controlTypeDetailProperties = GetType(ControlTypeDetail).GetProperties().Where(Function(prop) Not columnExclusions.Any(Function(ce) ce.ColumnName = prop.Name))

        Dim sControlColumns As String

        db.ProjectControls.Where(Function(pc) pc.ProjectID = projectID And pc.Position >= nInsertPosition).ToList().ForEach(Sub(pc) pc.Position = pc.Position + individualControls.Count)
        db.SaveChanges()


        sControlColumns = String.Join(",", controlTypeDetailProperties.Select(Function(prop) prop.Name))

        individualControls.ForEach(Sub(control)
                                       Dim newControl As New ProjectControl()
                                       Dim newDetail = db.ControlTypeDetails.First(Function(detail) detail.ControlID = control.IndividualControlType)

                                       controlTypeDetailProperties.ToList().ForEach(Sub(detailProp)
                                                                                        Dim controlProperty = projectControlProperties.FirstOrDefault(Function(prop) prop.Name = detailProp.Name)

                                                                                        If controlProperty IsNot Nothing Then
                                                                                            controlProperty.SetValue(newControl, detailProp.GetValue(newDetail))
                                                                                        End If
                                                                                    End Sub)


                                       newControl.ID = Nothing
                                       newControl.ProjectID = projectID
                                       newControl.PageID = pageID
                                       newControl.ControlType = nControlType
                                       newControl.Position = nInsertPosition

                                       db.ProjectControls.Add(newControl)
                                       db.SaveChanges()

                                       controlsAdded.Add(newControl)

                                       nInsertPosition += 1
                                   End Sub)
        If deleteOriginalControl Then
            db.Entry(db.ProjectControls.First(Function(pc) pc.ID = controlID)).State = Entity.EntityState.Deleted
            db.SaveChanges()
        End If

        Return controlsAdded
    End Function


    Function GetDetailTypes(ByVal nDetailTypeCategory As Integer) As String
        Return GetListofValues("SELECT * FROM " & DT_WEBRAD_CONTROLTYPEDETAILTYPES & " WHERE Category = " & nDetailTypeCategory, "Name", "<br />")
    End Function

    Sub SetProjectControlProperties()
        For Each currentPC As ProjectControl In projectControls
            currentPC.ControlType1 = db.ControlTypes.Where(Function(ct) ct.ID = currentPC.ControlType).DefaultIfEmpty(Nothing).First()
            currentPC.ProjectControlListItems = db.ProjectControlListItems.Where(Function(pcli) pcli.ParentID = currentPC.ID).OrderBy(Function(pcli) pcli.ID).ToList()
            currentPC.ProjectDataSource = db.ProjectDataSources.FirstOrDefault(Function(ds) ds.ID = currentPC.DataSourceID)
            currentPC.ProjectControlPostbackActions = db.ProjectControlPostbackActions.Where(Function(action) action.TriggerControl = currentPC.ID).ToList().Select(Function(action)
                                                                                                                                                                        action.ControlActionType = Nothing
                                                                                                                                                                        action.ProjectControl = Nothing
                                                                                                                                                                        action.ProjectControlPostbackActionTriggerValues = Nothing
                                                                                                                                                                        Return action
                                                                                                                                                                    End Function).ToList()
        Next
    End Sub

    Private Sub SetControlTypeDataTypes()
        For Each ct As ControlType In db.ControlTypes
            ct.ControlDataType = db.ControlDataTypes.FirstOrDefault(Function(dt) dt.ID = ct.DataType)
        Next
    End Sub

    Public Sub GetControlOptions()
        Dim jss As New JavaScriptSerializer()

        Dim detailTypes = db.ControlTypeDetailTypes.ToList() '.Where(Function(ctdt) ctdt.ParentTypeID Is Nothing).ToList()

        _displayOptions = jss.Serialize(detailTypes.Where(Function(ctdt) ctdt.Category = 1).OrderBy(Function(ctdt) ctdt.Order).ToList())
        _dataOptions = jss.Serialize(detailTypes.Where(Function(ctdt) ctdt.Category = 2).OrderBy(Function(ctdt) ctdt.Order).ToList())
        _generalOptions = jss.Serialize(detailTypes.Where(Function(ctdt) ctdt.Category = 3).OrderBy(Function(ctdt) ctdt.Order).ToList())
        _actionOptions = jss.Serialize(detailTypes.Where(Function(ctdt) ctdt.Category = 4).OrderBy(Function(ctdt) ctdt.Order).ToList())
    End Sub

    Private Sub UpdateCustomDetailTypeItems(ByVal typeName As String, ByVal dataList As ListItem())
        Dim detailType = db.ControlTypeDetailTypes.First(Function(ctdt) ctdt.Name = typeName)
        Dim items = db.ControlTypeDetailTypeItems.Where(Function(item) item.ControlTypeDetailTypeID = detailType.ID).OrderBy(Function(item) item.Text).Select(Function(item) New ListItem() With {.Text = item.Text, .Value = item.Value}).ToArray()
        Dim needsUpdate As Boolean = False

        If items.Count <> dataList.Count Then
            needsUpdate = True
        Else
            For counter As Integer = 0 To items.Count - 1
                If items(counter).Text <> dataList(counter).Text Or items(counter).Value <> dataList(counter).Value Then
                    needsUpdate = True
                    Exit For
                End If
            Next
        End If

        If needsUpdate Then
            For Each dataMethodItem In items
                db.Entry(dataMethodItem).State = Entity.EntityState.Deleted
            Next

            For Each methodType In dataList
                db.ControlTypeDetailTypeItems.Add(New ControlTypeDetailTypeItem() With {.ControlTypeDetailType = detailType, .ControlTypeDetailTypeID = detailType.ID, .Text = methodType.Text, .Value = methodType.Value})
            Next
        End If
    End Sub

    Public Shared projectName As String

    Public Function DND() As ActionResult
        Dim fdsjakl As Common.EnclosuresWriter

        Return View()
    End Function

    Public Function Index(ByVal projectID As Integer, Optional ByVal reloadCache As Boolean = False) As ActionResult
        HomeController.projectID = projectID
        HomeController.currentProject = db.Projects.FirstOrDefault(Function(p) p.ID = projectID)

        If currentProject IsNot Nothing Then
            projectName = currentProject.PageTitle
            ViewBag.ProjectName = projectName
        End If

        SetupControlData(reloadCache)

        'ConvertOldStyleDatasources()

        Return View()
    End Function

    Private Sub ConvertOldStyleDatasources()
        CType(Queryable.Where(db.ProjectControls, Function(pc) pc.ProjectID = projectID And pc.DataSourceType = 2), IEnumerable(Of ProjectControl)).ToList().ForEach(
                    Sub(pc)
                        pc = ConvertDataSource(pc)
                    End Sub)
    End Sub

    Private Function ConvertDataSource(pc As ProjectControl) As ProjectControl
        Dim dataSource = pc.DataSource

        Dim parser = New Regex("Select(?'SelectStatement'.+?)FROM(?'FromStatement'.*?)(WHERE(?'WhereStatement'.+?))?(GROUP\s*BY(?'GroupByStatement'.+?))?(ORDER\s*BY(?'OrderByStatement'.+?))?$", RegexOptions.IgnoreCase)
        Dim match = parser.Match(dataSource)

        Dim ds As New ProjectDataSource()

        ds.Select = match.Groups("SelectStatement").Value.ToString()
        ds.Table = match.Groups("FromStatement").Value.ToString()
        ds.Where = match.Groups("WhereStatement").Value.ToString()
        ds.GroupBy = match.Groups("GroupByStatement").Value.ToString()
        ds.OrderBy = match.Groups("OrderByStatement").Value.ToString()
        ds.TextField = pc.DataTextField
        ds.ValueField = pc.DataValueField
        ds.Type = 1
        ds.ParentType = 1

        Try
            db.ProjectDataSources.Add(ds)
            db.SaveChanges()

            pc.DataSourceType = 1
            pc.DataSourceID = ds.ID
            db.SaveChanges()

        Catch ex As Exception

        End Try

        Return pc
    End Function

    <OutputCache(Location:=OutputCacheLocation.None)>
    Public Function getPlacedControls() As ActionResult
        Return Json(db.ProjectControls.Where(Function(control) control.ProjectID = projectID).ToList(), JsonRequestBehavior.AllowGet)
    End Function

    '<OutputCache(Location:=OutputCacheLocation.None)> _
    'Public Function DisplayOptions() As ActionResult
    '    Return Json(_displayOptions, JsonRequestBehavior.AllowGet)
    'End Function

    <OutputCache(Location:=OutputCacheLocation.None)>
    Public Function DisplayOptions(ByVal nID As Integer) As ActionResult
        Dim options As New List(Of ControlTypeDetailType)
        Dim co As ControlTypeDetailType

        For Each Currentrow As DataRow In GetDataTable("select Name, Category, HTMLType, Heading, Required, Value from ControlTypeDetailRequirements R left outer join ControlTypeDetailValues V ON (R.DetailTypeID = V.DetailTypeID AND ControlTypeID = " & nID & ") left outer join ControlTypeDetailTypes T on r.detailtypeid = t.id where ProfileID = (select requirementsprofile from ControlTypeDetails where ControlID=" & nID & ")", cnx).Rows
            co = New ControlTypeDetailType
            co.Name = Currentrow.Item("Name")
            co.Category = Currentrow.Item("Category")
            co.HTMLType = Currentrow.Item("HTMLType")
            co.Heading = Currentrow.Item("Heading")
            'co.Required = Currentrow.Item("Required")
            'co.Value = Currentrow.Item("Value")
            options.Add(co)
        Next

        Return Json(options, JsonRequestBehavior.AllowGet)
    End Function

    <OutputCache(Location:=OutputCacheLocation.None)>
    Public Function DataOptions() As ActionResult
        Return Json(_dataOptions, JsonRequestBehavior.AllowGet)
    End Function

    <OutputCache(Location:=OutputCacheLocation.None)>
    Public Function GeneralOptions() As ActionResult
        Return Json(_generalOptions, JsonRequestBehavior.AllowGet)
    End Function

    <OutputCache(Location:=OutputCacheLocation.None)>
    Public Function ActionOptions() As ActionResult
        Return Json(_actionOptions, JsonRequestBehavior.AllowGet)
    End Function

    <ValidateInput(False)>
    Public Sub UpdateControl(ByVal controlID As String, ByVal prop As String, ByVal value As String)
        Dim control = db.ProjectControls.FirstOrDefault(Function(pc) pc.ID = controlID)

        If control Is Nothing Then
            Throw New System.Exception("Attempted to update control #" & controlID & " which does not exist")
        Else
            Try

                Dim myType = GetType(ProjectControl)
                Dim myPropInfo As PropertyInfo = myType.GetProperty(prop)
                Dim oldValue As String = myPropInfo.GetValue(control, Nothing)

                ExecuteNonQuery($"INSERT INTO web3.WebRAD.dbo.ControlChangeHistory (ControlID, Property, OldValue, NewValue) VALUES ({controlID}, '{prop}', '{oldValue}', '{CleanSQL(value)}')", cnx)

                myPropInfo.SetValue(control, value)
            Catch ex As Exception
                'Throw New System.Exception($"Error updating control #{controlID} property {prop} - " & ex.tostring)
            End Try

            db.SaveChanges()
        End If

        ExecuteNonQuery("UPDATE " & DT_WEBRAD_PROJECTCONTROLS & " SET " & prop & " = '" & CleanSQL(value) & "' WHERE ID = " & controlID, cnx)
    End Sub

    Public Function GetAvailableActions(ControlTypeID As Integer, ControlID As Integer) As JsonResult
        Return Json(db.ControlActionTypes.ToList().Where(Function(actionType)
                                                             Dim actionAllowed = False

                                                             If actionType.ID = 7 Then
                                                                 actionAllowed = True
                                                             Else
                                                                 Dim controlType = db.ControlTypes.FirstOrDefault(Function(ct) ct.ID = ControlTypeID)

                                                                 If controlType IsNot Nothing Then
                                                                     If actionType.ControlTypeActions.Any(Function(typeAction)
                                                                                                              Return typeAction.ActionControlDataType = controlType.DataType And db.ProjectControls.Any(Function(pc) pc.ProjectID = projectID And pc.ControlType1.DataType = typeAction.TargetControlDataType)
                                                                                                          End Function) Then
                                                                         actionAllowed = True
                                                                     End If
                                                                 End If
                                                             End If

                                                             Return actionAllowed
                                                         End Function).Select(Function(actionType) New With {.Value = actionType.ID, .Text = actionType.Type}), JsonRequestBehavior.AllowGet)

        '        Select Case* From controlactiontypes Where ID = 7 Or
        'ID In (Select actiontype from controltypeactions where ActionControlDataType = " & GetControlDataType(ddlControlType.SelectedValue) & " And 
        'TargetControlDataType in (select datatype from controltypes where ID in (select ControlType from projectcontrols where ProjectID=0 
        'And Not ID = " & lblCurrentControlID.Text & ")))
    End Function

    Public Sub UpdateDataSource(ByVal controlID As String, ByVal dataSource As ProjectDataSource)
        Dim control = db.ProjectControls.First(Function(pc) pc.ID = controlID)

        If (control.ProjectDataSource IsNot Nothing) Then
            db.ProjectDataSources.Remove(control.ProjectDataSource)
            db.SaveChanges()
        End If

        control.ProjectDataSource = dataSource
        db.SaveChanges()
    End Sub

    <ValidateInput(False)>
    Public Function UpdateListItems(ByVal controlID As String, ByVal listItems As ICollection(Of ProjectControlListItem)) As JsonResult
        Dim control = db.ProjectControls.First(Function(pc) pc.ID = controlID)

        control.ProjectControlListItems.Clear()
        db.SaveChanges()

        listItems.ToList().ForEach(Sub(item)
                                       control.ProjectControlListItems.Add(item)
                                       db.SaveChanges()
                                   End Sub)

        Return Json(listItems.OrderBy(Function(item) item.ID))
    End Function

    Public Sub UpdateActions(ByVal controlID As String, ByVal actions As ICollection(Of ProjectControlPostbackAction))
        Dim control = db.ProjectControls.First(Function(pc) pc.ID = controlID)

        For Each currentItem As ProjectControlPostbackAction In control.ProjectControlPostbackActions.ToList()
            db.ProjectControlPostbackActions.Remove(currentItem)
        Next

        db.SaveChanges()

        control.ProjectControlPostbackActions = actions
        db.SaveChanges()
    End Sub

    Sub GetControlContent(ByRef CurrentRow As DataRow, ByRef sContent As String, Optional ByVal sNewRepeaterRow As String = "")
        Dim sCurrentToolkitType, sCurrentPrefix, sCurrentControlType, sCurrentValueAttribute, sParentControlID As String
        Dim bRepeaterHasColumns As Boolean

        Try
            With CurrentRow
                If ControlDisplayAllowed(.Item("DisplayLocation")) Then
                    If .Item("DisplayLocation") = "3" And IsFirstAdminControl(.Item("ID")) And Not bSearch Then
                        sContent &= "<h1>Administrative Use</h1>" & vbCrLf
                    End If

                    Dim ChildControlTypesDT As DataTable = GetDataTable("Select CT.*, '" & CleanSQL(.Item("Name")) & "' as Name, '" & CleanSQL(.Item("Heading")) & "' as Heading, CT.ID as ControlType, ToolkitType, Description, Prefix, ValueAttribute,ValidatorMessage From " & DT_WEBRAD_CONTROLTYPES & "  CT left outer join " & DT_WEBRAD_CONTROLDATATYPES & "  DT on CT.DataType = DT.ID Where ParentControlTypeID = " & .Item("ControlType"), cnx)
                    Dim ChildControlsDT As DataTable = GetChildControls(.Item("ID"))
                    Dim sLayoutType, sLayoutSubtype As String

                    If ParentIsRepeaterControl(.Item("ID"), -1, 0, sParentControlID) Then
                        sLayoutType = GetControlColumnValue(sParentControlID, "LayoutType", ControlsDT)
                        sLayoutSubtype = GetControlColumnValue(sParentControlID, "LayoutSubtype", ControlsDT)
                        bRepeaterHasColumns = RepeaterHasColumns(GetControlColumnValue(sParentControlID, "RepeatColumns", ControlsDT))

                        If sLayoutType = S_LAYOUTTYPE_HORIZONTAL And Not ParentIsControlType(.Item("ID"), "Panel") And .Item("Visible") = "1" Then
                            sContent &= "<td>" & vbCrLf
                        End If
                    End If

                    CheckDisplayType(sContent, .Item("ID"), .Item("PageID"), .Item("DisplayType"), .Item("Position"))

                    If bFrontend = False Or GetPagecount() > 1 Then
                        If IsFileUploadControl(.Item("ControlType")) Then
                            sContent &= "<asp:panel id=""pnlUploaded" & .Item("Name") & """ runat=""server"" visible=""False"">" & vbCrLf
                            sContent &= "<br />" & vbCrLf
                            sContent &= "<BR>" & vbCrLf
                            sContent &= GetHeadingStyle() & "Currently Uploaded " & .Item("Heading") & GetHeadingStyle(False) & "" & vbCrLf
                            sContent &= "<br />"

                            If IsImageUploadControl(.Item("ControlType")) Then
                                sContent &= "<asp:image id=""imgUploaded" & .Item("Name") & """ runat=""server"" width=""100%""></asp:image>"
                            Else
                                sContent &= "<asp:Label id=""lblUploaded" & .Item("Name") & """ runat=""server""></asp:Label>"
                            End If

                            sContent &= "<asp:label id=""lblHiddenUploaded" & .Item("Name") & """ runat=""server"" visible=""False""></asp:label></asp:panel>"
                        End If
                    End If

                    sCurrentToolkitType = If(.Item("ToolkitType") <> "", .Item("ToolkitType"), "asp")
                    sCurrentPrefix = .Item("Prefix")
                    sCurrentControlType = GetDataTypeDescription(.Item("DataType"))
                    sCurrentValueAttribute = .Item("ValueAttribute")

                    If ChildControlsDT.Rows.Count = 0 And Not IsControlType(.Item("ControlType"), "Panel") And Not sLayoutType = S_LAYOUTTYPE_HORIZONTAL And Not .Item("Visible") = "0" Then
                        sContent &= "<br />" & vbCrLf
                    End If

                    If Not bPrintable And Not bSearch Then
                        If ChildControlTypesDT.Rows.Count > 0 Then
                            For nCounter3 As Integer = 0 To ChildControlTypesDT.Rows.Count - 1
                                WriteControlValidators(sContent, ChildControlTypesDT.Rows(nCounter3))
                            Next
                        Else
                            WriteControlValidators(sContent, CurrentRow)
                        End If
                    End If

                    If sLayoutType <> S_LAYOUTTYPE_HORIZONTAL Or (sLayoutType = S_LAYOUTTYPE_HORIZONTAL And bRepeaterHasColumns) Then
                        WriteControlHeading(sContent, .Item("Heading"), .Item("Required"), .Item("ControlType"), .Item("Name"), If(.Item("Visible") = "0", False, True), .Item("ListSelections"))

                        If sLayoutType = S_LAYOUTTYPE_HORIZONTAL And bRepeaterHasColumns Then
                            sContent &= "<br />" & vbCrLf
                        End If
                    ElseIf sLayoutType = S_LAYOUTTYPE_VERTICAL Or sLayoutType = S_LAYOUTTYPE_HORIZONTAL Then
                        Dim nRepeatColumns As Integer

                        Try
                            nRepeatColumns = .Item("RepeatColumns")
                        Catch ex As Exception

                        End Try
                    End If

                    If IsRepeaterControl(.Item("ControlType")) Then
                        If .Item("LayoutType") = S_LAYOUTTYPE_VERTICAL Then
                            sContent &= vbCrLf

                            If .Item("LayoutSubtype") = "1" Then
                                sContent &= "<ol>" & vbCrLf
                            ElseIf .Item("LayoutSubtype") = "2" Then
                                sContent &= "<ul>" & vbCrLf
                            End If
                        ElseIf .Item("LayoutType") = S_LAYOUTTYPE_HORIZONTAL Then
                            sContent &= vbCrLf & "<br /><br />"
                            sContent &= vbCrLf & "<table width='800'>" & vbCrLf
                            sContent &= vbCrLf & "<tr>" & vbCrLf

                            If .Item("LayoutSubtype") = "4" Then
                                sContent &= vbCrLf & "<td>&nbsp;</td>" & vbCrLf
                            End If

                            If Not RepeaterHasColumns(.Item("RepeatColumns")) Then
                                For nChildCounter As Integer = 0 To ControlsDT.Rows.Count - 1
                                    If ParentIsRepeaterControl(ControlsDT.Rows(nChildCounter).Item("ID"), .Item("ID")) And ControlsDT.Rows(nChildCounter).Item("Visible") = "1" Then
                                        'If ParentIsRepeaterControl(ControlsDT.Rows(nChildCounter).Item("ID"), .Item("ID")) And ControlsDT.Rows(nChildCounter).Item("Heading") <> "" Then
                                        sContent &= vbCrLf & "<td valign='top'>"
                                        WriteControlHeading(sContent, ControlsDT.Rows(nChildCounter).Item("Heading"), ControlsDT.Rows(nChildCounter).Item("Required"), .Item("ControlType"), .Item("Name"), False, ControlsDT.Rows(nChildCounter).Item("ListSelections"))
                                        sContent &= "</td>" & vbCrLf
                                    End If
                                Next

                            End If

                            sContent &= vbCrLf & "</tr>" & vbCrLf
                        End If
                    End If

                    If ChildControlsDT.Rows.Count = 0 And Not IsControlType(.Item("ControlType"), "Panel") And Not sLayoutType = S_LAYOUTTYPE_HORIZONTAL And Not .Item("Visible") = "0" Then
                        sContent &= "<br />" & vbCrLf
                    End If

                    If ChildControlTypesDT.Rows.Count > 0 Then
                        For nCounter3 As Integer = 0 To ChildControlTypesDT.Rows.Count - 1
                            WriteControlDefinition(sContent, ChildControlTypesDT.Rows(nCounter3), ChildControlTypesDT.Rows(nCounter3).Item("ToolkitType"), GetDataTypeDescription(ChildControlTypesDT.Rows(nCounter3).Item("DataType")), ChildControlTypesDT.Rows(nCounter3).Item("Prefix"), ChildControlTypesDT.Rows(nCounter3).Item("ValueAttribute"))
                        Next
                    Else
                        WriteControlDefinition(sContent, CurrentRow, sCurrentToolkitType, sCurrentControlType, sCurrentPrefix, sCurrentValueAttribute)
                    End If

                    If IsRepeaterControl(.Item("ControlType")) Then
                        If .Item("LayoutType") = S_LAYOUTTYPE_VERTICAL Then
                            sContent &= vbCrLf

                            If .Item("LayoutSubtype") = "1" Then
                                sContent &= "</ol>"
                            ElseIf .Item("LayoutSubtype") = "2" Then
                                sContent &= "</ul>"
                            End If
                        ElseIf .Item("LayoutType") = S_LAYOUTTYPE_HORIZONTAL Then
                            sContent &= vbCrLf & "</table>"
                        End If
                    End If

                    If .Item("Required") = "1" And .Item("Heading") = "" And .Item("Text") <> "" And Not bPrintable Then
                        sContent &= " <span class=""required"">(required)</span>"
                    End If

                    sContent &= vbCrLf & vbCrLf

                    If .Item("RequireVerification") = "1" And bFrontend Then
                        sContent &= "<br />" & vbCrLf
                        sContent &= "<asp:CustomValidator ID=""cvVerify" & .Item("Name") & """ runat=""server"" OnServerValidate=""" & GetRepeaterHandlerReference(.Item("ID")) & "cvVerify" & .Item("Name") & "_ServerValidate"" CssClass=""Error85"" ErrorMessage=""Sorry, your entry for Verify " & .Item("Heading") & " did not match what you entered above.  Please try again.""></asp:CustomValidator>" & vbCrLf
                        sContent &= "<br />" & vbCrLf
                        sContent &= GetHeadingStyle() & "Verify " & .Item("Heading") & "" & GetHeadingStyle(False) & "" & vbCrLf
                        sContent &= "<br />" & vbCrLf
                        WriteControlDefinition(sContent, CurrentRow, sCurrentToolkitType, sCurrentControlType, sCurrentPrefix, sCurrentValueAttribute, "Verify" & .Item("Name"))
                    End If

                    If ParentIsRepeaterControl(.Item("ID"), -1, 0, sParentControlID) Then
                        If sLayoutType = S_LAYOUTTYPE_HORIZONTAL And Not ParentIsControlType(.Item("ID"), "Panel") And .Item("Visible") = "1" Then
                            sContent &= "</td>" & vbCrLf

                            If sNewRepeaterRow <> "" Then
                                sContent &= sNewRepeaterRow & vbCrLf
                            End If
                        End If
                    End If

                    CheckDisplayType(sContent, .Item("ID"), .Item("PageID"), .Item("DisplayType"), .Item("Position"), False)
                End If
            End With
        Catch ex As Exception
            ViewBag.ControlTypes = ex.ToString
            'Response.Write(ex.ToString & " - " & CurrentRow.Item("ParentControlID") & " - " & CurrentRow.Item("Name") & "<br /><Br />")
        End Try
    End Sub

    Function GetProjectID() As String
        Return If(Session("ProjectID") <> "", Session("ProjectID"), Request.QueryString("ID"))
    End Function


    Sub CheckDisplayType(ByRef sContent As String, ByVal nID As Integer, nPageID As Integer, ByVal nDisplayType As Integer, nPosition As Integer, Optional ByVal bOpen As Boolean = True)
        If nDisplayType = 2 Then
            If bOpen Then
                sContent &= "<div style=""clear:both;""></div>" & vbCrLf
                sContent &= "<div class=""FloatLeft"">" & vbCrLf
            Else
                sContent &= "</div>" & vbCrLf
            End If
        ElseIf nDisplayType = 3 Then
            sContent &= If(bOpen, "<div class=""FloatLeft"">" & vbCrLf, "</div>" & vbCrLf)
        End If

        If nDisplayType <> 1 Then
            Try
                Dim nNextDisplayType As Integer = GetControlColumnValue(GetDataTable("Select ID FROM " & DT_WEBRAD_PROJECTCONTROLS & " WHERE ProjectID = " & GetProjectID() & " AND PageID = " & nPageID & " AND Position = (SELECT MIN(Position) FROM " & DT_WEBRAD_PROJECTCONTROLS & " WHERE ProjectID = " & GetProjectID() & " AND PageID = " & nPageID & " AND Position > " & nPosition & ")", cnx).Rows(0).Item("ID"), "DisplayType")

                If nNextDisplayType = 1 And Not bOpen Then
                    sContent &= "<div style=""clear:both;""></div>" & vbCrLf
                End If
            Catch ex As Exception
                WriteLine(ex.ToString)
            End Try
        End If
    End Sub

    Sub WriteControlValidators(ByRef sContent As String, ByRef CurrentRow As DataRow)
        With CurrentRow
            If CustomValidatorRequired(GetDataTable("Select * From " & DT_WEBRAD_CONTROLTYPES & "  Where ID = " & .Item("ControlType"), cnx).Rows(0).Item("CustomValidatorRequired"), .Item("SQLInsertItemTable"), .Item("TextMode"), .Item("CustomValidation")) Then
                Dim sName, sHeading As String

                sName = .Item("Name")
                sHeading = FormatControlHeading(.Item("Heading"))

                If .Item("Heading") = "" Then
                    sHeading = FormatControlHeading(.Item("Name"))
                End If

                sContent &= "<asp:CustomValidator Display=""Dynamic"" CssClass=""Error85"" id=""cv" & .Item("Name") & """ Runat=""server"" ErrorMessage="

                If .Item("ValidatorMessage") <> "" Then
                    sContent &= """" & Replace(.Item("ValidatorMessage"), "ShortHeadingHere", .Item("ShortHeading")) & """"
                Else
                    sContent &= GetErrorMessage("Please enter/select the following: ", sHeading)
                End If

                sContent &= " OnServerValidate=""" & GetRepeaterHandlerReference(.Item("ID")) & "cv" & .Item("Name") & "_ServerValidate"""

                'sContent &= "></asp:CustomValidator><br />" & vbCrLf
                sContent &= "></asp:CustomValidator>" & vbCrLf
            Else
                If .Item("Required") = "1" Then
                    sContent &= "<asp:requiredfieldValidator Display=""Dynamic"" CssClass=""Error85"" id=""rfv" & .Item("Name") & """"
                    sContent &= " Runat=""server"" ErrorMessage=" & GetErrorMessage("Please enter/select the following: ", .Item("Heading")) & " ControlToValidate=""" & .Item("Prefix") & .Item("Name") & """></asp:RequiredFieldValidator>" & vbCrLf
                End If

                If .Item("DataType") = N_TEXTBOX_DATA_TYPE Then
                    If .Item("SQLDataType") = N_SQL_INT_TYPE Or .Item("SQLDataType") = N_SQL_FLOAT_TYPE Then
                        sContent &= "<asp:CompareValidator id=""cmp" & .Item("Name") & """ runat=""server"" CssClass=""Error85"" Display=""Dynamic"" ControlToValidate=""" & .Item("Prefix") & .Item("Name") & """ Operator=""DataTypeCheck"" Type=""" & If(.Item("SQLDataType") = N_SQL_INT_TYPE, "Integer", "Double") & """ ErrorMessage=" & GetErrorMessage("Please enter/select the following using only numbers" & If(.Item("SQLDataType") = N_SQL_FLOAT_TYPE, " and at most one decimal point", "") & ": ", .Item("Heading")) & "></asp:CompareValidator>" & vbCrLf
                    End If
                End If
            End If

            Dim ValidatorsDT As New DataTable
            Dim nValidatorsCount As Integer

            ValidatorsDT = GetDataTable(cnx, "Select Name, Prefix, ErrorMessage, ValidationExpression From " & DT_WEBRAD_CONTROLTYPEVALIDATORS & "  V left outer join " & DT_WEBRAD_VALIDATORTYPES & "  T on V.Type = T.ID Where ControlID = " & .Item("ControlType"))

            For nValidatorsCount = 0 To ValidatorsDT.Rows.Count - 1
                sContent &= "<asp:" & ValidatorsDT.Rows(nValidatorsCount).Item("Name") & " Display=""Dynamic"" CssClass=""Error85"" id=""" & ValidatorsDT.Rows(nValidatorsCount).Item("Prefix") & .Item("Name") & """ Runat=""server"""

                If ValidatorsDT.Rows(nValidatorsCount).Item("Errormessage") <> "" Then
                    sContent &= " ErrorMessage='" & ValidatorsDT.Rows(nValidatorsCount).Item("ErrorMessage") & "'"
                Else
                    sContent &= " ErrorMessage=" & GetErrorMessage("Please enter/select the following: ", .Item("Heading"))
                End If

                If ValidatorsDT.Rows(nValidatorsCount).Item("ValidationExpression") <> "" Then
                    sContent &= " ValidationExpression=""" & ValidatorsDT.Rows(nValidatorsCount).Item("ValidationExpression") & """"
                End If

                sContent &= " ControlToValidate=""" & .Item("Prefix") & .Item("Name") & """></asp:" & ValidatorsDT.Rows(nValidatorsCount).Item("Name") & ">" & vbCrLf
            Next
        End With
    End Sub

    'Bug here, if responsive is selected and frontend is on web2, need to update web2 CSS files to 
    'have responsive class references.
    Function GetHeadingStyle(Optional ByVal bStart As Boolean = True) As String
        If bResponsive Then
            Return If(bStart, "<span class=""InputHeader"">", "</span>")
        Else
            Return If(bStart, "<strong>", "</strong>")
        End If
    End Function

    Sub WriteControlHeading(ByRef sContent As String, ByVal sHeading As String, ByVal sRequired As String, ByVal nControlType As Integer, ByVal sName As String, ByVal bIncludeSpacing As Boolean, ByVal sListSelections As String)
        If sHeading <> "" Then
            'If IsFileUploadControl(nControlType) Then
            '    sHeading = "Upload " & sHeading
            'End If
            If bIncludeSpacing Then
                sContent &= "<br />" & vbCrLf
            End If

            If bFrontend And bResponsive Then
                sContent &= "<span class=""InputHeader"">" & sHeading & "</span>"
            Else
                sContent &= GetHeadingStyle() & sHeading & "" & GetHeadingStyle(False) & ""
            End If

            sContent &= " <span id=""RequiredText"" class=""required"" style=""display:" & If(sRequired = "1" And Not bPrintable And Not bSearch, "inline", "none")
            sContent &= """>(required)</span> <div id=""RequiredCheckbox"" style=""display:none;""><input type=""checkbox"" id=""Required""> Required?</div>"

            If sListSelections = "1" And bSearch = False Then
                sContent &= "<br />" & vbCrLf
                sContent &= "<asp:label id=""lbl" & sName & "SelectedItems"" runat=""server"" />" & vbCrLf
                sContent &= "<br />" & vbCrLf
            End If
        End If
    End Sub

    Function GetPostbackAction(ByRef CurrentRow As DataRow, ByVal sPrefix As String)
        If CurrentRow.Item("PerformPostbackAction") = "1" And GetDataTable("Select * From " & DT_WEBRAD_PROJECTCONTROLPOSTBACKACTIONS & "  Where TriggerControl = " & CurrentRow.Item("ID"), cnx).Rows.Count > 0 Then
            Dim dtDataType As DataTable = GetDataTable("Select * from " & DT_WEBRAD_CONTROLDATATYPES & "  Where ID = (Select DataType From " & DT_WEBRAD_CONTROLTYPES & "  Where ID = " & CurrentRow.Item("ControlType") & ")", cnx)

            Return " On" & dtDataType.Rows(0).Item("ActionMethod") & "=""" & GetRepeaterHandlerReference(CurrentRow.Item("ID")) & sPrefix & CurrentRow.Item("Name") & "_" & dtDataType.Rows(0).Item("ActionMethod") & """"
        End If
    End Function

    Function GetRepeaterHandlerReference(ByVal nID As Integer) As String
        Dim sParentControlID, sParentControlName, sParentControlType As String

        ParentIsRepeaterControl(nID, "-1", 0, sParentControlID)
        sParentControlName = GetControlColumnValue(sParentControlID, "Name", ControlsDT)
        sParentControlType = GetControlColumnValue(sParentControlID, "ControlType", ControlsDT)

        'Potential cause of bug.  Didn't previously need try catch around IsRepeaterControl below
        Try
            If Not IsRepeaterControl(sParentControlType) Then
                sParentControlName = ""
            End If
        Catch ex As Exception
            WriteLine(nID & " - " & sParentControlType)
        End Try


        Return If(sParentControlName <> "", "rpt" & sParentControlName & "_", "")
    End Function

    Function GetControlDeclaration(ByVal sToolkitType As String, ByVal sControlType As String, ByVal nControlType As Integer)
        'If IsFileUploadControl(nControlType) Then
        '    Return sControlType & " type=""file"" "
        'Else
        Return sToolkitType & ":" & sControlType
        'End If
    End Function

    Sub WriteControlDefinition(ByRef sContent As String, ByRef CurrentRow As DataRow, ByVal sToolkitType As String, ByVal sControlType As String, ByVal sPrefix As String, ByVal sValueAttribute As String, Optional ByVal sSubstituteName As String = "")
        With CurrentRow
            Dim sName As String = If(sSubstituteName <> "", sSubstituteName, .Item("Name"))
            Dim dt As DataTable = GetDataTable(cnx, "Select * From " & DT_WEBRAD_CONTROLDATATYPES & "  Where ID = (Select DataType From " & DT_WEBRAD_CONTROLTYPES & "  Where ID = " & .Item("ControlType") & ")")

            If IsLiteralControlType(.Item("ControlType")) Then
                sContent &= .Item("Value")
            Else
                If bPrintable And dt.Rows(0).Item("LabelOnPrintable") = "1" Then
                    sContent &= "<asp:label ID=""lbl" & sName & """ Runat=""server"" />"
                Else
                    Dim sDeclaration As String = GetControlDeclaration(sToolkitType, dt.Rows(0).Item("Description"), .Item("ControlType"))

                    If .Item("ControlType") = N_CHECKBOX_CONTROL_TYPE And .Item("TextPosition") = "Before" Then
                        sContent &= .Item("Text")
                    End If

                    If .Item("HTMLType") <> "" And bHTML Then
                        sContent &= "<" & .Item("HTMLType") & " " & If(.Item("HTMLType") = "input", "type=""text""", "") & " ID=""" & sPrefix & sName & """"
                    Else
                        sContent &= "<" & sDeclaration & " ID=""" & sPrefix & sName & """ Runat=""server"""
                    End If

                    If Not bSearch Then
                        sContent &= GetPostbackAction(CurrentRow, sPrefix)
                    End If

                    sContent &= " "

                    If (.Item("Autopostback") = "1" And Not bSearch) Or .Item("ListSelections") = "1" Then
                        sContent &= " Autopostback=""True"" "
                    End If

                    If .Item("ListSelections") = "1" And bSearch = False Then
                        sContent &= " OnSelectedIndexChanged=""" & sPrefix & sName & "_SelectedIndexChanged"" "
                    End If

                    If .Item("Onchange") = "1" Then
                        sContent &= " onchange = """ & Replace(.Item("OnchangeCall"), """", "'") & """ "
                    End If

                    If GetDataTypeDescription(.Item("DataType")) <> "Label" Then
                        CheckAttribute(CurrentRow, sContent, "Value", sValueAttribute)
                    End If

                    If Not (.Item("ControlType") = N_CHECKBOX_CONTROL_TYPE And .Item("TextPosition") = "Before") Then
                        CheckAttribute(CurrentRow, sContent, "Text")
                    End If

                    CheckAttribute(CurrentRow, sContent, "Placeholder")
                    CheckAttribute(CurrentRow, sContent, "Visible", "", True)
                    CheckAttribute(CurrentRow, sContent, "Enabled", "", True)
                    CheckAttribute(CurrentRow, sContent, "TextMode")
                    CheckAttribute(CurrentRow, sContent, "Rows")
                    CheckAttribute(CurrentRow, sContent, "Columns")

                    If .Item("TextMode") <> "MultiLine" Then
                        CheckAttribute(CurrentRow, sContent, "MaxLength")

                        'If Not bResponsive Then
                        CheckAttribute(CurrentRow, sContent, "Width")
                        'End If
                    End If

                    CheckAttribute(CurrentRow, sContent, "CSSClass")
                    CheckAttribute(CurrentRow, sContent, "RepeatDirection")

                    If .Item("ControlType") <> N_REPEATER_CONTROL_TYPE Then
                        CheckAttribute(CurrentRow, sContent, "RepeatColumns")
                    End If

                    CheckAttribute(CurrentRow, sContent, "SelectionMode")
                    CheckAttribute(CurrentRow, sContent, "GroupName")


                    sContent = Trim(sContent) & ">"

                    If IsRepeaterControl(.Item("ControlType")) Then
                        sContent &= vbCrLf & "<ItemTemplate>" & vbCrLf

                        If .Item("LayoutType") = S_LAYOUTTYPE_VERTICAL And .Item("LayoutSubtype") <> "3" Then
                            sContent &= "<li>" & vbCrLf
                        ElseIf .Item("LayoutType") = S_LAYOUTTYPE_HORIZONTAL Then
                            sContent &= "<tr>" & If(.Item("LayoutSubtype") = "4", "<td>" & GetHeadingStyle() & "<%# Container.ItemIndex + 1 %>" & GetHeadingStyle(False) & "</td>", "") & vbCrLf
                        End If
                    End If

                    Dim ChildControlsDT As DataTable = GetChildControls(.Item("ID"))
                    Dim sNewRepeaterRow As String

                    For nCounter2 = 0 To ChildControlsDT.Rows.Count - 1
                        sNewRepeaterRow = ""

                        If CStr(.Item("RepeatColumns")) <> "" And CStr(.Item("RepeatColumns")) <> "0" Then
                            If (nCounter2 + 1) Mod CInt(.Item("RepeatColumns")) = 0 And nCounter2 <> ChildControlsDT.Rows.Count - 1 Then
                                sNewRepeaterRow = "</tr><tr>"
                            End If
                        End If

                        GetControlContent(ChildControlsDT.Rows(nCounter2), sContent, sNewRepeaterRow)
                    Next

                    Try
                        If CStr(.Item("SelectionItems")) <> "-1" And CStr(.Item("SelectionItems")) <> "" Then
                            sContent &= vbCrLf

                            If .Item("IncludePleaseSelect") = "1" Then
                                sContent &= "<asp:listitem value="""">Please Select</asp:listitem>" & vbCrLf
                            End If

                            Dim ListItemsDT As New DataTable
                            ListItemsDT = GetDataTable(cnx, "Select * from " & DT_WEBRAD_PROJECTCONTROLLISTITEMS & "  Where Type = 1 AND ParentID = " & .Item("ID"))

                            For nCounter2 = 0 To ListItemsDT.Rows.Count - 1
                                sContent &= "<asp:listitem value=""" & ListItemsDT.Rows(nCounter2).Item("Value") & """" & GetListItemDefaultSelected(bInsert, .Item("Required"), ListItemsDT.Rows(nCounter2).Item("Text"), ListItemsDT.Rows(nCounter2).Item("Value")) & ">" & ListItemsDT.Rows(nCounter2).Item("Text") & "</asp:listitem>" & vbCrLf
                            Next
                        End If
                    Catch ex As Exception
                        WriteLine(ex.ToString)
                    End Try

                    If IsRepeaterControl(.Item("ControlType")) Then
                        If .Item("LayoutType") = S_LAYOUTTYPE_VERTICAL And .Item("LayoutSubtype") <> "3" Then
                            sContent &= "</li>" & vbCrLf
                        ElseIf .Item("LayoutType") = S_LAYOUTTYPE_HORIZONTAL Then
                            sContent &= "</tr>" & vbCrLf
                        End If

                        sContent &= "<asp:label id=""lblID"" runat=""server"" visible=""false"" text='<%# Container.DataItem(""ID"") %>' />" & vbCrLf
                        sContent &= "</ItemTemplate>" & vbCrLf
                    End If

                    If GetDataTypeDescription(.Item("DataType")) = "Label" Then
                        If .Item("Value") <> "" Then
                            sContent &= .Item("Value")
                        End If
                    End If

                    If Not bHTML Then
                        sContent &= "</" & sDeclaration & ">"
                    End If

                    If .Item("Calendar") = "1" Then
                        sContent &= vbCrLf
                        sContent &= "<ajaxToolkit:CalendarExtender ID=""cex" & sName & """ runat=""server"" TargetControlID=""" & sPrefix & sName & """></ajaxToolkit:CalendarExtender>"
                    End If
                End If
            End If
        End With
    End Sub

    Sub CheckAttribute(ByRef CurrentRow As DataRow, ByRef sContent As String, ByVal sCurrentAttribute As String, Optional ByVal sCurrentAttributeName As String = "", Optional ByVal bIsBool As Boolean = False)
        Dim sCurrentAttributeValue As String

        If AttributeAllowed(CurrentRow, sCurrentAttribute) Then
            With CurrentRow
                Try
                    If CStr(.Item(sCurrentAttribute)) <> "" And CStr(.Item(sCurrentAttribute)) <> "-1" Then
                        If sCurrentAttributeName <> "" Then
                            sContent &= sCurrentAttributeName
                        Else
                            sContent &= sCurrentAttribute
                        End If

                        If sCurrentAttribute = "Visible" And .Item(sCurrentAttribute) = "2" Then
                            sContent &= "=" & .Item("CustomVisibleValue") & " "
                        Else
                            If bIsBool Then
                                sContent &= "=""" & CBool(.Item(sCurrentAttribute)) & """ "
                            Else
                                sContent &= "=""" & .Item(sCurrentAttribute) & """ "
                            End If
                        End If
                    End If
                Catch ex As Exception
                    'Response.Write(.Item("Heading") & " - " & sCurrentAttribute & " - " & .Item(sCurrentAttribute) & " - " & sCurrentAttributeName & "<br /><br />" & ex.ToString & "<br /><br />")
                End Try
            End With
        End If
    End Sub

    Function AttributeAllowed(ByRef CurrentRow As DataRow, ByVal sCurrentAttribute As String) As Boolean
        If sCurrentAttribute = "Enabled" Or sCurrentAttribute = "Visible" Then
            Return If(CurrentRow(sCurrentAttribute) <> "1", True, False)
        ElseIf (sCurrentAttribute = "Rows" Or sCurrentAttribute = "Columns") And GetControlDataType(CurrentRow.Item("ControlType")) = N_REPEATER_DATA_TYPE Then
            Return False
        Else
            Return True
        End If
    End Function

    Function CustomValidatorRequired(ByVal sCustomValidatorRequired As Integer, Optional ByVal sSQLInsertItemTable As String = "", Optional ByVal sTextMode As String = "", Optional ByVal nCustomValidation As String = "")
        If sSQLInsertItemTable <> "" Or sCustomValidatorRequired = 1 Or sTextMode = "MultiLine" Or nCustomValidation = "1" Then
            Return True
        End If

        Return False
    End Function


    Shared Function GetCheckScheduleCall() As String
        Dim sCheckClosedCall As String = ""

        If GetBackendOption("Schedule page") And bFrontend And Not bInsert Then
            sCheckClosedCall = "If GetQueryString(""Maintenance"") <> S_TRUE Then" & vbCrLf
            sCheckClosedCall &= "CheckSchedule()" & vbCrLf
            sCheckClosedCall &= "End If" & vbCrLf
        End If

        Return sCheckClosedCall
    End Function

    Shared Function GetScheduleTableName(ByVal ProjectDTRow As DataRow) As String
        Return Replace(FormatProjectNameForFolder(ProjectDTRow.Item("PageTitle")), "-", "") & "Schedule"
    End Function

    Function ExecuteNonQuery(ByRef cmd As SqlCommand, Optional ByVal sAssignTo As String = "", Optional ByVal nErrorImportance As Integer = N_ERROR_IMPORTANCE_IMMEDIATE, Optional ByVal bShowErrorAlert As Boolean = True, Optional ByRef cnx As SqlConnection = Nothing, Optional ByVal bWriteInfo As Boolean = False, Optional ByVal bReportError As Boolean = True, Optional bCheckAttacks As Boolean = True) As Integer
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Check if the connection exists, if not create a generic one connected to Communications.
        'This can be used for any database, but the full path to the tables must be part of the queries.
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'CheckConnection(cnx)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Check that the SqlCommand has a connection string
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        If cmd.Connection Is Nothing Then
            cmd.Connection = cnx
        End If
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Declare local variables
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim nReturn As Integer
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Check for a database intrusion
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Check for a database intrusion
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        If bCheckAttacks Then
            If CheckQueryContainsAttacks(cmd.CommandText) Then
                Return 0
            End If
        End If
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Open the connection
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        If cmd.Connection.State = ConnectionState.Closed Then
            cmd.Connection.Open()
        End If
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Execute the query
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Try
            nReturn = cmd.ExecuteNonQuery()
        Catch ex As Exception
            'If bReportError Then
            '    ReportError(ex, sAssignTo, "<p>" & GetCmdValues(cmd, False) & "</p><p>" & GetPageURL() & "</p>", nErrorImportance, bShowErrorAlert)
            'Else
            '    If sAssignTo = "" Then
            '        sAssignTo = GetCurrentUsername()
            '    End If
            '    If IsWebTeamMember(sAssignTo) Then
            '        WhitTools.Email.SendEmail(GetWebTeamMemberEmail(sAssignTo), "Error in WhitTools.SQL.ExecuteNonQuery()", "<p>The function ExecuteNonQuery threw an error, but was marked as ""Do not send error report"". This is likely because the error is in WhitTools.ErrorHandler.</p><p>ex.Message:<br />" & ex.Message & "</p><p>" & GetCmdValues(cmd, False) & "</p>", EMAIL_ERROR_REPORT, "", "", N_EMAIL_PRIORITY_HIGH)
            '    End If
            'End If
            'nReturn = cmd.ExecuteNonQuery()
        End Try
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Close the connection
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        cmd.Connection.Close()
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Check if the information should be written to the page
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        If bWriteInfo Then
            Write("<hr />" & GetCmdValues(cmd, False) & "<br /><br />Query Return Value: " & nReturn & "<hr />")
        End If
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Return the number of row affected
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Return nReturn
    End Function

    ''' <summary>
    ''' Shortens the code required to execute a non-query sql call. Returns the number of rows affected.
    ''' </summary>
    ''' <param name="cmd">The sql command to use.</param>
    ''' <param name="cnx">The sql connection to use.</param>
    ''' <param name="sAssignTo">The web team member to assign error reports to.</param>
    ''' <param name="nErrorImportance">The global enum error importance of the error.</param>
    ''' <param name="bShowErrorAlert">Show an error alert upon an error?</param>
    ''' <param name="bWriteInfo">Write the query information to the page?</param>
    ''' <param name="bReportError">Report any errors?</param>
    ''' <returns>The number of rows affected.</returns>
    ''' <remarks></remarks>
    Function ExecuteNonQuery(ByRef cmd As SqlCommand, ByRef cnx As SqlConnection, Optional ByVal sAssignTo As String = "", Optional ByVal nErrorImportance As Integer = N_ERROR_IMPORTANCE_IMMEDIATE, Optional ByVal bShowErrorAlert As Boolean = True, Optional ByVal bWriteInfo As Boolean = False, Optional ByVal bReportError As Boolean = True, Optional ByVal bCheckAttacks As Boolean = True) As Integer
        Return ExecuteNonQuery(cmd, sAssignTo, nErrorImportance, bShowErrorAlert, cnx, bWriteInfo, bReportError, bCheckAttacks)
    End Function

    ''' <summary>
    ''' Shortens the code required to execute a non-query sql call. Returns the number of rows affected.
    ''' </summary>
    ''' <param name="bWriteInfo">Write the query information to the page?</param>
    ''' <param name="cmd">The sql command to use.</param>
    ''' <param name="cnx">The sql connection to use.</param>
    ''' <param name="sAssignTo">The web team member to assign error reports to.</param>
    ''' <param name="nErrorImportance">The global enum error importance of the error.</param>
    ''' <param name="bShowErrorAlert">Show an error alert upon an error?</param>
    ''' <param name="bReportError">Report any errors?</param>
    ''' <returns>The number of rows affected.</returns>
    ''' <remarks></remarks>
    Function ExecuteNonQuery(ByVal bWriteInfo As Boolean, ByRef cmd As SqlCommand, ByRef cnx As SqlConnection, Optional ByVal sAssignTo As String = "", Optional ByVal nErrorImportance As Integer = N_ERROR_IMPORTANCE_IMMEDIATE, Optional ByVal bShowErrorAlert As Boolean = True, Optional ByVal bReportError As Boolean = True, Optional ByVal bCheckAttacks As Boolean = True) As Integer
        Return ExecuteNonQuery(cmd, sAssignTo, nErrorImportance, bShowErrorAlert, cnx, bWriteInfo, bReportError, bCheckAttacks)
    End Function

    ''' <summary>
    ''' Shortens the code required to execute a non-query sql call. Returns the number of rows affected.
    ''' </summary>
    ''' <param name="sQuery">The query to run.</param>
    ''' <param name="sAssignTo">The web team member to assign error reports to.</param>
    ''' <param name="nErrorImportance">The global enum error importance of the error.</param>
    ''' <param name="bShowErrorAlert">Show an error alert upon an error?</param>
    ''' <param name="cnx">The sql connection to use.</param>
    ''' <param name="bWriteInfo">Write the query information to the page?</param>
    ''' <param name="bReportError">Report any errors?</param>
    ''' <returns>The number of rows affected.</returns>
    ''' <remarks></remarks>
    Function ExecuteNonQuery(ByVal sQuery As String, Optional ByVal sAssignTo As String = "", Optional ByVal nErrorImportance As Integer = N_ERROR_IMPORTANCE_IMMEDIATE, Optional ByVal bShowErrorAlert As Boolean = True, Optional ByRef cnx As SqlConnection = Nothing, Optional ByVal bWriteInfo As Boolean = False, Optional ByVal bReportError As Boolean = True, Optional ByVal bCheckAttacks As Boolean = True) As Integer
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Check if the connection exists, if not create a generic one connected to Communications.
        'This can be used for any database, but the full path to the tables must be part of the queries.
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        CheckConnection(cnx)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Execute the query
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Return ExecuteNonQuery(New SqlCommand(sQuery, cnx), sAssignTo, nErrorImportance, bShowErrorAlert, cnx, bWriteInfo, bReportError, bCheckAttacks)
    End Function

    ''' <summary>
    ''' Shortens the code required to execute a non-query sql call. Returns the number of rows affected.
    ''' </summary>
    ''' <param name="bWriteInfo">Write the query information to the page?</param>
    ''' <param name="sQuery">The query to run.</param>
    ''' <param name="sAssignTo">The web team member to assign error reports to.</param>
    ''' <param name="nErrorImportance">The global enum error importance of the error.</param>
    ''' <param name="bShowErrorAlert">Show an error alert upon an error?</param>
    ''' <param name="cnx">The sql connection to use.</param>
    ''' <param name="bReportError">Report any errors?</param>
    ''' <returns>The number of rows affected.</returns>
    ''' <remarks></remarks>
    Function ExecuteNonQuery(ByVal bWriteInfo As Boolean, ByVal sQuery As String, Optional ByVal sAssignTo As String = "", Optional ByVal nErrorImportance As Integer = N_ERROR_IMPORTANCE_IMMEDIATE, Optional ByVal bShowErrorAlert As Boolean = True, Optional ByRef cnx As SqlConnection = Nothing, Optional ByVal bReportError As Boolean = True, Optional ByVal bCheckAttacks As Boolean = True) As Integer
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Check if the connection exists, if not create a generic one connected to Communications.
        'This can be used for any database, but the full path to the tables must be part of the queries.
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        CheckConnection(cnx)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Execute the query
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Return ExecuteNonQuery(New SqlCommand(sQuery, cnx), sAssignTo, nErrorImportance, bShowErrorAlert, cnx, bWriteInfo, bReportError, bCheckAttacks)
    End Function

    ''' <summary>
    ''' Shortens the code required to execute a non-query sql call. Returns the number of rows affected.
    ''' </summary>
    ''' <param name="sQuery">The query to run.</param>
    ''' <param name="cnx">The sql connection to use.</param>
    ''' <param name="sAssignTo">The web team member to assign error reports to.</param>
    ''' <param name="nErrorImportance">The global enum error importance of the error.</param>
    ''' <param name="bShowErrorAlert">Show an error alert upon an error?</param>
    ''' <param name="bWriteInfo">Write the query information to the page?</param>
    ''' <param name="bReportError">Report any errors?</param>
    ''' <returns>The number of rows affected.</returns>
    ''' <remarks></remarks>
    Function ExecuteNonQuery(ByVal sQuery As String, ByRef cnx As SqlConnection, Optional ByVal sAssignTo As String = "", Optional ByVal nErrorImportance As Integer = N_ERROR_IMPORTANCE_IMMEDIATE, Optional ByVal bShowErrorAlert As Boolean = True, Optional ByVal bWriteInfo As Boolean = False, Optional ByVal bReportError As Boolean = True, Optional ByVal bCheckAttacks As Boolean = True) As Integer
        Return ExecuteNonQuery(sQuery, sAssignTo, nErrorImportance, bShowErrorAlert, cnx, bWriteInfo, bReportError, bCheckAttacks)
    End Function

    ''' <summary>
    ''' Shortens the code required to execute a non-query sql call. Returns the number of rows affected.
    ''' </summary>
    ''' <param name="bWriteInfo">Write the query information to the page?</param>
    ''' <param name="sQuery">The query to run.</param>
    ''' <param name="cnx">The sql connection to use.</param>
    ''' <param name="sAssignTo">The web team member to assign error reports to.</param>
    ''' <param name="nErrorImportance">The global enum error importance of the error.</param>
    ''' <param name="bShowErrorAlert">Show an error alert upon an error?</param>
    ''' <param name="bReportError">Report any errors?</param>
    ''' <returns>The number of rows affected.</returns>
    ''' <remarks></remarks>
    Function ExecuteNonQuery(ByVal bWriteInfo As Boolean, ByVal sQuery As String, ByRef cnx As SqlConnection, Optional ByVal sAssignTo As String = "", Optional ByVal nErrorImportance As Integer = N_ERROR_IMPORTANCE_IMMEDIATE, Optional ByVal bShowErrorAlert As Boolean = True, Optional ByVal bReportError As Boolean = True, Optional ByVal bCheckAttacks As Boolean = True) As Integer
        Return ExecuteNonQuery(bWriteInfo, sQuery, sAssignTo, nErrorImportance, bShowErrorAlert, cnx, bReportError, bCheckAttacks)
    End Function

    Sub GetTopLevelControls()
        ControlsDT = GetDataTable(cnx, "Select * FROM " & DT_TOPLEVELPROJECTCONTROLS_V & " WHERE ProjectID = " & GetProjectID() & " AND NOT ControlType IS NULL Order by PageID asc, Position asc")
    End Sub


    Function GetSearchQuery(ByVal sSelectStatement As String) As String
        Dim sSearchQuery As String = ""

        If GetBackendOption("Archive view") Then
            sSearchQuery &= "If rblArchive.SelectedValue = ""Archive"" Then" & vbCrLf
            sSearchQuery &= "sSearchQuery = ""SELECT MT.*, 'ViewArchive' as UpdateLocation FROM Archive_" & GetAncillaryProject("SQLMainTableName") & " MT """ & vbCrLf
            sSearchQuery &= "ElseIf rblArchive.SelectedValue = ""Both"" Then" & vbCrLf
            sSearchQuery &= "sSearchQuery = ""SELECT * FROM (SELECT MT.*, 'Update' as UpdateLocation FROM " & GetAncillaryProject("SQLMainTableName") & " MT UNION SELECT AMT.*, 'ViewArchive' as UpdateLocation FROM Archive_" & GetAncillaryProject("SQLMainTableName") & " AMT) AS MT """ & vbCrLf
            sSearchQuery &= "Else" & vbCrLf
            sSearchQuery &= "sSearchQuery = ""SELECT MT.*, 'Update' as UpdateLocation FROM (" & sSelectStatement & ") AS MT """ & vbCrLf
            sSearchQuery &= "End If" & vbCrLf & vbCrLf
        Else
            sSearchQuery &= "sSearchQuery = ""SELECT MT.*, 'Update' as UpdateLocation FROM (" & sSelectStatement & ") AS MT """ & vbCrLf
        End If

        Return sSearchQuery
    End Function

    Function GetAncillaryProject(ByVal sColumnName As String) As String
        Return GetCurrentProjectDT.Rows(0).Item(sColumnName)
    End Function

    Function GetCurrentProjectDT() As DataTable
        Return If(IsAncillaryProject(), CType(GetCurrentPage().Session("AncillaryProjectDT"), DataTable), ProjectDT)
    End Function


    Function IsAncillaryProject() As Boolean
        Return If(CStr(GetSessionVariable("ProjectID")) <> "", True, False)
    End Function

    Function ControlDisplayAllowed(ByVal nDisplayLocation As Integer) As Boolean
        Return (bFrontend = True And (nDisplayLocation = 1 Or nDisplayLocation = 2)) Or (bFrontend = False And (nDisplayLocation = 1 Or nDisplayLocation = 3))
    End Function

    Public Function GetStackContainerOpening(ByVal controlID As Integer)
        Dim content As String = ""

        AddStackContainerOpen(content, controlID)

        Return content
    End Function

End Class
