import ConfigurationButton from '../../configuration/configurationButton.jsx';

export default class ConfigurationButtons extends React.Component {
constructor(props)
{
super(props);
}

    render() {
        return (
		<div className='overlay' style={{display: this.props.visible}}>
		<ConfigurationButton id='DisplayButton' showPanel={this.props.showPanel} src='displayicon.png' />
		<ConfigurationButton id='DataButton' showPanel={this.props.showPanel} src='dataicon.png' />
		<ConfigurationButton id='GeneralButton' showPanel={this.props.showPanel} src='generalicon.jpg' />
		<ConfigurationButton id='ActionButton' showPanel={this.props.showPanel} src='actionicon.png' />
		</div>
		);
}
}

