import EditableText from '../editableText.jsx';

export default class ListControl extends React.Component {
    constructor(props) {
        super(props);
        this.valueUpdated = this.valueUpdated.bind(this);
        this.GetListItemButtons = this.GetListItemButtons.bind(this);
        this.GetClassName = this.GetClassName.bind(this);
        this.SetItems = this.GetItems.bind(this);

        let control = this;

        if (!this.props.data) {
            console.log('null data');
            console.log(this);
        }

        this.state = { listItems: [] };
        
    }

    componentWillMount()
    {
        this.GetItems();
    }

    componentWillReceiveProps() {
        this.GetItems();
    }

    GetItems() {
        let control = this;
        if (control.props.data.Name == 'FirstTimeSubscriber') {
            console.log(control.props.data);
        }
        new Promise(function (resolve, reject) {
            control.setState({ listItems: control.props.data.ControlTypeDetailTypeItems || control.props.data.ProjectControlListItems });
            if (control.props.data.SelectionItems == "2" || control.props.data.SelectionItems == "4")
            {
                console.log('getting items');
                $.ajax({
                    url: dataItemsURL,
                    data: { control: control.props.data },
                    type: 'POST',
                    success: function (data) {
                        console.log('received listitems: ', data);
                        control.setState({ listItems: data });
                    }
                }).then(function (response) {
                });
            }
            else if (control.props.data.ControlTypeDetailTypeItems || control.props.data.ProjectControlListItems) {
           
                control.setState({ listItems: control.props.data.ControlTypeDetailTypeItems ? control.props.data.ControlTypeDetailTypeItems : control.props.data.ProjectControlListItems });
            }

            if (control.state.listItems == null) {
                control.state.listItems = [];
            }
        }).then(function (response) {

        }, function (error) {
            console.error("Failed!", error);
        });
    }
    
clicked(e)
{
    e.stopPropagation();
}

valueUpdated(e) {
    e.stopPropagation();
    $(e.target).blur();

    if (this.props !== undefined) {
        this.props.updateValue(this.props.data.Name, e.target.value,e);
    }


}


GetListItemButtons(controlName, type) {
    let buttons = null;

    if (this.state.listItems) {
        buttons = this.state.listItems.map(function (button, index) {
            var buttonID = controlName + '_' + index;

            return (<li key={buttonID}>
                
                <input id={buttonID} type={type} name={controlName} value={button.Value} />
                
                <label htmlFor={buttonID} ></label>
                <EditableText updateValue={function (one, two, three) { console.log(one); console.log(two); console.log(three); }} Name={buttonID} ID={buttonID} initialText={button.Text} />
        
            </li>);
        });
    }

    return buttons;
}

GetClassName() {
    let className = "unstyled-list";

    if (this.props.data.RepeatDirection == "Horizontal") {
        className += "-inline";
    }

    return className;
}

    render() {
        
        return (
            <span>
                <select id={this.props.data.Name} defaultValue={this.props.value} onClick={this.clicked} onChange={this.valueUpdated}>
                    {this.state.listItems.map(item => <option key={item.Value} value={item.Value}>{item.Text}</option>)}
            </select>
                </span>
        );
    }
}

