var RequiredCheckbox = require('./requiredCheckbox.jsx');
import EditableText from '../../editableText.jsx';
import Textarea from '../../elementTypes/textarea.jsx';

export default class ControlHeader extends React.Component {
constructor(props)
{
super(props);
}

    render() {

        return (
            <div className="Heading" style={{ display: this.props.visible }}>
                <div className={this.props.data.Required == 1 ? "required" : ""}>
                    <label dangerouslySetInnerHTML={{ __html: this.props.data.Heading }} htmlFor={this.props.data.Name}></label>
                </div>
                
                
                <small dangerouslySetInnerHTML={{ __html: this.props.data.Subheading}}></small>
        </div>
	    );
    }
}

