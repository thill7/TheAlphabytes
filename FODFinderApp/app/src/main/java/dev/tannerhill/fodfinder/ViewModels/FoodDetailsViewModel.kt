package dev.tannerhill.fodfinder.ViewModels

import android.util.Log
import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import dev.tannerhill.fodfinder.FoodDataRepository
import dev.tannerhill.fodfinder.FoodDataService
import dev.tannerhill.fodfinder.Models.Food.FoodDetails

class FoodDetailsViewModel: ViewModel() {
    private val TAG: String = "FOOD DETAILS VIEW MODEL"

    private val foodDetails: MutableLiveData<FoodDetails> by lazy {
        MutableLiveData<FoodDetails>()
    }

    fun getFoodDetails() : LiveData<FoodDetails> {
        return this.foodDetails
    }

    fun get(fdcId: String) {
        foodDetails.value = null
        FoodDataRepository.details(fdcId,foodDetails) {
            Log.d(TAG, it.message ?: "ERROR")
        }
    }
}