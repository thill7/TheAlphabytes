package dev.tannerhill.fodfinder


import android.opengl.Visibility
import android.os.Bundle
import android.view.LayoutInflater
import android.view.MotionEvent
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.Observer
import androidx.navigation.findNavController
import androidx.recyclerview.widget.DividerItemDecoration
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import dev.tannerhill.fodfinder.Models.Food.FoodSearchResultItem
import dev.tannerhill.fodfinder.ViewModels.FoodDetailsViewModel
import dev.tannerhill.fodfinder.ViewModels.FoodSearchViewModel
import kotlinx.android.synthetic.main.fragment_search.*


/**
 * A simple [Fragment] subclass.
 *
 */
class SearchFragment : Fragment(), FoodItemAdapter.FoodItemAdapterListener {

    private val foodSearchViewModel : FoodSearchViewModel by activityViewModels()
    private val foodDetailsViewModel : FoodDetailsViewModel by activityViewModels()
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

        adapter = FoodItemAdapter(requireContext(),this)
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

    override fun selectFoodItem(id: String) {
        foodDetailsViewModel.get(id)
        requireActivity().findNavController(R.id.nav_host_fragment).navigate(R.id.details_fragment_nav)
    }
}
