package dev.tannerhill.fodfinder

import dev.tannerhill.fodfinder.Models.Food.FoodDetails
import dev.tannerhill.fodfinder.Models.Food.FoodSearchResult
import retrofit2.*
import retrofit2.http.GET
import retrofit2.http.Path
import retrofit2.http.Query

interface FoodDataService {
    @GET("api/food/search")
    fun search(
        @Query("query") query: String,
        @Query("pageNumber") pageNumber: Int,
        @Query("ingredients") ingredients: String?,
        @Query("requireAllWords") requireAllWords: Boolean?
    ) : Call<FoodSearchResult>

    @GET("api/food/details")
    fun details(
        @Query("id") id: String
    ) : Call<FoodDetails>
}