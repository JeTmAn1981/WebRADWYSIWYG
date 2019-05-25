import Button from './elementTypes/button.jsx';
import InputControl from './elementTypes/inputControl.jsx';

export default class ItemsTemplate extends React.Component {

    constructor(props) {
        super(props);

        this.AddItem = this.AddItem.bind(this);
        this.RemoveItem = this.RemoveItem.bind(this);
     }
    
    AddItem(e) {
        let items = this.props.items;

        let newItem = this.state.itemTemplate;

        items.push(newItem);
     
        console.log('Adding item to ', this.state.controlPropertyName, newItem, items);
        this.props.updateValue(this.state.controlPropertyName, items, e);
    }

    SetupItem(item) {
        console.log('No item setup needed.');
    }

    RemoveItem(e, index)
    {
        let items = this.props.items;

        if (items.length > 1)
        {
            console.log('removing item at index', index);
            items.splice(index, 1);

            this.UpdateItems(items, e);
        }
    }
        
    render() {
        let listItemsControl = this;
        let items = this.props.controldata.ProjectControlListItems || [];
        let valueOption = this.props.childOptions.filter(o => o.Name == "ListItemValue")[0];
        let textOption = this.props.childOptions.filter(o => o.Name == "ListItemText")[0];

        let childItems = items.map((item,itemIndex) =>
        {
            return <tr id={itemIndex} key={itemIndex}>
                <td id="Text"><InputControl useTextArea="true" updateValue={listItemsControl.UpdateItem} value={item["Text"]} data={textOption} controldata={this.props.controldata} /></td>
                <td id="Value"><InputControl updateValue={listItemsControl.UpdateItem} value={item["Value"]} data={valueOption} controldata={this.props.controldata} /></td>
                <td><a href='javascript:void(0)' onClick={this.RemoveItem}>Remove</a>
                </td>
            </tr>
            }
        );

        return (<table>
            <tbody>
                <tr>
                    <td><label>Text</label></td>
                    <td><label>Value</label></td>
                </tr>
            {childItems}
            <tr>
                <td colSpan='100'><Button value="Add Item" Click={this.AddItem} />
                </td>
            </tr>
                </tbody>
        </table>);

    }
}