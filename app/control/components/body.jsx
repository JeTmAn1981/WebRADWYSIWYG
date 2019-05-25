import InputControl from '../../elementTypes/inputControl.jsx';
import Panel from '../../elementTypes/panel.jsx';
import Repeater from '../../elementTypes/repeater.jsx';
import SelectControl from '../../elementTypes/selectControl.jsx';
import Textarea from '../../elementTypes/textarea.jsx';
import CheckboxControl from '../../elementTypes/checkboxControl.jsx';
import ButtonListControl from '../../elementTypes/buttonListControl.jsx';
import EditableText from "../../editableText.jsx";
import CKEditor from '@ckeditor/ckeditor5-react';
import ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import InlineEditor from '@ckeditor/ckeditor5-build-inline';


export default class ControlBody extends React.Component {
constructor(props)
{
    super(props);
    
}
    
render() {
    
        var controlType;

        if (this.props.data.ControlType1 != null)
        {
            if (!this.props.data.ControlType1.ControlDataType)
            {
                console.log("Didn't find HTML Type:");
                console.log(this.props.data);
            }

            let commonProps = {
                updateValue: this.props.updateValue,
                value: this.props.data.Value,
                data: this.props.data,
                controldata: this.props.data,
                selectControl: this.props.selectControl,
                selectedControl: this.props.selectedControl 
            };
            
            if (this.props.data.ControlType1.ControlDataType.Description == "Radiobuttonlist")
                controlType = (<ButtonListControl {...commonProps} buttonType="radio" />);
            else if (this.props.data.ControlType1.ControlDataType.Description == "Checkboxlist")
                controlType = (<ButtonListControl {...commonProps} buttonType="checkbox" />);
            else if (this.props.data.ControlType1.ControlDataType.Description == "Panel")
                controlType = (<Panel {...commonProps} />);
            else if (this.props.data.ControlType1.Type == "Form-group container")
                controlType = (<Panel {...commonProps} controlClass={"form-group show-form-group"} />);
            else if (this.props.data.ControlType1.ControlDataType.Description == "Repeater")
                controlType = (<Repeater {...commonProps} controlClass={"form-group show-form-group"} />);
            else if (this.props.data.ControlType1.ControlDataType.Description == "Checkbox")
                controlType = <CheckboxControl {...commonProps} />;
            else if (this.props.data.ControlType1.ControlDataType.Description == "Label")
                controlType = <span dangerouslySetInnerHTML={{ __html: this.props.data.Value }}></span>;
            else if (this.props.data.ControlType1.ControlDataType.Description == "FileUpload")
                controlType = <input type="file" />;
            else if (this.props.data.ControlType1.ControlDataType.HTMLType == "input")
                controlType = (<InputControl {...commonProps} />);
            else if (this.props.data.ControlType1.ControlDataType.HTMLType == "select")
                controlType = (<SelectControl {...commonProps} />);
            else if (this.props.data.ControlType1.ControlDataType.HTMLType == "text")
                //controlType = (<EditableText {...commonProps} Name={this.props.data.Name} ID={this.props.data.Name} initialText={this.props.data.Value} />);
//                controlType = <span dangerouslySetInnerHTML={{ __html: this.props.value || (this.props.data ? this.props.data.Value : '') }}></span>;
            
                controlType = <Textarea {...commonProps} />;
            else
                controlType = (<div>{this.props.data.Name + ' - ' + this.props.data.ControlType}</div>);
        }
        else
            controlType = (<div>{this.props.data.Name + ' - ' + this.props.data.ControlType}</div>);
        
        try
        {
            return (

                <div>
                    {controlType}
                </div>
            );
        }
        catch(e)
        {
            console.log('error rendering control body - ');
            console.log(e);
            console.log(this.props.data);
        }
     
    }
}

