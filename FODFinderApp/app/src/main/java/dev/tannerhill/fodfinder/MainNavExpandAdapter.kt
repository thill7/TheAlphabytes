package dev.tannerhill.fodfinder

import android.content.Context
import android.database.DataSetObserver
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.BaseExpandableListAdapter
import android.widget.TextView

class MainNavExpandAdapter(val context: Context, val listData: HashMap<String,List<String>>) :
    BaseExpandableListAdapter() {

    private var mLayoutInflater : LayoutInflater? = null

    init {
        mLayoutInflater = LayoutInflater.from(context)
    }

    override fun getGroup(groupPosition: Int): Any {
        return listData.keys.elementAt(groupPosition)
    }

    override fun isChildSelectable(groupPosition: Int, childPosition: Int): Boolean {
        return true
    }

    override fun hasStableIds(): Boolean {
        return false
    }

    override fun getGroupView(groupPosition: Int, isExpanded: Boolean, convertView: View?, parent: ViewGroup?): View {
        val headerTitle = getGroup(groupPosition)
        var v = convertView
        if(v == null) {
            v = mLayoutInflater?.inflate(R.layout.menu_list_group,null)
        }

        val titleView: TextView = v!!.findViewById(R.id.menuGroupItem)
        titleView.text = headerTitle as String?

        return v
    }

    override fun getChildrenCount(groupPosition: Int): Int {
        return listData[getGroup(groupPosition)]?.size ?: 0
    }

    override fun getChild(groupPosition: Int, childPosition: Int): Any {
        return listData[getGroup(groupPosition) as String]!![childPosition]
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
        val childText = getChild(groupPosition,childPosition)
        var v = convertView
        if(v == null) {
            v = mLayoutInflater?.inflate(R.layout.menu_list_item,null)
        }

        val titleView: TextView = v!!.findViewById(R.id.menuListItem)
        titleView.text = childText as String?

        return v
    }

    override fun getChildId(groupPosition: Int, childPosition: Int): Long {
        return childPosition.toLong()
    }

    override fun getGroupCount(): Int {
        return listData.keys.size
    }
}