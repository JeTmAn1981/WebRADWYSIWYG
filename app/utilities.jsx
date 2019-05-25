import Control from "./control/control.jsx";

export default function GetChildControls(parentControl)
{
    return allControls.filter(function (control) { return control.ParentControlID == parentControl.props.data.ID }).map(function (currentControl) {
        return (<Control key={currentControl.ID} data={currentControl} selectControl={parentControl.props.selectControl} selectedControl={parentControl.props.selectedControl} />);
    });
}