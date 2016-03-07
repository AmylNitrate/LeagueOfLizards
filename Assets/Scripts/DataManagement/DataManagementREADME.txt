Data Management README

1. Apps must be signed and linked on Google Play in order to activate the Google Play Services. 
	- must create a keystore for each app
2. Player Entity and Quiz Entity will store a list of all the different variables we want to record about 
the player and on the quizzes specifically for each game. They need to have the name of their tables changed depending on what game it is to match the name of the table for that game

3. TableManager holds functions for creating, updating and deleting this information from the online database. 

4. TableManager functions may need to be tailored for each game depending on the required functionality.
i.e. Create and update functions can be tailored to specific data that we want to save. 

5. App ID needs to be changed in InitializePlayer to be the same as the app ID on Amazon.

