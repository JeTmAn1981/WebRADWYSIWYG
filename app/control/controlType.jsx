import ControlHeader from './components/header.jsx';
import ControlBody from './components/body.jsx';

export default class ControlType extends React.Component {
constructor(props)
{
    super(props);
    this.state = { required: "1", placedControls: placedControls };
}

    placeThisControl(e) {
        this.props.addPlacedControl(address);
    }


render() {
        return (
		<div id={this.props.data.ID} className="PlacedControl draggableField droppedField" tabIndex="0" onClick={this.placeThisControl}>
		<ControlHeader visible={this.props.data.DisplayHeading == "1" ? "inline" : "none"} required={this.state.required} data={this.props.data} />
	    <ControlBody data={this.props.data} />

		</div>

		);
}
}

