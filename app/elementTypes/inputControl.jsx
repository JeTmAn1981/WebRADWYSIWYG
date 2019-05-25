import ErrorLabel from '../control/components/errorLabel.jsx';

export default class InputControl extends React.Component {
constructor(props)
{
    super(props);

    this.ValueUpdated = this.ValueUpdated.bind(this);
    this.UseTextArea = this.UseTextArea.bind(this);
    this.ChangeStateValue = this.ChangeStateValue.bind(this);

    this.state = { value: (this.props.value || this.props.data.Value || '') };
    
}

ValueUpdated(e) {
    e.stopPropagation();

    if (this.props !== undefined)
    {
        this.props.updateValue(this.props.data.Name, e.target.value, e);
    }
}


ChangeStateValue(e) {
    e.stopPropagation();

    this.setState({ value: e.target.value });
}

Clicked(e)
{
    e.stopPropagation();
}

Focus(e)
    {
        e.stopPropagation();
    }

UseTextArea()
{
    return this.props.useTextArea == "true" || this.props.data.TextMode == "MultiLine";
}

render() {
    if (!this.props.data)
        return <span>No data found for control!</span>

        var dataVal = (this.props.data.Required == "1" ? "true" : "false");
        var dataValRequired = (this.props.data.Required == "1" ? "This field is required." : "");
        let commonProps = {
            id: this.props.data.Name + '_' + this.props.controldata.ID,
            type: "text",
            name: this.props.Name || this.props.data.Name,
                    placeHolder: this.props.data.Placeholder,
                    className: this.props.data.CssClass,
                    maxLength: this.props.data.MaxLength != null ? this.props.data.MaxLength : '',
                    value: this.state.value,
                    onChange:this.ChangeStateValue,
                    onBlur: this.ValueUpdated,
                    onClick: this.Clicked,
                    onFocus: this.Focus,
                    'data-val': dataVal,
                    'data-val-required': dataValRequired
        };

        return (<span>
            <ErrorLabel data={this.props.data} controldata={this.props.controldata} />

            {this.UseTextArea() ?
                <textarea {...commonProps} /> : <input {...commonProps} />}
    </span>);

}
}

