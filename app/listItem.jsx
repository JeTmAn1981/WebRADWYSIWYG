
import Button from './elementTypes/button.jsx';
import InputControl from './elementTypes/inputControl.jsx';
import ItemsTemplate from './itemsTemplate.jsx';
import EditableInput from './editableInput.jsx';
import EditableText from './editableText.jsx';

export default class ListItem extends React.Component
{
    constructor(props) {
        super(props);
        this.RemoveItem = this.RemoveItem.bind(this);
        this.UpdateText = this.UpdateText.bind(this);
        this.UpdateValue = this.UpdateValue.bind(this);
    }
    
    RemoveItem(e) {
        this.props.removeItem(e, this.props.index);
    }

    UpdateText(name, value, e)
    {
        this.props.updateItem(e, this.props.index, 'Text', value);
    }

    UpdateValue(name, value, e) {
        this.props.updateItem(e, this.props.index, 'Value', value);
    }

    render() {
        let itemIndex = this.props.index;

        return <tr>
            <td id="Text">
                <InputControl Name={"ProjectControlListItems[" + itemIndex + "].Text"} updateValue={this.UpdateText} value={this.props.text} data={this.props.textOption} controldata={this.props.controlData} />
            </td>
            <td id="Value">
                <InputControl Name={"ProjectControlListItems[" + itemIndex + "].Value"} updateValue={this.UpdateValue} value={this.props.value} data={this.props.valueOption} controldata={this.props.controlData} />
            </td>
            <td>
                <a href='javascript:void(0)' onClick={this.RemoveItem}>Remove</a>
            </td>
        </tr>;
    }
}