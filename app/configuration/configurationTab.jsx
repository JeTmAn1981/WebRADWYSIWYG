import CheckboxControl from '../elementTypes/checkboxControl.jsx';
import InputControl from '../elementTypes/inputControl.jsx';
import Textarea from '../elementTypes/textarea.jsx';
import Template from '../elementTypes/template.jsx';
import SelectControl from '../elementTypes/selectControl.jsx';
import ControlHeader from '../control/components/header.jsx';
import ControlItems from '../controlItems.jsx';
import ListItems from '../listItems.jsx';
import Actions from '../actions.jsx';
import DataSource from '../dataSource.jsx';

import CKEditor from '@ckeditor/ckeditor5-react';
import ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import InlineEditor from '@ckeditor/ckeditor5-build-inline';

export class ConfigurationTab extends React.Component {
constructor(props)
{
    super(props);
    this.state = { options: this.props.options };
    this.GetTabOption = this.GetTabOption.bind(this);

    
}
    
RequirementsProfileIncludesOption(requirementsProfile) {
        return function (option) {
            return (controlTypeDetailRequirements.filter(function (requirement) { return requirement.ProfileID == requirementsProfile && requirement.DetailTypeID == option.ID }).length > 0);
        }
    }

GetRequirementsProfile(controlType) {
        var detail = controlTypeDetails.filter(function (detail) { return (detail.ControlID == controlType); })[0];

        if (detail)
            return detail.RequirementsProfile;
        else {
            return 0;
        }
    }

GetTabOption(option, updateEvent, data)
    {
    let className = (option.CssClass ? option.CssClass : "controlOptionItem");

    let currentUpdateEvent = updateEvent || this.props.updateValue;
    let controlContent;
    var header, body;
    let controlData = data || this.props.controldata;

    if (!controlData[option.Name]) {
        controlData[option.Name] = '';
    }

    let childOptions = this.props.options.filter(childOption => childOption.ParentTypeID == option.ID);

    header = <ControlHeader data={option} />;

    let commonProps = {
        GetTabOption: this.GetTabOption,
        childOptions: childOptions,
        updateValue: currentUpdateEvent,
        value: controlData[option.Name],
        data: option,
        controldata: controlData 
    };

    if (option.Name == "ControlItems")
    {
        body = <ControlItems {...commonProps} />;
    }
    else if (option.Name == "ListItems")
    {
        
        body = <ListItems {...commonProps} items={controlData.ProjectControlListItems} />;
    }
    else if (option.Name == "Actions")
    {
        
        body = <Actions {...commonProps} items={controlData.ProjectControlPostbackActions} />;
    }
    else if (option.Name == "DataSource")
    {
        
        body = <DataSource {...commonProps} />;
    }
    else
    {
        if (option.HTMLType == "input") {
            
            body = <InputControl {...commonProps} />;
        }
        else if (option.HTMLType == "textarea") {
            body = <Textarea {...commonProps} />;
        }
        else if (option.HTMLType == "checkbox") {
            
            body = <CheckboxControl {...commonProps} />;
        }
        else if (option.HTMLType == "select") {
            
            option.ControlTypeDetailTypeItems = controlTypeDetailTypeItems.filter(item => item.ControlTypeDetailTypeID == option.ID);
            
            body = <SelectControl {...commonProps} />;
        }
        else if (option.HTMLType == "template") {
            
            body = <Template {...commonProps} />;
        }
        else {
            
            header = null;
            body = <span><br />HTML Type - {option.HTMLType}<a href=''>{option.Name}</a><br /></span>;
        }
    }
    
            return (<div className={className} key={this.props.controldata.ID + option.Name}>
        {header}
        {body}
    </div>);
}

render() {
        var currentTab = this;
        var controlType = this.props.controldata.ControlType;
        var requirementsProfile = this.GetRequirementsProfile(controlType);
        var options = this.state.options.filter(this.RequirementsProfileIncludesOption(requirementsProfile)).filter(option => option.ParentTypeID == null);

        for (var i = 0; i < options.length; i++)
        {
            var option = options[i];
            option.Required = controlTypeDetailRequirements.filter(requirement => requirement.DetailTypeID == option.ID && requirement.ProfileID == requirementsProfile)[0].Required;
        }
        
    var tabOptions = options.sort((option1, option2) => option2.required - option1.required).map((option, index) => this.GetTabOption(option));

    return <div id={this.props.title + "Tab"} style={{ display: (options.length > 0) ? this.props.visible : 'none'}} className="form-group">
                {this.props.showHeader ? <h2>{this.props.title}</h2> : null}
                <div className="flex-container">
                    {tabOptions}
                </div>
            </div>;
    }
}

