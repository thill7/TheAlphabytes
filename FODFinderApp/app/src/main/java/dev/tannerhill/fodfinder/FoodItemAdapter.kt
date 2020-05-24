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
    private val foodSearchResultItems: ArrayList<FoodSearchResultItem> = arrayListOf()
    private var foodSearchResult: FoodSearchResult? = null

    val seen: ArrayList<Int> = arrayListOf()

    fun setFoodItems(newItems: FoodSearchResult?) {
        foodSearchResult = newItems
        if(newItems?.CurrentPage == 1) {
            foodSearchResultItems.clear()
        }
        foodSearchResultItems.addAll(newItems?.Foods ?: arrayListOf())
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

        holder.itemView.setOnClickListener {
            listener.selectFoodItem(foodItem.FdcId.toString())
        }

        if(!seen.contains(foodItem.FdcId)) {
            seen.add(foodItem.FdcId)
        }

        if(seen.size == foodSearchResultItems.size && foodSearchResult!!.CurrentPage < foodSearchResult!!.TotalPages) {
            listener.paginate(foodSearchResult!!)
        }
    }

    class FoodHolder(v: View): RecyclerView.ViewHolder(v) {
        val foodItemName: TextView = v.findViewById(R.id.foodItemName)
        val foodItemBrand: TextView = v.findViewById(R.id.foodItemBrand)
        val foodItemUpc: TextView = v.findViewById(R.id.foodItemUpc)
    }

    interface FoodItemAdapterListener {
        fun selectFoodItem(id: String)
        fun paginate(foodSearchResult: FoodSearchResult)
    }
}