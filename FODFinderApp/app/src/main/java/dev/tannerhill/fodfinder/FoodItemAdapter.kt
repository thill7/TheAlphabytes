package dev.tannerhill.fodfinder

import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import dev.tannerhill.fodfinder.Models.Food.FoodSearchResultItem

class FoodItemAdapter : RecyclerView.Adapter<RecyclerView.ViewHolder>() {
    val foodItems: ArrayList<FoodSearchResultItem> = arrayListOf()

    fun setFoodItems(newItems: ArrayList<FoodSearchResultItem>) {
        foodItems.clear()
        foodItems.addAll(newItems)
        notifyDataSetChanged()
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): RecyclerView.ViewHolder {
        TODO("not implemented") //To change body of created functions use File | Settings | File Templates.
    }

    override fun getItemCount(): Int {
        TODO("not implemented") //To change body of created functions use File | Settings | File Templates.
    }

    override fun onBindViewHolder(holder: RecyclerView.ViewHolder, position: Int) {
        TODO("not implemented") //To change body of created functions use File | Settings | File Templates.
    }
}