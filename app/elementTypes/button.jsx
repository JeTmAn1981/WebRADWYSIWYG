export default class Button extends React.Component {
constructor(props)
{
super(props);
}

    render() {
        return <input type="button" className="button" value={this.props.value} onClick={this.props.Click} />;
    }
}

