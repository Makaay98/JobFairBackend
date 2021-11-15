# JobFairBackend
Program starts with collection of clubs.
That collection is then sorted and a function that will select club pairs is called.
That function select every club(from top to bottom, two by two) from sorted array, check if they are in acceptable skill level(gap) and pair them together.
Then "The Function" returns list of paired clubs and list of leftover clubs . 
In the next sequence, the user can choose to add additional clubs and call "The Function" again.
That sequence, can be repeated infinite amount of times.

Nikola Filipovic 

Notes:
"The Function" == SkillBasedMatchmakeing()
