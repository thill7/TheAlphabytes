package dev.tannerhill.fodfinder

import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import dev.tannerhill.fodfinder.Models.Food.FoodSearchResult
import dev.tannerhill.fodfinder.Models.Food.FoodSearchResultItem
import dev.tannerhill.fodfinder.Util.TextParser
import org.w3c.dom.Text

class FoodItemAdapter(val context: Context, val listener: FoodItemAdapterListener) : RecyclerView.Adapter<FoodItemAdapter.FoodHolder>() {
    var foodSearchResult: FoodSearchResult? = null

    val foodSearchResultItems: ArrayList<FoodSearchResultItem> = arrayListOf()

    val seen: ArrayList<Int> = arrayListOf()

    fun setFoodItems(newSearch: FoodSearchResult?) {
        foodSearchResult = newSearch
        if(foodSearchResult == null) {
            foodSearchResultItems.clear()
            seen.clear()
        }
        else if(foodSearchResult!!.CurrentPage != 1) {
            foodSearchResultItems.addAll(foodSearchResult!!.Foods)
        }
        else {
            foodSearchResultItems.clear()
            foodSearchResultItems.addAll(foodSearchResult!!.Foods)
        }
        notifyDataSetChanged()
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): FoodHolder {
        return FoodHolder(LayoutInflater.from(context).inflate(R.layout.food_item_result,parent,false))
    }

    override fun getItemCount(): Int {
        return foodSearchResultItems.size
    }

    override fun onBindViewHolder(holder: FoodHolder, position: Int) {
        val foodItem = foodSearchResultItems[position]
        holder.foodItemName.text = TextParser.capitalize(foodItem.Description)
        holder.foodItemBrand.text = TextParser.capitalize(foodItem.BrandOwner)
        holder.foodItemUpc.text = foodItem.GtinUPC

        if(!seen.contains(foodItem.FdcId)) {
            seen.add(foodItem.FdcId)
        }

        holder.itemView.setOnClickListener {
            listener.selectFoodItem(foodItem.FdcId.toString())
        }

        if(seen.size == foodSearchResultItems.size && foodSearchResult!!.CurrentPage < foodSearchResult!!.TotalPages) {
            listener.paginate(foodSearchResult!!.Query, foodSearchResult!!.CurrentPage + 1)
        }
    }

    class FoodHolder(v: View): RecyclerView.ViewHolder(v) {
        val foodItemName: TextView = v.findViewById(R.id.foodItemName)
        val foodItemBrand: TextView = v.findViewById(R.id.foodItemBrand)
        val foodItemUpc: TextView = v.findViewById(R.id.foodItemUpc)
    }

    interface FoodItemAdapterListener {
        fun selectFoodItem(id: String)
        fun paginate(query: String, page: Int)
    }
}