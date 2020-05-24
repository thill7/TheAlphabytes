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
<<<<<<< Updated upstream
        @Query("query") query: String
=======
        @Query("query") query: String,
        @Query("pageNumber") pageNumber: Int,
        @Query("ingredients") ingredients: String?,
        @Query("requireAllWords") requireAllWords: Boolean?
>>>>>>> Stashed changes
    ) : Call<FoodSearchResult>

    @GET("api/food/details")
    fun details(
        @Query("id") id: String
    ) : Call<FoodDetails>
}