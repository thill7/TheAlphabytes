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
            <div className="container pt-3">
            <form className="form-inline w-100" onSubmit={(e) => { this.onQuerySubmit(e) }}>
                <div className="form-group w-100">
                    <div className="input-group w-100">
                        <div className="input-group-prepend">
                            <span className="input-group-text">
                                Search
                            </span>
                            <div className="dropdown">
                                <button type="button" className="btn btn-primary dropdown-toggle dropdown-toggle-split rounded-0" data-toggle="dropdown" id="SearchByButton">
                                    <span className="sr-only">
                                        Search by
                                    </span>
                                </button>
                                <div className="dropdown-menu bg-secondary" aria-labelledby="SearchByButton">
                                    <h6 className="dropdown-header text-gray">Search by:</h6>
                                    <a href="#" className={"dropdown-item text-gray" + (!isUpc ? " active bg-primary" : "")} onClick={() => { this.onUpcToggle(false) }}>
                                        Name
                                    </a>
                                    <a href="#" className={"dropdown-item text-gray" + (isUpc ? " active bg-primary" : "")} onClick={() => { this.onUpcToggle(true) }}>
                                        UPC
                                    </a>
                                </div>
                            </div>
                        </div>
                        <input type="text" required={true} onChangeCapture={(e) => { this.onQueryChanged(e) }} className="form-control border-left-0 border-right-0" />
                        <div className="input-group-append">
                            <button className="btn btn-primary" type="submit">Go!</button>
                        </div>
                    </div>
                </div>
            </form>
            </div>
        );
    }
}