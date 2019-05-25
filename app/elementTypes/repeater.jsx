import Control from '../control/control.jsx';
import ControlBody from '../control/components/body.jsx';
import GetChildControls from '../utilities.jsx';

export default class Repeater extends React.Component {
constructor(props)
{
    super(props);
    this.render = this.render.bind(this);
}
    render() {
        var currentSelectedControl = null;
        var selectControlEvent = this.selectControl;
        var controlID = this.props.data.ID;
        var currentPanel = this;

        var childControls = allControls.filter(function (control) { return control.ParentControlID == controlID }).map(function (currentControl) {
            return (<Control key={currentControl.ID} data={currentControl} selectControl={currentPanel.props.selectControl} selectedControl={currentPanel.props.selectedControl} />);
            
        });

        try {
            return (<div id={this.props.data.Name} className={this.props.controlClass}> 
          
                <ul className="sortable">
                    {GetChildControls(this)}
                </ul>
            </div>);
        }
        catch (e) {
            console.log('error rendering panelody - ');
            console.log(e);
            console.log(this.props.data);
        }

    }
}
