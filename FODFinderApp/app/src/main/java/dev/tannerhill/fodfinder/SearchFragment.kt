package dev.tannerhill.fodfinder


import android.Manifest
import android.content.pm.PackageManager
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.*
import androidx.appcompat.app.AlertDialog
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
import com.google.android.material.dialog.MaterialAlertDialogBuilder
import dev.tannerhill.fodfinder.BarcodeScannerFragment.Companion.PERMISSION_CAMERA
import dev.tannerhill.fodfinder.Models.Food.FoodSearchOptions
import dev.tannerhill.fodfinder.Models.Food.FoodSearchResult
import dev.tannerhill.fodfinder.Util.TextParser
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

    private lateinit var includeIngredientListAdapter: IngredientOptionsListAdapter
    private lateinit var discludeIngredientListAdapter: IngredientOptionsListAdapter

    private var requireAllWords: Boolean? = null

    private var searchOptionsDialogView: View? = null
    private var searchOptionsDialog: AlertDialog? = null

    private fun requestCameraPermission() {
        ActivityCompat.requestPermissions(requireActivity(),
            arrayOf(Manifest.permission.CAMERA),
            PERMISSION_CAMERA)
    }
    private lateinit var adapter: FoodItemAdapter

    private fun foodSearchParametersDialog() {
        searchOptionsDialogView = LayoutInflater.from(requireContext()).inflate(R.layout.food_search_options,null)

        val updateFoodSearchOptionsButton: Button = searchOptionsDialogView!!.findViewById(R.id.updateFoodSearchOptionsButton)
        val addIncludeIngredientEditText: EditText = searchOptionsDialogView!!.findViewById(R.id.addIncludeIngredientEditText)
        val addDiscludeIngredientEditText: EditText = searchOptionsDialogView!!.findViewById(R.id.addDiscludeIngredientEditText)
        val includeIngredientListRecycler: RecyclerView = searchOptionsDialogView!!.findViewById(R.id.includeIngredientListRecycler)
        val discludeIngredientListRecycler: RecyclerView = searchOptionsDialogView!!.findViewById(R.id.discludeIngredientListRecycler)
        val addIncludeIngredientButton: Button = searchOptionsDialogView!!.findViewById(R.id.addIncludeIngredientButton)
        val addDiscludeIngredientButton: Button = searchOptionsDialogView!!.findViewById(R.id.addDiscludeIngredientButton)
        val requireAllWordsCheckbox: CheckBox = searchOptionsDialogView!!.findViewById(R.id.requireAllWordsCheckbox)

        requireAllWordsCheckbox.isChecked = requireAllWords ?: false

        val lLM = LinearLayoutManager(requireContext())
        val lLM2 = LinearLayoutManager(requireContext())

        includeIngredientListRecycler.layoutManager = lLM
        discludeIngredientListRecycler.layoutManager = lLM2

        includeIngredientListRecycler.adapter = includeIngredientListAdapter
        discludeIngredientListRecycler.adapter = discludeIngredientListAdapter

        addIncludeIngredientButton.setOnClickListener {
            val include = addIncludeIngredientEditText.text.toString()
            if(!include.isBlank()) {
                val currentList = arrayListOf<String>()
                currentList.addAll(includeIngredientListAdapter.ingredients)
                currentList.add(if(include.contains(" ")) "\"$include\"" else include)
                includeIngredientListAdapter.setIngredients(currentList)
            }
        }

        addDiscludeIngredientButton.setOnClickListener {
            val disclude = addDiscludeIngredientEditText.text.toString()
            if(!disclude.isBlank()) {
                val currentList = arrayListOf<String>()
                currentList.addAll(discludeIngredientListAdapter.ingredients)
                currentList.add(if(disclude.contains(" ")) "-\"$disclude\"" else "-$disclude")
                discludeIngredientListAdapter.setIngredients(currentList)
            }
        }

        updateFoodSearchOptionsButton.setOnClickListener {
            val ingredientList: ArrayList<String> = arrayListOf()
            ingredientList.addAll(includeIngredientListAdapter.ingredients)
            ingredientList.addAll(discludeIngredientListAdapter.ingredients)
            var ingredients: String? = ingredientList.joinToString(" ")
            if(ingredients.isNullOrBlank()) {
                ingredients = null
            }
            foodSearchViewModel.setSearchOptions(
                FoodSearchOptions(
                    ingredients
                    , if(!requireAllWordsCheckbox.isChecked) null else true)
            )
            searchOptionsDialog?.dismiss()
            Toast.makeText(requireContext(),"Search Options Updated",Toast.LENGTH_SHORT).show()
        }

        searchOptionsDialog = MaterialAlertDialogBuilder(requireContext())
            .setTitle("Search Parameters")
            .setView(searchOptionsDialogView)
            .create()

        searchOptionsDialog?.show()
    }

    private fun setFoodSearchOptions(foodSearchOptions: FoodSearchOptions?) {
        if(foodSearchOptions?.Ingredients != null) {
            val ingredientList = TextParser.ingredientStringToList(foodSearchOptions.Ingredients)
            includeIngredientListAdapter.setIngredients(ingredientList.filter { i -> !i.startsWith("-") })
            discludeIngredientListAdapter.setIngredients(ingredientList.filter { i -> i.startsWith("-") })
        }
        else {
            includeIngredientListAdapter.setIngredients(listOf())
            discludeIngredientListAdapter.setIngredients(listOf())
        }
        requireAllWords = foodSearchOptions?.requireAllWords
    }

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
            Log.d("Food Search",it?.toString() ?: "NULL")
            foodSearchViewModel.setSearchOptions(if(it == null) null else FoodSearchOptions(it.Ingredients,it.RequireAllWords))
        })

        foodSearchViewModel.getfoodSearchOptions().observe(viewLifecycleOwner, Observer {
            setFoodSearchOptions(it)
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

        searchOptionsButton.setOnClickListener {
            foodSearchParametersDialog()
        }

        includeIngredientListAdapter = IngredientOptionsListAdapter(requireContext())
        discludeIngredientListAdapter = IngredientOptionsListAdapter(requireContext())

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

    override fun paginate(foodSearchResult: FoodSearchResult) {
        foodSearchViewModel.search(
            foodSearchResult.Query,
            foodSearchResult.CurrentPage + 1,
            foodSearchResult.Ingredients,
            if(!foodSearchResult.RequireAllWords) null else foodSearchResult.RequireAllWords) {}
        Toast.makeText(requireContext(),"Page ${foodSearchResult.CurrentPage+1}",Toast.LENGTH_SHORT).show()
    }
}
