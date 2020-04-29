package dev.tannerhill.fodfinder

import androidx.lifecycle.MutableLiveData
import dev.tannerhill.fodfinder.Models.Food.FoodDetails
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
            .baseUrl(BuildConfig.API_DOMAIN)
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

    fun details(
        id: String,
        data: MutableLiveData<FoodDetails>,
        onError: (t: Throwable) -> Unit
    ) {
        api.details(id)
            .enqueue(object: Callback<FoodDetails> {
                override fun onFailure(call: Call<FoodDetails>, t: Throwable) {
                    onError.invoke(t)
                    data.value = null
                }

                override fun onResponse(call: Call<FoodDetails>, response: Response<FoodDetails>) {
                    data.value = response.body()
                }
            })
    }
}