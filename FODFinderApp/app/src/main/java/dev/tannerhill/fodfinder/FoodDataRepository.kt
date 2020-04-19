package dev.tannerhill.fodfinder

import androidx.lifecycle.MutableLiveData
import dev.tannerhill.fodfinder.Models.Food.FoodSearchResult
import retrofit2.*
import retrofit2.converter.gson.GsonConverterFactory

object FoodDataRepository {
    private val api: FoodDataService

    init {
        val retrofit = Retrofit.Builder()
            .addConverterFactory(
                GsonConverterFactory.create()
            )
            .baseUrl("http://192.168.0.17:55759/")
            .build()
        api = retrofit.create(FoodDataService::class.java)
    }

    fun search(
        query: String,
        data: MutableLiveData<FoodSearchResult>,
        onError: (t: Throwable) -> Unit
    ) {
        api.search(query)
            .enqueue(object: Callback<FoodSearchResult> {
                override fun onFailure(call: Call<FoodSearchResult>, t: Throwable) {
                    data.value = null
                }

                override fun onResponse(call: Call<FoodSearchResult>, response: Response<FoodSearchResult>) {
                    data.value = response.body()
                }
            })
    }
}