import Button from './elementTypes/button.jsx';
import InputControl from './elementTypes/inputControl.jsx';
import SelectControl from './elementTypes/selectControl.jsx';
import ItemsTemplate from './itemsTemplate.jsx';
import Action from './action.jsx';

export default class Actions extends ItemsTemplate {
    constructor(props) {
        super(props);
        this.UpdateItems = this.UpdateItems.bind(this);
        this.SetAvailableActions = this.SetAvailableActions.bind(this);
        
       // this.SetAvailableActions();
        this.state = { itemTemplate: { Action: "", TargetControl: "", TriggerControl: "" }, controlPropertyName: 'ProjectControlPostbackActions' };
        //console.log('data suppled ti actions:',this.props.controldata,this.props.data);
    }

    SetAvailableActions() {
        let actionsComponent = this;

        $.ajax({
            type: "GET",
            url: getAvailableActionsURL,
            data: { ControlTypeID: actionsComponent.props.controldata.ControlType, ControlID: actionsComponent.props.controldata.ID },
            success: function (info) {
                actionsComponent.setState({availableActions: info});
                console.log('set available actions to');
                console.log(info);
            }
        });
    }
        
    UpdateItems(items, event) {
        let actionsControl = this;
        
        actionsControl.props.updateValue('ProjectControlPostbackActions', items, event);

        $.ajax({
            type: "POST",
            url: updateActionsURL,
            data: { controlID: actionsControl.props.controldata.ID, actions: items },
            success: function (info) {
                //console.log('set actions to');
                //console.log(items);
                //console.log(info);
            }
        });
    }
    
    render() {
        let actionsControl = this;

        let items = this.props.items || [];
        let triggerControlOption = this.props.childOptions.filter(o => o.Name == "TriggerControl")[0];
        let targetControlOption = this.props.childOptions.filter(o => o.Name == "TargetControl")[0];
        let data = actionsControl.props.data;

        //console.log('action items', items);


        let childItems = items.map((item, itemIndex) =>
            <Action controlData={this.props.controldata} actionsData={data} index={itemIndex} removeItem={actionsControl.RemoveItem} updateItem={actionsControl.UpdateItem} data={item} />
        );

        return (<table>
            <tbody>
                <tr>
                    <td><label>Action</label></td>
                    <td><label>Target Control</label></td>
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