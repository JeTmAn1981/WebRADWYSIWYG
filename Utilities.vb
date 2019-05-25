Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.Web.Mail
Imports System.Security
Imports System.Security.Principal
Imports System.IO
Imports System.Linq
Imports WhitTools.eCommerce
Imports WhitTools.Email
Imports WhitTools.Getter
Imports WhitTools.Setter
Imports WhitTools.File
Imports WhitTools.Filler
Imports WhitTools.Utilities
Imports WhitTools.DataTables
Imports WhitTools.Converter
Imports WhitTools.Validator
Imports WhitTools.ErrorHandler
Imports WhitTools.Formatter
Imports WhitTools.GlobalEnum
Imports WhitTools.Repeaters
Imports WhitTools.SQL
Imports WhitTools.Encryption

Public Class Utilities
    Public Const DT_WEBRAD_CONTROLACTIONTYPES As String = "web3.WebRAD.dbo.ControlActionTypes"
    Public Const DT_WEBRAD_CONTROLDATATYPES As String = "web3.WebRAD.dbo.ControlDataTypes"
    Public Const DT_WEBRAD_CONTROLSQLTYPES As String = "web3.WebRAD.dbo.ControlSQLTypes"
    Public Const DT_WEBRAD_CONTROLTYPEACTIONS As String = "web3.WebRAD.dbo.ControlTypeActions"
    Public Const DT_WEBRAD_CONTROLTYPEDETAILREQUIREMENTS As String = "web3.WebRAD.dbo.ControlTypeDetailRequirements"
    Public Const DT_WEBRAD_CONTROLTYPEDETAILS As String = "web3.WebRAD.dbo.ControlTypeDetails"
    Public Const DT_WEBRAD_CONTROLTYPEDETAILTYPES As String = "web3.WebRAD.dbo.ControlTypeDetailTypes"
    Public Const DT_WEBRAD_CONTROLTYPEDETAILCOLUMNEXCLUSIONS As String = "web3.WebRAD.dbo.ControlTypeDetailColumnExclusions"
    Public Const DT_WEBRAD_CONTROLTYPEFILETYPESALLOWED As String = "web3.WebRAD.dbo.ControlTypeFileTypesAllowed"
    Public Const DT_WEBRAD_CONTROLTYPEITEMS As String = "web3.WebRAD.dbo.ControlTypeItems"
    Public Const DT_WEBRAD_CONTROLTYPES As String = "web3.WebRAD.dbo.ControlTypes"
    Public Const DT_WEBRAD_COMPOSITECONTROLS As String = "web3.WebRAD.dbo.CompositeControls"
    Public Const DT_WEBRAD_CONTROLTYPEVALIDATORS As String = "web3.WebRAD.dbo.ControlTypeValidators"
    Public Const DT_WEBRAD_DATAMETHODTYPES As String = "web3.WebRAD.dbo.DataMethodTypes"
    Public Const DT_WEBRAD_FILETYPES As String = "web3.WebRAD.dbo.FileTypes"
    Public Const DT_WEBRAD_LAYOUTTYPES As String = "web3.WebRAD.dbo.LayoutTypes"
    Public Const DT_WEBRAD_LAYOUTSUBTYPES As String = "web3.WebRAD.dbo.LayoutSubtypes"
    Public Const DT_WEBRAD_LOGINCOLUMNTYPES As String = "web3.WebRAD.dbo.LoginColumnTypes"
    Public Const DT_WEBRAD_PROJECTBACKENDACTIONS As String = "web3.WebRAD.dbo.ProjectBackendActions"
    Public Const DT_WEBRAD_PROJECTBACKENDACTIONTYPES As String = "web3.WebRAD.dbo.ProjectBackendActionTypes"
    Public Const DT_WEBRAD_PROJECTBACKENDOPTIONCOLUMNS As String = "web3.WebRAD.dbo.ProjectBackendOptionColumns"
    Public Const DT_WEBRAD_PROJECTBACKENDOPTIONS As String = "web3.WebRAD.dbo.ProjectBackendOptions"
    Public Const DT_WEBRAD_PROJECTBACKENDOPTIONTYPES As String = "web3.WebRAD.dbo.ProjectBackendOptionTypes"
    Public Const DT_WEBRAD_PROJECTBACKENDOPTIONOPERATORTYPES As String = "web3.WebRAD.dbo.ProjectBackendOptionOperatorTypes"
    Public Const DT_WEBRAD_PROJECTBACKEND_EXPORTS As String = "web3.WebRAD.dbo.ProjectBackendExports"
    Public Const DT_WEBRAD_PROJECTBACKEND_REPORTS As String = "web3.WebRAD.dbo.ProjectBackendReports"
    Public Const DT_WEBRAD_PROJECTCONTROLFILETYPESALLOWED As String = "web3.WebRAD.dbo.ProjectControlFileTypesAllowed"
    Public Const DT_WEBRAD_PROJECTCONTROLLISTITEMS As String = "web3.WebRAD.dbo.ProjectControlListItems"
    Public Const DT_WEBRAD_PROJECTCONTROLPOSTBACKACTIONS As String = "web3.WebRAD.dbo.ProjectControlPostbackActions"
    Public Const DT_WEBRAD_PROJECTCONTROLPOSTBACKACTIONTRIGGERVALUES As String = "web3.WebRAD.dbo.ProjectControlPostbackActionTriggerValues"
    Public Const DT_WEBRAD_PROJECTCONTROLS As String = "web3.WebRAD.dbo.ProjectControls"
    Public Const DT_WEBRAD_PROJECTCOLUMNS As String = "web3.WebRAD.dbo.ProjectColumns"
    Public Const DT_WEBRAD_PROJECTPAGES As String = "web3.WebRAD.dbo.ProjectPages"
    Public Const DT_WEBRAD_PROJECTADDITIONALOPERATIONS As String = "web3.WebRAD.dbo.ProjectAdditionalOperations"
    Public Const DT_WEBRAD_PROJECTADDITIONALOPERATIONTYPES As String = "web3.WebRAD.dbo.ProjectAdditionalOperationTypes"
    Public Const DT_WEBRAD_PROJECTS As String = "web3.WebRAD.dbo.Projects"
    Public Const DT_WEBRAD_PROJECTSUPERVISORS As String = "web3.WebRAD.dbo.ProjectSupervisors"
    Public Const DT_WEBRAD_PROJECTANCILLARYMAINTENANCE As String = "web3.WebRAD.dbo.ProjectAncillaryMaintenance"
    Public Const DT_WEBRAD_PROJECTBUILDS As String = "web3.WebRAD.dbo.ProjectBuilds"
    Public Const DT_WEBRAD_PROJECTBUILDPAGES As String = "web3.WebRAD.dbo.ProjectBuildPages"
    Public Const DT_WEBRAD_SQLSERVERS As String = "web3.WebRAD.dbo.SQLServers"
    Public Const DT_WEBRAD_SQLDATABASES As String = "web3.WebRAD.dbo.SQLDatabases"
    Public Const DT_WEBRAD_VALIDATORTYPES As String = "web3.WebRAD.dbo.ValidatorTypes"
    Public Const DT_WEBRAD_PROJECTEMAILMESSAGES As String = "web3.WebRAD.dbo.ProjectEmailMessages"
    Public Const DT_WEBRAD_PROJECTEMAILMESSAGESUBMITTERCONTROLS As String = "web3.WebRAD.dbo.ProjectEmailMessageSubmitterControls"
    Public Const DT_TOPLEVELPROJECTCONTROLS_V As String = "web3.WebRAD.dbo.TopLevelProjectControls_v"
    Public Const DT_WEBRAD_PROJECTDATASOURCES As String = "web3.WebRAD.dbo.ProjectDataSources"
    Public Const DT_WEBRAD_DATASOURCEPARENTTYPES As String = "web3.WebRAD.dbo.DataSourceParentTypes"
    'Public Const DT_WEBRAD_PROJECTRETAINEDCOLUMNS As String = "web3.WebRAD.dbo.ProjectRetainedColumns"
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'Constants
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Const S_SUBMITTER_EMAIL_MESSAGE = "Thank you for submitting the <b>PageTitleHere</b> form.  Your submission has been received and is now being processed."
    Public Const S_PRINTABLE_LINK As String = "<a href=""updateprintable.aspx?ID=<%= Request.QueryString(""ID"")%>"" target=""_blank"">Printable Version</a>" & vbCrLf & "<br />" & vbCrLf
    Public Const S_CONFIRMATION_MESSAGE As String = "Your submission has been received and is now being processed."
    Public Const S_CLOSED_MESSAGE As String = "Sorry, the <strong>FormNameHere</strong> form is now closed."
    Public Const S_PROJECTFILESPATH As String = "f:\inetpub\~Whitworth\Administration\InformationSystems\Forms\WebRADProjectFiles\"
    Public Const S_PROJECTFILESNETWORKPATH As String = "\\web2\~Whitworth\Administration\InformationSystems\Forms\WebRADProjectFiles\"
    Public Const S_LAYOUTTYPE_VERTICAL As String = "1"
    Public Const S_LAYOUTTYPE_HORIZONTAL As String = "2"
    Public Const S_LAYOUTSUBTYPE_ORDEREDLIST As String = "1"
    Public Const S_LAYOUTSUBTYPE_UNORDEREDLIST As String = "2"
    Public Const S_LAYOUTSUBTYPE_NOLIST As String = "3"
    Public Const S_LAYOUTSUBTYPE_ITEMNUMBERS As String = "4"
    Public Const S_LAYOUTSUBTYPE_NOITEMNUMBERS As String = "5"
    Public Const N_REPEATER_CONTROL_TYPE As Integer = 18
    Public Const N_REPEATER_DATA_TYPE As Integer = 9
    Public Const N_PANEL_CONTROL_TYPE As Integer = 19
    Public Const N_RADIOBUTTON_CONTROL_TYPE As Integer = 3
    Public Const N_CHECKBOX_CONTROL_TYPE As Integer = 1
    Public Const N_CHECKBOXLIST_CONTROL_TYPE As Integer = 2
    Public Const N_DROPDOWNLIST_CONTROL_TYPE As Integer = 5
    Public Const N_RADIOBUTTONLIST_CONTROL_TYPE As Integer = 4
    Public Const N_LISTBOX_CONTROL_TYPE As Integer = 8
    Public Const N_TEXTBOX_CONTROL_TYPE As Integer = 6
    Public Const N_LABEL_CONTROL_TYPE As Integer = 7
    Public Const N_TEXTBOX_DATA_TYPE As Integer = 1
    Public Const N_DATE_CONTROL_TYPE As Integer = 20
    Public Const N_IDNUMBER_CONTROL_TYPE As Integer = 37
    Public Const N_SQL_INT_TYPE As Integer = 2
    Public Const N_SQL_FLOAT_TYPE As Integer = 3
    Public Const N_UPLOAD_FILE_CONTROL_TYPE As Integer = 29
    Public Const N_UPLOAD_IMAGE_CONTROL_TYPE As Integer = 28
    Public Const N_BACKENDUPDATEACTION_TYPE As Integer = 1
    Public Const N_CLASS_CONTROLID As Integer = -7
    Public Const N_DATESUBMITTED_CONTROLID As Integer = -6
    Public Const N_IDNUMBER_CONTROLID As Integer = -5
    Public Const N_FIRSTNAME_CONTROLID As Integer = -4
    Public Const N_LASTNAME_CONTROLID As Integer = -3
    Public Const N_EMAIL_CONTROLID As Integer = -2
    Public Const N_WHITWORTHID_CONTROLID As Integer = -1
    Public Const N_UPDATEREPEATERITEMS_ACTIONTYPE As Integer = 2
    Public Const N_DROPDOWNLIST_DATATYPE As Integer = 3
    Public Const N_RADIOBUTTONLIST_DATATYPE As Integer = 7
    Public Const N_LISTBOX_DATATYPE As Integer = 8
    Public Const N_LABEL_DATATYPE As Integer = 2
    Public Const N_CHECKBOXLIST_DATATYPE As Integer = 5
    Public Const N_CHECKBOX_DATATYPE As Integer = 4
    Public Const N_CUSTOMACTIONTYPE_ID As Integer = 7
    Public Const N_WEBRAD_AOPAGELOADTYPE As Integer = 1
    Public Const N_WEBRAD_AOPAGESUBMITTYPE As Integer = 2
    Public Const N_WEBRAD_AOPAGEHEADERTYPE As Integer = 3
    Public Const N_ADDITIONALOPERATIONTYPE_COMMON As Integer = 5
    Public Const N_INTEGER_SQLTYPE As Integer = 2
    Public Const N_FLOAT_SQLTYPE As Integer = 3
    Public Const N_CONTROL_DATASOURCEPARENTTYPE As Integer = 1
    Public Const N_REPORT_DATASOURCEPARENTTYPE As Integer = 2
    Public Const N_ACTION_DATASOURCEPARENTTYPE As Integer = 3
    Public Const N_DISPLAY_DETAILTYPECATEGORY As Integer = 1
    Public Const N_DATA_DETAILTYPECATEGORY As Integer = 2
    Public Const N_GENERAL_DETAILTYPECATEGORY As Integer = 3
    Public Const N_ACTION_DETAILTYPECATEGORY As Integer = 4
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'Variables
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Shared cnx As SqlConnection = CreateSQLConnection("WebRAD")

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="lsbFolders"></param>
    ''' <param name="lblSelectedfolder"></param>
    ''' <remarks></remarks>
    Shared Sub LoadDefaultFolders(ByRef lsbFolders As ListBox, ByRef lblSelectedfolder As Label)
        lblSelectedfolder.Text = "None"
        lsbFolders.Items.Clear()
        lsbFolders.Items.Add(New ListItem("Web1", "\\web1\~whitworth"))
        lsbFolders.Items.Add(New ListItem("Web2", "\\web2\~whitworth"))
        lsbFolders.Items.Add(New ListItem("Web2Dev", "\\web2\~whitworthdev"))
    End Sub

    Public Shared Function GetSQLConnection(ByVal databaseName As String)
        Return New SqlConnection("Provider=sqloledb;Data Source=web3;Initial Catalog=" & databaseName & ";Integrated Security=SSPI;")
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="lsbFolder"></param>
    ''' <param name="lblSelectedFolder"></param>
    ''' <param name="pnlFolder"></param>
    ''' <param name="txtLink"></param>
    ''' <param name="sFolder"></param>
    ''' <remarks></remarks>
    Shared Sub UpdateFolders(ByRef lsbFolder As ListBox, ByRef lblSelectedFolder As Label, ByRef pnlFolder As Panel, ByRef txtLink As TextBox, Optional ByVal sFolder As String = "")
        ImpersonateAsUser()
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Declare local variables
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim dis() As DirectoryInfo

        If sFolder <> "" And sFolder <> S_NONE Then
            Try
                dis = New DirectoryInfo(sFolder).GetDirectories
            Catch ex As Exception
                Directory.CreateDirectory(sFolder)
                dis = New DirectoryInfo(sFolder).GetDirectories
            End Try

            lblSelectedFolder.Text = sFolder

            Try
                dis = New DirectoryInfo(sFolder).GetDirectories
            Catch ex As Exception
                Try
                    Directory.CreateDirectory(sFolder)
                    dis = New DirectoryInfo(sFolder).GetDirectories
                Catch ex2 As Exception

                End Try

            End Try
        Else

            If lsbFolder.SelectedIndex > -1 Then

                Try

                    If lsbFolder.SelectedItem.Value = "return" Then

                        If lblSelectedFolder.Text = "\\web1\~whitworth" Or lblSelectedFolder.Text = "\\web2\~whitworth" Or lblSelectedFolder.Text = "\\web2\~whitworthdev" Then

                            LoadDefaultFolders(lsbFolder, lblSelectedFolder)
                            pnlFolder.Visible = lblSelectedFolder.Text <> S_NONE

                            Exit Sub
                        Else

                            dis = New DirectoryInfo(lblSelectedFolder.Text).Parent.GetDirectories

                            lblSelectedFolder.Text = New DirectoryInfo(lblSelectedFolder.Text).Parent.FullName
                        End If
                    Else
                        lblSelectedFolder.Text = lsbFolder.SelectedValue
                        dis = New DirectoryInfo(lsbFolder.SelectedValue).GetDirectories
                    End If
                Catch ex As Exception
                    'Empty catch statement
                End Try
            End If
        End If

        If lsbFolder.SelectedIndex > -1 Or (lblSelectedFolder.Text <> "" And lblSelectedFolder.Text <> S_NONE) Then

            lsbFolder.Items.Clear()
            lsbFolder.Items.Add(New ListItem("[return to parent]", "return"))

            Try
                For nCounter As Integer = 0 To dis.GetUpperBound(0)
                    'Write(dis(nCounter).Name & "<br /><br />")
                    lsbFolder.Items.Add(New ListItem(dis(nCounter).Name, dis(nCounter).FullName))
                Next
            Catch ex As Exception
                'Empty catch statement
            End Try

            pnlFolder.Visible = lblSelectedFolder.Text <> S_NONE
        End If

        If lblSelectedFolder.Text <> "" And Not txtLink Is Nothing Then
            SetLinkBasedOnFolder(txtLink, lblSelectedFolder.Text)
        End If

        UndoImpersonateAsUser()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="txtLink"></param>
    ''' <param name="sSelectedfolder"></param>
    ''' <remarks></remarks>
    Shared Sub SetLinkBasedOnFolder(ByRef txtLink As TextBox, ByVal sSelectedfolder As String)
        txtLink.Text = Replace(Replace(Replace(sSelectedfolder, "\\web1\~whitworth", ""), "\\web2\~whitworth", ""), "\", "/") & "/"
        If Left(txtLink.Text, 1) = "/" Then
            txtLink.Text = Right(txtLink.Text, txtLink.Text.Length - 1)
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sProjectID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function ProjectDetailsComplete(ByVal sProjectID As String) As Boolean
        If sProjectID <> "" Then
            Try
                If GetDataTable("Select * From " & DT_WEBRAD_PROJECTS & "  Where FrontendDetailsComplete = 1 and BackendDetailsComplete = 1 AND ID = '" & sProjectID & "'", cnx).Rows.Count > 0 Then
                    Return True
                End If
            Catch ex As Exception
                'Empty catch statement
            End Try
        End If
        Return False
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sProjectID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function CheckControlsComplete(ByVal sProjectID As String) As Boolean
        Return If(GetDataTable("Select * from " & DT_WEBRAD_PROJECTCONTROLS & " Where ID = '" & sProjectID & "' and ControlType IS NULL", cnx).Rows.Count > 0, False, True)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sPath"></param>
    ''' <param name="sLink"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function GetProjectLink(ByVal sPath As String, ByVal sLink As String) As String
        If Left(sPath, 6) = "\\web1" Then
            Return "https://www.whitworth.edu/" & sLink
        Else
            Return "http://web2/" & sLink
        End If
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sLink"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function FormatProjectLink(ByVal sLink As String) As String

        If Left(sLink, 1) = "/" Or Left(sLink, 1) = "\" Then
            sLink = Right(sLink, sLink.Length - 1)
        End If

        If Right(sLink, 1) <> "/" And Right(sLink, 1) <> "\" Then
            sLink = sLink & "/"
        End If

        Return sLink
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sPageTitle"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function FormatProjectNameForFolder(ByVal sPageTitle As String) As String
        Return Replace(Replace(sPageTitle, " ", ""), "'", "")
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sFrontendPath"></param>
    ''' <param name="sBackendPath"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function SameServerPath(ByVal sFrontendPath As String, ByVal sBackendPath As String) As Boolean
        Return If(Left(sFrontendPath, 7) = Left(sBackendPath, 7), True, False)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sPath"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function GetFilePath(ByVal sPath As String) As String

        sPath = Replace(sPath, "\\web1\~whitworth\", "c:\inetpub\~Whitworth\")
        sPath = Replace(sPath, "\\web2\~whitworth\", "f:\inetpub\~Whitworth\")

        Return sPath
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sProjectLocation"></param>
    ''' <param name="sLink"></param>
    ''' <param name="sText"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function GetFormattedLink(ByVal sProjectLocation As String, ByVal sLink As String, ByVal sText As String) As String
        If sProjectLocation = "Frontend" Then
            Return "<a href=""" & sLink & """>" & sText & "</a><span class=""SmTextWhite"">&nbsp;&gt;</span>"
        Else
            Return "<span class=""SmText""><a href=""" & sLink & """>" & sText & "</a>&nbsp;&gt;</span>"
        End If
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="nDataType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function GetDataTypeDescription(ByVal nDataType As Integer) As String
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Declare local variables
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim dtDataType As DataTable = GetDataTable("Select * From " & DT_WEBRAD_CONTROLDATATYPES & " Where ID = '" & nDataType & "'", cnx)

        If dtDataType.Rows.Count > 0 Then
            With dtDataType.Rows(0)
                Return If(.Item("DesignerDescription") <> "", .Item("DesignerDescription"), .Item("Description"))
            End With
        End If

        Return ""
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="nControlID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function GetChildControls(ByVal nControlID As Integer) As DataTable
        Return GetDataTable("Select C.*, T.DataType, D.ToolkitType,D.ValidatorMessage,D.ValueAttribute, D.Prefix, D.Description, D.ActionMethod From " & DT_WEBRAD_PROJECTCONTROLS & " C left outer join " & DT_WEBRAD_CONTROLTYPES & " T on C.ControlType = T.ID left outer join " & DT_WEBRAD_CONTROLDATATYPES & " D on T.DataType = D.ID Where ParentControlID = " & nControlID & " Order by position asc", cnx)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="nControlType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function IsPhoneControl(ByVal nControlType As Integer) As Boolean
        Return If(nControlType = "16", True, False)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="nControlType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function IsLiteralControlType(ByVal nControlType As Integer) As Boolean
        Return If(nControlType = "36" Or nControlType = "26" Or nControlType = "27", True, False)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="nControlType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function IsDateControl(ByVal nControlType As Integer) As Boolean
        Return If(nControlType = "20", True, False)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="nControlType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function IsGLAccountControl(ByVal nControlType As Integer) As Boolean
        Return If(nControlType = "30", True, False)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="nControlType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function IsYesNoControl(ByVal nControlType As Integer) As Boolean
        Return If(nControlType = "9", True, False)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="nControlID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function IsFirstAdminControl(ByVal nControlID As Integer) As Boolean
        Try
            Return If(GetDataTable("Select Top 1 ID From " & DT_WEBRAD_PROJECTCONTROLS & " Where DisplayLocation = 3 and ProjectID = '" & GetProjectID() & "' ORDER BY Position asc", cnx).Rows(0).Item("ID") = nControlID, True, False)
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="currentstatus"></param>
    ''' <param name="savedstatus"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function CheckSavedStatus(ByVal currentstatus As String, ByVal savedstatus As String) As Integer
        Try
            If savedstatus = "" Then
                Return 1
            ElseIf currentstatus <> savedstatus Then
                Return 2
            Else
                Return 3
            End If
        Catch ex As Exception
            'Empty catch statement
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="nActionType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function ActionRequiresListItems(ByVal nActionType As String) As Boolean
        Return If(nActionType = "5", True, False)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="nActionType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function ActionRequiresValueSelection(ByVal nActionType As String) As Boolean
        Return If(nActionType = "6", True, False)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="nSQLDataType"></param>
    ''' <param name="nID"></param>
    ''' <param name="sLocation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function GetSQLDataTypeName(ByVal nSQLDataType As Integer, Optional ByVal nID As Integer = 0, Optional ByVal sLocation As String = "") As String
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Declare local variables
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim dt As DataTable = GetDataTable(cnx, "Select * from " & DT_WEBRAD_CONTROLSQLTYPES & " Where ID = " & nSQLDataType)

        Try
            Return dt.Rows(0).Item("Datatype")
        Catch ex As Exception
            WriteLine(ex.ToString)
        End Try

        Return ""
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="nDataType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function GetSQLNullValue(ByVal nDataType As Integer) As String
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Declare local variables
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim dt As DataTable = GetDataTable(cnx, "Select * From " & DT_WEBRAD_CONTROLSQLTYPES & " Where ID = " & nDataType)

        If dt.Rows.Count > 0 Then

            Return dt.Rows(0).Item("NullValue")
        End If

        Return ""
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="nSQLDatabase"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function GetSQLDatabaseName(ByVal nSQLDatabase As Integer, ByVal sProjectType As String) As String

        If sProjectType = "Test" Then

            Return "Test2"
        Else

            Dim dt As DataTable = GetDataTable(cnx, "Select * From " & DT_WEBRAD_SQLDATABASES & " Where ID = " & nSQLDatabase)

            Return dt.Rows(0).Item("Name")
        End If
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="nControlType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function IsRepeaterControl(ByVal nControlType As Integer) As Boolean
        Return If(nControlType = N_REPEATER_CONTROL_TYPE, True, False)
    End Function

    Shared Function IsCompositeControl(ByVal nControlType As Integer) As Boolean
        Return GetDataTable("SELECT * FROM " & DT_WEBRAD_CONTROLTYPES & " WHERE ID = " & nControlType & " AND Composite = 1").Rows.Count > 0
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="nControlType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function IsFileUploadControl(ByVal nControlType As Integer) As Boolean
        Return If(nControlType = N_UPLOAD_FILE_CONTROL_TYPE Or nControlType = N_UPLOAD_IMAGE_CONTROL_TYPE, True, False)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="nControlType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function IsImageUploadControl(ByVal nControlType As Integer) As Boolean
        Return If(nControlType = N_UPLOAD_IMAGE_CONTROL_TYPE, True, False)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sControlID"></param>
    ''' <param name="sRepeaterID"></param>
    ''' <param name="nLayers"></param>
    ''' <param name="sNextParentControlID"></param>
    ''' <param name="sNextSQLInsertItemTable"></param>
    ''' <param name="sNextForeignID"></param>
    ''' <param name="bSearchAll"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function ParentIsRepeaterControl(ByVal sControlID As String, Optional ByVal sRepeaterID As String = "-1", Optional ByRef nLayers As Integer = 0, Optional ByRef sNextParentControlID As String = "", Optional ByRef sNextSQLInsertItemTable As String = "", Optional ByRef sNextForeignID As String = "", Optional ByVal bSearchAll As Boolean = False) As Boolean
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Declare local variables
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim dt As New DataTable
        Dim sParentControlID As String = ""

        Try
            sParentControlID = GetDataTable("Select ParentControlID From " & DT_WEBRAD_PROJECTCONTROLS & " Where ID = " & sControlID, CreateSQLConnection("WebRAD")).Rows(0).Item("ParentControlID")
        Catch ex As Exception
            'WriteLine(sControlID)
            'WriteLine(ex.ToString)
        End Try

        dt = GetDataTable("Select PC.*, CT.ParentControlTypeID from " & DT_WEBRAD_PROJECTCONTROLS & " PC inner join " & DT_WEBRAD_CONTROLTYPES & " CT on PC.ControlType = CT.ID Where PC.ID = '" & sParentControlID & "'", True)

        If dt.Rows.Count > 0 Then

            sNextParentControlID = dt.Rows(0).Item("ID")
            sNextSQLInsertItemTable = dt.Rows(0).Item("SQLInsertItemTable")
            sNextForeignID = dt.Rows(0).Item("ForeignID")

            If IsRepeaterControl(dt.Rows(0).Item("ControlType")) Then

                If (sRepeaterID <> "-1" And sRepeaterID = dt.Rows(0).Item("ID")) Or sRepeaterID = "-1" Then

                    nLayers += 1

                    Return True
                ElseIf sRepeaterID <> "-1" And bSearchAll Then

                    nLayers += 1

                    Return ParentIsRepeaterControl(dt.Rows(0).Item("ID"), sRepeaterID, nLayers, sNextParentControlID, sNextSQLInsertItemTable, sNextForeignID, bSearchAll)
                End If
            ElseIf NoParentControl(dt.Rows(0).Item("ParentControlID")) = False Then

                nLayers += 1

                Return ParentIsRepeaterControl(dt.Rows(0).Item("ID"), sRepeaterID, nLayers, sNextParentControlID, sNextSQLInsertItemTable, sNextForeignID, bSearchAll)
            End If
        End If

        Return False
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sParentControlID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function NoParentControl(ByVal sParentControlID As String) As String
        Return sParentControlID = "0" Or sParentControlID = "" Or sParentControlID = "-1"
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sControlID"></param>
    ''' <param name="sParentControlType"></param>
    ''' <param name="sParentControlID"></param>
    ''' <param name="nLayers"></param>
    ''' <param name="sNextParentControlID"></param>
    ''' <param name="sNextSQLInsertItemTable"></param>
    ''' <param name="bSearchAll"></param>
    ''' <param name="bSearchParentRepeaters"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function ParentIsControlType(ByVal sControlID As String, ByVal sParentControlType As String, Optional ByVal sParentControlID As String = "-1", Optional ByRef nLayers As Integer = 0, Optional ByRef sNextParentControlID As String = "", Optional ByRef sNextSQLInsertItemTable As String = "", Optional ByVal bSearchAll As Boolean = False, Optional ByVal bSearchParentRepeaters As Boolean = True) As Boolean
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Declare local variables
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim dt As DataTable = GetDataTable("Select PC.*, CT.ParentControlTypeID from " & DT_WEBRAD_PROJECTCONTROLS & " PC inner join " & DT_WEBRAD_CONTROLTYPES & " CT on PC.ControlType = CT.ID Where PC.ID = (Select ParentControlID From " & DT_WEBRAD_PROJECTCONTROLS & " Where ID = " & sControlID & ")", True)

        If dt.Rows.Count > 0 Then

            sNextParentControlID = dt.Rows(0).Item("ID")
            sNextSQLInsertItemTable = dt.Rows(0).Item("SQLInsertItemTable")

            If IsControlType(dt.Rows(0).Item("ControlType"), sParentControlType) Then

                If (sParentControlID <> "-1" And sParentControlID = dt.Rows(0).Item("ID")) Or sParentControlID = "-1" Then

                    nLayers += 1

                    Return True
                ElseIf sParentControlID <> "-1" And bSearchAll And bSearchParentRepeaters Then

                    nLayers += 1

                    Return ParentIsControlType(dt.Rows(0).Item("ID"), sParentControlType, sParentControlID, nLayers, sNextParentControlID, sNextSQLInsertItemTable, bSearchAll)
                End If
            ElseIf NoParentControl(dt.Rows(0).Item("ParentControlID")) = False And bSearchAll Then

                nLayers += 1

                Return ParentIsControlType(dt.Rows(0).Item("ID"), sParentControlType, sParentControlID, nLayers, sNextParentControlID, sNextSQLInsertItemTable, bSearchAll)
            End If
        End If

        Return False
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="nControlTypeID"></param>
    ''' <param name="sDataType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function IsDataType(ByVal nControlTypeID As Integer, ByVal sDataType As String) As Boolean
        Dim dtDataType As DataTable = GetDataTable("Select * FROM " & DT_WEBRAD_CONTROLDATATYPES & " WHERE ID = (Select DataType FROM " & DT_WEBRAD_CONTROLTYPES & " WHERE ID = " & nControlTypeID & ")")

        If dtDataType.Rows.Count > 0 Then
            If sDataType = dtDataType.Rows(0).Item("Description") Then
                Return True
            End If
        Else
            WriteLine("not found - Select * FROM " & DT_WEBRAD_CONTROLDATATYPES & " WHERE ID = (Select DataType FROM " & DT_WEBRAD_CONTROLTYPES & " WHERE ID = " & nControlTypeID & ")")
        End If

        Return False
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="CurrentRow"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function ControlDisplayRequiresJoin(ByRef CurrentRow As DataRow) As Boolean
        Dim sDataTextField, sDataValueField, sDataSourceType As String
        With CurrentRow
            GetDataSource(CurrentRow.Item("DataSourceID"), sDataTextField, sDataValueField, sDataSourceType)

            Return (IsDataType(.Item("ControlType"), "Dropdownlist") Or IsDataType(.Item("ControlType"), "Radiobuttonlist")) And sDataSourceType = "1" And sDataTextField <> sDataValueField
        End With
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="nControlTypeID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function GetControlDataType(ByVal nControlTypeID As Integer) As Integer
        Return GetDataTable("Select DataType FROM " & DT_WEBRAD_CONTROLTYPES & " WHERE ID = " & nControlTypeID, cnx).Rows(0).Item("DataType")
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="nControlTypeID"></param>
    ''' <param name="sControlTypeName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function IsControlType(ByVal nControlTypeID As Integer, ByVal sControlTypeName As String) As Boolean

        Select Case sControlTypeName
            Case "Repeater"
                If nControlTypeID = N_REPEATER_CONTROL_TYPE Then
                    Return True
                End If
            Case "Panel"
                If nControlTypeID = N_PANEL_CONTROL_TYPE Then
                    Return True
                End If
            Case "Checkboxlist"
                If nControlTypeID = N_CHECKBOXLIST_CONTROL_TYPE Then
                    Return True
                End If
            Case "Dropdownlist"
                If nControlTypeID = N_DROPDOWNLIST_CONTROL_TYPE Then
                    Return True
                End If
            Case "Radiobuttonlist"
                If nControlTypeID = N_RADIOBUTTONLIST_CONTROL_TYPE Then
                    Return True
                End If
            Case "Listbox"
                If nControlTypeID = N_LISTBOX_CONTROL_TYPE Then
                    Return True
                End If
            Case "Textbox"
                If nControlTypeID = N_TEXTBOX_CONTROL_TYPE Then
                    Return True
                End If
            Case "IDNumber"
                Return If(nControlTypeID = N_IDNUMBER_CONTROL_TYPE, True, False)
            Case "Date"
                If nControlTypeID = N_DATE_CONTROL_TYPE Then
                    Return True
                End If
        End Select

        Return False
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function GetControlTypesWithValues() As String
        Return "(" & N_DROPDOWNLIST_DATATYPE & "," & N_LISTBOX_DATATYPE & "," & N_CHECKBOXLIST_DATATYPE & "," & N_RADIOBUTTONLIST_DATATYPE & "," & N_TEXTBOX_DATA_TYPE & "," & N_LABEL_DATATYPE & ")"
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ncontroltypeid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function IsListControlType(ByVal ncontroltypeid As Integer) As Boolean
        Return (IsControlType(ncontroltypeid, "Checkboxlist") Or IsControlType(ncontroltypeid, "Listbox") Or IsControlType(ncontroltypeid, "Dropdownlist") Or IsControlType(ncontroltypeid, "Radiobuttonlist") Or IsControlType(ncontroltypeid, "ListBox"))
    End Function

    ''' <summary>
    ''' Checks to see if a page has already been created for this project.  If no pages exist, creates a page.  Then returns the first page's ID.
    ''' </summary>
    ''' <param name="nProjectID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function GetFirstPage(ByVal nProjectID As Integer) As Integer
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Declare local variables
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim dtPages As DataTable
        Dim nPageID As Integer

        dtPages = GetDataTable("Select * From " & DT_WEBRAD_PROJECTPAGES & " WHERE ProjectID = " & nProjectID & " Order By ID asc")

        If dtPages.Rows.Count = 0 Then

            Dim cmd As New SqlCommand

            cmd.Connection = cnx
            cmd.CommandText = "Insert " & DT_WEBRAD_PROJECTPAGES & " (ProjectID) VALUES (" & nProjectID & ")"

            ExecuteNonQuery(cmd, cnx)

            cmd.CommandText = "Select ident_current('ProjectPages')"

            cnx.Open()
            nPageID = cmd.ExecuteScalar
            cnx.Close()
        Else
            nPageID = dtPages.Rows(0).Item("ID")
        End If

        Return nPageID
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="nPageID"></param>
    ''' <remarks></remarks>
    Shared Sub CheckLastPage(Optional ByVal nPageID As Integer = -1)

        If nPageID = -1 Then
            nPageID = GetQueryString("PageID")
        End If

        Dim dtPages As DataTable = GetDataTable("Select * From " & DT_WEBRAD_PROJECTPAGES & " Where ProjectID = " & GetProjectID() & " Order by ID desc", cnx)

        If dtPages.Rows(0).Item("ID") = nPageID Then
            Redirect("frontend.aspx?ID=" & GetProjectID())
        Else

            For nCounter As Integer = 0 To dtPages.Rows.Count - 1

                If dtPages.Rows(nCounter).Item("ID") = nPageID Then

                    Redirect("controls.aspx?ID=" & GetProjectID() & "&PageID=" & dtPages.Rows(nCounter - 1).Item("ID"))
                End If
            Next
        End If
    End Sub

    Shared Function GetPreviousPage(Optional ByVal nPageID As Integer = -1) As Integer

        If nPageID = -1 Then
            nPageID = GetQueryString("PageID")
        End If

        Return GetDataTable("Select top 2 * from " & DT_WEBRAD_PROJECTPAGES & " Where ProjectID = " & GetProjectID() & " and ID <= " & nPageID & " Order by ID desc").Rows(1).Item("ID")
    End Function

    Shared Function GetSectionLinks() As String
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Declare local variables
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sSectionLinks As String = ""
        Dim dtPages As DataTable = GetDataTable("Select * From " & DT_WEBRAD_PROJECTPAGES & " WHERE ProjectID = " & GetProjectID() & " Order by ID asc", cnx)

        For nCounter As Integer = 0 To dtPages.Rows.Count - 1
            sSectionLinks &= "<tr>" & vbCrLf
            sSectionLinks &= "<td><a class=""StatusSectionLink"" href='Section" & nCounter + 1 & ".aspx'>" & dtPages.Rows(nCounter).Item("Purpose") & "</a></td>" & vbCrLf
            sSectionLinks &= "<td align=""center""><asp:label id=""lblSection" & nCounter + 1 & """ Runat=""server""></asp:label></td>" & vbCrLf
            sSectionLinks &= "</tr>" & vbCrLf
            sSectionLinks &= "<tr><td colspan=""2"">&nbsp;</td></tr>" & vbCrLf
        Next

        If DefaultCertificationPage() Then
            sSectionLinks &= "<tr>"
            sSectionLinks &= "       <td  style=""padding-right:20px""><A href=""Certification.aspx"">Certification</A></td>"
            sSectionLinks &= "       <td align=""center""><asp:label id=""lblCertification"" Runat=""server""></asp:label></td>"
            sSectionLinks &= "        </tr>"
        End If

        Return sSectionLinks
    End Function

    Shared Function GetChangePasswordLink() As String
        Return "<asp:Button id=""btnChangePassword"" runat=""server"" text=""Change Password"" cssclass=""Button"" />&nbsp;&nbsp;&nbsp;"
    End Function

    Shared Function GetChangePasswordMethod() As String
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Declare local variables
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sChangePasswordMethod As String

        sChangePasswordMethod = "Private Sub btnChangePassword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChangePassword.Click" & vbCrLf
        sChangePasswordMethod &= "Redirect(""changepassword.aspx"")" & vbCrLf
        sChangePasswordMethod &= "End Sub" & vbCrLf

        Return sChangePasswordMethod
    End Function

    Shared Function GetPagecount() As Integer
        Try
            Return GetDataTable("Select * From " & DT_WEBRAD_PROJECTPAGES & " Where ProjectID = " & GetProjectID(), cnx, True, "-1", "", False, False, False, False, False).Rows.Count
        Catch ex As Exception
            'HttpContext.Current.Response.Write("Select * From " & DT_WEBRAD_PROJECTPAGES & " Where ProjectID = " & GetProjectID())
        End Try

        Return 1
    End Function

    Shared Function GetPageInfo(ByVal nPageNumber As Integer, ByVal sColumn As String) As String
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Declare local variables
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim dtPages As DataTable = GetDataTable("Select * from " & DT_WEBRAD_PROJECTPAGES & " Where ProjectID = " & GetProjectID() & " order by ID asc")

        'WriteLine(nPageNumber)
        Try
            Return If(nPageNumber = -1, dtPages.Rows(0).Item(sColumn), dtPages.Rows(nPageNumber - 1).Item(sColumn))
        Catch ex As Exception
            Return dtPages.Rows(0).Item(sColumn)
        End Try
    End Function

    Shared Function BelongsToPage(ByVal nPageNumber As Integer, ByVal nPageID As Integer) As Boolean

        If nPageNumber = -1 Then
            Return True
        ElseIf nPageID = GetPageInfo(nPageNumber, "ID") Then
            Return True
        End If

        Return False
    End Function

    Shared Function GetSelectCertification(Optional ByVal bSearch As Boolean = False, Optional ByVal sFilterStatement As String = "") As String
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Declare local variables
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sSelectString, sSelectVariableName As String

        sSelectVariableName = GetQueryVariable(bSearch)

        If GetPagecount() > 1 Then

            sSelectString = sSelectVariableName & " &= "

            If sFilterStatement <> "" Then
                sSelectString &= " If(ddlFilter.SelectedIndex > 0, "" AND "", "" WHERE "") & "" Certification='1'"""
            Else
                sSelectString &= """ WHERE Certification = '1'"""
            End If

            Return sSelectString
        End If

        Return ""
    End Function

    Shared Function GetSectionTitle(ByVal nPageNumber As Integer) As String
        Return If(nPageNumber <> -1, " - Section " & nPageNumber & " - " & GetPageInfo(nPageNumber, "Purpose"), "")
    End Function

    Shared Function GetMainStoredProcedure(ByRef sMainStoredProcedure As String, ByVal sMainStoredProcedureParameters As String, ByVal sSQLInsertStoredProcedureName As String, ByVal nPageNumber As Integer) As String

        'If MainStoredProcedureRequired(nPageNumber) Then
        sMainStoredProcedure = "Dim cmd As New SqlCommand" & vbCrLf
        sMainStoredProcedure &= "cmd.Connection = cnx" & vbCrLf
        sMainStoredProcedure &= "cmd.CommandType = CommandType.StoredProcedure" & vbCrLf
        sMainStoredProcedure &= "cmd.CommandText = ""usp_" & If(nPageNumber >= 1, "Update", "Insert") & sSQLInsertStoredProcedureName & If(nPageNumber <> -1, "Section" & nPageNumber, "") & vbCrLf & vbCrLf

        sMainStoredProcedure &= "With cmd" & vbCrLf
        sMainStoredProcedure &= sMainStoredProcedureParameters & vbCrLf
        sMainStoredProcedure &= "End With" & vbCrLf
        'End If

        Return ""
    End Function

    Shared Function GetCertificationText() As String
        Return "I hereby certify that all information submitted via this application is genuine and true.  By clicking the Certify button below I am submitting my finished application."
    End Function

    Shared Sub AssembleIndexPage(ByRef sPageBody As String, ByVal sProjectLocation As String, Optional ByVal sEcommerce As String = "")
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Declare local variables
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sTemplatePath As String

        sTemplatePath = "f:\inetpub\~whitworth\~Templates\WebRAD\" & sProjectLocation & "\Header.eml"
        sPageBody = GetMailFile(sTemplatePath)

        sTemplatePath = "f:\inetpub\~whitworth\~Templates\WebRAD\Frontend\Index" & sEcommerce & "HTML.eml"
        sPageBody &= GetMailFile(sTemplatePath)

        sTemplatePath = "f:\inetpub\~whitworth\~Templates\WebRAD\" & sProjectLocation & "\Footer.eml"
        sPageBody &= GetMailFile(sTemplatePath)
    End Sub

    Shared Sub AssembleInfoMessagePage(ByRef sPageBody As String, ByVal sProjectLocation As String)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Declare local variables
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sTemplatePath As String

        sTemplatePath = "f:\inetpub\~whitworth\~Templates\WebRAD\" & sProjectLocation & "\Header.eml"
        sPageBody = GetMailFile(sTemplatePath)

        sTemplatePath = "f:\inetpub\~whitworth\~Templates\WebRAD\Frontend\InfoMessageHTML.eml"
        sPageBody &= GetMailFile(sTemplatePath)

        sTemplatePath = "f:\inetpub\~whitworth\~Templates\WebRAD\" & sProjectLocation & "\Footer.eml"
        sPageBody &= GetMailFile(sTemplatePath)
    End Sub

    Shared Function GetNotFinishedText() As String
        If DefaultCertificationPage() Then
            Return "Sorry, you must first finish all sections of this application before proceeding to the certification stage.  Please click the following link to return to the status page and review your completion status:<Br /><br /><a href='status.aspx'>Application Status</a>" & vbCrLf
        Else
            Return "Sorry, you must first finish all previous sections of this application before proceeding to the final section and finishing the application process.  Please click the following link to return to the status page and review the previous sections' completion statuses:<Br /><br /><a href='status.aspx'>Application Status</a>" & vbCrLf
        End If
    End Function

    Shared Function GetAlreadySubmittedText() As String
        Return "Sorry, our records show you have already submitted this application once and may not submit it again."
    End Function

    Shared Sub GetAvailableControls(ByRef dtSupplied As WhitTools.DataTablesSupplied)
        dtSupplied.AddRow("ddlControlID", "SQLSelect", "Select * FROM " & GetColumnSelectTable() & " Where ProjectID = " & GetProjectID() & " AND NOT ID IN (Select ID FROM web3.WebRAD.dbo.ProjectControls WHERE ControlType =" & N_REPEATER_CONTROL_TYPE & ") AND IncludeDatabase = 1 Order by [Position]", "Heading", "ID")
    End Sub

    Shared Function GetHeadData(ByRef ControlsDT As DataTable) As String
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Declare local variables
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sEditorReferences As String = ""
        Dim sHeadData As String = ""

        If ControlsDT Is Nothing Then

            Return vbCrLf & "<meta name=""robots"" content=""noindex"">"
        Else

            For Each CurrentRow As DataRow In ControlsDT.Rows
                With CurrentRow

                    If IsControlType(.Item("ControlType"), "Textbox") And .Item("TextMode") = "MultiLine" And .Item("RichTextUser") = 1 Then
                        sEditorReferences &= "CKEDITOR.replace('txt" & .Item("Name") & "');" & vbCrLf
                    End If
                End With
            Next

            If sEditorReferences <> "" Then
                sHeadData = "<script type=""text/javascript"" src=""/js/ckeditor/ckeditor.js""></script>" & vbCrLf
                sHeadData &= "<script>" & vbCrLf
                sHeadData &= "window.onload = function () {" & vbCrLf
                sHeadData &= sEditorReferences
                sHeadData &= "};" & vbCrLf
                sHeadData &= "</script>"
            End If
        End If

        Return sHeadData
    End Function

    Shared Function GetApplicationIDMethod(ByVal sSQLMainTable As String) As String
        Dim sGetApplicationID As String

        sGetApplicationID = "Shared Function GetCurrentApplicationID() As Integer" & vbCrLf
        sGetApplicationID &= "        Dim dtApplication As DataTable = GetDataTable(""SELECT * FROM " & sSQLMainTable & " WHERE (Certification = '0' OR Certification = 'N' OR Certification IS NULL) AND Username = '"" & Common.GetCurrentUsername() & ""'"", cnx)" & vbCrLf
        sGetApplicationID &= "        Return If(dtApplication.Rows.Count > 0, dtApplication.Rows(0).Item(""ID""), 0)" & vbCrLf
        sGetApplicationID &= "End Function" & vbCrLf & vbCrLf

        Return sGetApplicationID
    End Function

    Shared Function GetCheckApplicationFinishedMethod(ByVal sSQLMainTable As String) As String

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Declare local variables
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sCheckApplicationFinished, sSectionsComplete As String

        sSectionsComplete = ""

        For nCounter As Integer = 1 To If(DefaultCertificationPage(), GetPagecount(), GetPagecount() - 1)

            If sSectionsComplete <> "" Then
                sSectionsComplete &= " AND "
            End If

            sSectionsComplete &= "Section" & nCounter & "Complete = '1'"
        Next

        sSectionsComplete = "(" & sSectionsComplete & ")"

        sCheckApplicationFinished = "Shared Sub CheckApplicationFinished(byval sUsername as string)" & vbCrLf
        sCheckApplicationFinished &= "If GetDataTable(""Select * From " & sSQLMainTable & " Where " & GetCertificationCondition() & " AND Username = '"" & sUsername & "" ' AND " & sSectionsComplete & " "",cnx).Rows.Count = 0 Then" & vbCrLf
        sCheckApplicationFinished &= "Redirect(""notfinished.aspx"")" & vbCrLf
        sCheckApplicationFinished &= "End If" & vbCrLf
        sCheckApplicationFinished &= "End Sub" & vbCrLf & vbCrLf

        Return sCheckApplicationFinished
    End Function

    Shared Function GetCheckAlreadySubmittedMethod(ByVal sSQLMainTable As String, ByVal sMultipleSubmissions As String, ByVal sSQLAdditionalCertificationStatement As String) As String
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Declare local variables
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sCheckApplicationAlreadySubmitted, sLoginColumnNames, sLoginColumnValues As String

        LoginColumnTypes.FindAll(Function(l) l.IncludeSelectStatement = True).ForEach(
        Sub(l)
            sLoginColumnNames &= l.ColumnName & ","
            sLoginColumnValues &= "(SELECT " & l.ColumnName & " FROM "" & DV_USERINFO_V & "" WHERE Username = '"" & sUsername & ""'),"
        End Sub)

        sCheckApplicationAlreadySubmitted = "Shared Sub CheckAlreadySubmitted(ByVal sUsername as string)" & vbCrLf
        sCheckApplicationAlreadySubmitted &= "If GetDataTable(""Select * FROM " & sSQLMainTable & " WHERE Username = '"" & sUsername & ""'" & sSQLAdditionalCertificationStatement & """, cnx).Rows.Count = 0 Then" & vbCrLf
        sCheckApplicationAlreadySubmitted &= "ExecuteNonQuery(""Insert " & sSQLMainTable & " (Username, " & sLoginColumnNames & " Certification) VALUES ('"" & sUsername & ""', " & sLoginColumnValues & " NULL)"", cnx)" & vbCrLf
        sCheckApplicationAlreadySubmitted &= "ElseIf GetDataTable(""Select * From " & sSQLMainTable & " Where " & GetCertificationCondition() & " AND Username = '"" & sUsername & ""'"",cnx).Rows.Count = 0 Then" & vbCrLf

        If sMultipleSubmissions = "1" Then
            'Dim dtTables As DataTable = GetDataTable("select Distinct(TableControlID), SQLInsertItemTable, Name, ForeignID from " & DT_WEBRAD_PROJECTCOLUMNS & " PCL LEFT OUTER JOIN " & DT_WEBRAD_PROJECTCONTROLS & " C ON PCL.TableControlID = C.ID WHERE Type='Retained' AND PCL.ProjectID=" & GetProjectID() & " AND NOT TableControlID IN (Select ID FROM " & DT_WEBRAD_PROJECTCONTROLS & " Where NOT ParentControlID IS NULL)")
            Dim dtTables As DataTable = GetDataTable("select Distinct(TableControlID), SQLInsertItemTable, Name, ForeignID from " & DT_WEBRAD_PROJECTCOLUMNS & " PCL LEFT OUTER JOIN " & DT_WEBRAD_PROJECTCONTROLS & " C ON PCL.TableControlID = C.ID WHERE Type='Retained' AND PCL.ProjectID=" & GetProjectID())

            Dim sInsertRetained As String = ""

            For Each CurrentTable As DataRow In dtTables.Rows
                If CurrentTable.Item("TableControlID") = "0" Then
                    GetInsertRetainedColumns(CurrentTable.Item("TableControlID"), If(CurrentTable.Item("TableControlID") = "0", sSQLMainTable, CurrentTable.Item("SQLInsertItemTable")), sInsertRetained, sSQLMainTable, CurrentTable.Item("ForeignID"))
                ElseIf Not ParentIsRepeaterControl(GetControlColumnValue(CurrentTable.Item("TableControlID"), "ParentControlID")) Then
                    GetInsertRetainedColumns(CurrentTable.Item("TableControlID"), If(CurrentTable.Item("TableControlID") = "0", sSQLMainTable, CurrentTable.Item("SQLInsertItemTable")), sInsertRetained, sSQLMainTable, CurrentTable.Item("ForeignID"))
                End If
            Next

            If sInsertRetained <> "" Then
                sCheckApplicationAlreadySubmitted &= "dim nPreviousID, nCurrentID, nPreviousApplicationID, nCurrentApplicationID as Integer" & vbCrLf & vbCrLf
                sCheckApplicationAlreadySubmitted &= "nPreviousApplicationID = GetDataTable(""SELECT TOP 1 ID FROM " & sSQLMainTable & " WHERE Certification = '1' AND Username = '"" & sUsername & ""' ORDER BY ID DESC"", cnx).Rows(0).Item(""ID"")" & vbCrLf & vbCrLf
                sCheckApplicationAlreadySubmitted &= sInsertRetained
            Else
                sCheckApplicationAlreadySubmitted &= "ExecuteNonQuery(""INSERT INTO " & sSQLMainTable & " (Username) VALUES ('"" & sUsername & ""')"")" & vbCrLf
            End If
        Else

            sCheckApplicationAlreadySubmitted &= "Redirect(""AlreadySubmitted.aspx"")" & vbCrLf
        End If

        sCheckApplicationAlreadySubmitted &= "End If" & vbCrLf
        sCheckApplicationAlreadySubmitted &= "End Sub" & vbCrLf & vbCrLf

        Return sCheckApplicationAlreadySubmitted
    End Function

    Shared Sub GetInsertRetainedColumns(ByVal nTableControlID As Integer, ByVal sSQLTable As String, ByRef sCheckApplicationAlreadySubmitted As String, ByVal sSQLMainTableName As String, Optional ByVal sForeignID As String = "", Optional ByVal nCounter As Integer = 1, Optional ByVal sPreviousID As String = "nPreviousApplicationID", Optional ByVal sCurrentID As String = "nCurrentApplicationID")
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Declare local variables
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim dtControlInfo As DataTable = GetDataTable("SELECT * FROM " & DT_WEBRAD_PROJECTCONTROLS & " WHERE ID = " & nTableControlID & " AND ParentControlID IS NULL")

        WriteLine("SELECT * FROM " & DT_WEBRAD_PROJECTCONTROLS & " WHERE ID = " & nTableControlID & " AND ParentControlID IS NULL")

        If nTableControlID = 0 Then
            sCheckApplicationAlreadySubmitted &= "ExecuteNonQuery(""Insert Into " & sSQLTable & " ("
            sCheckApplicationAlreadySubmitted &= GetRetainedColumns(nTableControlID)
            sCheckApplicationAlreadySubmitted &= ",Username"
            sCheckApplicationAlreadySubmitted &= ") SELECT TOP 1 " & GetRetainedColumns(nTableControlID) & ",Username FROM " & sSQLTable & " WHERE ID = "" & nPreviousApplicationID,cnx)" & vbCrLf & vbCrLf
            sCheckApplicationAlreadySubmitted &= "nCurrentApplicationID = ExecuteScalar(""Select TOP 1 ID FROM " & sSQLMainTableName & " ORDER BY ID DESC"", cnx)" & vbCrLf & vbCrLf
        Else
            sCheckApplicationAlreadySubmitted &= "For Each CurrentRow" & nCounter & " as DataRow in GetDataTable(""SELECT * FROM " & sSQLTable & " WHERE " & sForeignID & " = "" & " & sPreviousID & ",cnx).Rows" & vbCrLf
            sCheckApplicationAlreadySubmitted &= "nPreviousID = CurrentRow" & nCounter & ".Item(""ID"")" & vbCrLf
            sCheckApplicationAlreadySubmitted &= "ExecuteNonQuery(""Insert Into " & sSQLTable & " (" & sForeignID & "," & GetRetainedColumns(nTableControlID) & ") "
            sCheckApplicationAlreadySubmitted &= "SELECT "" & " & sCurrentID & " & ""," & GetRetainedColumns(nTableControlID) & " FROM " & sSQLTable & " WHERE ID = "" & CurrentRow" & nCounter & ".Item(""ID""),cnx)" & vbCrLf & vbCrLf

            dtControlInfo = GetDataTable("select Distinct(TableControlID), SQLInsertItemTable, Name, ForeignID from " & DT_WEBRAD_PROJECTCOLUMNS & " PCL LEFT OUTER JOIN " & DT_WEBRAD_PROJECTCONTROLS & " C ON PCL.TableControlID = C.ID WHERE Type='Retained' and tableControlid in (select id from " & DT_WEBRAD_PROJECTCONTROLS & " where parentcontrolid = " & nTableControlID & ")")
            WriteLine("select Distinct(TableControlID), SQLInsertItemTable, Name, ForeignID from " & DT_WEBRAD_PROJECTCOLUMNS & " PCL LEFT OUTER JOIN " & DT_WEBRAD_PROJECTCONTROLS & " C ON PCL.TableControlID = C.ID WHERE Type='Retained' and tableControlid in (select id from " & DT_WEBRAD_PROJECTCONTROLS & " where parentcontrolid = " & nTableControlID & ")")

            If dtControlInfo.Rows.Count > 0 Then
                sCheckApplicationAlreadySubmitted &= "Dim nCurrentID" & nCounter & " As Integer = ExecuteScalar(""Select TOP 1 ID FROM " & sSQLTable & " ORDER BY ID DESC"", cnx)" & vbCrLf & vbCrLf
            End If

            For Each CurrentTable As DataRow In dtControlInfo.Rows
                GetInsertRetainedColumns(CurrentTable.Item("TableControlID"), CurrentTable.Item("SQLInsertItemTable"), sCheckApplicationAlreadySubmitted, sSQLMainTableName, CurrentTable.Item("ForeignID"), nCounter + 1, "nPreviousID", "nCurrentID" & nCounter)
            Next

            sCheckApplicationAlreadySubmitted &= "Next" & vbCrLf & vbCrLf
        End If
    End Sub

    Shared Function GetRetainedColumns(ByVal nTableControlID As Integer) As String
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Declare local variables
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim dtRetainedColumns As DataTable = GetDataTable("SELECT Name, ColumnControlID as ID FROM " & DT_WEBRAD_PROJECTCOLUMNS & " PCL LEFT OUTER JOIN (select ID, Name FROM " & DT_WEBRAD_PROJECTCONTROLS & " UNION Select IDNumber, ControlName as Name FROM " & DT_WEBRAD_LOGINCOLUMNTYPES & ") C ON C.ID = PCL.ColumnControlID WHERE Type='Retained' AND PCL.ProjectID = " & GetProjectID() & " AND TableControlID = " & nTableControlID)
        Dim sColumns As String = ""

        WriteLine("SELECT Name, ColumnControlID as ID FROM " & DT_WEBRAD_PROJECTCOLUMNS & " PCL LEFT OUTER JOIN (select ID, Name FROM " & DT_WEBRAD_PROJECTCONTROLS & " UNION Select IDNumber, ControlName as Name FROM " & DT_WEBRAD_LOGINCOLUMNTYPES & ") C ON C.ID = PCL.ColumnControlID WHERE Type='Retained' AND PCL.ProjectID = " & GetProjectID() & " AND TableControlID = " & nTableControlID)

        If dtRetainedColumns.Rows.Count = 0 Then
            Return ""
        ElseIf dtRetainedColumns.Rows(0).Item("ID") = "0" Then
            If IsListControlType(GetControlColumnValue(nTableControlID, "ControlType")) Then
                Return GetControlColumnValue(nTableControlID, "Name")
            Else
                dtRetainedColumns = GetDataTable("SELECT Name, ID FROM " & DT_WEBRAD_PROJECTCONTROLS & " WHERE IncludeDatabase = 1 AND ProjectID = " & GetProjectID() & " AND (SQLInsertItemTable IS NULL OR SQLInsertItemTable = '')")
                WriteLine("SELECT Name, ID FROM " & DT_WEBRAD_PROJECTCONTROLS & " WHERE IncludeDatabase = 1 AND ProjectID = " & GetProjectID() & " AND (SQLInsertItemTable IS NULL OR SQLInsertItemTable = '')")
            End If
        End If

        For Each CurrentColumn As DataRow In dtRetainedColumns.Rows
            If (nTableControlID = 0 And Not ParentIsRepeaterControl(CurrentColumn.Item("ID"))) Or (nTableControlID <> 0 And ParentIsRepeaterControl(CurrentColumn.Item("ID"), nTableControlID)) Then
                If sColumns <> "" Then
                    sColumns &= ","
                End If

                sColumns &= CurrentColumn.Item("Name")
            End If
        Next

        Return sColumns
    End Function

    'Shared Function GetRetainedColumnInserts(ByVal sSQLMinTableName As String, Optional ByVal nRepeaterID As Integer = 0, Optional ByRef sCopyRetainedColumnsData As String = "") As String
    '    sCopyRetainedColumnsData &= "ExecuteNonQuery(""Insert Into " & .Item("SQLInsertItemTable") & " Select * from  " & .Item("SQLInsertItemTable") & " WHERE " & GetRepeaterIdentityReference(.Item("ID"), .Item("ForeignID"), sSQLMainTableName, sCopyRetainedColumnsData) & """,cnx)" & vbCrLf
    '    If ControlsDT.Rows.Count > 0 Then
    '        sCopyRetainedColumnsData &= "dim nCurrentID as Integer" & vbCrLf & vbCrLf
    '        For nCounter As Integer = 0 To ControlsDT.Rows.Count - 1
    '            With ControlsDT.Rows(nCounter)
    '                If .Item("SQLInsertItemTable") <> "" And ((nRepeaterID = 0 And Not ParentIsRepeaterControl(.Item("ID"))) Or ParentIsRepeaterControl(.Item("ID"), nRepeaterID)) Then
    '                    If IsRepeaterControl(.Item("ControlType")) Then
    '                        GetRetainedColumnInserts(sSQLMainTableName, .Item("ID"), sCopyRetainedColumnsData)
    '                    End If
    '                    If nRepeaterID <> 0 Then
    '                        sCopyRetainedColumnsData &= "ExecuteNonQuery(""Insert Into " & .Item("SQLInsertItemTable") & " Select * from  " & .Item("SQLInsertItemTable") & " WHERE " & GetRepeaterIdentityReference(.Item("ID"), .Item("ForeignID"), sSQLMainTableName, sCopyRetainedColumnsData) & """,cnx)" & vbCrLf
    '                    Else
    '                        sCopyRetainedColumnsData &= "ExecuteNonQuery(""Insert Into " & .Item("SQLInsertItemTable") & " Select * from " & .Item("SQLInsertItemTable") & " WHERE " & .Item("ForeignID") & " = "" & nPreviousApplicationID,cnx)" & vbCrLf
    '                    End If
    '                End If
    '            End With
    '        Next
    '    End If
    '    Return sCopyRetainedColumnsData
    'End Function

    Shared Function GetRepeaterIdentityReference(ByVal nParentControlID As Integer, ByVal sIdentity As String, ByVal sForeignID As String, ByVal sSQLMainTableName As String, Optional ByRef sGetArchiveAncillaryData As String = "") As String
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Declare local variables
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sNextParentControlID, sNextSQLInsertItemTable, sNextForeignID As String

        sNextParentControlID = ""
        sNextSQLInsertItemTable = ""
        sNextForeignID = ""

        If ParentIsRepeaterControl(nParentControlID, "-1", 0, sNextParentControlID, sNextSQLInsertItemTable, sNextForeignID) Then
            Return sForeignID & " IN (Select ID From " & sNextSQLInsertItemTable & " Where " & GetRepeaterIdentityReference(sNextParentControlID, sIdentity, sNextForeignID, sSQLMainTableName, sGetArchiveAncillaryData) & ")"
        Else
            Return sForeignID & " in (Select ID from " & sSQLMainTableName & " Where ID = "" & " & sIdentity & " & "")"
        End If
    End Function

    Shared Function GetCommonFunctions(ByVal sSQLMainTable As String, ByVal sRequireLogin As String, ByVal sMultipleSubmissions As String, ByVal sSQLAdditionalCertificationStatement As String, ByVal sCheckClosedMethod As String) As String
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Declare local variables
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sCommonFunctions As String = ""

        If GetPagecount() > 1 Then
            sCommonFunctions &= GetCheckApplicationFinishedMethod(sSQLMainTable)
            sCommonFunctions &= GetApplicationIDMethod(sSQLMainTable)
            sCommonFunctions &= GetCheckAlreadySubmittedMethod(sSQLMainTable, sMultipleSubmissions, sSQLAdditionalCertificationStatement)
        End If

        If sRequireLogin = "1" Then
            sCommonFunctions &= GetCurrentUsernameOverload()
        End If

        If sMultipleSubmissions = "1" Then
            sCommonFunctions &= GetCheckReviewInformationMethod(sSQLMainTable)
        End If

        sCommonFunctions &= sCheckClosedMethod

        Dim dtAdditionalOperations As DataTable = GetDataTable("SELECT * FROM " & DT_WEBRAD_PROJECTADDITIONALOPERATIONS & " WHERE Type = " & N_ADDITIONALOPERATIONTYPE_COMMON & " AND ProjectID = " & GetProjectID())

        For Each CurrentRow As DataRow In dtAdditionalOperations.Rows
            sCommonFunctions &= CurrentRow.Item("AdditionalOperations")
        Next

        Return sCommonFunctions
    End Function


    Shared Function GetCheckReviewInformationMethod(ByVal sSQLMainTable As String) As String
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Declare local variables
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sCheckReviewInformation As String = "Shared Sub CheckReviewInformation()" & vbCrLf

        sCheckReviewInformation &= "If GetDataTable(""Select * From " & sSQLMainTable & " Where " & GetCertificationCondition() & " AND Username = '"" & Common.GetCurrentUsername() & ""'"", cnx).Rows.Count = 0 And GetDataTable(""Select * From " & sSQLMainTable & " Where Certification = '1' AND Username = '"" & Common.GetCurrentUsername() & ""'"", cnx).Rows.Count > 0 Then" & vbCrLf
        sCheckReviewInformation &= "CType(GetCurrentPage().FindControl(""pnlReviewInformation""),Panel).Visible=True" & vbCrLf
        sCheckReviewInformation &= "End If" & vbCrLf
        sCheckReviewInformation &= "End Sub" & vbCrLf & vbCrLf

        Return sCheckReviewInformation
    End Function

    Shared Function GetCertificationLoadDDLsContent(ByVal sRequireLogin As String) As String
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Declare local variables
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sCertificationLoadDDLsContent As String

        sCertificationLoadDDLsContent = "CheckAlreadySubmitted(" & GetUsernameReference(sRequireLogin) & ")" & vbCrLf & vbCrLf
        sCertificationLoadDDLsContent &= "CheckApplicationFinished(" & GetUsernameReference(sRequireLogin) & ")"

        Return sCertificationLoadDDLsContent
    End Function

    Shared Function GetUsernameReference(ByVal sRequireLogin As String) As String
        Return If(sRequireLogin = "1", "Common.GetCurrentusername()", "GetSessionVariable(S_USERNAME)")
    End Function

    Shared Function GetColumnSelectTable(Optional ByVal bIncludeLogin As Boolean = True) As String
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Declare local variables
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sSelectTable As String
        Dim nProjectID As Integer = GetProjectID()

        sSelectTable = "("

        If bIncludeLogin Then
            sSelectTable &= "SELECT " & nProjectID & " as ProjectID,(SELECT MIN(ID) FROM " & DT_WEBRAD_PROJECTPAGES & " WHERE ProjectID = " & nProjectID & ") as PageID, " & N_IDNUMBER_CONTROLID & " as ID,'ID' as Name,'ID' as Heading,NULL as ParentControlID,'' as SQLInsertItemTable,-11 as Position,1 as IncludeDatabase "
            sSelectTable &= "UNION "
            sSelectTable &= GetLoginColumns()
        End If

        sSelectTable &= "Select ProjectID, PageID, ID, Name, Heading, ParentControlID, SQLInsertItemTable, Position,IncludeDatabase From web3.WebRAD.dbo.ProjectControls "
        sSelectTable &= "UNION "
        sSelectTable &= "Select " & nProjectID & " as ProjectID,(SELECT MAX(ID) FROM " & DT_WEBRAD_PROJECTPAGES & " WHERE ProjectID = " & nProjectID & ") as PageID, " & N_DATESUBMITTED_CONTROLID & " as ID, '" & GetDateColumnReference() & "' as Name,'Date Submitted' as Heading,NULL as ParentControlID, '' as SQLInsertItemTable,(Select MAX(Position)+1 FROM web3.WebRAD.dbo.ProjectControls Where ProjectID = " & nProjectID & ") as Position,1 as IncludeDatabase) "
        sSelectTable &= "AS ExportColumns "

        Return sSelectTable
    End Function

    Shared Function GetDateColumnReference() As String
        Return If(GetPagecount() > 1, "CertificationDate", "DateSubmitted")
    End Function

    Shared Function GetExportColumns(Optional ByVal nParentControlID As Integer = -1, Optional ByVal bIncludeMulti As Boolean = False, Optional ByVal bIncludeLogin As Boolean = True) As DataTable
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Declare local variables
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim dtFullList As DataTable = GetDataTable("Select * FROM " & GetColumnSelectTable(bIncludeLogin) & " Where ProjectID = " & GetProjectID() & If(Not bIncludeMulti, " AND (SQLInsertItemTable is null OR SQLInsertItemTable = '') ", "") & " AND IncludeDatabase = 1 Order by PageID, [Position]", cnx)
        Dim dtSelectedList As New DataTable
        Dim temprow As DataRow
        Dim sNextParentControlID As String = ""
        Dim bAddColumn As Boolean = False

        dtSelectedList.Columns.Add("ColumnControlID", GetType(String))
        dtSelectedList.Columns.Add("Name", GetType(String))

        For Each CurrentDatarow As DataRow In dtFullList.Rows
            With CurrentDatarow

                If NoParentControl(.Item("ParentControlID")) And nParentControlID = -1 Then
                    bAddColumn = True
                Else
                    If nParentControlID = -1 Then
                        If Not ParentIsControlType(.Item("ID"), "Repeater", -1, 0, "", "", True) Then
                            bAddColumn = True
                        End If
                    Else
                        If ParentIsControlType(.Item("ID"), "Repeater", nParentControlID, 0, sNextParentControlID, "", True, False) Then
                            bAddColumn = True
                        End If
                    End If
                End If

                If bAddColumn Then
                    temprow = dtSelectedList.NewRow
                    temprow.Item("ColumnControlID") = .Item("ID")
                    temprow.Item("Name") = .Item("Name")
                    dtSelectedList.Rows.Add(temprow)
                End If

                bAddColumn = False
            End With
        Next

        Return dtSelectedList
    End Function

    Shared Sub GetAdditionalExportColumns(ByRef rptTables As Repeater)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Declare local variables
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim dtAncillaryTables As DataTable = GetDataTable("Select ID,ControlType, Name, Heading, SQLInsertItemTable From " & DT_WEBRAD_PROJECTCONTROLS & " Where ProjectID = " & GetProjectID() & " and not SQLInsertItemTable is null and not SQLInsertItemTable = '' order by position asc", cnx)
        Dim dtChildControls As DataTable = GetDataTable("Select ID, Name, Heading, SQLInsertItemTable From " & DT_WEBRAD_PROJECTCONTROLS & " Where ProjectID = " & GetProjectID() & " And IncludeDatabase = 1", cnx)
        Dim sIDs As String = ""
        Dim dtCurrentChildren As DataTable

        SelectRepeaterData(rptTables, dtAncillaryTables, cnx)

        For nCounter = 0 To rptTables.Items.Count - 1

            'Dim dtChildControls As DataTable = GetDataTable("Select ID, Name, Heading, SQLInsertItemTable From " & DT_WEBRAD_PROJECTCONTROLS & " Where ProjectID = " & GetProjectID() & " and ParentControlID = " & dtAncillaryTables.Rows(nCounter).Item("ID") & " And IncludeDatabase = 1", cnx)

            For Each currentChild In dtChildControls.Rows
                If ParentIsControlType(currentChild.Item("ID"), "Repeater", dtAncillaryTables.Rows(nCounter).Item("ID"), 0, "", "", True, False) Then
                    sIDs &= If(sIDs <> "", "," & currentChild.item("ID"), currentChild.Item("ID"))
                End If
            Next

            If sIDs <> "" Then
                dtCurrentChildren = GetDataTable("Select ID, Name, Heading, SQLInsertItemTable From " & DT_WEBRAD_PROJECTCONTROLS & " Where ID IN (" & sIDs & ")", cnx)
                FillListData(CType(rptTables.Items(nCounter).FindControl("lsbColumns"), ListBox), dtCurrentChildren, "Name", "ID", False)
            Else
                If IsListControlType(dtAncillaryTables.Rows(nCounter).Item("ControlType")) Then
                    CType(rptTables.Items(nCounter).FindControl("lsbColumns"), ListBox).Items.Insert(0, New ListItem(dtAncillaryTables.Rows(nCounter).Item("Name"), dtAncillaryTables.Rows(nCounter).Item("ID")))
                End If
            End If

            sIDs = ""

            CType(rptTables.Items(nCounter).FindControl("lsbColumns"), ListBox).Items.Insert(0, New ListItem("All", "0"))
        Next
    End Sub

    Shared Function GetLoginColumns() As String
        'Shared Function GetLoginColumns(Optional ByVal bInline As Boolean = False) As String
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Declare local variables
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sLoginColumns As String = ""
        Dim nProjectID As Integer = GetProjectID()
        Dim dtProject As DataTable = GetDataTable("Select * From " & DT_WEBRAD_PROJECTS & " Where ID = " & nProjectID, CreateSQLConnection("WebRAD"))

        If dtProject.Rows(0).Item("RequireLogin") = "1" Then
            Dim nPositionIndex As Integer = -10

            LoginColumnTypes.FindAll(Function(l) l.IncludeSelectStatement = True).ForEach(
            Sub(l)
                'If bInline Then
                '    sLoginColumns &= "UI." & l.ColumnName & ","
                'Else
                sLoginColumns &= "Select " & nProjectID & " as ProjectID,(SELECT MIN(ID) FROM " & DT_WEBRAD_PROJECTPAGES & " WHERE ProjectID = " & nProjectID & ") as PageID, " & l.ID & " as ID, '" & l.ColumnName & "' as Name, '" & l.DisplayName & "' as Heading, -1 as ParentControlID, '' as SQLInsertItemTable," & nPositionIndex & " as Position, 1 as IncludeDatabase UNION "
                nPositionIndex += 1
                'End If
            End Sub)
        End If

        Return sLoginColumns
    End Function

    Shared Sub ValidateLink(ByRef cvLink As CustomValidator, ByRef txtLinkText As TextBox, args As ServerValidateEventArgs)
        If txtLinkText.Text.Contains("http") Then
            cvLink.ErrorMessage = "Http/https prefixes may not be included in the link.  Please start your link as if it was beginning at the root of the site."
            args.IsValid = False
        ElseIf Left(txtLinkText.Text, 1) = "/" Or Left(txtLinkText.Text, 1) = "\" Then
            cvLink.ErrorMessage = "Links may not start with a / or \ character."
            args.IsValid = False
        End If
    End Sub

    Shared Sub GetAdditionalButtons(ByVal nPageNumber As Integer, ByRef sAdditionalButtons As String, ByRef sAdditionalButtonsMethods As String)
        If nPageNumber >= 1 And nPageNumber < GetPagecount() Or (nPageNumber = GetPagecount() And DefaultCertificationPage()) Then
            sAdditionalButtons = "<asp:Button ID=""btnQuit"" runat=""server"" CssClass=""Button"" Text=""Save & Exit"" />"
            sAdditionalButtonsMethods &= "Private Sub btnQuit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuit.Click" & vbCrLf
            sAdditionalButtonsMethods &= "Page.Validate()" & vbCrLf
            sAdditionalButtonsMethods &= "If Page.IsValid Then" & vbCrLf
            sAdditionalButtonsMethods &= "SaveData()" & vbCrLf
            sAdditionalButtonsMethods &= "Response.redirect(""status.aspx?Quit=True"")" & vbCrLf
            sAdditionalButtonsMethods &= "End If" & vbCrLf
            sAdditionalButtonsMethods &= "End Sub" & vbCrLf
        End If
    End Sub

    'Shared Function ShortHeadingRequired(ByVal nRequired As Integer, ByVal nControlType As Integer) As Boolean
    '    Return nRequired = 1 And (nControlType = N_CHECKBOX_CONTROL_TYPE Or nControlType = N_RADIOBUTTON_CONTROL_TYPE)
    'End Function

    Shared Function GetSQLServerName(ByVal sSQLServerName As String) As String
        Return If(sSQLServerName <> "web3", ",""" & sSQLServerName & """", "")
    End Function

    Shared Function FormatControlHeading(ByVal sHeading As String) As String
        Return If(Not sHeading.ToLower.Contains("dataitem"), Replace(sHeading, """", "'"), sHeading)
    End Function


    Shared Function GetArchiveControl() As String
        Dim sControl As String = ""

        sControl &= "<br />" & vbCrLf
        sControl &= "<br />" & vbCrLf
        sControl &= "<strong>List</strong>" & vbCrLf
        sControl &= "<br />" & vbCrLf
        sControl &= "<asp:RadiobuttonList ID=""rblArchive"" runat=""server"" RepeatDirection=""Vertical"">" & vbCrLf
        sControl &= "<asp:ListItem value=""Main"" Selected=""True"">Main Only</asp:ListItem>" & vbCrLf
        sControl &= "<asp:ListItem value=""Archive"">Archive Only</asp:ListItem>" & vbCrLf
        sControl &= "<asp:ListItem value=""Both"">Both</asp:ListItem>" & vbCrLf
        sControl &= "</asp:RadioButtonList>" & vbCrLf

        Return sControl
    End Function

    Shared Function GetBackendOption(ByVal sOptionName As String) As Boolean
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Declare local variables
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim dtBackendOptions As DataTable = GetDataTable("Select OT.ID, OT.Name From " & DT_WEBRAD_PROJECTBACKENDOPTIONS & "  O left outer join " & DT_WEBRAD_PROJECTBACKENDOPTIONTYPES & "  OT on O.Type = OT.ID Where ProjectID = " & GetProjectID() & " AND Name = '" & CleanSQL(sOptionName) & "'")

        Return If(dtBackendOptions.Rows.Count > 0, True, False)
    End Function

    Shared Function GetControlColumnValue(ByVal sControlID As String, ByVal sColumn As String, Optional ByRef ControlsDT As DataTable = Nothing, Optional ByVal sTable As String = DT_TOPLEVELPROJECTCONTROLS_V) As String
        Dim loginColumn As LoginColumnType = LoginColumnTypes.Find(Function(l) l.ID = sControlID)

        If loginColumn Is Nothing Then
            If ControlsDT Is Nothing Then
                Try
                    Return GetDataTable(CreateSQLConnection("WebRAD"), "Select * from " & sTable & " Where ID = " & sControlID).Rows(0).Item(sColumn)
                Catch ex As Exception
                    WriteLine("error - Select * from " & sTable & " Where ID = " & sControlID)
                    Return ""
                End Try
            End If

            For Each CurrentRow As DataRow In ControlsDT.Rows
                If CurrentRow.Item("ID") = sControlID Then
                    Return CurrentRow.Item(sColumn)
                End If
            Next
        Else
            Return loginColumn.ColumnName
        End If

        Return ""
    End Function

    Shared Function GetListItemDefaultSelected(ByVal bInsert As Boolean, ByVal nRequired As Integer, ByVal sItemText As String, ByVal sItemValue As String) As String
        Return If(bInsert And nRequired = 1 And sItemValue = "0" And sItemText = "No", " Selected=""True""", "")
    End Function

    Shared Function GetQueryVariable(ByVal bSearch As Boolean) As String
        Return If(bSearch, "sSearchQuery", "sSelectQuery")
    End Function

    Shared Function RepeaterHasColumns(ByVal sRepeaterColumns As String) As Boolean
        Return sRepeaterColumns <> "" And sRepeaterColumns <> "0" And sRepeaterColumns <> "-1"
    End Function

    Shared Function GetErrorMessage(ByVal sMessage As String, ByVal sHeading As String, Optional ByVal bInMethod As Boolean = False) As String

        If InStr(sHeading, "<%#") Then

            If bInMethod = False Then

                Return "'" & Replace(FormatControlHeading(sHeading), "<%# ", "<%# """ & sMessage & """ & ") & "'"
            Else

                sHeading = Replace(sHeading, "<%# container.DataItem(""", """ & ctype(source.findcontrol(""lbl")
                sHeading = Replace(sHeading, """) %>", """),Label).Text & """)

                Return """" & sMessage & sHeading & "."""
            End If
        End If

        Return """" & sMessage & FormatControlHeading(sHeading) & "."""
    End Function

    Shared Function RequireSelectedValues(ByRef CurrentRow As DataRow) As Boolean
        With CurrentRow
            If .Item("DataSourceType") = "1" Or (.Item("DataSourceType") = 2 And GetDataTable("SELECT * FROM " & DT_WEBRAD_PROJECTDATASOURCES & " WHERE ID = " & .Item("DataSourceID") & " AND len([Where]) = 0").Rows.Count > 0) Then
                Return True
            End If
        End With

        Return False
    End Function

    Shared Function GetDataSource(ByVal nDataSourceID As Integer, Optional ByRef sDataTextField As String = "", Optional ByRef sDataValueField As String = "", Optional ByRef sDataSourceType As String = "", Optional ByVal sIDSelect As String = "") As String
        Dim dtDataSource As DataTable = GetDataTable("SELECT * FROM " & DT_WEBRAD_PROJECTDATASOURCES & " WHERE ID = " & nDataSourceID)

        If dtDataSource.Rows.Count > 0 Then
            With dtDataSource.Rows(0)
                sDataTextField = .Item("TextField")
                sDataValueField = .Item("ValueField")
                sDataSourceType = .Item("Type")
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                'Declare local variables
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                Dim sDataSource As String

                If .Item("Type") = "1" Then
                    If .Item("Source") = "" Then
                        sDataSource = "SELECT " & .Item("Select")

                        If .Item("Table") <> "" Then
                            sDataSource &= " FROM " & .Item("Table")
                        End If

                        If sIDSelect <> "" Then
                            sDataSource &= " WHERE ID IN ("" & GetListOfSelectedValues(rptSubmissions) & "")"
                        ElseIf .Item("Where") <> "" Then
                            sDataSource &= " WHERE " & .Item("Where")
                        End If

                        If .Item("GroupBy") <> "" Then
                            sDataSource &= " GROUP BY " & .Item("GroupBy")
                        End If

                        If .Item("OrderBy") <> "" Then
                            sDataSource &= " ORDER BY " & .Item("OrderBy")
                        End If
                    Else
                        sDataSource = .Item("Source")
                    End If

                    Return sDataSource
                Else
                    Return .Item("Source")
                End If
            End With
        End If

        Return ""
    End Function

    Shared Sub ShowDataSource(ByRef ucDataSource As UserControl)
        With ucDataSource
            CType(.FindControl("pnlDataSource"), Panel).Visible = IIf(CType(.FindControl("rblDataSourceType"), RadioButtonList).SelectedIndex = 1, True, False)
            CType(.FindControl("pnlDataSourceSpecific"), Panel).Visible = IIf(CType(.FindControl("rblDataSourceType"), RadioButtonList).SelectedIndex = 0, True, False)
        End With
    End Sub

    Shared Function GetSetValueDataTypes() As String
        Return "1,2,3,5,7,8"
    End Function

    Shared Sub SaveColumnsInfo(ByVal sType As String, ByRef lsbMainColumns As ListBox, ByRef pnlCurrent As Panel, Optional ByRef rptTables As Repeater = Nothing, Optional ByVal nTypeID As Integer = 0)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Declare local variables
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim cmd As New SqlCommand("usp_InsertProjectColumn", cnx)

        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@ProjectID", GetProjectID())
        cmd.Parameters.AddWithValue("@TableControlID", "")
        cmd.Parameters.AddWithValue("@ColumnControlID", "")
        cmd.Parameters.AddWithValue("@Type", sType)
        cmd.Parameters.AddWithValue("@TypeID", nTypeID)

        If pnlCurrent.Visible Then

            cmd.Parameters("@TableControlID").Value = "0"

            SaveColumn(lsbMainColumns, cmd)

            If Not rptTables Is Nothing Then

                For Each CurrentItem As RepeaterItem In rptTables.Items

                    cmd.Parameters("@TableControlID").Value = CType(CurrentItem.FindControl("lblID"), Label).Text

                    SaveColumn(CType(CurrentItem.FindControl("lsbColumns"), ListBox), cmd)
                Next
            End If
        End If
    End Sub

    Shared Sub SaveColumn(ByRef lsbCurrent As ListBox, ByRef cmd As SqlCommand)

        If lsbCurrent.Items(0).Selected And lsbCurrent.Items(0).Text = "All" Then

            cmd.Parameters("@ColumnControlID").Value = "0"

            ExecuteNonQuery(cmd, "tryan", 3, False, cnx)
        Else

            For Each CurrentItem As ListItem In lsbCurrent.Items

                If CurrentItem.Selected Then

                    cmd.Parameters("@ColumnControlID").Value = CurrentItem.Value

                    ExecuteNonQuery(cmd, "tryan", 3, False, cnx)
                End If
            Next
        End If
    End Sub

    Shared Function GetProjectID() As String
        Return If(HttpContext.Current.Session("ProjectID") <> "", HttpContext.Current.Session("ProjectID"), HttpContext.Current.Request.QueryString("ID"))
    End Function


    'Shared Function GetProjectID() As String
    '    Return If(GetSessionVariable("ProjectID") <> "", GetSessionVariable("ProjectID"), GetQueryString("ID"))
    'End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sCurrentStoredProcedure"></param>
    ''' <param name="nCurrentID"></param>
    ''' <param name="sCurrentPrefix"></param>
    ''' <param name="sCurrentName"></param>
    ''' <param name="cnx"></param>
    ''' <remarks></remarks>
    Shared Sub SaveAncillaryContent(ByVal sCurrentStoredProcedure As String, ByVal nCurrentID As Integer, ByVal sCurrentPrefix As String, ByVal sCurrentName As String, Optional ByRef cnx As SqlConnection = Nothing, Optional ByVal sCurrentForeignID As String = "ForeignID")
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Declare local variables
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sCurrentValue As String
        Dim nSaveAncillaryCounter, nItemCount As Integer
        Dim cmdSaveAncillary As New SqlCommand
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Check if the connection exists, if not create a generic one connected to Communications.
        'This can be used for any database, but the full path to the tables must be part of the queries.
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        CheckConnection(cnx)

        If sCurrentPrefix = "lsb" Then
            nItemCount = CType(GetCurrentPage().FindControl(sCurrentPrefix & sCurrentName), ListBox).Items.Count
        ElseIf sCurrentPrefix = "cbl" Then
            nItemCount = CType(GetCurrentPage().FindControl(sCurrentPrefix & sCurrentName), CheckBoxList).Items.Count
        End If

        cmdSaveAncillary.CommandText = "usp_Insert" & sCurrentStoredProcedure
        cmdSaveAncillary.CommandType = CommandType.StoredProcedure
        cmdSaveAncillary.Connection = cnx
        cmdSaveAncillary.Parameters.AddWithValue("@" & sCurrentForeignID, nCurrentID)
        cmdSaveAncillary.Parameters.AddWithValue("@" & sCurrentName, "")

        For nSaveAncillaryCounter = 0 To nItemCount - 1

            sCurrentValue = ""

            If sCurrentPrefix = "lsb" Then
                If CType(GetCurrentPage().FindControl(sCurrentPrefix & sCurrentName), ListBox).Items(nSaveAncillaryCounter).Selected Then
                    sCurrentValue = CType(GetCurrentPage().FindControl(sCurrentPrefix & sCurrentName), ListBox).Items(nSaveAncillaryCounter).Value
                End If
            ElseIf sCurrentPrefix = "cbl" Then
                If CType(GetCurrentPage().FindControl(sCurrentPrefix & sCurrentName), CheckBoxList).Items(nSaveAncillaryCounter).Selected Then
                    sCurrentValue = CType(GetCurrentPage().FindControl(sCurrentPrefix & sCurrentName), CheckBoxList).Items(nSaveAncillaryCounter).Value
                End If
            End If

            If sCurrentValue <> "" Then
                cmdSaveAncillary.Parameters("@" & sCurrentName).Value = sCurrentValue
                Try
                    ExecuteNonQuery(cmdSaveAncillary, "", 1, True, cnx)
                Catch ex As Exception
                    GetCurrentPage().Response.Write(ex.ToString & "<br /><br />")
                End Try
            End If
        Next
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sCurrentStoredProcedure"></param>
    ''' <param name="nCurrentID"></param>
    ''' <param name="cnx"></param>
    ''' <remarks></remarks>
    Shared Sub SaveAncillaryRepeaterContent(ByVal sCurrentStoredProcedure As String, ByVal nCurrentID As Integer, Optional ByRef cnx As SqlConnection = Nothing)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Declare local variables
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sCurrentValue, sCurrentPrefix As String
        Dim nCounter, nCounter2 As Integer
        Dim cmdSaveAncillary As New SqlCommand
        Dim dtChildControl, dtParentControl As DataTable
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Check if the connection exists, if not create a generic one connected to Communications.
        'This can be used for any database, but the full path to the tables must be part of the queries.
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        CheckConnection(cnx)

        cmdSaveAncillary.CommandText = "usp_Insert" & sCurrentStoredProcedure
        cmdSaveAncillary.CommandType = CommandType.StoredProcedure
        cmdSaveAncillary.Connection = cnx

        dtParentControl = GetDataTable("SELECT * FROM WebRADProjectControls C LEFT OUTER JOIN WebRADProjectControlTypes T ON C.ControlType=T.ID WHERE C.ID='" & nCurrentID & "'", cnx)

        dtChildControl = GetDataTable("SELECT * FROM WebRADProjectControls C LEFT OUTER JOIN WebRADProjectControlTypes T ON C.ControlType=T.ID LEFT OUTER JOIN WebRADControlDataTypes D ON D.ID=C.DataType WHERE C.ParentControlID='" & nCurrentID & "'", cnx)

        For nCounter = 0 To CType(GetCurrentPage().FindControl(dtParentControl.Rows(0).Item("Prefix") & dtParentControl.Rows(0).Item("Name")), Repeater).Items.Count - 1

            cmdSaveAncillary.Parameters.Clear()
            cmdSaveAncillary.Parameters.AddWithValue("@ForeignID", nCurrentID)

            For nCounter2 = 0 To dtChildControl.Rows.Count - 1
                With dtChildControl.Rows(nCounter2)

                    sCurrentPrefix = .Item("Prefix")
                    sCurrentValue = ""

                    If sCurrentPrefix = "txt" Then
                        sCurrentValue = CType(GetCurrentPage().FindControl(sCurrentPrefix & .Item("Name")), TextBox).Text
                    ElseIf sCurrentPrefix = "lbl" Then
                        sCurrentValue = CType(GetCurrentPage().FindControl(sCurrentPrefix & .Item("Name")), Label).Text
                    ElseIf sCurrentPrefix = "ddl" Or sCurrentPrefix = "rbl" Then
                        sCurrentValue = CType(GetCurrentPage().FindControl(sCurrentPrefix & .Item("Name")), DropDownList).SelectedValue
                    ElseIf sCurrentPrefix = "rbl" Then
                        sCurrentValue = CType(GetCurrentPage().FindControl(sCurrentPrefix & .Item("Name")), RadioButtonList).SelectedValue
                    ElseIf sCurrentPrefix = "chk" Or sCurrentPrefix = "rad" Then
                        sCurrentValue = CType(GetCurrentPage().FindControl(sCurrentPrefix & .Item("Name")), CheckBox).Checked
                    ElseIf sCurrentPrefix = "rad" Then
                        sCurrentValue = CType(GetCurrentPage().FindControl(sCurrentPrefix & .Item("Name")), RadioButton).Checked
                    End If

                    cmdSaveAncillary.Parameters.AddWithValue("@" & .Item("Name"), sCurrentValue)
                End With
            Next

            Try
                ExecuteNonQuery(cmdSaveAncillary)
            Catch ex As Exception
                ReportError(ex, "tryan", "<p>" & GetCmdValues(cmdSaveAncillary) & "</p>", N_ERROR_IMPORTANCE_NORMAL, False)
            End Try
        Next
    End Sub

    Shared Function GetCurrentUsernameOverload(ByVal nRequireLogin As Integer, ByVal bInsert As Boolean) As String
        Return If(nRequireLogin = "1" And Not bInsert, GetCurrentUsernameOverload(), "")
    End Function

    Shared Function GetCurrentUsernameOverload() As String
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Declare local variables
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sGetCurrentUsernameOverload As String = ""

        sGetCurrentUsernameOverload &= "Shared Function GetCurrentUsername() As String" & vbCrLf
        sGetCurrentUsernameOverload &= "              If IsWebTeamMember() " & GetProjectSupervisors() & " Then" & vbCrLf
        sGetCurrentUsernameOverload &= "            If GetQueryString(""Username"") = ""None"" Then" & vbCrLf
        sGetCurrentUsernameOverload &= "                SetSessionVariable(""Username"", """")" & vbCrLf
        sGetCurrentUsernameOverload &= "            ElseIf GetQueryString(""Username"") <> """" Then" & vbCrLf
        sGetCurrentUsernameOverload &= "                SetSessionVariable(""Username"", GetQueryString(""Username""))" & vbCrLf
        sGetCurrentUsernameOverload &= "            End If" & vbCrLf
        sGetCurrentUsernameOverload &= "" & vbCrLf
        sGetCurrentUsernameOverload &= "            If GetSessionVariable(""Username"") <> """" Then" & vbCrLf
        sGetCurrentUsernameOverload &= "                Return GetSessionVariable(""Username"")" & vbCrLf
        sGetCurrentUsernameOverload &= "            End If" & vbCrLf
        sGetCurrentUsernameOverload &= "        End If" & vbCrLf
        sGetCurrentUsernameOverload &= "" & vbCrLf
        sGetCurrentUsernameOverload &= "        Return WhitTools.Getter.GetCurrentUsername()" & vbCrLf
        sGetCurrentUsernameOverload &= "End Function" & vbCrLf

        Return sGetCurrentUsernameOverload
    End Function

    Shared Function GetProjectSupervisors() As String
        Dim sSupervisors As String = GetListofValues("SELECT * FROM " & DT_WEBRAD_PROJECTSUPERVISORS & " S  LEFT OUTER JOIN " & DV_USERINFO_V & " UI ON S.SupervisorID = UI.IDNumber WHERE ProjectID = " & GetProjectID(), "Username", """ OR WhitTools.Getter.GetCurrentUsername() = """)

        Return If(sSupervisors <> "", " OR WhitTools.Getter.GetCurrentUsername() = """ & sSupervisors & """", "")
    End Function

    Shared Function GetCertificationCondition() As String
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Declare local variables
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sAdditionalCertificationStatement As String = GetDataTable("SELECT * FROM " & DT_WEBRAD_PROJECTS & " WHERE ID = " & GetProjectID()).Rows(0).Item("SQLAdditionalCertificationStatement")

        Return "(Certification IS NULL OR Certification = '0' OR Certification = 'N')" & sAdditionalCertificationStatement
    End Function


    Shared Function DefaultCertificationPage() As Boolean
        Return GetDataTable("SELECT * FROM " & DT_WEBRAD_PROJECTPAGES & " WHERE ProjectID = " & GetQueryString("ID") & " AND Certification = 1").Rows.Count = 0
    End Function

    Shared Function GetPathLink(ByVal sCurrent As String, ByVal sProjectType As String, Optional ByVal sCurrentType As String = "Link") As String

        If sProjectType = "Test" Then

            Dim sFolder As String = ""

            If Right(sCurrent, 1) = "/" Then
                sCurrent = sCurrent.Substring(0, sCurrent.Length - 1)
            End If

            For nCounter As Integer = sCurrent.Length - 1 To 0 Step -1

                If sCurrent.Substring(nCounter, 1) <> "/" And sCurrent.Substring(nCounter, 1) <> "\" Then
                    sFolder &= sCurrent.Substring(nCounter, 1)
                Else
                    Exit For
                End If
            Next

            sFolder = StrReverse(sFolder)

            If sCurrentType = "Path" Then
                sFolder = "\\web" & If(InStr(sCurrent, "web1"), "1", "2") & "\~whitworth\~Test\Tom\" & sFolder
            Else
                sFolder = "~Test/Tom/" & sFolder
            End If

            Return sFolder
        End If

        Return sCurrent
    End Function


    Shared Function GetCustomScript() As String
        Dim sScript As String = ""

        For Each CurrentRow As DataRow In GetDataTable("SELECT * FROM " & DT_WEBRAD_PROJECTCONTROLS & " WHERE ProjectID = " & GetQueryString("ID") & " AND Onchange = 1 AND OnchangeBody <> '' ORDER BY Position asc").Rows
            sScript &= CurrentRow.Item("OnchangeBody") & vbCrLf & vbCrLf
        Next

        Return sScript
    End Function

    Public Shared Sub UpdateRedundantControlName(ByVal sName As String, ByVal nControlID As Integer, ByRef nAddIndex As Integer)
        WriteLine("SELECT * FROM " & DT_WEBRAD_PROJECTCONTROLS & " WHERE ProjectID = " & GetQueryString("ID") & " AND Name = '" & sName & nAddIndex & "'")
        If GetDataTable("SELECT * FROM " & DT_WEBRAD_PROJECTCONTROLS & " WHERE ProjectID = " & GetQueryString("ID") & " AND Name = '" & sName & nAddIndex & "'").Rows.Count = 0 Then
            WriteLine("UPDATE " & DT_WEBRAD_PROJECTCONTROLS & " SET Name = Name + '" & nAddIndex & "' WHERE ID = " & nControlID)
            ExecuteNonQuery("UPDATE " & DT_WEBRAD_PROJECTCONTROLS & " SET Name = Name + '" & nAddIndex & "' WHERE ID = " & nControlID)
            nAddIndex += 1
        End If
    End Sub

    Public Shared Sub CreateLoginColumnTypes()
        Dim currentType As LoginColumnType
        LoginColumnTypes = New List(Of LoginColumnType)

        currentType = New LoginColumnType(N_IDNUMBER_CONTROLID)
        currentType.DisplayName = "Submission ID"
        currentType.ColumnName = "ID"
        currentType.IncludeSelectStatement = False
        currentType.SQLType = "Int"
        LoginColumnTypes.Add(currentType)

        currentType = New LoginColumnType(N_WHITWORTHID_CONTROLID)
        currentType.DisplayName = "Whitworth ID Number"
        currentType.ColumnName = "IDNumber"
        currentType.ControlType = "Textbox"
        currentType.ControlReference = "txtIDNumber"
        currentType.ControlMaxLength = 7
        currentType.ControlWidth = 50
        currentType.SQLType = "varchar(10)"
        LoginColumnTypes.Add(currentType)

        currentType = New LoginColumnType(N_FIRSTNAME_CONTROLID)
        currentType.DisplayName = "First Name"
        currentType.ColumnName = "FirstName"
        currentType.ControlType = "Textbox"
        currentType.ControlReference = "txtFirstName"
        currentType.ControlMaxLength = 50
        currentType.ControlWidth = 300
        LoginColumnTypes.Add(currentType)

        currentType = New LoginColumnType(N_LASTNAME_CONTROLID)
        currentType.DisplayName = "Last Name"
        currentType.ColumnName = "LastName"
        currentType.ControlType = "Textbox"
        currentType.ControlReference = "txtLastName"
        currentType.ControlMaxLength = 50
        currentType.ControlWidth = 300
        LoginColumnTypes.Add(currentType)

        currentType = New LoginColumnType(N_EMAIL_CONTROLID)
        currentType.DisplayName = "E-mail"
        currentType.ColumnName = "Email"
        currentType.ControlType = "Textbox"
        currentType.ControlReference = "txtEmail"
        currentType.ControlMaxLength = 50
        currentType.ControlWidth = 300
        LoginColumnTypes.Add(currentType)

        currentType = New LoginColumnType(N_CLASS_CONTROLID)
        currentType.DisplayName = "Class"
        currentType.ColumnName = "Class"
        currentType.ControlType = "Dropdownlist"
        currentType.ControlReference = "ddlClass"
        LoginColumnTypes.Add(currentType)

        currentType = New LoginColumnType(N_DATESUBMITTED_CONTROLID)
        currentType.DisplayName = "Date Submitted"
        currentType.ControlType = "Textbox"
        currentType.ControlReference = "txtDateSubmitted"
        currentType.ColumnName = GetDateColumnReference()
        currentType.BackendDisplayValue = "<%# ctype(Container.Dataitem(""" & currentType.ColumnName & """),datetime).ToShortDateString %>"
        currentType.ControlMaxLength = 10
        currentType.ControlWidth = 100
        currentType.IncludeSelectStatement = False
        currentType.SQLType = "DateTime"
        LoginColumnTypes.Add(currentType)
    End Sub

    Public Shared LoginColumnTypes As List(Of LoginColumnType)

    Public Class LoginColumnType
        Public Sub New(ByVal ID As Integer)
            Me.ID = ID
            Display = False
            ControlMaxLength = 0
            ControlWidth = 0
            IncludeSelectStatement = True
            SQLType = "varchar(50)"
            BackendDisplayValue = ""
        End Sub

        Public ID, ControlMaxLength, ControlWidth As Integer
        Public DisplayName, ColumnName, ControlType, ControlReference, BackendDisplayValue, SQLType As String
        Public Display, IncludeSelectStatement As Boolean
    End Class

    Public Class WebRADControl
        Public key As String
        Public item As ContextMenuItem
    End Class

    Public Class ContextMenuItem
        Public name As String
    End Class
End Class
