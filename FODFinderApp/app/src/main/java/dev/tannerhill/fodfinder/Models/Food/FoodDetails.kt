package dev.tannerhill.fodfinder.Models.Food

import java.util.*


data class FoodDetails(
    val FdcId: Int,
    val Description: String,
    val BrandOwner: String,
    val PrimaryIngredients: List<Ingredient>,
    val SecondaryIngredients: List<Ingredient>,
    val ServingSize: Double,
    val ServingSizeUnit: String,
    val LabelNutrients: String,
    val UPC: String,
    val FodmapScore: String
)

data class Ingredient(
    val Name: String,
    val IsFodmap: Boolean,
    val Label: String,
    val IngredientPosition: Int
)

enum class Position(val value: Int) {
    Parent(0),
    LastChild(1),
    Other(2),
}