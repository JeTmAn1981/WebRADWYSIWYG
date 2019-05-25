
export default class EditLinks extends React.Component {
constructor(props)
{
super(props);
}

    render() {
        return <div className='edit-links'>
            <a href='javascript:void(0);' onClick={this.props.showDisplayTab}>Edit Control</a>
            <a href=''>Move Up 1</a>
            <a href=''>Move Down 1</a>
                </div>;
        }
    }
    
