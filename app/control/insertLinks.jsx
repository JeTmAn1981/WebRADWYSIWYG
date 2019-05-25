export default class InsertLinks extends React.Component
{
    render()
    {
        return (this.props.show ?
            (<div style={{ display: "flex", justifyContent: "space-between" }}>
                <div>
                    <a href='javascript:void(0);' onClick={this.props.insertBelow} className="insert-control">Insert Control Below</a>
                </div>
                <div>
                    <a href='javascript:void(0);' onClick={this.props.insertAbove} className="insert-control">Insert Control Above</a>
                </div>
                <div style={{ display: (this.props.isParent == 1) ? 'inline' : 'none' }}>
                    <a href='javascript:void(0);' onClick={this.props.insertInside} className="insert-control">Insert Control Inside</a>
                </div>
                <div>
                    <a href='javascript:void(0);' onClick={this.props.showDelete} className="insert-control">Delete Control</a>
                </div>
            </div>
            )
            :
            null);
    }
}