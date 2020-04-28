package dev.tannerhill.fodfinder.Models.Food

data class FoodSearchResultItem(
    val FdcId: Int,
    val GtinUPC: String,
    val Description: String,
    val PublishedDate: String,
    val BrandOwner: String,
    val Ingredients: String)