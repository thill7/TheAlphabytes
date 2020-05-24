package dev.tannerhill.fodfinder.Util

object TextParser {
    fun capitalize(text: String?) : String {
        if(text.isNullOrBlank()) {
            return ""
        } 
        return text.toLowerCase().split(" ").map { t -> t.capitalize() }.joinToString(" ")
    }

    fun ingredientStringToList(ingredients: String) : List<String> {
        val matcher: Regex = Regex("-?(\"\\w+[\\w+\\s+]*\\s+\\w+\"|\\w+)")
        return matcher.findAll(ingredients).map { m -> m.value }.toList()
    }

    fun listToIngredientString(ingredientList: List<String>) : String {
        return ingredientList.joinToString(" ")
    }
}