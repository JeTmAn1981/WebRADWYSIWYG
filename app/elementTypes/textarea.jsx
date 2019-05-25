import ErrorLabel from '../control/components/errorLabel.jsx';
//import { Editor } from 'slate-react';
import { Value } from 'slate';
import CKEditor from '@ckeditor/ckeditor5-react';
import ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import InlineEditor from '@ckeditor/ckeditor5-build-inline';
import { Editor, EditorState } from 'draft-js';
import ReactQuill from 'react-quill';
export default class InputControl extends React.Component {
    constructor(props) {
        super(props);

        this.valueUpdated = this.valueUpdated.bind(this);
        this.state = { editingBegun: false } // You can also pass a Quill Delta here
        this.handleChange = this.handleChange.bind(this);
        this.beginEditing = this.beginEditing.bind(this);
    }

    beginEditing(e) {
        e.stopPropagation();
        this.setState({ editingBegun: true});
    }

    handleChange(value) {
        this.setState({ text: value });
    }

    valueUpdated(e, editor) {
        console.log('text area changed', e,this.props.data);
                
        if (this.props !== undefined) {
            const data = editor.getData();

            //this.props.updateValue((this.props.data ? this.props.data.Name : null) || this.props.name  ||  'Value', data, e);
            this.props.updateValue('Value', data, e);
        }
    }

    Clicked(e) {
        e.stopPropagation();
    }

    Focus(e) {
        e.stopPropagation();
    }

    render() {
        //return <Editor editorState={this.state.editorState} onChange={this.onChange} />;

        if (!this.state.editingBegun) {
            return <span onClick={this.beginEditing} dangerouslySetInnerHTML={{ __html: this.props.value || (this.props.data ? this.props.data.Value : '') }}></span>
        }
        else {
            return <CKEditor
                editor={InlineEditor}
                data={this.props.value || (this.props.data ? this.props.data.Value : '')}
                onInit={editor => {
                    // You can store the "editor" and use when it is needed.
                    editor.editing.view.focus();
                }}
                onChange={this.valueUpdated}
            //onBlur={editor => {
            //    console.log('Blur.', editor);
            //}}
            //onFocus={editor => {
            //    console.log('Focus.', editor);
            //}}
            />;
        }
        

        //return <Editor value={this.state.value} onChange={this.onChange} />;

        //var dataVal = (this.props.data.Required == "1" ? "true" : "false");
        //var dataValRequired = (this.props.data.Required == "1" ? "This field is required." : "");
        //let cssClasses = (this.props.data.CssClass || "") + " ckeditor";

        //return (<span>
        //    <ErrorLabel data={this.props.data} controldata={this.props.controldata} />

        //    {   <textarea id={this.props.data.Name + '_' + this.props.controldata.ID} type="text"
        //            name={this.props.data.Name}
        //            className={cssClasses}
        //            maxLength={this.props.data.MaxLength != null ? this.props.data.MaxLength : ''}
        //            value={this.props.value || this.props.data.Value}
        //            onChange={this.valueUpdated}
        //            onClick={this.Clicked}
        //            onFocus={this.Focus}
        //            data-val={dataVal}
        //            data-val-required={dataValRequired}
        //        />    }

        //    </span>);
    }
}

