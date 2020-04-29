package dev.tannerhill.fodfinder.Models.Food


data class FoodDetails(
    val FdcId: Int,
    val Description: String,
    val BrandOwner: String,
    val Ingredients: List<Ingredient>,
    val PrimaryIngredients: List<List<Ingredient>>,
    val SecondaryIngredients: List<List<Ingredient>>,
    val ServingSize: Double,
    val ServingSizeUnit: String,
    val LabelNutrients: String,
    val UPC: String,
    val FodmapScore: String
)

data class Ingredient(
    val Name: String,
    val IsFodmap: Boolean,
    val Label: String
)