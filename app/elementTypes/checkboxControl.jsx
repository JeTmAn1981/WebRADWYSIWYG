import ErrorLabel from '../control/components/errorLabel.jsx';

export default class CheckboxControl extends React.Component {
constructor(props)
{
    super(props);

    this.changed = this.changed.bind(this);
}

    changed(e) {
        e.stopPropagation();
        this.props.updateValue(this.props.data.Name, e.target.checked ? 1 : 0, e);
    }

Clicked(e) {
        e.stopPropagation();
    }
    
render() {
    let id = this.props.controldata ? this.props.controldata.ID : this.props.data.ID;

        return (
            <div key={id + this.props.data.Name + 'Checkbox'}>
                <ErrorLabel data={this.props.data} controldata={this.props.controldata} />
                <input id={id + this.props.data.Name + 'Checkbox'} type="checkbox"
                    name={this.props.data.Name}
                    className={this.props.data.CssClass}
                    checked={this.props.value == "1" ? "checked" : ""}
                    onChange={this.changed}
                    onClick={this.Clicked} />
                <label htmlFor={id + this.props.data.Name + 'Checkbox'} dangerouslySetInnerHTML={{ __html: this.props.data.Text }}></label>
                {this.props.childOptions ? this.props.childOptions.map(this.props.GetTabOption) : null}
        </div>

            
        );
    }
}

