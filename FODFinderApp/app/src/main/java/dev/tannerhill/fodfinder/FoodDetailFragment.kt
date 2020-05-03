package dev.tannerhill.fodfinder


import android.graphics.Color
import android.os.Bundle
import android.text.Spannable
import android.text.SpannableString
import android.text.style.ForegroundColorSpan
import android.util.Log

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ExpandableListView
import android.widget.ImageView
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.Observer
import dev.tannerhill.fodfinder.Models.Food.FoodDetails
import dev.tannerhill.fodfinder.Util.TextParser
import dev.tannerhill.fodfinder.ViewModels.FoodDetailsViewModel
import kotlinx.android.synthetic.main.fragment_food_detail.*


class FoodDetailFragment : Fragment() {

    private val foodDetailsViewModel: FoodDetailsViewModel by activityViewModels()
    private lateinit var primaryIngredientListAdapter: IngredientListAdapter
    private lateinit var secondaryIngredientListAdapter: IngredientListAdapter

    private val ScoreMap: Map<String,Int> = mapOf(
        "Low" to Color.rgb(	100,149,237),
        "Medium" to Color.rgb(255,140,0),
        "High" to Color.rgb(139,0,0)
    )

    private fun setFoodDetails(foodDetails: FoodDetails) {
        Log.d("FOOD DETAILS", foodDetails.toString())
        primaryIngredientListAdapter.setIngredients(foodDetails.PrimaryIngredients)
        secondaryIngredientListAdapter.setIngredients(foodDetails.SecondaryIngredients)
        primaryIngredientsLayout.visibility = if(foodDetails.PrimaryIngredients.isEmpty()) View.GONE else View.VISIBLE
        secondaryIngredientsLayout.visibility = if(foodDetails.SecondaryIngredients.isEmpty()) View.GONE else View.VISIBLE
        foodDetailsTitle.text = TextParser.capitalize(foodDetails.Description)
        foodDetailsBrand.text = TextParser.capitalize(foodDetails.BrandOwner)
        foodDetailsUpc.text = foodDetails.UPC
        val fodmapScoreText = "Fodmap Score: ${foodDetails.FodmapScore}"
        val fodmapScoreSpan = SpannableString(fodmapScoreText)
        val colorSpanStart = fodmapScoreText.length - foodDetails.FodmapScore.length
        fodmapScoreSpan.setSpan(ForegroundColorSpan(ScoreMap[foodDetails.FodmapScore] ?: Color.GRAY), colorSpanStart, fodmapScoreText.length, Spannable.SPAN_EXCLUSIVE_EXCLUSIVE)
        foodDetailsFodmapScore.text = fodmapScoreSpan
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

        primaryIngredientListView.setOnGroupClickListener(object: ExpandableListView.OnGroupClickListener {
            override fun onGroupClick(parent: ExpandableListView?, v: View?, groupPosition: Int, id: Long): Boolean {
                val indicator: ImageView? = v?.findViewById(R.id.indicator)
                val expanded: Boolean = primaryIngredientListView.isGroupExpanded(groupPosition)
                if(primaryIngredientListAdapter.getChildrenCount(groupPosition) != 0) {
                    indicator?.setImageResource(if(expanded) R.drawable.ic_arrow_drop_down_black_24dp else R.drawable.ic_arrow_drop_up_black_24dp)
                }
                return false
            }
        })

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
