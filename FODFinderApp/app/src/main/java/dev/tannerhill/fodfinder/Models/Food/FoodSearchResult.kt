package dev.tannerhill.fodfinder.Models.Food

data class FoodSearchResult(
    val Query: String,
    val TotalHits: Int,
    val CurrentPage: Int,
    val TotalPages: Int,
    val Ingredients: String,
    val RequireAllWords: Boolean,
    val Foods: List<FoodSearchResultItem>)