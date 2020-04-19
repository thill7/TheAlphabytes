package dev.tannerhill.fodfinder

import dev.tannerhill.fodfinder.Models.Food.FoodSearchResult
import retrofit2.*
import retrofit2.http.GET
import retrofit2.http.Path
import retrofit2.http.Query

interface FoodDataService {
    @GET("api/food/search")
    fun search(
        @Query("query") query: String
    ) : Call<FoodSearchResult>
}