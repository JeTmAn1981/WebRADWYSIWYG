import CKEditor from "react-ckeditor-component";
import EditableText from "./editableText.jsx";
import InsertControl from "./insertControl.jsx";
import DeleteControl from "./deleteControl.jsx";
import PlacedControls from "./placedControls.jsx";
import Pages from "./pages.jsx";

let projectPages = ReactDOM.render(
    <Pages controls={allControls} pages={pages} />,
    document.getElementById('Pages')
);

ReactDOM.render(
    <EditableText Name="Form Title" ID="FormTitle" initialText={formTitle} />,
    document.getElementById('FormTitleEditable')
);

ReactDOM.render(
    <InsertControl AddPlacedControl={projectPages.addPlacedControl} ControlTypes={controlTypes} />,
    document.getElementById('InsertControlContainer')
);

ReactDOM.render(
    <DeleteControl DeletePlacedControl={projectPages.deletePlacedControl}  />,
    document.getElementById('DeleteControlContainer')
);











