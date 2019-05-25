export default class ErrorLabel extends React.Component {
constructor(props)
{
super(props);
}

render() {
    return (<label className="error" style={{ display: "none" }} id={this.props.data.Name + '_' + this.props.controldata.ID + "_ErrorLabel"}>Please enter a value for {this.props.data.Name}</label>);
}
}

