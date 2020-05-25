package dev.tannerhill.fodfinder

import android.content.Context
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView

class IngredientOptionsListAdapter(val context: Context) : RecyclerView.Adapter<IngredientOptionsListAdapter.FoodHolder>()  {
    val ingredients: ArrayList<String> = arrayListOf()

    fun setIngredients(newIngredients: List<String>) {
        ingredients.clear()
        ingredients.addAll(newIngredients)
        notifyDataSetChanged()
        Log.d("INGREDIENTS",ingredients.toString())
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): FoodHolder {
        return FoodHolder(LayoutInflater.from(context).inflate(R.layout.ingredient_list_item,null))
    }

    override fun getItemCount(): Int {
        return ingredients.size
    }

    override fun onBindViewHolder(holder: FoodHolder, position: Int) {
        val ingredient = ingredients[position].removePrefix("-").removeSurrounding("\"")
        holder.listItemTitle.text = ingredient
    }

    class FoodHolder(v: View): RecyclerView.ViewHolder(v) {
        val listItemTitle: TextView = v.findViewById(R.id.listItemTitle)
    }
}