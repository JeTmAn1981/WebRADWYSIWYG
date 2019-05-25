import Control from "./control/control.jsx";
import EditableText from './editableText.jsx';

export default class PlacedControls extends React.Component {
    constructor(props) {
        super(props);
        this.state = { placedControls: this.props.controls, selectedControl: null };
        this.selectControl = this.selectControl.bind(this);
        
    }

   
    selectControl(control) {
        this.setState({ selectedControl: control });
    }

    componentDidUpdate()
    {
        MakeDraggable();

    }
    componentDidMount()
    {
        MakeDraggable();
       
        //$(".placed-control").draggable({
        //    containment: "#PlacedControls",
        //    stop: function (event, ui) {
        //        if ($(this).draggable('option', 'revert'))
        //            $(this).draggable('option', 'revert', false);
        //    },
        //});

        //$('.placed-control').droppable({
        //    drop: function (event, ui) {
        //        ui.draggable.draggable('option', 'revert', true);
        //    }
        //});      
    }

    
    render() {
        var selectedControl = this.state.selectedControl;
        var selectControl = this.selectControl;

        var controlsList = this.state.placedControls.filter(function (control) { return control.ParentControlID == null; });

        
        let controls = controlsList.sort((control1, control2) => {
            let sortColumn = (control1.PageID == control2.PageID) ? 'Position' : 'PageID';   
            
            return control1[sortColumn] == control2[sortColumn] ? control2.ID - control1.ID : control1[sortColumn] - control2[sortColumn];
        }).map(
            function (currentControl)
            {
                return (<Control key={currentControl.ID} data={currentControl} selectControl={selectControl} selectedControl={selectedControl} />);
            }
        );

        
        return (
            <div className="row-fluid">
                <div id="selected-action-column" className="well droppedFields" style={{ minHeight: '80px' }}>
                    <ul id="PlacedControls" className="sortable">
                        {controls}
                    </ul>
                    {controlsList.length == 0 ? <a href='javascript:void(0)' onClick={(e) => { setInsertPosition(0); showInsert(); }}>Add Control</a> : null}
                </div>
            </div>
        );
    }
}
