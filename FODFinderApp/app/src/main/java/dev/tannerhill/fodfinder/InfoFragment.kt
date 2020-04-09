package dev.tannerhill.fodfinder


import android.icu.text.AlphabeticIndex
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import com.google.android.material.tabs.TabLayoutMediator
import kotlinx.android.synthetic.main.fragment_info.*

class InfoFragment : Fragment() {
    private lateinit var adapter: InfoPagerAdapter

    private lateinit var fodmapLabel : String

    private lateinit var ibsLabel : String

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_info, container, false)
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        fodmapLabel = context!!.resources.getString(R.string.label_nav_ibs_info)
        ibsLabel = context!!.resources.getString(R.string.label_nav_fodmap_info)

        adapter = InfoPagerAdapter(this,listOf(IbsInfoFragment(),FodmapInfoFragment()))
        infoPager.adapter = adapter

        TabLayoutMediator(tab_layout,infoPager) { tab, position ->
            tab.text = if(position == 1) ibsLabel else fodmapLabel
        }.attach()
    }
}
