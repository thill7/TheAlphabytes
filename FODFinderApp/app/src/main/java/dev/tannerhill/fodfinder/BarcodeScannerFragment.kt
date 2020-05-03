package dev.tannerhill.fodfinder

import android.Manifest
import android.app.Activity
import android.content.Context
import android.content.pm.PackageManager
import android.os.Bundle
import android.view.LayoutInflater
import android.view.SurfaceHolder
import android.view.View
import android.view.ViewGroup
import androidx.core.app.ActivityCompat
import androidx.core.content.ContextCompat
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.navigation.findNavController
import com.google.android.gms.vision.CameraSource
import com.google.android.gms.vision.Detector
import com.google.android.gms.vision.barcode.Barcode
import com.google.android.gms.vision.barcode.BarcodeDetector
import dev.tannerhill.fodfinder.ViewModels.FoodSearchViewModel
import kotlinx.android.synthetic.main.fragment_barcode_scanner.*

class BarcodeScannerFragment : Fragment() {

    private val foodSearchViewModel: FoodSearchViewModel by activityViewModels()
    private var barcodeDetector: BarcodeDetector? = null
    private var cameraSource: CameraSource? = null
    private val TAG = "BARCODE SCANNER FRAGMENT"
    private var detected = false

    companion object {
        const val PERMISSION_CAMERA = 1
    }
    private fun requestCameraPermission() {
        ActivityCompat.requestPermissions(requireActivity(),
            arrayOf(Manifest.permission.CAMERA),
            PERMISSION_CAMERA)
        requireActivity().findNavController(R.id.nav_host_fragment).navigateUp()
    }

    private val detectorProcessor = object: Detector.Processor<Barcode> {
        override fun release() {

        }

        override fun receiveDetections(p0: Detector.Detections<Barcode>?) {
            val barcodes = p0?.detectedItems
            if(barcodes != null && barcodes.size() > 0) {
                if(barcodes.valueAt(0) !== null && !detected) {
                    detected = true
                    searchForUPC(barcodes.valueAt(0).rawValue)
                }
            }
        }
    }

    private val surfaceHolderCallback = object: SurfaceHolder.Callback {
        override fun surfaceChanged(holder: SurfaceHolder?, format: Int, width: Int, height: Int) {

        }

        override fun surfaceDestroyed(holder: SurfaceHolder?) {
            cameraSource?.stop()
        }

        override fun surfaceCreated(holder: SurfaceHolder?) {
            if (ContextCompat.checkSelfPermission(context!!, Manifest.permission.CAMERA)
                != PackageManager.PERMISSION_GRANTED) {
                requestCameraPermission()
            }
            else {
                cameraSource?.start(holder)
            }
        }
    }

    override fun onCreate(savedInstanceState: Bundle?) {

        super.onCreate(savedInstanceState)
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_barcode_scanner, container, false)
    }

    override fun onAttach(context: Context) {
        super.onAttach(context)
    }

    override fun onDetach() {
        super.onDetach()
    }

    override fun onStop() {
        barcodeScannerSurface.holder.surface.release()
        super.onStop()
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        barcodeDetector = BarcodeDetector.Builder(requireContext().applicationContext)
            .setBarcodeFormats(Barcode.ALL_FORMATS)
            .build()

        barcodeDetector?.setProcessor(detectorProcessor)

        cameraSource = CameraSource.Builder(requireContext().applicationContext,barcodeDetector)
            .setFacing(CameraSource.CAMERA_FACING_BACK)
            .setAutoFocusEnabled(true)
            .build()

        barcodeScannerSurface.holder.addCallback(surfaceHolderCallback)
    }

    private fun searchForUPC(upc: String) {
        foodSearchViewModel.search("gtinUpc:*${upc.trimStart('0')}") {}
        requireActivity().findNavController(R.id.nav_host_fragment).navigateUp()
    }
}
