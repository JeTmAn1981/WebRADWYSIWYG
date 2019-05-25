import Button from './elementTypes/button.jsx';
import InputControl from './elementTypes/inputControl.jsx';
import SelectControl from './elementTypes/selectControl.jsx';
import ItemsTemplate from './itemsTemplate.jsx';

export default class Action extends ItemsTemplate {
    constructor(props) {
        super(props);

        this.GetAvailableActionTypes = this.GetAvailableActionTypes.bind(this);
        this.SetAvailableTargetControls = this.GetAvailableTargetControls.bind(this);
        this.UpdateSelectedAction = this.UpdateSelectedAction.bind(this);
        this.GetAvailableActionTypeListItems = this.GetAvailableActionTypeListItems.bind(this);
        this.GetAvailableTargetControlListItems = this.GetAvailableTargetControlListItems.bind(this);

        this.state = {
            selectedAction: this.props.data.Action,
            targetControl: allControls.filter(control => control.ID == this.props.data.TargetControl)[0],
            triggerControl: allControls.filter(control => control.ID == this.props.data.TriggerControl)[0],
            availableActionTypes: [],
            availableControlTypeActions: [],
            availableTargetControls: []
        };
                
        let actionTypeData = this.GetAvailableActionTypes();

        this.state.availableActionTypes = actionTypeData.availableActionTypes;
        this.state.availableControlTypeActions = actionTypeData.availableControlTypeActions;
        this.state.availableTargetControls = this.GetAvailableTargetControls(actionTypeData.availableControlTypeActions);

        console.log('initial action state', this.state);

    }

    UpdateSelectedAction(name, value, event) {

        let actionTypeData = this.GetAvailableActionTypes();
let availableTargetControls = this.GetAvailableTargetControls(actionTypeData.availableControlTypeActions);

        this.setState({ selectedAction: value, availableTargetControls:availableTargetControls });

        //this.props.updateItem(name, value,event);
    }

    GetAvailableActionTypes() {
        let availableControlTypeActions = controlTypeActions.filter(typeAction => !this.state.triggerControl || typeAction.ActionControlDataType == this.state.triggerControl.ControlType1.ControlDataType.ID);
        let availableActionTypes = controlActionTypes.filter(actionType => {
            if (actionType.ID == 7) {
                return true;
            }

            return availableControlTypeActions.filter(typeAction => typeAction.ActionType == actionType.ID).length > 0;
        });
       
        return {
            availableControlTypeActions: availableControlTypeActions,
            availableActionTypes: availableActionTypes
        };
    }

    GetAvailableTargetControls(availableControlTypeActions) {
        let actionsControls = this;
        
        let availableTargetControlDataTypes = availableControlTypeActions.filter(typeAction => typeAction.ActionType == actionsControls.state.selectedAction);
        let availableTargetControls = allControls.filter(control => (!actionsControls.state.triggerControl || (control.ID != actionsControls.state.triggerControl.ID)) && availableTargetControlDataTypes.filter(typeAction => typeAction.TargetControlDataType == control.ControlType1.ControlDataType.ID).length > 0);

        return availableTargetControls;
    }
      
    GetAvailableActionTypeListItems() {
        return this.state.availableActionTypes ? this.state.availableActionTypes.map(actionType => { return { Text: actionType.Type, Value: actionType.ID }; }) : [];
    }

    GetAvailableTargetControlListItems() {
        return this.state.availableTargetControls ? this.state.availableTargetControls.map(control => { return { Text: control.Name, Value: control.ID }; }) : [];
    }

    
    render() {
        let actionControlValid = true; //this.state.targetControl && this.state.targetControl.ID;

        return actionControlValid ? <tr id={this.props.index} key={this.props.index}>
            <td id="Action">
                <SelectControl value={this.state.selectedAction || ''} updateValue={this.UpdateSelectedAction} controldata={this.props.controlData} data={{ ProjectControlListItems: this.state.availableActionTypes.map(actionType => { return { Text: actionType.Type, Value: actionType.ID }; }) }} />
            </td>
            <td>
                <SelectControl value={this.state.targetControl ? this.state.targetControl.ID : ''} updateValue={this.props.UpdateItem} controldata={this.props.controlData} data={{ ProjectControlListItems: this.state.availableTargetControls.map(control => { return { Text: control.Name, Value: control.ID }; }) }} />
            </td>
            <td><a href='javascript:void(0)' onClick={this.props.RemoveItem}>Remove</a>
            </td>
        </tr> : null;
    }
}