package dev.tannerhill.fodfinder

import android.content.Context
import android.graphics.Color
import android.util.AttributeSet
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.BaseExpandableListAdapter
import android.widget.ImageView
import android.widget.TextView
import androidx.core.content.ContextCompat
import dev.tannerhill.fodfinder.Models.Food.FoodDetails
import dev.tannerhill.fodfinder.Models.Food.Ingredient
import dev.tannerhill.fodfinder.Models.Food.Position
import dev.tannerhill.fodfinder.Util.Convert

class IngredientListAdapter(val context: Context): BaseExpandableListAdapter() {
    private val ingredients: ArrayList<List<Ingredient>> = arrayListOf()

    fun setIngredients(newIngredients: List<Ingredient>) {
        ingredients.clear()
        val newIngredientList: ArrayList<ArrayList<Ingredient>> = arrayListOf()
        var isSub : Boolean = false
        for(x in newIngredients.indices) {
            val ingredient = newIngredients[x]
            if(!isSub) {
                newIngredientList.add(arrayListOf(ingredient))
            }
            else {
                newIngredientList[newIngredientList.size - 1].add(ingredient)
            }
            if(ingredient.IngredientPosition == Position.Parent.value) {
                isSub = true
            }
            else if(ingredient.IngredientPosition == Position.LastChild.value) {
                isSub = false
            }
        }

        ingredients.addAll(newIngredientList)
        notifyDataSetChanged()
    }

    override fun getGroup(groupPosition: Int): Any {
        return ingredients[groupPosition][0]
    }

    override fun isChildSelectable(groupPosition: Int, childPosition: Int): Boolean {
        return true
    }

    override fun hasStableIds(): Boolean {
        return false
    }

    override fun getGroupView(groupPosition: Int, isExpanded: Boolean, convertView: View?, parent: ViewGroup?): View {
        val ingr = getGroup(groupPosition) as Ingredient
        var v: View
        if(convertView == null) {
            v = LayoutInflater.from(context).inflate(R.layout.ingredient_list_group,null)
        }
        else {
            v = convertView
        }

        val indicator: ImageView = v.findViewById(R.id.indicator)
        val titleView: TextView = v!!.findViewById(R.id.listGroupTitle)
        titleView.text = ingr.Name
        if(ingr.IsFodmap) {
            v.setBackgroundColor(Color.RED)
            titleView.setTextColor(Color.WHITE)
        }
        else {
            v.setBackgroundColor(ContextCompat.getColor(context,R.color.color_primary))
            titleView.setTextColor(ContextCompat.getColor(context,R.color.color_on_primary))
        }

        indicator.visibility = if(getChildrenCount(groupPosition) != 0) View.VISIBLE else View.GONE

        return v
    }

    override fun getChildrenCount(groupPosition: Int): Int {
        return ingredients[groupPosition].size - 1
    }

    override fun getChild(groupPosition: Int, childPosition: Int): Any {
        return ingredients[groupPosition][childPosition+1]
    }

    override fun getGroupId(groupPosition: Int): Long {
        return groupPosition.toLong()
    }

    override fun getChildView(
        groupPosition: Int,
        childPosition: Int,
        isLastChild: Boolean,
        convertView: View?,
        parent: ViewGroup?
    ): View {
        val ingr = getChild(groupPosition,childPosition) as Ingredient
        var v: View
        if(convertView == null) {
            v = LayoutInflater.from(context).inflate(R.layout.ingredient_list_item,null)
        }
        else {
            v = convertView
        }

        val titleView: TextView = v!!.findViewById(R.id.listItemTitle)
        titleView.text = ingr.Name
        if(ingr.IsFodmap) {
            v.setBackgroundColor(Color.RED)
            titleView.setTextColor(Color.WHITE)
        }
        else {
            v.setBackgroundColor(ContextCompat.getColor(context,R.color.color_secondary))
            titleView.setTextColor(ContextCompat.getColor(context,R.color.color_on_secondary))
        }

        return v
    }

    override fun getChildId(groupPosition: Int, childPosition: Int): Long {
        return childPosition.toLong()
    }

    override fun getGroupCount(): Int {
        return ingredients.size
    }
}