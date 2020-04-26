class FoodDetails extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            details: JSON.parse(this.props.details),
            showUserLists: false,
            showLabels: false,
            ingredientId: null,
            userLists: null
        };
        this.handleclick.bind(this);
        this.showUserLists = this.showUserLists.bind(this);
        this.hideUserLists = this.hideUserLists.bind(this);
        this.showLabels = this.showLabels.bind(this);
        this.hideLabels = this.hideLabels.bind(this);
        this.getUserLists = this.getUserLists.bind(this);
    }

    GetFoodNutrientValue(key) {
        if (key != undefined) {
            return key.value;
        }
        return "";
    }

    GetIngredientListHelper(list) {
        var ingredientsList = "";
        for (var i = 0; i < list.length; i++) {
            var position = list[i].IngredientPosition;
            if (position == 0) {
                ingredientsList += list[i].Name + " (";
            }
            else if (position == 1 || position == 2) {
                ingredientsList += list[i].Name;
                ingredientsList += i == list.length - 1 ? ")" : "), ";
            }
            else if (position == 3) {
                ingredientsList += list[i].Name;
                ingredientsList += i == list.length - 1 ? "" : ", ";
            }
        }
        return ingredientsList;
    }

    GetIngredientList() {
        var primaryIngredients = this.GetIngredientListHelper(this.state.details.PrimaryIngredients);
        var secondaryIngredients = this.GetIngredientListHelper(this.state.details.SecondaryIngredients);
        if (secondaryIngredients != "") {
            return primaryIngredients.concat(", contains 2% or less of: ", secondaryIngredients, ".");
        }
        return primaryIngredients.concat(".");
    }
    
    componentDidMount() {
        var { details } = this.state;
        var labelNutrients = JSON.parse(details.LabelNutrients);

        $('#nutritionLabel').nutritionLabel({
            allowCustomWidth: true,
            allowNoBorder: true,
            showServingUnitQuantity: true,
            itemName: details.Description,
            ingredientList: this.GetIngredientList(),
            showCalories: labelNutrients.calories != undefined,
            showFatCalories: false,
            showTotalFat: labelNutrients.fat != undefined,
            showSatFat: labelNutrients.saturatedFat != undefined,
            showTransFat: labelNutrients.transFat != undefined,
            showPolyFat: labelNutrients.polyFat != undefined,
            showMonoFat: labelNutrients.monoFat != undefined,
            showCholesterol: labelNutrients.cholesterol != undefined,
            showSodium: labelNutrients.sodium != undefined,
            showPotassium_2018: labelNutrients.potassium != undefined,
            showTotalCarb: labelNutrients.carbohydrates != undefined,
            showFibers: labelNutrients.fiber != undefined,
            showSugars: labelNutrients.sugars != undefined,
            showAddedSugars: false,
            showProteins: labelNutrients.protein != undefined,
            showVitaminA: labelNutrients.vitaminA != undefined,
            showVitaminC: labelNutrients.vitaminC != undefined,
            showVitaminD: labelNutrients.vitaminD != undefined,
            showCalcium: labelNutrients.calcium != undefined,
            showIron: labelNutrients.iron != undefined,
            showCaffeine: labelNutrients.caffeine != undefined,
            valueCalories: this.GetFoodNutrientValue(labelNutrients.calories),
            valueTotalFat: this.GetFoodNutrientValue(labelNutrients.fat),
            valueSatFat: this.GetFoodNutrientValue(labelNutrients.saturatedFat),
            valueTransFat: this.GetFoodNutrientValue(labelNutrients.transFat),
            valuePolyFat: this.GetFoodNutrientValue(labelNutrients.polyFat),
            valueMonoFat: this.GetFoodNutrientValue(labelNutrients.monoFat),
            valueCholesterol: this.GetFoodNutrientValue(labelNutrients.cholesterol),
            valueSodium: this.GetFoodNutrientValue(labelNutrients.sodium),
            valueTotalCarb: this.GetFoodNutrientValue(labelNutrients.carbohydrates),
            valueFibers: this.GetFoodNutrientValue(labelNutrients.fiber),
            valueSugars: this.GetFoodNutrientValue(labelNutrients.sugars),
            valueProteins: this.GetFoodNutrientValue(labelNutrients.protein),
            valueVitaminA: this.GetFoodNutrientValue(labelNutrients.vitaminA),
            valueVitaminC: this.GetFoodNutrientValue(labelNutrients.vitaminC),
            valueVitaminD: this.GetFoodNutrientValue(labelNutrients.vitaminD),
            valuePotassium_2018: this.GetFoodNutrientValue(labelNutrients.potassium),
            valueCalcium: this.GetFoodNutrientValue(labelNutrients.calcium),
            valueIron: this.GetFoodNutrientValue(labelNutrients.iron),
            valueCaffeine: this.GetFoodNutrientValue(labelNutrients.caffeine),
            valueServingSizeUnit: details.ServingSizeUnit,
            valueServingUnitQuantity: details.ServingSize,
            showLegacyVersion: false
        });
    }

    async handleclick(e) {
        var id = parseInt(this.state.details.FdcId);
        var button = e.target;
        var listID = button.dataset.list;
        var brand = this.state.details.BrandOwner;
        var desc = this.state.details.Description;
        var barcode = this.state.details.UPC;
        var saveFood = await axios.post(`/SavedFoods/Create`, { usdaFoodID: id, listID: listID, brandOwner: brand, description: desc, upc: barcode });
        var result = saveFood.data;
        var message = result.message;
        if (result.redirect == true) {
            window.location.replace("/Account/Login?ReturnUrl=%2ffood%2fdetails%2f" + id);
        } else {
            alert(message);
        }
        
        window.console.log(message);
    }

    showUserLists(event) {
        event.preventDefault();

        this.setState({ showUserLists: true }, () => {
            document.addEventListener('click', this.hideUserLists);
        });
    }

    hideUserLists(event) {
        if (!this.saveFoodDropdown.contains(event.target)) {
            this.setState({ showUserLists: false }, () => {
                document.removeEventListener('click', this.hideUserLists);
            });
        }
    }

    async getUserLists(event) {
        var result = await axios.get(`/UserLists/getLists`);
        if (result.data.success === true) {
            this.state.details.userLists = result.data.lists;
        }
        this.showUserLists(event);
    }

    showLabels(event, index) {
        event.preventDefault();

        this.setState({ ingredientId: index });
        this.setState({ showLabels: true }, () => {
            document.addEventListener('click', this.hideLabels);
        });
    }

    hideLabels(event) {
        if (this.dropdownMenu != null) {
            if (!this.dropdownMenu.contains(event.target)) {
                this.setState({ showLabels: false }, () => {
                    document.removeEventListener('click', this.hideLabels);
                });
            }
            this.setState({ ingredientId: null });
        }
    }

    async addLabel(label, ingredient) {
        var id = parseInt(this.state.details.FdcId);
        var saveLabel = await axios.post(`/FODMAPIngredients/Create`, { assignLabel: label, ingredientName: ingredient });
        var result = saveLabel.data;
        var message = result.message;
        if(result.redirect == true) {
            window.location.replace("/Account/Login?ReturnUrl=%2ffood%2fdetails%2f" + id);
        } else {
            alert(message);
        }
        window.console.log("Label: " + label + " Ingredient: " + ingredient);
    }

    setFlag(flag) {
        flag = 1;
    }

    render() {
        var { details } = this.state;
        var flagBlacklist = false;
        var primaryLength = this.state.details.PrimaryIngredients.length;
        var secondaryLength = this.state.details.SecondaryIngredients.length;

        return (
            <div className="pt-4">
                <div className="card bg-secondary text-gray shadow">
                    <div className="card-header">
                        <h2 className="display-4 font-weight-normal text-capitalize">{details.Description.toLowerCase()}</h2>
                        <h3 className="font-weight-light">{details.BrandOwner}</h3>
                        <button type="button" onClick={this.getUserLists} className="btn btn-primary text-white">Save Food</button>
                        {
                            this.state.showUserLists
                                ? (
                                    <div className="dropdown show" ref={(element) => { this.saveFoodDropdown = element; }}>
                                        <div className="dropdown-menu show">
                                            {details.userLists != null ?
                                                details.userLists.map(list => <button className="dropdown-item" data-list={list.listID} onClick={(e) => this.handleclick(e)}> {list.listName} </button>) : null}
                                            <div className="dropdown-divider"></div>
                                            <a className="dropdown-item" href="/UserLists/Create">Create new list</a>
                                        </div>
                                    </div>
                                )
                                : (
                                    null
                                )
                        }
                    </div>
                    <div className="card-body">
                        <div className="row">
                            <div className="col-md-6 d-inline-flex flex-column justify-content-start align-items-start">
                                <div className="text-lowercase pb-3">
                                    <span className="font-weight-bold text-capitalize">Ingredients:&nbsp;</span>
                                    {details.PrimaryIngredients.map((i, index, array) =>
                                        <span key={`primary-outer-span-${index}`}>
                                            {
                                                i.IngredientPosition == 1 || i.IngredientPosition == 2 ? <span key={`primary-inner-span-${index}`} onClick={(e) => { this.showLabels(e, index) }} className={"cursor-pointer rounded " + i.Label + (i.IsFodmap ? " px-1 bg-danger-50 text-white rounded" : "")}>{i.Name}</span>
                                                    : <span>{i.Name}</span>
                                            }
                                            {
                                                i.IngredientPosition == 2 ? index == primaryLength - 1 ? secondaryLength != 0 ? ", " : "." : ", "
                                                    : i.IngredientPosition == 1 ? index == primaryLength - 1 ? secondaryLength != 0 ? "), " : ")" : "), "
                                                    : i.IngredientPosition == 0 ? " (" : ""
                                            }
                                            {i.Label == "Blacklist" ? flagBlacklist = true : null}
                                            {
                                                this.state.showLabels && this.state.ingredientId == index
                                                    ? (
                                                        <div id="label" className="labels list-group"
                                                            ref={(element) => {
                                                                this.dropdownMenu = element;
                                                            }}>
                                                            <button className="list-group-item list-group-item-dark">{i.Name}</button>
                                                            <button className="list-group-item list-group-item-action " onClick={() => { this.addLabel("High-Risk",i.Name) }}> High Risk </button>
                                                            <button className="list-group-item list-group-item-action " onClick={() => { this.addLabel("Low-Risk",i.Name) }}> Low Risk </button>
                                                            <button className="list-group-item list-group-item-action " onClick={() => { this.addLabel("Blacklist",i.Name) }}> Blacklist </button>
                                                        </div>
                                                    )
                                                    : (
                                                        null
                                                    )
                                            }
                                        </span>)
                                    }
                                    {
                                        secondaryLength > 0 &&
                                        <span className="font-weight-bold">contains 2% or less of:&nbsp;</span>
                                    }
                                    {details.SecondaryIngredients.map((i, index, array) =>
                                        <span key={`secondary-outer-span-${index}`}>
                                            {
                                                i.IngredientPosition == 2 ? <span key={`secondary-inner-span-${index}`} onClick={(e) => { this.showLabels(e, index + primaryLength) }} className={"cursor-pointer rounded " + i.Label + (i.IsFodmap ? " px-1 bg-danger-50 text-white rounded" : "")}>{i.Name}</span>
                                                    : i.IngredientPosition == 0 ? <span>{i.Name}</span>
                                                        : i.IngredientPosition == 1 ? <span key={`secondary-inner-span-${index}`} onClick={(e) => { this.showLabels(e, index + primaryLength) }} className={"cursor-pointer rounded " + i.Label + (i.IsFodmap ? " px-1 bg-danger-50 text-white" : "")}>{i.Name}</span>
                                                            : null
                                            }
                                            {
                                                i.IngredientPosition == 2 ? index == secondaryLength - 1 ? "." : ", "
                                                    : i.IngredientPosition == 1 ? index == secondaryLength - 1 ? ")" : "), "
                                                    : i.IngredientPosition == 0 ? " (" : ""
                                            }
                                            {i.Label == "Blacklist" ? flagBlacklist = true : null}
                                            {
                                                this.state.showLabels && this.state.ingredientId == index + primaryLength
                                                    ? (
                                                        <div id="label" className="labels list-group"
                                                            ref={(element) => {
                                                                this.dropdownMenu = element;
                                                            }}>
                                                            <button className="list-group-item list-group-item-dark">{i.Name}</button>
                                                            <button className="list-group-item list-group-item-action " onClick={() => { this.addLabel("High-Risk", i.Name) }}> High Risk </button>
                                                            <button className="list-group-item list-group-item-action " onClick={() => { this.addLabel("Low-Risk", i.Name) }}> Low Risk </button>
                                                            <button className="list-group-item list-group-item-action " onClick={() => { this.addLabel("Blacklist", i.Name) }}> Blacklist </button>
                                                        </div>
                                                    )
                                                    : (
                                                        null
                                                    )
                                            }
                                        </span>)
                                    }
                                </div>
                                {flagBlacklist == true ? <p className='pt-2 d-inline-block font-weight-bold font-italic'>This food contains an item you blacklisted</p> : ""}
                                <p className="d-inline-block"><span className="font-weight-bold">UPC:</span> {details.UPC}</p>
                                <p>
                                    <span className="font-weight-bold">Serving Size:</span> {details.ServingSizeFullText}
                                    {
                                        (details.ServingSizeFullText == "") ? <span> {details.ServingSize}{details.ServingSizeUnit}</span> : <span> ({details.ServingSize}{details.ServingSizeUnit})</span>
                                    }
                                </p>
                                <p className="d-inline-block"><span className="font-weight-bold">This is a {details.FodmapScore} FODMAP food</span></p>
                            </div>
                            <div className="col-md-6">
                                <div className="shadow rounded bg-gray p-4">
                                    <div id="nutritionLabel">

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}