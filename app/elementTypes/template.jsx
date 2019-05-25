import Button from './button.jsx';

export default class Template extends React.Component {
    constructor(props) {
        super(props);
        this.state = { items: [this.props.childOptions] };
        this.AddItem = this.AddItem.bind(this);
        this.RemoveItem = this.RemoveItem.bind(this);
    }

    AddItem(e) {
        let items = this.state.items;
        let newRow = [];

        for (var i = 0; i < items[0].length; i++)
        {
            let copiedOption = jQuery.extend(true, {}, items[0][i]);
            copiedOption.ID = i;
            newRow.push(copiedOption);
        }

        //let copiedRow = jQuery.extend(true, {}, items[0]);
        //console.log('copied row');
        //console.log(copiedRow);

        //for (var i = 0; i < copiedRow.length; i++)
        //{
        //    copiedRow[i].ID = i;
        //}

        items.push(newRow);
        console.log(items);
        this.setState({ items: items });

        console.log(this.props.controldata);

    }

    RemoveItem(e)
    {
        let removeIndex = $(e.target).closest('tr')[0].id;
        let items = this.state.items;
        items.splice(removeIndex,1);
        this.setState({ items: items });
    }

    render() {
        
        let childItems = this.state.items.map((item, itemIndex) => {
            let childOptions = item.map((option) => {
                var optionControl = this.props.GetTabOption(option);

                return <td key={option.ID}>{optionControl}</td>;
            });
            
            return <tr id={itemIndex} key={itemIndex}>
                {childOptions}
                <td><a href='javascript:void(0)' onClick={this.RemoveItem}>Remove</a>
                    </td>
            </tr>;
        });

        return (<table>
            <tbody>
            {childItems}
            <tr>
                <td colSpan='100'><Button value="Add Item" Click={this.AddItem} />
                </td>
            </tr>
                </tbody>
        </table>);

    }
}