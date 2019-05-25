export default class ConfigurationButton extends React.Component {
constructor(props)
{
    super(props);
    this.showSpecificPanel = this.showSpecificPanel.bind(this);
}

    showSpecificPanel(e) {
        this.props.showPanel(this.props.id.replace('Button',''));
    }

render() {
        return (
            <img className='ConfigurationButton' id={this.props.id} onClick={this.showSpecificPanel} src={'https://www.whitworth.edu/Administration/InformationSystems/Forms/WebRADMVCVB/~Images/' + this.props.src} />
      );
}
}

