import Button from './elementTypes/button.jsx';

export default class DataSource extends React.Component {
    constructor(props) {
        super(props);

        this.updateSelectionItems = this.updateSelectionItems.bind(this);
        this.updateDataMethod = this.updateDataMethod.bind(this);
        this.updateDataSource = this.updateDataSource.bind(this);
        this.state = { SelectionItems: '1', DataMethod: '0' };
    }

    updateSelectionItems(name, value) {
        this.setState({ SelectionItems: value });
        this.props.updateValue(name, value);
    }

    updateDataMethod(name, value) {
        this.setState({ DataMethod: value });
        this.props.updateValue(name, value);
    }


    updateDataSource(name, value, event)
    {
        let dataSource = this.props.controldata.ProjectDataSource || {};
        let dataSourceControl = this;

        dataSource[name] = value;

        $.ajax({
            type: "POST",
            url: updateDataSourceURL,
            data: { controlID: this.props.controldata.ID, dataSource: dataSource },
            success: function (info) {
                console.log('updated data source');
                console.log(info);

                dataSourceControl.props.updateValue("ProjectDataSource", dataSource, event);
            }
        });
    }

    render() {
        //console.log('data source data');
        //console.log(this.props.controldata.ProjectDataSource);

        let BackendDataSource = this.props.GetTabOption(this.props.childOptions.filter(o => o.Name == "BackendSource")[0], this.updateDataSource,this.props.controldata.ProjectDataSource);
        let DataSourceSelect = this.props.GetTabOption(this.props.childOptions.filter(o => o.Name == "Select")[0], this.updateDataSource,this.props.controldata.ProjectDataSource);
        let DataSourceTable = this.props.GetTabOption(this.props.childOptions.filter(o => o.Name == "Table")[0], this.updateDataSource, this.props.controldata.ProjectDataSource);
        let DataSourceWhere = this.props.GetTabOption(this.props.childOptions.filter(o => o.Name == "Where")[0], this.updateDataSource, this.props.controldata.ProjectDataSource);
        let DataSourceGroupBy = this.props.GetTabOption(this.props.childOptions.filter(o => o.Name == "GroupBy")[0], this.updateDataSource, this.props.controldata.ProjectDataSource);
        let DataSourceOrderBy = this.props.GetTabOption(this.props.childOptions.filter(o => o.Name == "OrderBy")[0], this.updateDataSource, this.props.controldata.ProjectDataSource);
        let DataTextField = this.props.GetTabOption(this.props.childOptions.filter(o => o.Name == "TextField")[0], this.updateDataSource, this.props.controldata.ProjectDataSource);
        let DataValueField = this.props.GetTabOption(this.props.childOptions.filter(o => o.Name == "ValueField")[0], this.updateDataSource, this.props.controldata.ProjectDataSource);

        return (<div>
            {BackendDataSource}

            <div style={{float:'left'}}>
                {DataSourceSelect}
            </div>

                <div style={{ float: 'left' }}>
                    {DataSourceTable}
                </div>

                    <div style={{ float: 'left' }}>
                        {DataSourceWhere}
                    </div>

                        <div style={{ float: 'left' }}>
                            {DataSourceGroupBy}
                        </div>

                            <div style={{ float: 'left' }}>
                                {DataSourceOrderBy}
                                </div>
                            <br style={{clear:'both'}} />
            {DataTextField}
            {DataValueField}
        </div>);




    }
}