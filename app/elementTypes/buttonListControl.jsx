import ListControl from './listControl.jsx';

export default class ButtonListControl extends ListControl
{
    
    render()
    {
        
    let buttons = this.GetListItemButtons(this.props.data.Name,this.props.buttonType);
    let className = this.GetClassName();

    return (

        <fieldset>
            <ul id={this.props.data.Name} onClick={e => { e.stopPropagation(); }} className={className}>
            {buttons}
        </ul>
        </fieldset>
        );
    }
}

