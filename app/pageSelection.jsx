import Control from "./control/control.jsx";

export default class PageSelection extends React.Component {
    constructor(props) {
        super(props);
    }
    
    render() {
           return <div className="page-selection" onClick={this.props.selectPage} className={(this.props.page.ID == this.props.selectedPageID) ? "selected-page" : ""}>
               Page #{this.props.index + 1}
               <p dangerouslySetInnerHTML={{ __html: this.props.page.Purpose }}></p>
                <input type="hidden" id="pageID" value={this.props.page.ID}></input>
            </div>;
      }
}
