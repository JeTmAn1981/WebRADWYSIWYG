import ConfigurationButtons from './components/configurationButtons.jsx';
import ControlHeader from './components/header.jsx';
import ControlBody from './components/body.jsx';
import ControlType from './controlType.jsx';
import InsertLinks from './insertLinks.jsx';
import OptionsPanel from '../configuration/optionsPanel.jsx';
import EditableText from '../editableText.jsx';
import EditLinks from './components/editLinks.jsx';

export default class Control extends React.Component {
constructor(props)
{
    super(props);

    this.state = {
        data: this.props.data, buttons: false, panel: false, currentTab: "Display", required: this.props.data.Required
    };

    this.controlSelected = this.controlSelected.bind(this);
    this.closePanel = this.closePanel.bind(this);
    this.controlSelected = this.controlSelected.bind(this);
    this.showPanel = this.showPanel.bind(this);
    this.selectTab = this.selectTab.bind(this);

    this.showDisplayTab = this.showDisplayTab.bind(this);
    this.showButtons = this.showButtons.bind(this);
    this.showHeader = this.showHeader.bind(this);
    this.hideButtons = this.hideButtons.bind(this);
    this.updateValue = this.updateValue.bind(this);
    this.panelShowing = this.panelShowing.bind(this);
    this.buttonsShowing = this.buttonsShowing.bind(this);

    this.mouseEnter = this.mouseEnter.bind(this);
    this.mouseLeave = this.mouseLeave.bind(this);
    this.showDelete = this.showDelete.bind(this);

    this.insertBelow = this.insertBelow.bind(this);
    this.insertAbove = this.insertAbove.bind(this);
    this.insertInside = this.insertInside.bind(this);
    this.insert = this.insert.bind(this);
    this.getItemClass = this.getItemClass.bind(this);
}

showButtons()	{
        this.setState({buttons: true});
    }

hideButtons()	{
        this.setState({buttons: false});
    }

showPanel(selectedTab)	
    {
        this.selectTab(selectedTab);
        this.setState({buttons: false,panel: true});
    }

selectTab(selectedTab)
    {
        this.setState({currentTab: selectedTab});
    }

showDisplayTab(e)
{
    e.stopPropagation();

    if (this.props.selectedControl == null || this.props.selectedControl.props.data.ID != this.props.data.ID) {
        this.props.selectControl(this);
        this.showPanel("Display");
    }
    
}

closePanel(e)
    {
        this.setState({ panel: false });
        this.props.selectControl(null);
    }

updateValue(optionName, value,e) {
    var currentData = this.state.data;
    let foundProperty = false;

    for (var prop in currentData)
    {
        if (currentData.hasOwnProperty(prop) && prop.toLowerCase() == optionName.toLowerCase())
        {
            console.log('setting ' + prop + ' to ');
            console.log(value);
            currentData[prop] = value;

            $.ajax({
                type: "POST",
                url: updateControlURL,
                data: { controlID: currentData.ID, prop: prop, value: value},
                success: function (info) {
                    console.log('updated control');
                    console.log(info);

                //    insertControl.props.AddPlacedControl(info);
                }
            });
            foundProperty = true;

            break;
        }
    }

    if (!foundProperty)
    {
        console.log('could not find property ' + prop);
    }
        
        this.setState({ data: currentData });
}

controlSelected(e) {
        if (this.props.selectedControl == null || this.props.selectedControl.props.data.ID != this.props.data.ID) {
            this.props.selectControl(this);
            this.showButtons();    
        }
    }

panelShowing() {
        return this.state.panel && (this.props.selectedControl == this);
    }

buttonsShowing() {
        return (this.state.buttons && !this.state.panel) && (this.props.selectedControl == this);
    }

mouseEnter(e)
{
    e.stopPropagation();
    this.setState({ ShowLinks: true });
}

mouseLeave(e)
{
    e.stopPropagation();
    this.setState({ ShowLinks: false });
}

    moveUp1(e) {

    }

insertBelow(e)
{
    this.insert(e, parseInt(this.state.data.Position) + 1);
}

insertAbove(e)
{
    this.insert(e, parseInt(this.state.data.Position) - 1);
}

insertInside(e)
{
    this.insert(e);
}

insert(e, position) {
    e.stopPropagation();

    setInsertPosition(position);

    $('#InsertPageID').val(this.state.data.PageID);
    $('#InsertParentControlID').val(this.state.data.ParentControlID);

    showInsert();
    }

showDelete(e) {
    e.stopPropagation();
    
    document.getElementById('DeleteID').innerText = this.state.data.ID;
    document.getElementById('DeleteName').innerText = this.state.data.Name;

    $('#DeleteControl').show();
}

showHeader()
{
    return (this.state.data.ControlType1.DataType == 9) ? true : this.state.data.DisplayHeading == "1";    
    
    }

    getItemClass() {
        let displayType = this.state.data.DisplayType;
        let itemClasses = ['control-item'];
        
        if (displayType === 2 || displayType === 8 || displayType === 1) {
            itemClasses.push('clear-float');
        }

        if (displayType === 2 || displayType === 8 || displayType === 3 || displayType === 4) {
            itemClasses.push('float-left');
        }

        if (this.state.ShowLinks) {
            itemClasses.push('selected-control');
        }

        return itemClasses.join(' ');
    }

    render() {
        
        return (
            <li className={this.getItemClass()} onMouseEnter={this.mouseEnter} onMouseLeave={this.mouseLeave} onBlur={this.blur} key={this.props.data.ID}>
                {this.state.ShowLinks ? <EditLinks showDisplayTab={this.showDisplayTab} /> : null}

                <div id={this.state.data.ID} tabIndex="0"  className={(this.state.data.Visible != "1" ? "border-dashed" : "")}>
                    <input type="hidden" name="Position" value={this.state.data.Position} />
                    <input type="hidden" name="ID" value={this.state.data.ID} />
                    <input type="hidden" name="ParentControlID" value={this.state.data.ParentControlID} />
                    <ConfigurationButtons visible={this.buttonsShowing() ? "inline" : "none"} showPanel={this.showPanel} />
                    <OptionsPanel controldata={this.state.data} visible={this.panelShowing() ? "block" : "none"} updateValue={this.updateValue} showPanel={this.showPanel} closePanel={this.closePanel} selectTab={this.selectTab} selectedTab={this.state.currentTab} />
                    <ControlHeader visible={this.showHeader() ? "inline" : "none"} displayRequired={(this.state.selectedControl == this)} updateRequired={this.updateRequired} updateValue={this.updateValue} data={this.state.data} required={this.state.required} buttons={this.state.buttons} panel={this.state.panel} required={this.state.required} data={this.state.data} />
                    <ControlBody selectControl={this.props.selectControl} selectedControl={this.props.selectedControl} data={this.state.data} updateValue={this.updateValue} />
                    <InsertLinks show={this.state.ShowLinks} isParent={this.state.data.ControlType1.IsParent} insertBelow={this.insertBelow} insertInside={this.insertInside} showDelete={this.showDelete} />  
                </div>
                
            </li>
                
		);
}
}

