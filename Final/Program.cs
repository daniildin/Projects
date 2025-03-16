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
    - Code to terminate execution if the terminal was resized.
*/

Random random = new Random();
Console.CursorVisible = false;
int height = Console.WindowHeight - 1;
int width = Console.WindowWidth - 5;
bool shouldExit = false;
bool speedBoostEnabled = true; // I can toggle this to enable or disable speed boost

// My player's position in the console
int playerX = 0;
int playerY = 0;

// The food's position in the console
int foodX = 0;
int foodY = 0;

// Different player and food appearances
string[] states = {"('-')", "(^-^)", "(X_X)"};
string[] foods = {"@@@@@", "$$$$$", "#####"};

// My player's current appearance
string player = states[0];

// The current type of food displayed
int food = 0;

InitializeGame(); // Setting up the game
while (!shouldExit) 
{
    if (TerminalResized()) 
    {
        Console.Clear();
        Console.WriteLine("Console was resized. Program exiting.");
        shouldExit = true;
        break;
    }
    
    if (ShouldFreezePlayer()) // If I ate the bad food, I freeze
    {
        FreezePlayer();
    }
    
    Move(true); // Move my player based on input
    
    if (PlayerConsumedFood()) // Check if I ate food
    {
        ChangePlayer(); // Change my appearance
        ShowFood(); // Display a new food item
    }
}

// Checks if the terminal size changed
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

// Changes my player's appearance when I eat food
void ChangePlayer() 
{
    player = states[food];
    Console.SetCursorPosition(playerX, playerY);
    Console.Write(player);
}

// Temporarily stops me from moving if I ate bad food
void FreezePlayer() 
{
    System.Threading.Thread.Sleep(1000); // Wait for 1 second
    player = states[0]; // Reset my appearance
}

// Checks if I should be frozen
bool ShouldFreezePlayer()
{
    return player == "(X_X)"; // If I'm sick, I freeze
}

// Checks if I should move faster
bool ShouldSpeedBoost()
{
    return speedBoostEnabled && player == "(^-^)"; // If I'm happy and speed boost is on, I move faster
}

// Moves me around based on key input
void Move(bool allowExitOnOtherKeys = false) 
{
    int lastX = playerX;
    int lastY = playerY;
    
    int speed = ShouldSpeedBoost() ? 3 : 1; // If I have a boost, I move faster
    
    ConsoleKey key = Console.ReadKey(true).Key;
    
    switch (key) 
    {
        case ConsoleKey.UpArrow:
            playerY--; // Move up
            break;
        case ConsoleKey.DownArrow: 
            playerY++; // Move down
            break;
        case ConsoleKey.LeftArrow:  
            playerX -= speed; // Move left
            break;
        case ConsoleKey.RightArrow: 
            playerX += speed; // Move right
            break;
        case ConsoleKey.Escape:     
            shouldExit = true; // Quit game
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

    // Keep my player within the game boundaries
    playerX = Math.Clamp(playerX, 0, width);
    playerY = Math.Clamp(playerY, 0, height);

    // Draw my player at the new location
    Console.SetCursorPosition(playerX, playerY);
    Console.Write(player);
}

// Checks if I ate the food
bool PlayerConsumedFood() 
{
    return playerX == foodX && playerY == foodY;
}

// Sets up the game at the beginning
void InitializeGame() 
{
    Console.Clear(); // Clear screen
    ShowFood(); // Display food
    Console.SetCursorPosition(0, 0);
    Console.Write(player); // Show my player at the start position
}
