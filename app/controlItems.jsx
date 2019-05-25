import Button from './elementTypes/button.jsx';

export default class ControlItems extends React.Component {
    constructor(props) {
        super(props);

        this.updateSelectionItems = this.updateSelectionItems.bind(this);
        this.updateDataMethod = this.updateDataMethod.bind(this);
        this.state = { SelectionItems: this.props.controldata.SelectionItems, DataMethod: this.props.controldata.DataMethod};
    }

    updateSelectionItems(name,value)
    {
        this.setState({ SelectionItems: value });
        this.props.updateValue(name,value);
    }

    updateDataMethod(name, value) {
        this.setState({ DataMethod: value });
        this.props.updateValue(name, value);
    }
    
    render()
    {
        let SelectionItems = this.props.GetTabOption(this.props.childOptions.filter(o => o.Name == "SelectionItems")[0], this.updateSelectionItems);
        let IncludePleaseSelect = this.props.GetTabOption(this.props.childOptions.filter(o => o.Name == "IncludePleaseSelect")[0]);
        let DataMethod = this.props.GetTabOption(this.props.childOptions.filter(o => o.Name == "DataMethod")[0], this.updateDataMethod);
        let TextField = this.props.childOptions.filter(o => o.Name == "TextField")[0] ? this.props.GetTabOption(this.props.childOptions.filter(o => o.Name == "TextField")[0]) : null;
        let MinimumValue = this.props.GetTabOption(this.props.childOptions.filter(o => o.Name == "MinimumValue")[0]);
        let MaximumValue = this.props.GetTabOption(this.props.childOptions.filter(o => o.Name == "MaximumValue")[0]);
        let OtherDataMethod = this.props.GetTabOption(this.props.childOptions.filter(o => o.Name == "OtherDataMethod")[0]);
        let DataSource = this.props.GetTabOption(this.props.childOptions.filter(o => o.Name == "DataSource")[0]);
        let ListItems = this.props.GetTabOption(this.props.childOptions.filter(o => o.Name == "ListItems")[0]);
        
        let childOptions = this.props.childOptions.map((option,index) => {
            let optionControl = this.props.GetTabOption(option);

            return <div key={option.ID} style={{width:'25%',float:'left'}}>{optionControl}</div>;
        });
        
        return (<div>
            <div style={{ float: 'left', width:'50%', paddingRight:'20px'}}>
                            {SelectionItems}
                            {IncludePleaseSelect}
                            {MinimumValue}
                            {MaximumValue}
                </div>
                        <div style={{ float: 'left', width: '50%' }}>
                {this.state.SelectionItems == "4" ? DataMethod : null}
                {TextField}
                        {this.state.DataMethod == "4" ? OtherDataMethod : null}
                        {this.state.SelectionItems == "1" ? ListItems : null}

                        {this.state.SelectionItems == "2" ? DataSource : null}
                        </div>
                        <br style={{ clear: 'both' }} />
        </div>);

	 }
}