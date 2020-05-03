package dev.tannerhill.fodfinder.ViewModels

import android.widget.Toast
import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import dev.tannerhill.fodfinder.FoodDataRepository
import dev.tannerhill.fodfinder.FoodDataService
import dev.tannerhill.fodfinder.Models.Food.FoodSearchResult
import dev.tannerhill.fodfinder.R
import kotlinx.android.synthetic.main.fragment_search.view.*

class FoodSearchViewModel : ViewModel() {
    private val foodSearchResult: MutableLiveData<FoodSearchResult> by lazy {
        MutableLiveData<FoodSearchResult>()
    }

    private val searchExpanded: MutableLiveData<Boolean> by lazy {
        MutableLiveData<Boolean>(false)
    }

    private val selectedSearchToggle: MutableLiveData<Int> by lazy {
        MutableLiveData<Int>(R.id.searchByTextButton)
    }

    fun getFoodSearchResult() : LiveData<FoodSearchResult?> {
        return foodSearchResult
    }

    fun getSearchExpanded(): LiveData<Boolean> {
        return searchExpanded
    }

    fun getSelectedSearchToggle(): LiveData<Int> {
        return selectedSearchToggle
    }

    fun setSelectedSearchToggle(selected: Int) {
        selectedSearchToggle.value = selected
    }

    fun setSearchExpanded(expanded: Boolean) {
        searchExpanded.value = expanded
    }

    fun search(query: String, onError: (t: Throwable) -> Unit ) {
        FoodDataRepository.search(query,foodSearchResult, onError)
    }
}