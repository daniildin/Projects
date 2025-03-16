using System;
using System.Linq;
using System.Threading;

// Declaring variables to store pet information
string animalSpecies = "";
string animalID = "";
string animalAge = "";
string animalPhysicalDescription = "";
string animalPersonalityDescription = "";
string animalNickname = "";
string suggestedDonation = "";

// Setting a maximum limit for pets
int maxPets = 8;
string? readResult;
string menuSelection = "";
decimal decimalDonation = 0.00m;

// Initializing a two-dimensional array to store pet details
string[,] ourAnimals = new string[maxPets, 7];

// Populating the array with sample data
for (int i = 0; i < maxPets; i++)
{
    switch (i)
    {
        case 0:
            // Assigning values for a specific pet
            animalSpecies = "dog";
            animalID = "d1";
            animalAge = "2";
            animalPhysicalDescription = "medium sized cream colored female golden retriever weighing about 45 pounds. housebroken.";
            animalPersonalityDescription = "loves to have her belly rubbed and likes to chase her tail. gives lots of kisses.";
            animalNickname = "lola";
            suggestedDonation = "85.00";
            break;

        case 1:
            animalSpecies = "dog";
            animalID = "d2";
            animalAge = "9";
            animalPhysicalDescription = "large reddish-brown male golden retriever weighing about 85 pounds. housebroken.";
            animalPersonalityDescription = "loves to have his ears rubbed when he greets you at the door, or at any time! loves to lean-in and give doggy hugs.";
            animalNickname = "gus";
            suggestedDonation = "49.99";
            break;

        case 2:
            animalSpecies = "cat";
            animalID = "c3";
            animalAge = "1";
            animalPhysicalDescription = "small white female weighing about 8 pounds. litter box trained.";
            animalPersonalityDescription = "friendly";
            animalNickname = "snow";
            suggestedDonation = "40.00";
            break;

        case 3:
            // Partial data for a cat
            animalSpecies = "cat";
            animalID = "c4";
            animalAge = "";
            animalPhysicalDescription = "";
            animalPersonalityDescription = "";
            animalNickname = "lion";
            suggestedDonation = "";
            break;

        default:
            // Empty values for uninitialized entries
            animalSpecies = "";
            animalID = "";
            animalAge = "";
            animalPhysicalDescription = "";
            animalPersonalityDescription = "";
            animalNickname = "";
            suggestedDonation = "";
            break;
    }

    // Storing pet details in the array
    ourAnimals[i, 0] = "ID #: " + animalID;
    ourAnimals[i, 1] = "Species: " + animalSpecies;
    ourAnimals[i, 2] = "Age: " + animalAge;
    ourAnimals[i, 3] = "Nickname: " + animalNickname;
    ourAnimals[i, 4] = "Physical description: " + animalPhysicalDescription;
    ourAnimals[i, 5] = "Personality: " + animalPersonalityDescription;
    
    // Ensuring the suggested donation is a valid decimal, else defaulting to 45.00
    if (!decimal.TryParse(suggestedDonation, out decimalDonation))
    {
        decimalDonation = 45.00m;
    }
    ourAnimals[i, 6] = $"Suggested Donation: {decimalDonation:C2}";
}

// Main menu loop
do
{
    Console.Clear();
    Console.WriteLine("Welcome to the Contoso PetFriends app. Your main menu options are:");
    Console.WriteLine(" 1. List all of our current pet information");
    Console.WriteLine(" 2. Display all dogs with a specified characteristic");
    Console.WriteLine();
    Console.WriteLine("Enter your selection number (or type Exit to exit the program)");

    readResult = Console.ReadLine();
    if (readResult != null)
    {
        menuSelection = readResult.ToLower();
    }

    switch (menuSelection)
    {
        case "1":
            // Listing all pets stored in the array
            for (int i = 0; i < maxPets; i++)
            {
                if (ourAnimals[i, 0] != "ID #: ")
                {
                    Console.WriteLine();
                    for (int j = 0; j < 7; j++)
                    {
                        Console.WriteLine(ourAnimals[i, j].ToString());
                    }
                }
            }
            Console.WriteLine("\r\nPress the Enter key to continue");
            readResult = Console.ReadLine();
            break;

        case "2":
            // Searching for dogs with specific characteristics
            Console.WriteLine("\nEnter dog characteristics to search for, separated by commas:");
            readResult = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(readResult)) break;
            
            // Cleaning and sorting search terms
            string[] dogSearchTerms = readResult.ToLower().Split(',').Select(term => term.Trim()).Where(term => term != "").ToArray();
            Array.Sort(dogSearchTerms);

            string[] searchingIcons = { "|", "/", "-", "\\", "*" };
            bool matchesAnyDog = false;

            // Iterating through stored pet data to find matches
            for (int i = 0; i < maxPets; i++)
            {
                if (!ourAnimals[i, 1].Contains("dog")) continue;

                string dogDescription = ourAnimals[i, 4] + " " + ourAnimals[i, 5];
                bool matchesCurrentDog = false;

                foreach (string term in dogSearchTerms)
                {
                    // Simulating a searching animation
                    for (int countdown = 2; countdown >= 0; countdown--)
                    {
                        foreach (string icon in searchingIcons)
                        {
                            Console.Write($"\rSearching our dog {ourAnimals[i, 3]} for {term} {icon} / {countdown}");
                            Thread.Sleep(100);
                        }
                    }
                    Console.Write("\r" + new string(' ', Console.BufferWidth));
                    
                    // Checking if description matches search term
                    if (dogDescription.Contains(term))
                    {
                        Console.WriteLine($"\rOur dog {ourAnimals[i, 3]} matches your search for {term}!");
                        matchesCurrentDog = true;
                        matchesAnyDog = true;
                    }
                }
                if (matchesCurrentDog)
                {
                    Console.WriteLine($"\r{ourAnimals[i, 3]} ({ourAnimals[i, 0]})\n{dogDescription}\n");
                }
            }

            if (!matchesAnyDog)
            {
                Console.WriteLine("No matches found for any available dogs.");
            }
            Console.WriteLine("\n\rPress the Enter key to continue");
            readResult = Console.ReadLine();
            break;
    }
} while (menuSelection != "exit");
