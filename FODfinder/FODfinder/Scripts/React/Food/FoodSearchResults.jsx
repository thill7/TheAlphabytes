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

    onPageNavigate = (pNum) => {
        console.log(pNum);
    }

    render() {
        var { results, currentPage, totalPages, totalHits } = this.state;

        var pageNumbers = Array.apply(null, Array(totalPages)).map((_, i) => { return i; });

        return (
            <div className="container">
                <div className="container-fluid d-flex flex-column justify-content-center align-items-center">
                    <p><span className="font-weight-bold">Current Page</span>: {currentPage}</p>
                    <p><span className="font-weight-bold">Total Pages</span>: {totalPages}</p>
                    <p><span className="font-weight-bold">Total Hits</span>: {totalHits}</p>
                </div>
                <FoodSearchResultItems results={results} />
                <ul className="pagination pagination-lg justify-content-center flex-wrap">
                    {
                        pageNumbers.map((p, i) => {
                            return (<li className={"page-item" + (i + 1 == currentPage ? " active" : "")}><a onClick={() => { this.onPageNavigate(i+1) }} className="page-link">{i + 1}</a></li>)
                        })
                    }
                </ul>
            </div>
        )
    }
}