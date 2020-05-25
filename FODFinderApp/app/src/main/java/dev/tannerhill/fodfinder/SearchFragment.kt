package dev.tannerhill.fodfinder


import android.Manifest
import android.content.pm.PackageManager
import android.opengl.Visibility
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.MotionEvent
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.core.app.ActivityCompat
import androidx.core.content.ContextCompat
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.Observer
import androidx.navigation.findNavController
import androidx.recyclerview.widget.DividerItemDecoration
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import dev.tannerhill.fodfinder.BarcodeScannerFragment.Companion.PERMISSION_CAMERA
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

    private fun requestCameraPermission() {
        ActivityCompat.requestPermissions(requireActivity(),
            arrayOf(Manifest.permission.CAMERA),
            PERMISSION_CAMERA)
    }
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
            adapter.setFoodItems(it)
        })

        foodSearchViewModel.getSelectedSearchToggle().observe(viewLifecycleOwner, Observer {
            searchToggleGroup.check(it)
            barcodeScannerButton.visibility = if(it == R.id.searchByTextButton) View.GONE else View.VISIBLE
        })

        foodSearchViewModel.getSearchExpanded().observe(viewLifecycleOwner, Observer {
            searchToggleLayout.visibility = if(it) View.VISIBLE else View.GONE
        })

        searchByTextButton.setOnClickListener {
            foodSearchViewModel.setSelectedSearchToggle(R.id.searchByTextButton)
            barcodeScannerButton.visibility = View.GONE
        }

        searchByUpcButton.setOnClickListener {
            foodSearchViewModel.setSelectedSearchToggle(R.id.searchByUpcButton)
            barcodeScannerButton.visibility = View.VISIBLE
        }

        barcodeScannerButton.setOnClickListener {
            if (ContextCompat.checkSelfPermission(requireContext(), Manifest.permission.CAMERA)
                != PackageManager.PERMISSION_GRANTED) {
                requestCameraPermission()
            }
            else {
                foodSearchViewModel.setSearchExpanded(false)
                requireActivity().findNavController(R.id.nav_host_fragment).navigate(R.id.barcode_scanner_fragment_nav)
            }
        }
    }

    override fun selectFoodItem(id: String) {
        foodDetailsViewModel.get(id)
        requireActivity().findNavController(R.id.nav_host_fragment).navigate(R.id.details_fragment_nav)
    }

    override fun paginate(query: String, page: Int) {
        foodSearchViewModel.search(query, page) {}
        Toast.makeText(requireContext(),"Page $page",Toast.LENGTH_SHORT).show()
    }
}
