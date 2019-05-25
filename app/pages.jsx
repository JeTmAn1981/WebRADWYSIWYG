import PageSelection from "./pageSelection.jsx";
import Page from "./page.jsx";
import CKEditor from '@ckeditor/ckeditor5-react';
import ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import InlineEditor from '@ckeditor/ckeditor5-build-inline';
import Textarea from './elementTypes/textarea.jsx';

export default class Pages extends React.Component {
    constructor(props) {
        super(props);
        this.state = { pages: this.props.pages, selectedPage: this.getSelectedPage(), controls: this.props.controls};
        this.selectPage = this.selectPage.bind(this);
        this.addPage = this.addPage.bind(this);
        this.deletePage = this.deletePage.bind(this);
        this.addPlacedControl = this.addPlacedControl.bind(this);
        this.deletePlacedControl = this.deletePlacedControl.bind(this);
        this.getSelectedPage = this.getSelectedPage.bind(this);
    }

    getSelectedPage()
    {
        let selectedPage = this.props.pages.filter(page => page.ID == currentPageID);

        if (selectedPage.length > 0) {
            return selectedPage[0];
        }
        else {
            return this.props.pages[0];
        }
    }
    addPage(page) {
        try {
            var pages = this.state.pages;

                pages.push(page);
            
            this.setState({ pages: pages });
   }
        catch (ex) {
            console.log('error adding page - ');
            console.log(ex);
        }
    }

    deletePage(pageID) {
        var pages = this.state.pages;

        for (var i = 0; i < pages.length; i++) {
            if (pages[i].ID == pageID) {
                pages.splice(i, 1);
                break;
            }
        }

        this.setState({ pages: pages });
    }

    selectPage(e) {
        console.log('page selection came from', e.target);
        let selectedPage = this.state.pages.find(page => page.ID == $(e.target).find('#pageID')[0].value);

        this.setState({ selectedPage: selectedPage });
        $('#InsertPageID').val(selectedPage.ID);
        
    }

    addPlacedControl(controls) {
        try {

            console.log('control(s) to add - ');
            console.log(controls);

            var allPlacedControls = this.state.controls;

            for (let i = 0; i < controls.length; i++) {
                console.log('adding - ', controls[i]);
                SetControlType(controls[i]);
                SetControlDataType(controls[i]);
                allPlacedControls.push(controls[i]);
            }
            console.log('new placed controls', allPlacedControls);
            this.setState({ controls: allPlacedControls });

            //console.log('added control - ');
        }
        catch (ex) {
            console.log('error adding - ');
            console.log(ex);
        }
    }

    deletePlacedControl(controlID) {
        var controls = this.state.controls;

        for (var i = 0; i < controls.length; i++) {
            if (controls[i].ID == controlID) {
                controls.splice(i, 1);
                break;
            }
        }
        console.log('controls after delete', controls);
        this.setState({ controls: controls });
    }

    addPage(e) {
        
    }

    render() {
       
    let pagesControl = this;

                let pages = pagesControl.state.pages.sort((page1, page2) => page1.ID - page2.ID).map((page,index) =>
        {
            return <PageSelection selectPage={pagesControl.selectPage} selectedPageID={pagesControl.state.selectedPage.ID} page={page} index={index} />;
        });

        let pageControls = pagesControl.state.pages.filter(page => page.ID === this.state.selectedPage.ID).map(page => {
            let currentPageControls = pagesControl.state.controls.filter(control => control.PageID == page.ID);
                    
            return <Page controls={currentPageControls} currentPage={page} selectedPageID={this.state.selectedPage.ID} />;
        });

        return <span>
                <div className="page-container">
                {pages}
                <div style={{border:"0 px"}}><button onClick={this.addPage}>Add Page</button></div>
                </div>
            
            <Page controls={pagesControl.state.controls.filter(control => control.PageID == pagesControl.state.selectedPage.ID)} currentPage={this.state.selectedPage} selectedPageID={this.state.selectedPage.ID} />
        </span>;
    }
}
