import Editable from './editable.jsx';

export default class EditableInput extends Editable {
constructor(props)
{
    super(props);
}

render()
{
    return (
        <input type="text" {...(this.GetAttributes())} ></input>
            );
}

}
