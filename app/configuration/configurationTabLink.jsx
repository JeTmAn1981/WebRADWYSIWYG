import ConfigurationButton from './configurationButton.jsx';

export class ConfigurationTabLink extends React.Component {
constructor(props)
{
    super(props);
    this.Clicked = this.Clicked.bind(this);
}

    Clicked(e)
    {
        this.props.Click(this.props.title);
    }

render() {

        return (
	                    <a onClick={this.Clicked}>
							<ConfigurationButton src={this.props.title + 'icon.png'} />{this.props.title}
	                    </a>

      );
}
}
