class FoodDetails extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            details: JSON.parse(this.props.details)
        };
        this.handleclick.bind(this);
    }

    GetFoodNutrientValue(key) {
        if (key != undefined) {
            return key.value;
        }
        return "";
    }

    componentDidMount() {
        var { details } = this.state;
        var labelNutrients = JSON.parse(details.LabelNutrients);

        $('#nutritionLabel').nutritionLabel({
            allowCustomWidth: true,
            allowNoBorder: true,
            showServingUnitQuantity: true,
            itemName: details.Description,
            ingredientList: details.Ingredients,
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

    async handleclick() {
        var temp = parseInt(this.state.details.FdcId);
        var saveFood = await axios.post(`/SavedFoods/Create`, { usdaFoodID: temp });
        var result = saveFood.data;
        window.console.log(result);
        //alert(temp);
        //window.location.href = "/Home/Index";
    }

    render() {
        var { details } = this.state;
        

        return (
            <div className="pt-4">
                <div className="card bg-secondary text-gray shadow">
                    <div className="card-header">
                        <h2 className="display-4 font-weight-normal text-capitalize">{details.Description.toLowerCase()}</h2>
                        <h3 className="font-weight-light">{details.BrandOwner}</h3>
                        <button type="button" onClick={() => { this.handleclick() }} className="btn-primary text-white">Save Food</button>
                    </div>
                    <div className="card-body">
                        <div className="row">
                            <div className="col-md-6 d-inline-flex flex-column justify-content-start align-items-start">
                                <p className="text-lowercase"><span className="font-weight-bold text-capitalize">Ingredients:</span> {details.Ingredients}</p>
                                
                                <p className="d-inline-block"><span className="font-weight-bold">UPC:</span> {details.UPC}</p>
                                <p><span className="font-weight-bold">Serving Size:</span> {details.ServingSize}{details.ServingSizeUnit}</p>
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