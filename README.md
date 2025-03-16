![image](https://github.com/user-attachments/assets/f0de6a80-a2c7-4e12-b1fc-b3149ba4da20)



# Challenge-project-Create-methods-in-CSharp

Starter and Final code for the Challenge project: "Create methods C# console applications" from the Microsoft Learn collection "Getting started with C#"


# Terminal Mini-Game

A simple console-based game where you move a character across the screen to consume food. The food changes the player's state, affecting movement speed and behavior.

## Features
- Move the player using arrow keys.
- Different food types alter the player's state.
- Some foods grant speed boosts, while others cause a temporary freeze.
- The game regenerates food at a random location after consumption.
- The game exits if the terminal is resized or the `Escape` key is pressed.

## Controls
- `Arrow Keys` → Move the player
- `Escape` → Quit the game

## How It Works
1. The player moves across the terminal.
2. When the player reaches food, their state changes.
3. Some food speeds up movement, while others freeze the player.
4. A new food item appears after consumption.
5. The game ends if the terminal is resized.

## Requirements
- .NET SDK installed to run C# programs.

## Run the Game
Compile and run using:
```sh
dotnet run
