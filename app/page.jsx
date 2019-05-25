import PlacedControlsNew from "./placedControlsNew.jsx";

export default class Page extends React.Component {
    constructor(props) {
        super(props);
    }
    
    render() {
        return <span>
            <PlacedControlsNew controls={this.props.controls} />
        </span>;
      }
}
