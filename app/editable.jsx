export default class Editable extends React.Component {
constructor(props)
{
    super(props);

    this.state = { ShowEdit: false, ShowText: true, Editor: null };
    this.ShowEdit = this.ShowEdit.bind(this);
    this.ShowText = this.ShowText.bind(this);
    this.updateData = this.updateData.bind(this);
    this.editorClicked = this.editorClicked.bind(this);
    this.editorChanged = this.editorChanged.bind(this);
    this.CreateEditor = this.CreateEditor.bind(this);
  
}
   

    GetAttributes() {
        return {
            onBlur: this.blurred,
            onChange: this.editorChanged,
            onClick: this.editorClicked,
            className: "editable",
            id: this.props.ID + 'Text',
            ref: (t) => this.text = t
        };
    }

ShowEdit() {
        $(ReactDOM.findDOMNode(this.editor)).val($(ReactDOM.findDOMNode(this.text)).html());
        this.setState({ ShowEdit: true, ShowText: false });
    }

ShowText()
{
    $(ReactDOM.findDOMNode(this.text)).html($(ReactDOM.findDOMNode(this.editor)).val());
    this.setState({ ShowEdit: false, ShowText: true });
    }

    componentDidMount() {
        if (this.props.editableImmediately) {
            this.CreateEditor();
        }
    }

    editorChanged(e)
    {
        setTimeout(() => this.updateData(e), 2000);
    }

    updateData(e) {
        let currentData = this.state.CurrentEditor.getData();

        if (this.state.CurrentData != currentData) {
            this.setState({ CurrentData: this.state.CurrentEditor.getData() })
            e.target = this.state.CurrentEditor.element;

            this.props.updateValue("Value", currentData, e);
            console.log('updating editable text value');
        }
        else {
            console.log('editable text is up to date');
        }
    }

CreateEditor()
{
    let currentEditable = this;

    if (!this.state.Editor) {
        console.log('setting editor at', document.querySelector('#' + this.props.ID + 'Text'));
             InlineEditor
                .create(document.querySelector('#' + this.props.ID + 'Text'))
                .catch(error => {
                    console.error(error);
                }).then(editor => {
                    console.log(editor);

                    editor.model.document.on('change', this.editorChanged);
                  
                    this.setState({
                        Editor: true,
                        CurrentEditor: editor,
                        CurrentData: editor.getData()
                    });
                    
                 });

        }
    }

editorClicked(e) 
{
        this.CreateEditor();
    }



render()
{
    return (
        <span onBlur={this.blurred} onChange={this.editorChanged} onClick={this.editorClicked} className="editable" id={this.props.ID + 'Text'} ref={(t) => { this.text = t; }} dangerouslySetInnerHTML={{ __html: this.props.initialText }}></span>
            );
}

}
