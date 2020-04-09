package dev.tannerhill.fodfinder

import androidx.fragment.app.Fragment
import androidx.viewpager2.adapter.FragmentStateAdapter

class InfoPagerAdapter(val contextFragment: Fragment, val pages: List<Fragment>) : FragmentStateAdapter(contextFragment) {


    override fun getItemCount(): Int {
        return pages.size
    }

    override fun createFragment(position: Int): Fragment {
        return pages[position]
    }
}