package dev.tannerhill.fodfinder.Util

import android.content.Context

object Convert {
    public fun dpToPx(dp: Int, context: Context) : Int {
        return (dp * context.resources.displayMetrics.density).toInt()
    }
}