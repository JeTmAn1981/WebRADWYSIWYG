
import Button from './elementTypes/button.jsx';
import InputControl from './elementTypes/inputControl.jsx';
import ItemsTemplate from './itemsTemplate.jsx';
import EditableInput from './editableInput.jsx';
import EditableText from './editableText.jsx';
import ListItem from './listItem.jsx';

export default class ListItems extends ItemsTemplate {
    constructor(props) {
        super(props);
        this.state = { itemTemplate: { Type: "1", Text: "", Value: "" }, controlPropertyName:'ProjectControlListItems' };
        this.UpdateItem = this.UpdateItem.bind(this);
        this.UpdateItems = this.UpdateItems.bind(this);
    }
        
    UpdateItems(items, event) {
        let listItemsControl = this;
        
               $.ajax({
            type: "POST",
            url: updateListItemsURL,
            data: { controlID: listItemsControl.props.controldata.ID, listItems: items },
            success: function (updatedItems) {
                console.log('set listItems to');
                console.log(updatedItems);
                
                listItemsControl.props.updateValue('ProjectControlListItems', updatedItems, event);
            }
        });
    }

    UpdateItem(e, index, type, value)
    {
        let items = this.props.items;
        console.log(e, index, type, value, items);

        items[index][type] = value;
        console.log('new items');
        console.log(items);

        this.UpdateItems(items, e);
    }

    render() {
        let listItemsControl = this;
        let items = this.props.items || [];
        let valueOption = this.props.childOptions.filter(o => o.Name == "ProjectControlListItemValue")[0];
        let textOption = this.props.childOptions.filter(o => o.Name == "ProjectControlListItemText")[0];
        
        let childItems = items.map((item,itemIndex) => 
        <ListItem
                index={itemIndex}
                key={itemIndex}
                updateItem={listItemsControl.UpdateItem}
                removeItem={listItemsControl.RemoveItem}
                value={item["Value"]}
                text={item["Text"]}
                textOption={textOption}
                valueOption={valueOption}
                controlData={this.props.controldata}
            />
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