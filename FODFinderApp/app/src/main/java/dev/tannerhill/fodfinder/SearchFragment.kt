package dev.tannerhill.fodfinder


import android.opengl.Visibility
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.Observer
import androidx.recyclerview.widget.DividerItemDecoration
import androidx.recyclerview.widget.LinearLayoutManager
import dev.tannerhill.fodfinder.Models.Food.FoodSearchResultItem
import dev.tannerhill.fodfinder.ViewModels.FoodSearchViewModel
import kotlinx.android.synthetic.main.fragment_search.*


/**
 * A simple [Fragment] subclass.
 *
 */
class SearchFragment : Fragment() {

    private val foodSearchViewModel : FoodSearchViewModel by activityViewModels()
    private lateinit var adapter: FoodItemAdapter

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_search, container, false)
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        adapter = FoodItemAdapter(requireContext())
        val lLM = LinearLayoutManager(requireContext())
        val dividerItemDecoration = DividerItemDecoration(requireContext(), LinearLayoutManager.VERTICAL)
        foodRecycler.addItemDecoration(dividerItemDecoration)
        foodRecycler.layoutManager = lLM
        foodRecycler.adapter = adapter

        foodSearchViewModel.getFoodSearchResult().observe(viewLifecycleOwner, Observer {
            searchPlaceholderDisplay.visibility = if(it == null) View.VISIBLE else View.GONE
            adapter.setFoodItems(it?.Foods ?: listOf())
        })
    }
}
