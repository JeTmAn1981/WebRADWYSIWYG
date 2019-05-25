import ListControl from './listControl.jsx';

export default class RadioButtonListControl extends ListControl {

render() {
    var buttons;
    var controlName = this.props.data.Name;

    if (this.props.data.ProjectControlListItems != null) {
        buttons = this.props.data.ProjectControlListItems.map(function (button,index) {
            var buttonID = controlName + '_' + index;

            return (<li key={buttonID}><input id={buttonID} type="radio" name={controlName} value={button.Value} /><label htmlFor={buttonID}>{button.Text}</label></li>);
        });
    }

    
    return (

        <fieldset>
        <ul id={this.props.data.Name} onClick={ e => { e.stopPropagation(); }} className="unstyled-list-inline">
            {buttons}
        </ul>
        </fieldset>
        );
    }
}

