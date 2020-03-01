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
                        return (
                            <a className="list-group-item list-group-item-info FoodLink" key={i} href={`/food/details/${result.FdcId}`}>
                                <div className="row">
                                    <div className="col-sm-8">
                                        <div className="row">
                                            <div className="col-md-6">
                                                <span className="text-capitalize">{result.Description.toLowerCase()}</span>
                                            </div>
                                            <div className="col-md-6">
                                                <span className="text-capitalize font-weight-bold">{result.BrandOwner.toLowerCase()}</span>
                                            </div>
                                            
                                        </div>
                                    </div>
                                    <div className="col-sm-4 text-lg-right">
                                        <span>UPC: {result.GtinUPC}</span>
                                    </div>
                                </div>
                            </a>
                        );
                    })
                }
                </ul>
            </div>
        )
    }
}