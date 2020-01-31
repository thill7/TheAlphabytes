FODMapper Project Inception
=====================================

## Summary of Our Approach to Software Development

We will take a database first approach and focus on responsive and scalable design. We will prioritize high value features and layer on detail through multiple sprints.

## Initial Vision Discussion with Stakeholders

For people suffering from IBS, FODMAP foods and aggravate their symptoms. It can be hard to tell, especially with highly processed foods, what the FODMAP content is and how likely the food is to aggravate IBS. Our product aims to help people with IBS identify which foods are safe to eat.

The product is centered around three core features:

1. There will be an algorithm that ranks the FODMAP content of a food and takes into account the amount of FODMAP ingredients as well as user specified foods.
2. By creating an account, users will be able to designate foods that aggravate their symptoms or foods that they don't have trouble with so that the algorithm can be tailored to the specific individual. The login will also permit users to save lists of what foods they can eat safely which they can export into a printable grocery list or sort by nutrition.
3. Users will be able to scan barcodes at the store to find out the FODMAP content of a food when they are at a store. This will be done through a barcode API and be incorporated into a mobile app.

## Initial Requirements Elaboration and Elicitation

### Interviews
1. *Do we care about people being able change languages on the site?*
      * Not yet, the language of the site will be English
2. *Are we assuming the user has knowledge of FODMAPs and IBS prior to using the site?* 
      * There should be information pages with basic explanations for users who don't have prior knowledge. There should also be links to other sources of information on that page.
3. *What are the important considerations when creating the algorithm?* 
      * The algorithm should incorporate ingredient amounts, and user inputted high and low risk foods.
4. *How will the website be monetized?*
      * The website will be free to use, there will be ads eventually but it is not a priority. There should also be place for users to donate money to the page/development team.

## Full Vision Statement

For people suffering from Irritable Bowel Syndrome who want to conveniently determine if the foods they are choosing to buy or consume will impact them negatively, the FODfinder website is a web-based application that will allow users to explore foods, both generic and from well-known name brands, and determine whether they are suitable for their diet. Using a proprietary algorithm, foods will be automatically ranked from low to high FODMAP content based on their ingredients, which are checked against a database containing hundreds of ingredients from well-established sources. In addition, registered users will be able to input additional ingredients that they've found to be problematic in their experience. For extra convenience, a mobile android application will also be integrated so that users can scan barcodes in store and get a quick score for foods they're unsure about. Unlike other nutritional databases, our product will specialize in just one dietary need, as opposed to other sites that cast a wide net and provide marginal support for several diets.

## List of Needs and Features


1. A clean, nice looking site that is easily navigable with a serene and professional style. Features should be easy to find and the site should have information pages for visitors who want to learn more about IBS, FODMAPs, our algorithm, etc. Visitors to the site should be able to search for foods from an external database like USDA and/or other databases and APIs. There should be a detailed page dynamically generated for each food. If a food is not in the database, there should be a notification that the food could not be found and a list of common FODMAP ingredients to look out for. Visitors should also be able to contact the development team to send feedback on features. Since the application will be entirely free to use, there should also be an area for users who have benefitted from the site to express their gratitude to the developers by sending them money. This area should say something cute and non-aggressive like "Like our website? Buy us a coffee".
2. Users will be able to log in and tag foods as being either higher or lower risk for triggering symptoms for them specifically. A logged in user will be able to make lists of foods that are safe to eat which they can then sort by major food groups, and export as a grocery list. When a user searches for a food, an algorithm will rank the food based on analysis of the ingredients pulled from the USDA database. The algorithm should factor in the amounts of high FODMAPs ingredients, as well as user specified high or low risk foods. Visitors will be able to search for a food and the algorithm will return an overall ranking for the food and a visual representation of the breakdown (for example a pie chart that is color coded) so there is some transparency in how the food is analyzed.
3. There will be a barcode scanner so that users can analyze foods while they are out shopping. It is especially important that this component of the web application has a scalable design since mobile will be the most likely platform this feature would be used on. The barcode scanned should automatically find the food in the database and use the algorithm to analyze the food returning the same results as if the user had searched for it. There will be  a companion mobile app that will emphasize features that users will be more likely to use on the go like the barcode scanner. All features and content should be easy to use on the mobile app.
4. The more users log foods into the site the "smarter" the site should become. The application should look for trends across both a large scale and an individual scale. It should identify trends in foods marked higher risk by a specific user to predict other foods which may be high risk. On the large scale, it should also be able to identify overall trends within demographics such as gender, ethnicity, and age about high risk foods. Additionally, if many users log a food as high risk, a food that currently is not considered a FODMAP, the site should "discover" this food and incorporate it into the algorithm as well as notify the developers that a new FODMAP food as been discovered.

## Initial Modeling

### Mind Map
![Mind_Map](../milestone_03/IBS_MindMap.jpg)

### Visual Representation of Ranking
![visual_rank](visual_ranking_diagram.jpg)

### Routing for a Barcode Scanner
![barcode](barcode_diagram.jpg)


## Identify Non-Functional Requirements

1. User accounts and data must be stored indefinitely.  They don't want to delete; rather, mark items as "deleted" but don't actually delete them.  They also used the word "inactive" as a synonym for deleted.
2. Passwords should not expire
3. Site should never return debug error pages.  Web server should have a custom 404 page that is cute or funny and has a link to the main index page.
5. English will be the default language.
6. All styling will be done with Bootstrap 4 when possible.
7. Pages will be designed with HTML 5.
8. Only data critical to the user experience will be stored, and no user data will be shared with outside resources.
9. In order to render the initial state of React components server side, a javascript engine is required to be installed on the web app server.

## Identify Functional Requirements (User Stories)

E: Epic  
U: User Story  
T: Task  

1. [U] As a visitor to the site I would like to see a fantastic and modern homepage that introduces me to the site and the features currently available.
   1. [T] Create starter ASP dot NET MVC 5 Web Application with Individual User Accounts and no unit test project
   2. [T] Choose CSS library (Bootstrap 3, 4, or ?) and use it for all pages
   3. [T] Create nice homepage: write initial content, customize navbar, hide links to login/register
   4. [T] Create SQL Server database locally. Hide credentials.
2. [U] As a visitor to the site I would like to be able to register an account so I will be able to access athlete statistics
   1. [T] Copy SQL schema from an existing ASP.NET Identity database and integrate it into our UP script
   2. [T] Configure web app to use our db with Identity tables in it
   3. [T] Create a user table and customize user pages to display additional data
   4. [T] Re-enable login/register links
   5. [T] Manually test register and login; user should easily be able to see that they are logged in
3. [E] As an administrator I want to be able to upload a spreadsheet of results so that new data can be added to our system
   1. [U] As an administrator, I want to have privileged, secure access to our records so that I can modify them as needed
      1. [T] Define a super-user account type with elevated privileges for accessing the database
      2. [T] Make record-modifying actions restricted to this super-user account type and and inaccessible to normal users or guests without accounts
   2. [U] As an administrator, I want to be able to add records to our system by uploading a standardly-formatted spreadsheet, so that I can save time and avoid redundancy
      1. [T] Create an action method for adding an individual record to the database
      2. [T] Create an action method that takes a .csv file and adds each row to the database
4. [U] As a visitor I want to be able to search for an athlete and then view their athlete page so I can find out more information about them
5. [U] As a visitor I want to be able to view race results for an athlete so I can see how they have performed
6. [U] As a visitor I want to be able to view PR's (personal records) for an athlete so I can see their best performances
7. [U] As a user I want to be able to view a graph of race results for a specific athlete and event, so that I can see the athlete's progress over time
8. [U] As a visitor I would like to see cute or funny web pages when errors occur, so that I don't have to worry about complex details of why a piece of the website didn't work
9. [U] As a robot I would like to be prevented from creating an account on your website so I don't ask millions of my friends to join your website and try to add comments about male enhancement drugs.
10. [U] As a coach, I would like to see predictive performances for the athletes on my team, so I can strategize for upcoming meets.
11. [U] As a coach, I would like the ability to have athletes automatically assigned to races for the upcoming meets, so that the team has the optimum chance of winning the meet.
	1. [T] Develop an algorithm to optimize athlete race assignments assuming no knowlege of opponent teams..
	2. [T] Integrate knowlege of opponent team's performances and predict their time.

## Initial Architecture Envisioning
(To be completed)

## Agile Data Modeling
(To be completed)

## Timeline and Release Plan
(To be completed)

