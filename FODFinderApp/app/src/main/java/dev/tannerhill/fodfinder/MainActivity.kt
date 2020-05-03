package dev.tannerhill.fodfinder

import android.content.pm.PackageManager
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.text.Layout
import android.util.Log
import android.view.*
import android.widget.ExpandableListView
import android.widget.LinearLayout
import android.widget.TableLayout
import android.widget.Toast
import androidx.activity.viewModels
import androidx.appcompat.widget.SearchView
import androidx.lifecycle.Observer
import androidx.navigation.NavController
import androidx.navigation.NavDestination
import androidx.navigation.findNavController
import androidx.navigation.ui.AppBarConfiguration
import androidx.navigation.ui.navigateUp
import androidx.navigation.ui.setupActionBarWithNavController
import androidx.navigation.ui.setupWithNavController
import com.google.android.material.navigation.NavigationView
import dev.tannerhill.fodfinder.ViewModels.FoodDetailsViewModel
import dev.tannerhill.fodfinder.ViewModels.FoodSearchViewModel
import kotlinx.android.synthetic.main.activity_main.*

class MainActivity : AppCompatActivity(), NavController.OnDestinationChangedListener, SearchView.OnQueryTextListener, View.OnClickListener, SearchView.OnCloseListener {

    private var navController: NavController? = null
    private var appBarConfiguration: AppBarConfiguration? = null
    private val foodSearchViewModel: FoodSearchViewModel by viewModels()
    private val foodDetailsViewModel: FoodDetailsViewModel by viewModels()
    private var selectedSearchToggle: Int = R.id.searchByTextButton

    private var optionsMenu: Menu? = null
    private var searchView: SearchView? = null

    private var currentMenu : Int = R.menu.home_options_menu

    private val menuMap: HashMap<Int,Int> = hashMapOf(
        R.id.search_fragment_nav to R.menu.search_options_menu,
        R.id.home_fragment_nav to R.menu.home_options_menu
    )

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        navController = findNavController(R.id.nav_host_fragment)
        navController!!.addOnDestinationChangedListener(this)

        foodSearchViewModel.getSelectedSearchToggle().observe(this, Observer {
            selectedSearchToggle = it
        })

        appBarConfiguration = AppBarConfiguration(navController!!.graph,drawer_layout)
        nav_view.setupWithNavController(navController!!)
        setupActionBarWithNavController(navController!!, appBarConfiguration!!)
    }

    override fun onSupportNavigateUp(): Boolean {

        if(appBarConfiguration != null) {
            if(navController != null && navController!!.previousBackStackEntry != null) {
                nav_view.setCheckedItem(navController!!.previousBackStackEntry!!.destination.id)
            }
            return navController!!.navigateUp(appBarConfiguration!!)
        }
        return super.onSupportNavigateUp()
    }

    override fun onDestinationChanged(controller: NavController, destination: NavDestination, arguments: Bundle?) {
        if(optionsMenu != null) {
            currentMenu = menuMap[destination.id] ?: R.menu.home_options_menu
            invalidateOptionsMenu()
        }
    }


    override fun onCreateOptionsMenu(menu: Menu?): Boolean {
        optionsMenu = menu
        return super.onCreateOptionsMenu(menu)
    }

    override fun onPrepareOptionsMenu(menu: Menu?): Boolean {
        menuInflater.inflate(currentMenu,menu)
        if(currentMenu == R.menu.search_options_menu) {
            searchView = menu?.findItem(R.id.navBarSearchView)?.actionView as SearchView
            searchView?.setOnQueryTextListener(this)
            searchView?.setOnSearchClickListener(this)
            searchView?.setOnCloseListener(this)
        }
        return super.onPrepareOptionsMenu(menu)
    }

    override fun onQueryTextSubmit(query: String?): Boolean {
        if(!query.isNullOrBlank()) {
            val fullQuery = if(selectedSearchToggle == R.id.searchByTextButton) query else "gtinUpc:*${query.trimStart('0')}"
            Log.d("Search Term",fullQuery)
            foodSearchViewModel.search(fullQuery) {
                Toast.makeText(this,"Couldn't search... Check your internet connection.", Toast.LENGTH_SHORT).show()
            }
            searchView?.onActionViewCollapsed()
        }
        foodSearchViewModel.setSearchExpanded(false)
        return true
    }

    override fun onQueryTextChange(newText: String?): Boolean {
        return true
    }

    override fun onClick(v: View?) {
        foodSearchViewModel.setSearchExpanded(true)
    }

    override fun onClose(): Boolean {
        foodSearchViewModel.setSearchExpanded(false)
        return false
    }

    override fun onRequestPermissionsResult(requestCode: Int, permissions: Array<out String>, grantResults: IntArray) {
        when(requestCode) {
            BarcodeScannerFragment.PERMISSION_CAMERA -> {
                if(grantResults.isNotEmpty() && grantResults[0] == PackageManager.PERMISSION_GRANTED) {
                    navController?.navigate(R.id.barcode_scanner_fragment_nav)
                }
            }
        }
    }
}
