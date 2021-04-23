Hello there!

To be on the same page let me make brief summary of API conditions for client's code as far as I understood according to the task:

1. Start game action:
When game started client calls appropriate method of our module and passes team names. (HomeTeamName, AwayTeamName)

2. Finish game action:
When game finished client calls appropriate method of our module and passes team names. (HomeTeamName, AwayTeamName)

3. Update score action:
In any time client can call appropriate method of our module to update game score. For this purpose client passes pair of team names (HomeTeamName, AwayTeamName) and pair of new team scores (HomeTeamScore, AwayTeamScore)

4. Get a summary of games by total score:
Client calls appropriate method of our module and expects a string represents the results of all games currently present in system.
Results should be sorted by total scores (sum of both teams scores within game), in case the total scores of are equal, such games should be sorted by order the've been added into system (first - most recent). 
__________

Since client code uses pair of team names for game identification, let's accept it as main game identifying criteria.
Assuming there are impossible case within championship when two games take part with the same names of Home team and Awy team.

