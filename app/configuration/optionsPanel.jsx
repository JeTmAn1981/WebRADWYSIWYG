import { ConfigurationTab } from './configurationTab.jsx';
import { ConfigurationTabLink } from './configurationTabLink.jsx';

export default class OptionsPanel extends React.Component {

    constructor(props)
    {
        super(props);
        this.selectTab = this.selectTab.bind(this);
        this.closePanel = this.closePanel.bind(this);
        this.escapePressed = this.escapePressed.bind(this);
    }

    componentDidMount() {
        document.addEventListener("keydown", this.escapePressed, false);
    }

    componentWillUnmount() {
        document.removeEventListener("keydown", this.escapePressed, false);
    }

    escapePressed(event) {
        if (event.keyCode === 27) {
            this.closePanel(event);
        }
    }

selectTab(selection) {
        this.props.selectTab(selection);
    }

closePanel(e) {
        this.props.closePanel(e);
    }
    


    render() {
        return (
            <div id={this.props.id} style={{ display: this.props.visible }} className="modal">
               <div className="modal-content">
                    <div className="modal-header">
                        <h2>Properties for <strong>{this.props.controldata.Name} - {this.props.controldata.ControlType1.Type} </strong>&nbsp;<a href="javascript:void(0);" onClick={this.closePanel}>X</a></h2>
                    </div>
                    <div className="modal-body" onKeyPress={this.onKeyPress}>
      <ul id='ConfigurationTabs'>
					<ConfigurationTabLink title="Display" Click={this.selectTab} />
					<ConfigurationTabLink title="Data" Click={this.selectTab} />
					<ConfigurationTabLink title="General" Click={this.selectTab} />
					<ConfigurationTabLink title="Action" Click={this.selectTab} />
          
      </ul>
      <form id="UpdateControlForm" action="Home/UpdateControl">
                <ConfigurationTab title="Display" updateValue={this.props.updateValue} visible={this.props.selectedTab == "Display" ? "inline" : "none"} controldata={this.props.controldata} options={ displayOptions } />
                <ConfigurationTab title="Data" updateValue={this.props.updateValue} visible={this.props.selectedTab == "Data" ? "inline" : "none"} controldata={this.props.controldata} options={dataOptions} />
                <ConfigurationTab title="General" updateValue={this.props.updateValue} visible={this.props.selectedTab == "General" ? "inline" : "none"} controldata={this.props.controldata} options={generalOptions} />
                <ConfigurationTab title="Action" updateValue={this.props.updateValue} visible={this.props.selectedTab == "Action" ? "inline" : "none"} controldata={this.props.controldata} options={actionOptions} />
            
        </form>
    </div>
               </div>
            </div>
      );
}
}

