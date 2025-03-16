using System;

Random random = new Random();
Console.CursorVisible = false;
int height = Console.WindowHeight - 1;
int width = Console.WindowWidth - 5;
bool shouldExit = false;

// Console position of the player
int playerX = 0;
int playerY = 0;

// Console position of the food
int foodX = 0;
int foodY = 0;

// Available player and food strings
string[] states = { "('-')", "(^-^)", "(X_X)" };
string[] foods = { "@@@@@", "$$$$$", "#####" };

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
    Move(true);
    if (PlayerConsumedFood())
    {
        ShowFood();  // First, spawn new food
        ChangePlayer(); // Then change player
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
    foodX = random.Next(1, width - player.Length);
    foodY = random.Next(1, height - 1);
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

// Reads directional input from the Console and moves the player
void Move(bool allowExitOnOtherKeys = false)
{
    int lastX = playerX;
    int lastY = playerY;

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
            playerX--;
            break;
        case ConsoleKey.RightArrow:
            playerX++;
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
    Console.Write(new string(' ', player.Length));

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
    return playerX >= foodX && playerX < foodX + foods[food].Length && playerY == foodY;
}

// Clears the console, displays the food and player
void InitializeGame()
{
    Console.Clear();
    ShowFood();
    Console.SetCursorPosition(0, 0);
    Console.Write(player);
}
