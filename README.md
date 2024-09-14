#Prisoner's Riddle
##History
This was written to disprove the presentation I saw on Veritasium's channel.

https://www.youtube.com/watch?v=iSNsgj1OCLA&t=547s

The just is that I could not believe the results, so I wrote a simulator.

The simulator proved that **they were correct after all**!

I fixed this up with a GUI so I could play with the results.

## Politically correct

Instead of all the prisoners dies if one gets it wrong, I decided this was a game with players and boxes. It's seemed more fun like that to me.

## Prisoner's Riddle / Player's Riddle

- 100 Hunderd players in a team, numbers 1 to 1000
- 100 Boxes with randomly assigned numbers, 1 to 100
- Each player can check 50 boxes for thier number
- All playuers must find their number in a box for the team to win.
- What is the best strategy?
- If each player tries 50 boxes, there is a 50% change they will get it right. Then the next player has a 50% chance, and so on.
- this strategy results in something the likelyhood of the team succes at 7.88860905E-31
-
- BUT there is another strategy represented in this code, resulting in a bit over a 30% chance the team will succeed.
- The solution is loops, better explained in the video linked above

## The code

This is currently developed in WPF with Visual Studio 2022, utilizing
.NET 8.0 I don't know if I used anything special from .Net 8, the last WPF program I wrote was 8 years ago.

## Run the program

- It defaults to 100 players, each with 50 attempts each, and runs the game 10000 times.
- when each player has 50 chances to fund their box, it generally works out that all prisoners will find their box 30% to 32% of the time
- Play with the numbers to see different results
- what if each prisoner only has 40 chances to find their box, simulation success drop to 13% to 14% 

Inline-style:
![alt text](https://github.com/rwgreene999/PrisonersRiddle/blob/main/ScreenResults.png "screen view")

## Future plans

I might bundle the code into Blazer just for kicks and grins. Also I might write this in cross platform for Windows and Android just to learn that process.
