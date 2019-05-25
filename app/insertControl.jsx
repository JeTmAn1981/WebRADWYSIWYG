import { ConfigurationTab } from './configuration/configurationTab.jsx';

export default class InsertControl extends React.Component {
constructor(props)
{
    super(props);

    this.state = { selectedControlType: null, valid: false, data: {} };
    this.ControlTypeSelected = this.ControlTypeSelected.bind(this);
    this.CloseInsert = this.CloseInsert.bind(this);
    this.SubmitForm = this.SubmitForm.bind(this);
    this.updateValue = this.updateValue.bind(this);
    this.escapePressed = this.escapePressed.bind(this);
}

componentDidMount()
{
    document.addEventListener("keydown", this.escapePressed, false);

    let insertControl = this;
    
    $("#AddControlForm").on("submit", function (event) {
        event.preventDefault();

        ConvertFormValues(this);
    
        var data = $(this).serialize();
            
        $.ajax({
            type: "POST",
            url: addControlURL,
            data: data, 
            success: function (info) {
                info.ControlType = insertControl.state.selectedControlType;
                              
                insertControl.props.AddPlacedControl(info);
            }
        });
    });
}

componentWillUnmount() {
    document.removeEventListener("keydown", this.escapePressed, false);
}

    ControlTypeSelected(e) {
        var data = {};
        let defaultValue = null;
        let controlType = e.target.value;

        for (var prop in allControls[0]) {
            if (prop != "Position" && allControls[0].hasOwnProperty(prop)) {
                let detailType = controlTypeDetailTypes.filter(ctdt => ctdt.Name == prop)[0];

                if (detailType) {
                    if (prop == "SQLDataType") {
                        console.log(controlTypeDetailTypes);
                        console.log(detailType);
                        console.log(controlType);

                        defaultValue = controlTypeDetailValues.filter(ctdv => ctdv.ControlTypeID == controlType && ctdv.DetailTypeID == detailType.ID)[0];
                        console.log(defaultValue);
                    }

                    defaultValue = controlTypeDetailValues.filter(ctdv => ctdv.ControlTypeID == controlType && ctdv.DetailTypeID == detailType.ID)[0];
                }

                data[prop] = defaultValue ? defaultValue.Value : '';

            }
        }

        data.ProjectControlListItems = controlTypeItems.filter(item => item.ControlID == controlType);

        console.log('default data');
        console.log(data);

        data.ControlType = controlType;

        this.setState({ selectedControlType: controlType, data: data });

        $(e.target).blur();
    }

ParseForm()
{
    var valid = true;

    $('#AddControlForm').find('input').map(function (index, element) {
        if ($(element).attr('data-val') == 'true' && $(element).val().length == 0) {
            $(element).prev('label')[0].style.display = "inline";
            valid = false;
        }
        else
        {
            let previousLabel = $(element).prev('label')[0];

            if (previousLabel) {
                previousLabel.style.display = "none";
            }
        }
    });

    this.setState({ valid: valid });

    return valid;
}

CloseInsert(e)
{
    if (e) {
        e.stopPropagation();
    }
    
    this.setState({ selectedControlType: null });
    $('#InsertControlType').val('');
    CloseInsertControl();
}

AddControl(e)
{
    $('#AddControlForm').submit();
}

SubmitForm(e)
{
    var formValid = this.ParseForm();
    
    if (formValid == true)
    {
        $('#AddControlForm').submit();
        this.CloseInsert();
    }
}

    updateValue(optionName, value)
    {
    var currentData = this.state.data;

    currentData[optionName] = value;
    this.setState({ data: currentData });
}


    escapePressed(event)
    {
    if (event.keyCode === 27) {
        this.CloseInsert();
    }
    }

render() {
    var controlTypes = this.props.ControlTypes.map((controlType) => {
        return (<option key={controlType.ID} value={controlType.ID}>{controlType.Type}</option>);
    });

    var configurationTabs;

    if (this.state.selectedControlType)
    {
        
        configurationTabs =
            (<div>
            <ConfigurationTab title="Display" showHeader={true} updateValue={this.updateValue} visible={"inline"} controldata={this.state.data} options={displayOptions} />
            <ConfigurationTab title="Data" showHeader={true} updateValue={this.updateValue} visible={"inline"} controldata={this.state.data} options={dataOptions} />
            <ConfigurationTab title="General" showHeader={true} updateValue={this.updateValue} visible={"inline"} controldata={this.state.data} options={generalOptions} />
            <ConfigurationTab title="Action" showHeader={true} updateValue={this.updateValue} visible={"inline"} controldata={this.state.data} options={actionOptions} />
            </div>);
    }

    return (
        <div id="InsertControl" style={{ display: 'none' }} className="modal">

        <form id="AddControlForm" action="Home/AddControl">
            <input type="hidden" id="InsertPosition" name="Position" />
            <input type="hidden" id="InsertParentControlID" name="ParentControlID" />
            <input type="hidden" id="InsertPageID" name="PageID" />
            
        <div className="modal-content">
            <div className="modal-header">
                    <h2>Insert Control&nbsp;<a href="javascript:void(0);" onClick={this.CloseInsert}>X</a></h2>
            </div>
            <div className="modal-body">
                    <label htmlFor="ControlType">Control Type</label>
                    <select id="InsertControlType" onChange={this.ControlTypeSelected} name="ControlType">
                        <option value=''>Please Select</option>
                        {controlTypes}
                    </select>

                    {configurationTabs}
                    
                    <div style={{ textAlign: 'center' }}>
                            
                        <a href="javascript:void(0)" onClick={this.SubmitForm} className="button">Add</a>
                        </div>
                    
            </div>
        </div>
        </form>
        </div>

    
);
}

}
