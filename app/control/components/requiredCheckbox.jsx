export default class RequiredCheckbox extends React.Component {
constructor(props)
{
super(props);
}

    render()
    {
        return (
			<div id="RequiredCheckbox">
            <input type="checkbox" id={this.props.data.Name + "Required"} onClick={this.props.updateRequired} checked={this.props.required == "1" ? "checked" : "" } />
                <label htmlFor={this.props.data.Name + "Required"}>Required?</label>
			</div>

		);
    }
}


