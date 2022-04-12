# Cooking-Master

This program is a simple salad making game, where the goal is to make vegetable combinations by chopping them up, and serving them to your customers. 

Controls:
P1:                                                                 P2:
Move:    Interact:  Pick up combo from board:                       Move:     Interact:    Pick up combo from board:
 W        E             F                                             I          O              ;
ASD                                                                  JKL


Features:
  "Couch Co-Op" Two players can play on the same keyboard
  Customer orders are randomly generated and can be either 2 or 3 ingredients
  Players can hold two vegetables at a time, with the first vegetable picked up being the first put down
  Hi Scores are saved using an external save file with serialization
  If players serve a customer before 70% of their waiting time is crossed, a speed, time, or point powerup will spawn
  Points can be lost by losing a customer, or serving them the wrong salad
  Customers that need 3 ingredients wait for 60s and customers that need 2 ingredients wait for 45s
  Reset Button reloads the scene, but maintains the top scores
  
Future Goals:
  Adding more art to give the game a stronger visual presence
  Adding more kinds of powerups
  Adding level requirements that must be met to complete a stage
  Adding difficulty options
  Changing customer timers with animated bars
  Adding an animated bar for the duration of the chopping action
  Replacing generic vegetables with real vegetables with visuals indicating whether they have been chopped yet
