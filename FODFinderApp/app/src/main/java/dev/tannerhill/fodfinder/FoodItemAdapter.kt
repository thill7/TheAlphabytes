package dev.tannerhill.fodfinder

import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import dev.tannerhill.fodfinder.Models.Food.FoodSearchResultItem
import dev.tannerhill.fodfinder.Util.TextParser
import org.w3c.dom.Text

class FoodItemAdapter(val context: Context) : RecyclerView.Adapter<FoodItemAdapter.FoodHolder>() {
    val foodItems: ArrayList<FoodSearchResultItem> = arrayListOf()

    fun setFoodItems(newItems: List<FoodSearchResultItem>) {
        foodItems.clear()
        foodItems.addAll(newItems)
        notifyDataSetChanged()
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): FoodHolder {
        return FoodHolder(LayoutInflater.from(context).inflate(R.layout.food_item_result,parent,false))
    }

    override fun getItemCount(): Int {
        return foodItems.size
    }

    override fun onBindViewHolder(holder: FoodHolder, position: Int) {
        val foodItem = foodItems[position]
        holder.foodItemName.text = TextParser.capitalize(foodItem.Description)
        holder.foodItemBrand.text = TextParser.capitalize(foodItem.BrandOwner)
        holder.foodItemUpc.text = foodItem.GtinUPC
    }

    class FoodHolder(v: View): RecyclerView.ViewHolder(v) {
        val foodItemName: TextView = v.findViewById(R.id.foodItemName)
        val foodItemBrand: TextView = v.findViewById(R.id.foodItemBrand)
        val foodItemUpc: TextView = v.findViewById(R.id.foodItemUpc)
    }
}