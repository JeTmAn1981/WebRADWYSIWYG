import Editable from './editable.jsx';

export default class EditableText extends Editable {
    constructor(props) {
        super(props);
    }

    render() {
        let attributes = this.GetAttributes();

        return (

            <span dangerouslySetInnerHTML={{ __html: this.props.initialText }} {...attributes}></span>
        );
    }

}
