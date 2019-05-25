@Code
    ViewData("Title") = "Home Page"
End Code
<div id="CKEditor"></div>

<strong>Current Project</strong>
@Html.DropDownList("ProjectList", CType(ViewBag.Projects, List(Of SelectListItem)), New With {.id = "ProjectList", .onChange = "window.location = '?projectID=' + this.value + '&reloadcache=True'"})
<div Class="tabbable">
    <div Class="row-fluid">
        <div id="selected-content">
            @*@Html.Partial("~/Views/Shared/Frontend1.vbhtml")*@
            <form id="MainForm">
                <h1>
                    <span id="FormTitleEditable"></span>
                </h1>
                <style>
                    .page-container {
                        display: flex;
                        flex-direction: row;
                    }

                        .page-container div {
                            padding: 7px;
                            margin-left: 15px;
                            margin-right: 15px;
                        }

                        .page-container .page-selection {
                            border: 1px solid black;
                        }

                    .selected-page {
                        border: 3px solid red !important;
                    }
                </style>
                <span id="Pages"></span>
                @*<div class="page-container">
                        <div class="selected-page">Page 1</div>
                        <div>Page 2</div>
                    </div>*@
            </form>

            @*@Html.Partial("~/Views/Shared/Frontend2.vbhtml")*@

            <span id="InsertControlContainer"></span>
            <span id="DeleteControlContainer"></span>

        </div>
    </div>
</div>


<div id="Controls"></div>
@Section scripts

    <script src="https://unpkg.com/react@16/umd/react.production.min.js"></script>
    <script src="https://unpkg.com/react-dom@16/umd/react-dom.production.min.js"></script>
    <script src="@Url.Content("~/scripts/utilities.js")"></script>

    <script type="text/javascript">
    let displayOptions = @Html.Raw(ViewBag.DisplayOptions);
    let dataOptions = @Html.Raw(ViewBag.DataOptions);
    let generalOptions = @Html.Raw(ViewBag.GeneralOptions);
    let actionOptions = @Html.Raw(ViewBag.ActionOptions);
    let currentPageID = @Html.Raw(ViewBag.CurrentPageID);

    let allControls = @Html.Raw(ViewBag.PlacedControls);

    let pages =  @Html.Raw(ViewBag.ProjectPages);
    let controlTypes =  @Html.Raw(ViewBag.ControlTypes);
    let controlTypeItems =  @Html.Raw(ViewBag.ControlTypeItems);
    let controlDataTypes = @Html.Raw(ViewBag.ControlDataTypes);
    let controlActionTypes = @Html.Raw(ViewBag.ControlActionTypes);
    let controlTypeActions = @Html.Raw(ViewBag.ControlTypeActions);
    let controlTypeDetails = @Html.Raw(ViewBag.ControlTypeDetails);
    let controlTypeDetailRequirements = @Html.Raw(ViewBag.ControlTypeDetailRequirements);
    let controlTypeDetailTypes = @Html.Raw(ViewBag.ControlTypeDetailTypes);
    let controlTypeDetailValues = @Html.Raw(ViewBag.ControlTypeDetailValues);
    let controlTypeDetailTypeItems = @Html.Raw(ViewBag.ControlTypeDetailTypeItems);
    let formTitle = '@ViewBag.ProjectName';


    allControls.map(SetControlDataType);
        console.log(allControls);

    let dataItemsURL = '@Url.Action("GetDataItems")';
    let addControlURL = '@Url.Action("AddControl")';
    let deleteControlURL = '@Url.Action("DeleteControl")';
    let updateControlURL = '@Url.Action("UpdateControl")';
    let updateListItemsURL = '@Url.Action("UpdateListItems")';
    let updateActionsURL = '@Url.Action("UpdateActions")';
    let updateParentURL = '@Url.Action("UpdateParent")';
    let updatePositionURL = '@Url.Action("UpdatePosition")';
    let getAvailableActionsURL = '@Url.Action("GetAvailableActions")';
    let updateDataSourceURL = '@Url.Action("UpdateDataSource")';

    </script>
    <script src="@Url.Content("~/Scripts/dist/Home/React/bundle.js")"></script>

    <script>

        $("#Projectlist").val(@ViewBag.ProjectID);
        console.log('setinsertposition funciton:', setInsertPosition);
    </script>

End Section
