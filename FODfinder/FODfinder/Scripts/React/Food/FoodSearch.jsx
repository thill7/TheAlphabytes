class FoodSearch extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            query: "",
            isUpc: false
        };

        this.onQueryChanged.bind(this);
        this.onQuerySubmit.bind(this);
        this.onUpcToggle.bind(this);
    }

    async onQueryChanged(event) {
        await this.setState({ query: event.target.value });
    }

    async onUpcToggle(toggled) {
        await this.setState({ isUpc: toggled });
        console.log(this.state.isUpc);
    }

    onQuerySubmit(event) {
        event.preventDefault();
        var { isUpc, query } = this.state;
        if (query != "") {
            let url = `/Food/Index?query=${(isUpc ? "gtinUpc:" : "") + query}`;
            window.location.href = url;
        }
    }

    render() {
        var { isUpc } = this.state;

        return (
            <div className="container-fluid px-0">
                <form className="form w-100" onSubmit={(e) => { this.onQuerySubmit(e) }}>
                    <div className="form-group w-100 mb-0">
                        <div className="input-group w-100">
                            <div className="input-group-prepend">
                                <span className="input-group-text">
                                    Search
                                </span>
                            </div>
                            <input type={(!isUpc ? "text" : "number")} required={true} onChangeCapture={(e) => { this.onQueryChanged(e) }} className="form-control border-left-0 border-right-0" />
                            <div className="input-group-append">
                                <div className="btn-group">
                                    <button type="button" className="btn btn-block btn-primary border border-0 border-right border-dark" data-toggle="collapse" data-target="#CollapseFilter">
                                        <span className="small">
                                            ▼
                                        </span>
                                    </button>
                                    <button className="btn btn-primary" type="submit">Go</button>
                                </div>
                            </div>
                        </div>             
                    </div>
                </form>
                <div className="pt-1">
                    <button type="button" className="btn btn-block btn-primary" data-toggle="collapse" data-target="#CollapseFilter">
                        <span>
                            Apply Filters
                        </span>
                    </button>
                    <div className="collapse pt-1" id="CollapseFilter">
                        <div className="card card-body">
                            <div className="dropdown">
                                <button type="button" className="btn btn-primary dropdown-toggle" data-toggle="dropdown" id="SearchByButton">
                                    {"Search by " + (!isUpc ? "Name" : "UPC")}
                                </button>
                                <div className="dropdown-menu bg-secondary" aria-labelledby="SearchByButton">
                                    <a href="#" className={"dropdown-item text-gray" + (!isUpc ? " active bg-primary" : "")} onClick={() => { this.onUpcToggle(false) }}>
                                        Search by Name
                                    </a>
                                    <a href="#" className={"dropdown-item text-gray" + (isUpc ? " active bg-primary" : "")} onClick={() => { this.onUpcToggle(true) }}>
                                        Search by UPC
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}