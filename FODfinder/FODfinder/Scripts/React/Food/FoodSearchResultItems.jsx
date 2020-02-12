class FoodSearchResultItems extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            results: this.props.results
        };
    }

    componentWillReceiveProps(props) {
        if (this.props != props) {
            this.props = props;
        }
    }

    render() {
        var { results } = this.state;

        return (
            <div className="container-fluid">
                {
                    results.map((result,i) => {
                        return (<p key={i}>{result.Description}</p>);
                    })
                }
            </div>
        )
    }
}