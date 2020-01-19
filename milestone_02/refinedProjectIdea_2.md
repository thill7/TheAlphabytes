<<<<<<< HEAD
## Household Management
This solution aims to help independent adults navigate living together in a house. Common trouble points in households with multiple people are money issues, like who is responsible for paying rent and when everyone else should have their money in by, and differing standards of etiquette particularly with cleanliness. This website would aim to ease some of these tensions by providing a platform to organize any house rules, monetary transactions, and schedules all in one place. Household members would receive reminders about when and who they need to give money to for rent and utilities, be able to request hosting events at the house or quiet nights, and have an automatically generated chore schedule. Ideally, this solution would also have an API to facilitate payments between housemates providing an easy way to keep track of who has paid and when and contain the interaction all in one place. 

Other solutions that already exist for household management tend to focus on families and don't facilitate transactions between fiscally independent members whereas our solution would be designed specifically for fiscally independent household members. Additionally there are many apps that may assist with one or another facets of household management but their scope is limited. Our solution aims to consolidate resources for household management. Why use separate apps for a shared calendar, shared documents, fiscal transactions, chore schedule, grocery lists, etc. when it could all be done in one place?

This idea is worth doing because many college students end up living in a house or apartment together and need to learn to navigate living with other people. Even after college, it is common for people to live together as a result of the discrepancy between housing costs and wages and this solution could improve people's experiences living together.

A few major features include:
-	Reminders about upcoming bills
-	An automatic chore assignment page
-	A shared calendar with ability to request hosting events
-	Storage of shared documents like a lease or housing agreements
-	An optional shared grocery list
-	A house rules page

For this solution, we will use an API that can handle monetary transactions like Paypal or Venmo. We will also use an API for the shared calendar like Google Calendar or Nylas. Nylas also has email functionality which could be used for payment reminders.

In the chore assignment feature, we would need to create an algorithm that would assign chores to people. Chores could have different levels of difficulty, urgency, and frequency. The algorithm would need to assign chores fairly and could maybe even take into account chore preferences of the household members.

On a scale of 1 to 10, this solution would be rated 7 because the features are fairly independent of each other so it would be easier to create value quickly early on without being dependent on one massive and complex feature. However, there is some amount of difficulty because it would incorporate multiple APIs and the algorithmic component's "fairness" for chore assignments might be hard to quantify or have an acceptance test for.
=======
## Household Management
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

>>>>>>> 65572d807af06a17d236a94be13e8b2ac1faaecd
