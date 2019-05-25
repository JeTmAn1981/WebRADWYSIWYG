import { ConfigurationTab } from './configuration/configurationTab.jsx';

export default class DeleteControl extends React.Component {
constructor(props)
{
    super(props);

    this.deleteControl = this.deleteControl.bind(this);
    }


deleteControl()
{
    var controlID = document.getElementById('DeleteID').innerText;
    var deleteControl = this;

    $.ajax({
            type: "POST",
            url: deleteControlURL,
            data: {ControlID: controlID}, 
            success: function (info) {
                deleteControl.props.DeletePlacedControl(controlID);
            }
    });

    CloseDeleteControl();
}
    
closeDelete(e)
{
    
    e.stopPropagation();
    CloseDeleteControl();
}
    
render() {
    return (
        <div id="DeleteControl" style={{display:'none'}} className="modal">
            <div className="modal-content">
                <div className="modal-header">
                    <h2>Delete Control&nbsp;<a href="javascript:void(0);" onClick={this.closeDelete}>X</a></h2>
                </div>
                <div className="modal-body">
                    <label>Really Delete Control #<span id="DeleteID"></span> - <span id="DeleteName"></span>?</label>

                    <div style={{ textAlign:'center'}}>
                        <a href="javascript:void(0)" onClick={this.deleteControl} className="button">Delete</a>
                    </div>

                </div>
            </div>
        </div>);
}

}
