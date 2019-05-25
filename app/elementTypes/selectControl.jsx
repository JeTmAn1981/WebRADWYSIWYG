import ListControl from './listControl.jsx';

export default class SelectControl extends ListControl {
    render() {
        
        let isListbox = (this.props.controldata && this.props.controldata.ControlType1 && this.props.controldata.ControlType1.DataType && (this.props.controldata.ControlType1.DataType || '') == "8");

        if (isListbox == undefined)
            isListbox = false;
        let listbox = isListbox ? { 'size': '4' } : {};
        let multiple = ((this.props.controldata.SelectionMode || '') == "Multiple") ? { 'multiple': 'multiple' } : {};

//        console.log((this.props.controldata.Name || this.props.data.Name) + ' is listbox: ' + isListbox);
        //if (this.props.data.Name == "SQLDataType") {
        //    console.log('value for ' + this.props.data.Name + ' - ' + this.props.value);
        //    console.log(this.props.controldata);
        //}
        
        return (
            <span>
                <select id={this.props.data.Name} value={isListbox ? [this.props.value] : this.props.value} onClick={this.clicked} onChange={this.valueUpdated} name={this.props.data.Name} {...listbox} {...multiple}>
                    {this.props.data.IncludePleaseSelect ? <option value="">Please Select</option> : null}
                    {this.state.listItems ? this.state.listItems.map(item => <option key={item.Value} value={item.Value}>{item.Text}</option>) : null}
            </select>
                </span>
        );
    }
}

