class FoodSearch extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            query: "",
            isUpc: false,
            requireAllWords: false,
            isCollapsed: true,
            includeItem: "",
            includeList: [],
            excludeItem: "",
            excludeList: []
        };

        this.onQueryChanged.bind(this);
        this.onQuerySubmit.bind(this);
        this.onUpcToggle.bind(this);
        this.onRequireAllWords.bind(this);
        this.onCollapse.bind(this);
        this.addToIncludeList.bind(this);
        this.removeFromIncludeList.bind(this);
        this.addToExcludeList.bind(this);
    }

    async onQueryChanged(event) {
        await this.setState({ query: event.target.value });
    }

    async onUpcToggle(toggled) {
        await this.setState({ isUpc: toggled });
    }

    async onRequireAllWords(toggled) {
        await this.setState({ requireAllWords: toggled })
    }

    async onCollapse(toggled) {
        await this.setState({ isCollapsed: toggled })
    }

    async onAddInclude(item) {
        await this.setState({ includeItem: item.target.value })
    }

    async addToIncludeList() {
        var {  includeItem, includeList  } = this.state;
        if (!includeList.includes(includeItem) && includeItem != "") {
            await this.setState({ includeList: [...includeList,includeItem] });
            $('#IncludeInput').val('');
        }
    }

    async addIncludeOnEnter(event) {
        if (event.key === "Enter") {
            await this.addToIncludeList();
        }
    }

    async removeFromIncludeList(item) {
        var index = this.state.includeList.indexOf(item);
        if(index > -1) {
            var newIncludeList = this.state.includeList;
            newIncludeList.splice(index, 1);
            this.setState({ includeList : newIncludeList })
            this.setState({ includeItem : "" })
        }
    }

    showIncludeList() {
        if (this.state.includeList.length != 0) {
            return (
                <div className="shadow rounded">
                    <ul className="list-group">
                        <li className="list-group-item font-weight-bold text-capitalize text-gray text-center">Included Ingredients</li>
                        {this.state.includeList.map(item => (
                            <li className="list-group-item" key={item}>
                                <div className="container p-0 m-0">
                                    <div className="row d-flex p-0 m-0">
                                        <div className="col-8 inline-block align-middle p-0 m-0">
                                            <span className="align-middle">
                                                {item}
                                            </span>
                                        </div>
                                        <div className="col-4 p-0 pl-1 m-0 text-right align-self-center">
                                            <button className="btn btn-primary text-gray" onClick={() => this.removeFromIncludeList(item)}>
                                                &#128940;        
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </li>
                        ))}
                    </ul>
                </div>
            );
        }
    }

    async onAddExclude(item) {
        await this.setState({ excludeItem: item.target.value })
    }

    async addToExcludeList() {
        var {  excludeItem, excludeList  } = this.state;
        if (!excludeList.includes(excludeItem) && excludeItem != "") {
            await this.setState({ excludeList: [...excludeList,excludeItem] });
            $('#ExcludeInput').val('');
        }
    }

    async addExcludeOnEnter(event) {
        if (event.key === "Enter") {
            await this.addToExcludeList();
        }
    }

    async removeFromExcludeList(item) {
        var index = this.state.excludeList.indexOf(item);
        if(index > -1) {
            var newExcludeList = this.state.excludeList;
            newExcludeList.splice(index, 1);
            this.setState({ excludeList : newExcludeList })
            this.setState({ excludeItem : "" })
        }
    }

    showExcludeList() {
        if (this.state.excludeList.length != 0) {
            return (
                <div className="shadow rounded">
                    <ul className="list-group">
                        <li className="list-group-item font-weight-bold text-capitalize text-gray text-center">Excluded Ingredients</li>
                        {this.state.excludeList.map(item => (
                            <li className="list-group-item" key={item}>
                                <div className="container p-0 m-0">
                                    <div className="row d-flex p-0 m-0">
                                        <div className="col-8 inline-block align-middle p-0 m-0">
                                            <span className="align-middle">
                                                {item}
                                            </span>
                                        </div>
                                        <div className="col-4 p-0 pl-1 m-0 text-right align-self-center">
                                            <button className="btn btn-primary text-gray" onClick={() => this.removeFromExcludeList(item)}>
                                                &#128940;        
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </li>
                        ))}
                    </ul>
                </div>
            );
        }
    }

    removeLeadingZeros(query) {
        return parseInt(query).toString();
    }

    onQuerySubmit(event) {
        event.preventDefault();
        var { isUpc, query, requireAllWords, includeList, excludeList } = this.state;
        var searchQuery = new URLSearchParams();
        var totalIngredientList = [...includeList.map(ingr => ingr.includes(" ") ? `"${ingr}"` : ingr), ...excludeList.map(ingr => ingr.includes(" ") ? `-"${ingr}"` : `-${ingr}`)];
        console.log(isUpc);
        if (totalIngredientList.length > 0 && isUpc == false) {
            searchQuery.append("ingredients", totalIngredientList.join(" "))
        }
        searchQuery.append("query", isUpc ? ("gtinUpc:*" + this.removeLeadingZeros(query)) : query)
        searchQuery.append("requireAllWords", requireAllWords)
        if (query != "") {
            let url = "/Food/Index?" + searchQuery.toString();
            window.location.href = url;
        }
    }

    render() {
        var { isUpc, requireAllWords, isCollapsed } = this.state;
        return (
            <div className="container px-0">
                <form className="form w-100" onSubmit={(e) => { this.onQuerySubmit(e) }}>
                    <div className="form-group w-100 mb-0">
                        <div className="input-group w-100">
                            <div className="input-group-prepend">
                                <span className="input-group-text">
                                    Search
                                </span>
                            </div>
                            <input type={(!isUpc ? "text" : "number")} required={true} onChangeCapture={(e) => { this.onQueryChanged(e) }} className="form-control border-left-0 border-right-0" title="" />
                            <div className="input-group-append">
                                <div className="btn-group" role="group">
                                    <span className="d-inline-block" data-toggle="tooltip" data-placement="bottom" title="Apply filters!" data-trigger="hover" data-delay='{"show":100, "hide":500}'>
                                        <button type="button" className="btn btn-primary m-0 rounded-0" data-toggle="collapse" data-target="#CollapseFilter" onClick={ () => this.onCollapse(!isCollapsed) }>
                                            <span className="small">
                                                {isCollapsed ? "▼" : "▲"}
                                            </span>
                                        </button>
                                    </span>
                                    <button className="btn btn-primary m-0" type="submit">Go</button>
                                </div>
                            </div>
                        </div>             
                    </div>
                </form>
                <div className="collapse" id="CollapseFilter">
                    <div className="card card-body shadow bg-secondary">
                        <div className="row d-flex justify-content-center">
                            <div className="col-12 text-center">
                                <span className="font-weight-bold text-capitalize text-gray">
                                    Apply Filters
                                </span>
                            </div>
                            <div className="col-lg-6">
                                <div className="input-group py-2">
                                    <div className="input-group-prepend text-center" data-toggle="tooltip" data-placement="top" title="Toggle between searching by food name and by UPC." data-trigger="hover" data-delay='{"show":100, "hide":500}'>
                                        <span className="input-group-text">
                                            Search by
                                        </span>
                                    </div>
                                    <div className="input-group-append">
                                        <button type="button" className="btn btn-primary dropdown-toggle text-gray" data-toggle="dropdown" id="SearchByButton">
                                            {!isUpc ? "Name" : "UPC"}
                                        </button>
                                        <div className="dropdown-menu bg-secondary" aria-labelledby="SearchByButton">
                                            <a href="#" className={"dropdown-item text-gray" + (!isUpc ? " active bg-primary" : "")} onClick={() => { this.onUpcToggle(false) }}>
                                                Name
                                            </a>
                                            <a href="#" className={"dropdown-item text-gray" + (isUpc ? " active bg-primary" : "")} onClick={() => { this.onUpcToggle(true) }}>
                                                UPC
                                            </a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div className="col-lg-6">
                                <div className="input-group py-2">
                                    <div className="input-group-prepend text-center" data-toggle="tooltip" data-placement="top" title="Enable or disable exact phrase searching." data-trigger="hover" data-delay='{"show":100, "hide":500}'>
                                        <span className="input-group-text">
                                            Exact phrase search
                                        </span>
                                    </div>
                                    <div className="input-group-append">
                                        <button className={"btn " + (isUpc ? "btn-danger" : !requireAllWords ? "btn-danger" : "btn-success")} disabled={isUpc ? "disabled" : ""} onClick={() => { this.onRequireAllWords(!requireAllWords) }}>
                                            {isUpc ? "Disabled" : !requireAllWords ? "Disabled" : "Enabled"}
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div className="row d-flex justify-content-center">
                            <div className="col-lg-6">
                                <div className="input-group py-2">
                                    <div className="input-group-prepend text-center" data-toggle="tooltip" data-placement="top" data-html="true" title='Type in the name of an ingredient you <b>do</b> want to see in the results, then click "+" or press enter.'  data-trigger="hover" data-delay='{"show":100, "hide":500}'>
                                        <span id="IncludeExclude" className="input-group-text">
                                            Include
                                        </span>
                                    </div>
                                    <input id="IncludeInput" type="text" className="form-control" disabled={isUpc ? "disabled" : ""} onChange={(e) => { this.onAddInclude(e) }} onKeyPress={(e) => this.addIncludeOnEnter(e)} />
                                    <div className="input-group-append">
                                        <button className="btn btn-primary text-gray" disabled={isUpc ? "disabled" : ""} onClick={() => this.addToIncludeList()}>
                                            &#10010;
                                        </button>
                                    </div>
                                </div>
                                <div>
                                    {!isUpc ? this.showIncludeList() : ""}
                                </div>
                            </div>
                            <div className="col-lg-6">
                                <div className="input-group py-2">
                                    <div className="input-group-prepend text-center" data-toggle="tooltip" data-placement="top" data-html="true" title='Type in the name of an ingredient you <b>do not</b> want to see in the results, then click "+" or press enter.' data-trigger="hover" data-delay='{"show":100, "hide":500}'>
                                        <span id="IncludeExclude" className="input-group-text">
                                            Exclude
                                        </span>
                                    </div>
                                    <input id="ExcludeInput" type="text" className="form-control" disabled={isUpc ? "disabled" : ""} onChange={(e) => { this.onAddExclude(e) }} onKeyPress={(e) => this.addExcludeOnEnter(e)} />
                                    <div className="input-group-append">
                                        <button type="submit" className="btn btn-primary text-gray" disabled={isUpc ? "disabled" : ""} onClick={() => this.addToExcludeList()}>
                                            &#10010;
                                        </button>
                                    </div>
                                </div>
                                <div>
                                    {!isUpc ? this.showExcludeList() : ""}
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}