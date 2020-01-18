## IBS Food Search
This will be a free resource available to anyone pursuing a low FODMAP diet (primarily those suffering from IBS, or Irritable Bowel Syndrome). Foods can be searched by name (including brand names) and will be automatically ranked either on a continuous or discrete scale ranging from low to high FODMAP. Additionally, logged in users will be able to add ingredients that they personally find to be triggering, which will be factored into the scoring algorithm with searches. Later on, we would like to create a companion app that utilizes the backend of our web application, but adds an additional method of searching: barcode scanning. 

What’s new/original about this idea is the concept that it is entirely free-to-use, focusing on a particular dietary restriction instead of trying to act as a catch-all for several.

This idea is worth doing because an estimated 10-15 percent of Americans suffer from IBS symptoms, whereas only 3.4 percent of Americans pursue a vegetarian diet and 1 percent of health Americans suffer from celiac disease, requiring a gluten-free diet. It’s useful because the rudimentary process of trying to memorize lists of high FODMAP ingredients and then comparing this to nutrition facts labels is tedious and discourages people from sticking to the diets that keep them healthy.

A few major features include:
-	Food search (quick ajax) with immediate feedback on FODMAP warnings on results
-	Optional login with the ability to add ingredients that affect a user in particular, but may not be part of the high FODMAP ingredient list by default
-	A barcode scanner to quickly check the FODMAP potential of a food through a mobile application

For this idea, we’ll require an API that provides ingredient information and can recognize queries by UPC (barcode) values. Furthermore, we’ll require databases both to track users and to keep track of FODMAP ingredients that we will populate based on research, since there is no API for that purpose.

In terms of algorithmic content, we need to at least implement an algorithm to score foods, either on a continuous or discrete scale, from low FODMAP to high FODMAP, factoring in the decreasing quantity of ingredients by listing order, the amount of high FODMAP ingredients, etc.

This project could be rated 8 on a scale of 1 to 10. The concepts are straightforward, but it is algorithmically complex, requires multiple databases, utilizes an API, and will potentially include a separate mobile application. We have all of the tools needed to construct this, but there are several implementation decision and architecture questions to consider.

Sources: [IBS](https://gi.org/topics/irritable-bowel-syndrome/), [Vegetarianism](https://en.wikipedia.org/wiki/Vegetarianism_by_country#United_States), [Celiac Disease](https://www.cureceliacdisease.org/wp-content/uploads/341_CDCFactSheets8_FactsFigures.pdf)

