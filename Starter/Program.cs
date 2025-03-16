using System;

/*- The code declares the following variables:
    - Variables to determine the size of the Terminal window.
    - Variables to track the locations of the player and food.
    - Arrays `states` and `foods` to provide available player and food appearances
    - Variables to track the current player and food appearance

- The code provides the following methods:
    - A method to determine if the Terminal window was resized.
    - A method to display a random food appearance at a random location.
    - A method that changes the player appearance to match the food consumed.
    - A method that temporarily freezes the player movement.
    - A method that moves the player according to directional input.
    - A method that sets up the initial game state.

- The code doesn't call the methods correctly to make the game playable. The following features are missing:
    - Code to determine if the player has consumed the food displayed.
    - Code to determine if the food consumed should freeze player movement.
    - Code to determine if the food consumed should increase player movement.
    - Code to increase movement speed.
    - Code to redisplay the food after it's consumed by the player.
    - Code to terminate execution if an unsupported key is entered.
    - Code to terminate execution if the terminal was resized.*/

Random random = new Random();
Console.CursorVisible = false;
int height = Console.WindowHeight - 1;
int width = Console.WindowWidth - 5;
bool shouldExit = false;
bool speedBoostEnabled = true; // Option to enable/disable speed boost

// Console position of the player
int playerX = 0;
int playerY = 0;

// Console position of the food
int foodX = 0;
int foodY = 0;

// Available player and food strings
string[] states = {"('-')", "(^-^)", "(X_X)"};
string[] foods = {"@@@@@", "$$$$$", "#####"};

// Current player string displayed in the Console
string player = states[0];

// Index of the current food
int food = 0;

InitializeGame();
while (!shouldExit) 
{
    if (TerminalResized()) 
    {
        Console.Clear();
        Console.WriteLine("Console was resized. Program exiting.");
        shouldExit = true;
        break;
    }
    
    if (ShouldFreezePlayer()) 
    {
        FreezePlayer();
    }
    
    Move(true);
    
    if (PlayerConsumedFood()) 
    {
        ChangePlayer();
        ShowFood();
    }
}

// Returns true if the Terminal was resized 
bool TerminalResized() 
{
    return height != Console.WindowHeight - 1 || width != Console.WindowWidth - 5;
}

// Displays random food at a random location
void ShowFood() 
{
    food = random.Next(0, foods.Length);
    foodX = random.Next(0, width - player.Length);
    foodY = random.Next(0, height - 1);
    Console.SetCursorPosition(foodX, foodY);
    Console.Write(foods[food]);
}

// Changes the player to match the food consumed
void ChangePlayer() 
{
    player = states[food];
    Console.SetCursorPosition(playerX, playerY);
    Console.Write(player);
}

// Temporarily stops the player from moving if frozen
void FreezePlayer() 
{
    System.Threading.Thread.Sleep(1000);
    player = states[0];
}

// Checks if the player should freeze
bool ShouldFreezePlayer()
{
    return player == "(X_X)";
}

// Checks if the player should move faster
bool ShouldSpeedBoost()
{
    return speedBoostEnabled && player == "(^-^)";
}

// Reads directional input from the Console and moves the player
void Move(bool allowExitOnOtherKeys = false) 
{
    int lastX = playerX;
    int lastY = playerY;
    
    int speed = ShouldSpeedBoost() ? 3 : 1;
    
    ConsoleKey key = Console.ReadKey(true).Key;
    
    switch (key) 
    {
        case ConsoleKey.UpArrow:
            playerY--; 
            break;
        case ConsoleKey.DownArrow: 
            playerY++; 
            break;
        case ConsoleKey.LeftArrow:  
            playerX -= speed; 
            break;
        case ConsoleKey.RightArrow: 
            playerX += speed; 
            break;
        case ConsoleKey.Escape:     
            shouldExit = true; 
            break;
        default:
            if (allowExitOnOtherKeys) 
            {
                shouldExit = true;
            }
            break;
    }

    // Clear the previous position
    Console.SetCursorPosition(lastX, lastY);
    for (int i = 0; i < player.Length; i++) 
    {
        Console.Write(" ");
    }

    // Keep player within bounds
    playerX = Math.Clamp(playerX, 0, width);
    playerY = Math.Clamp(playerY, 0, height);

    // Draw the player
    Console.SetCursorPosition(playerX, playerY);
    Console.Write(player);
}

// Determines if the player has consumed the food
bool PlayerConsumedFood() 
{
    return playerX == foodX && playerY == foodY;
}

// Clears the console, displays the food and player
void InitializeGame() 
{
    Console.Clear();
    ShowFood();
    Console.SetCursorPosition(0, 0);
    Console.Write(player);
}