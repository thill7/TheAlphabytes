package dev.tannerhill.fodfinder.Util

object TextParser {
    fun capitalize(text: String?) : String {
        if(text.isNullOrBlank()) {
            return ""
        } 
        return text.toLowerCase().split(" ").map { t -> t.capitalize() }.joinToString(" ")
    }
}