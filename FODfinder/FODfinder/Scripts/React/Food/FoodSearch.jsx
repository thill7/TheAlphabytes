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

    async onUpcToggle(event) {
        await this.setState({ isUpc: event.target.value != "0" });
    }

    onQuerySubmit(event) {
        event.preventDefault();
        var { isUpc,query } = this.state;
        let url = `/Food/Index?query=${(isUpc ? "gtinUpc:" : "") + query}`;
        window.location.href = url;
    }

    render() {
        var { isUpc } = this.state;

        return (
            <form className="form-inline w-100" onSubmit={(e) => { this.onQuerySubmit(e) }}>
                <div className="input-group w-100">
                    <div className="input-group-prepend">
                        <span className="input-group-text">Search:</span>
                    </div>
                        <select className="form-control" onChange={(e) => { this.onUpcToggle(e) }}>
                            <option value="0" defaultValue>
                                Name
                            </option>
                            <option value="1">
                                UPC
                            </option>
                        </select>
                    <input type="text" onChangeCapture={(e) => { this.onQueryChanged(e) }} className="form-control" />
                    <div className="input-group-append">
                        <button className="btn btn-success" type="submit">Go!</button>
                    </div>
                </div>
            </form>
        );
    }
}