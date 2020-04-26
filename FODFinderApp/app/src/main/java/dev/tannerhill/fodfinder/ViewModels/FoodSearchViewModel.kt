package dev.tannerhill.fodfinder.ViewModels

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import dev.tannerhill.fodfinder.FoodDataRepository
import dev.tannerhill.fodfinder.FoodDataService
import dev.tannerhill.fodfinder.Models.Food.FoodSearchResult

class FoodSearchViewModel : ViewModel() {
    private val foodSearchResult: MutableLiveData<FoodSearchResult> by lazy {
        MutableLiveData<FoodSearchResult>()
    }

    fun getFoodSearchResult() : LiveData<FoodSearchResult?> {
        return foodSearchResult
    }

    fun search(query: String) {
        FoodDataRepository.search(query,foodSearchResult) { t->
        }
    }
}