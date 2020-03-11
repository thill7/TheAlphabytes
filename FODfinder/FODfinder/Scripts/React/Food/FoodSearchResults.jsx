class FoodSearchResults extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            results: JSON.parse(this.props.results),
            currentPage: this.props.currentPage,
            totalPages: this.props.totalPages <= 200 ? this.props.totalPages : 200,
            totalHits: this.props.totalHits,
            ingredients: this.props.ingredients,
            requireAllWords: this.props.requireAllWords,
            query: this.props.query, 
            loading: false
        };

        this.onPageNavigate.bind(this);
        this.onGetPage.bind(this);
    }
    
    async onGetPage(pNum) {
        var { ingredients, query, requireAllWords } = this.state;
        var searchQuery = new URLSearchParams();
        if (ingredients != "" && ingredients != null) {
            searchQuery.append("ingredients", ingredients);
        }
        //console.log(totalIngredientList)
        searchQuery.append("query", query);
        searchQuery.append("requireAllWords", requireAllWords);
        searchQuery.append("pageNumber", pNum);
        var response = await axios.get(`/Food/Get?${searchQuery.toString()}`);
        return response.data;
    }
    
    
    async onPageNavigate(pNum) {
        this.setState({ loading: true });
        var newResults = await this.onGetPage(pNum);
        this.setState({
            results: newResults.Foods,
            currentPage: newResults.CurrentPage,
            totalPages: newResults.TotalPages <= 200 ? newResults.TotalPages : 200,
            totalHits: newResults.TotalHits,
            loading: false
        });
    }

    render() {
        var { results, currentPage, totalPages, totalHits, loading } = this.state;

        const ETC = "...";

        var pageNumbers = undefined;
        if (totalPages < 7) {
            pageNumbers = Array.apply(null, Array(totalPages)).map((_, i) => { return i; });
        }
        else {
            if (currentPage > 4) {
                if (currentPage < totalPages - 3) {
                    let pageStartOffset = currentPage - 4;
                    pageNumbers = Array.apply(null, Array(7)).map((_, i) => { return pageStartOffset + i; });
                }
                else {
                    let pageStartOffset = totalPages - 7;
                    pageNumbers = Array.apply(null, Array(7)).map((_, i) => { return pageStartOffset + i; });
                }
            }
            else {
                pageNumbers = Array.apply(null, Array(7)).map((_, i) => { return i; });
            }
            pageNumbers[0] = 0;
            pageNumbers[6] = totalPages - 1;
            pageNumbers[1] = pageNumbers[1] != 1 ? ETC : pageNumbers[1];
            pageNumbers[5] = pageNumbers[5] != totalPages - 2 ? ETC : pageNumbers[5];
        }

        return (
            <div className="container p-0 p-lg-3">
                <div className="row rounded my-0">
                    <div className="col-md-4  p-4 d-inline-flex flex-column align-items-center justify-content-center">
                        <p className="bg-secondary rounded p-2 text-gray shadow"><span className="font-weight-bold">Current Page</span>: {currentPage}</p>
                    </div>
                    <div className="col-md-4 p-4 d-inline-flex flex-column align-items-center justify-content-center">
                        <p className="bg-secondary rounded p-2 text-gray shadow"><span className="font-weight-bold">Total Pages</span>: {totalPages}</p>
                    </div>
                    <div className="col-md-4 p-4 d-inline-flex flex-column align-items-center justify-content-center">
                        <p className="bg-secondary rounded p-2 text-gray shadow"><span className="font-weight-bold">Total Hits</span>: {totalHits}</p>
                    </div>
                </div>
                <div className={loading ? "LoadingWrapper LoadingBlur" : "LoadingWrapper"}>
                    <FoodSearchResultItems results={results} />
                </div>
                <ul className="pagination pagination-lg justify-content-center flex-wrap">
                    {
                        pageNumbers.map((p, i) => {
                            return (
                                p == ETC ?
                                    <li key={"etc-" + (i)} className={"page-item disabled"}><a className="page-link">{ETC}</a></li>
                                    :
                                    <li key={"page-" + (i)} className={"page-item" + (p + 1 == currentPage ? " active" : "")}><a onClick={() => { this.onPageNavigate(p + 1) }} className="page-link">{p + 1}</a></li>
                            )
                        })
                    }
                </ul>
            </div>
        );
    }
}