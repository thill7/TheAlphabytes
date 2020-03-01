class FoodSearchResultItems extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            results: this.props.results
        };
    }

    componentWillReceiveProps(nextProps) {
        if (this.props != nextProps) {
            this.setState({ results: nextProps.results });
        }
    }

    render() {
        var { results } = this.state;

        return (
            <div className="container-fluid">
                <p className="bg-secondary display-4 p-2 rounded shadow text-gray">Results:</p>
                <ul className="list-group list-group-lg">
                {
                    results.map((result,i) => {
                        return (<li className="list-group-item" key={i}><a href={`/food/details/${result.FdcId}`}>{result.Description}</a></li>);
                    })
                }
                </ul>
            </div>
        )
    }
}