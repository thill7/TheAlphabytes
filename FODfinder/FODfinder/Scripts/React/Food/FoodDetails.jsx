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
            ingredientsList += list[i][0].Name;
            if (list[i].length > 1) {
                ingredientsList += " (";
                for (var j = 1; j < list[i].length; j++) {
                    if (j == list[i].length - 1) {
                        ingredientsList += list[i][j].Name
                    }
                    else {
                        ingredientsList += list[i][j].Name + ", ";
                    }
                }
                if (i != list.length - 1) {
                    ingredientsList += "), ";
                }
                else {
                    ingredientsList += ")";
                }
            }
            else if (i != list.length - 1) {
                ingredientsList += ", ";
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
    

/*(details.PrimaryIngredients.map(i => i.map(j => j.Name).join(", ")).join(", ")).concat("contains 2% or less of: ", details.SecondaryIngredients.map(i => i.map(j => j.Name).join(", ")).join(", "), "."),*/

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
        var saveFood = await axios.post(`/api/savedfoods/create`, { usdaFoodID: id, listID: listID, brandOwner: brand, description: desc, upc: barcode });
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
        var result = await axios.get(`/api/userlists/get`);
        console.log(result);
        if (result.data.success === true) {
            this.state.details.userLists = result.data.lists;
        }
        this.showUserLists(event);
    }

   /* ingredientStatus(name) {
        var statusOfIngredient = await axios.post(`FODMAPIngredients/GetLabel`, { ingredient: name });
        var statusResult = statusOfIngredient.data;
        var label = statusResult.ingredientLabel;
        return label;
    }*/

    showLabels(event, index) {
        event.preventDefault();

        this.setState({ ingredientId: index });
        this.setState({ showLabels: true }, () => {
            document.addEventListener('click', this.hideLabels);
        });
    }

    hideLabels(event) {
        if (!this.dropdownMenu.contains(event.target)) {
            this.setState({ showLabels: false }, () => {
                document.removeEventListener('click', this.hideLabels);
            });
        }
        this.setState({ ingredientId: null });
    }

    async addLabel(label, ingredient) {
        var id = parseInt(this.state.details.FdcId);
        var saveLabel = await axios.post(`/api/fodmapingredients/create`, { assignLabel: label, ingredientName: ingredient });
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
        var primaryCount = details.PrimaryIngredients.length - 1;
        var secondaryCount = details.SecondaryIngredients.length - 1;
        var { flagBlacklist } = 0;

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
                                <div className="text-lowercase">
                                    <span className="font-weight-bold text-capitalize">Ingredients:&nbsp;</span>
                                    {
                                        details.PrimaryIngredients.map((i, iCount) => i.map((j, jCount, jKey = (iCount + "-primary-inner-span-" + jCount + "-" + Math.floor(Math.random() * 10000))) =>
                                            <span key={"primary-outer-span-" + iCount + "-" + Math.floor(Math.random() * 10000)}>
                                                {jCount == 1 ? "(" : ""}
                                                <span key={jKey} onLoad={j.Label == "Blacklist" ? flagBlacklist = 1 : null} onClick={(e) => { this.showLabels(e, jKey) }} className={"p2 cursor-pointer rounded " + j.Label + (j.IsFodmap ? " bg-danger-50 text-white rounded px-1" : "")}>
                                                    {j.Name}
                                                </span>
                                                {jCount != (i.length - 1) ? "" : i.length > 1 ? ")" : ""}
                                                {iCount == primaryCount ? (secondaryCount != 0 ? ", " : " ") : i.length > 1 ? (jCount == 0 ? " " : ", ") : ", "}
                                                {
                                                    this.state.showLabels && this.state.ingredientId == jKey
                                                        ? (
                                                            <div id="label" className="labels list-group"
                                                                ref={(element) => {
                                                                    this.dropdownMenu = element;
                                                                }}>
                                                                <button className="list-group-item list-group-item-dark">{j.Name}</button>
                                                                <button className="list-group-item list-group-item-action " onClick={() => { this.addLabel("High-Risk", j.Name) }}> High Risk </button>
                                                                <button className="list-group-item list-group-item-action " onClick={() => { this.addLabel("Low-Risk", j.Name) }}> Low Risk </button>
                                                                <button className="list-group-item list-group-item-action " onClick={() => { this.addLabel("Blacklist", j.Name) }}> Blacklist </button>
                                                            </div>
                                                        )
                                                        : (
                                                            null
                                                        )
                                                }
                                            </span>))
                                    }
                                    {
                                        details.SecondaryIngredients.length > 0 &&
                                        <span className="font-weight-bold">contains 2% or less of:&nbsp;</span>
                                    }
                                    {
                                        details.SecondaryIngredients.map((i, iCount) => i.map((j, jCount, jKey = (iCount + "-secondary-inner-span-" + jCount + "-" + Math.floor(Math.random() * 10000))) =>
                                            <span key={"secondary-outer-span-" + i.Count + "-" + Math.floor(Math.random() * 10000)}>
                                                {jCount == 1 ? "(" : ""}
                                                <span key={jKey} onLoad={j.Label == "Blacklist" ? flagBlacklist = 1 : null} onClick={(e) => { this.showLabels(e, jKey) }} className={"p2 cursor-pointer rounded " + j.Label + (j.IsFodmap ? " bg-danger-50 text-white rounded px-1" : "")}>
                                                    {j.Name}
                                                </span>
                                                {jCount != (i.length - 1) ? "" : i.length > 1 ? ")" : ""}
                                                {iCount == secondaryCount ? " " : i.length > 1 ? (jCount == 0 ? " " : ", ") : ", "}
                                                {
                                                    this.state.showLabels && this.state.ingredientId == jKey
                                                        ? (
                                                            <div id="label" className="labels list-group"
                                                                ref={(element) => {
                                                                    this.dropdownMenu = element;
                                                                }}>
                                                                <button className="list-group-item list-group-item-dark">{j.Name}</button>
                                                                <button className="list-group-item list-group-item-action " onClick={() => { this.addLabel("High-Risk", j.Name) }}> High Risk </button>
                                                                <button className="list-group-item list-group-item-action " onClick={() => { this.addLabel("Low-Risk", j.Name) }}> Low Risk </button>
                                                                <button className="list-group-item list-group-item-action " onClick={() => { this.addLabel("Blacklist", j.Name) }}> Blacklist </button>
                                                            </div>
                                                        )
                                                        : (
                                                            null
                                                        )
                                                }
                                            </span>))
                                    }
                                    {/*details.Ingredients.map((i, index) => <span><span key={index} onLoad={i.Label == "Blacklist" ? flagBlacklist = 1 : ""} onClick={(e) => { this.showLabels(e, index) }} className={"p2 cursor-pointer rounded " + i.Label + (i.IsFodmap ? " bg-danger-50 text-white rounded" : "")}>{i.Name}</span>{
                                            this.state.showLabels && this.state.ingredientId == index
                                                ? (
                                                    <div id = "label" className="labels list-group"
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
                                        }{index < details.Ingredients.length - 1 ? ", " : ""}</span>)
                                    }*/}
                                </div>
                                {flagBlacklist == 1 ? <p className='pt-2 d-inline-block font-weight-bold font-italic'>This food contains an item you blacklisted</p> : ""}
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