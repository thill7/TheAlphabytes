package dev.tannerhill.fodfinder


import android.os.Bundle

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.Observer
import dev.tannerhill.fodfinder.Models.Food.FoodDetails
import dev.tannerhill.fodfinder.ViewModels.FoodDetailsViewModel
import kotlinx.android.synthetic.main.fragment_food_detail.*


class FoodDetailFragment : Fragment() {

    private val foodDetailsViewModel: FoodDetailsViewModel by activityViewModels()
    private lateinit var primaryIngredientListAdapter: IngredientListAdapter
    private lateinit var secondaryIngredientListAdapter: IngredientListAdapter

    private fun setFoodDetails(foodDetails: FoodDetails) {
        primaryIngredientListAdapter.setIngredients(foodDetails.PrimaryIngredients)
        secondaryIngredientListAdapter.setIngredients(foodDetails.SecondaryIngredients)
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_food_detail, container, false)
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)
        primaryIngredientListAdapter = IngredientListAdapter(requireContext())
        secondaryIngredientListAdapter = IngredientListAdapter(requireContext())

        primaryIngredientListView.setAdapter(primaryIngredientListAdapter)
        secondaryIngredientListView.setAdapter(secondaryIngredientListAdapter)

        foodDetailsViewModel.getFoodDetails().observe(this, Observer {
            loadingScreen?.visibility = if(it == null) View.VISIBLE else View.GONE
            if(it != null) {
                setFoodDetails(it)
            }
        })
    }
}
