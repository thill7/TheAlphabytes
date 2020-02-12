class FoodSearchResults extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            results: JSON.parse(this.props.results),
            currentPage: this.props.currentPage,
            totalPages: this.props.totalPages,
            totalHits: this.props.totalHits
        };

        console.log(this.state);
    }

    render() {
        var { results, currentPage, totalPages, totalHits } = this.state;

        return (
            <div className="container">
                <div className="container-fluid d-flex flex-column justify-content-center align-items-center">
                    <p><span className="font-weight-bold">Current Page</span>: {currentPage}</p>
                    <p><span className="font-weight-bold">Total Pages</span>: {totalPages}</p>
                    <p><span className="font-weight-bold">Total Hits</span>: {totalHits}</p>
                </div>
                This Component is rendered!
            </div>
        )
    }
}