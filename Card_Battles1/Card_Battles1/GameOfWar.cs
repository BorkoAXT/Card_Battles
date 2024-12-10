using GameOfWar;
using System.Media;
using System.Text;
Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine(@"
================================================================================
|| Welcome to the Game of War!                                                ||
||                                                                            ||
|| HOW TO PLAY:                                                               ||
|| + Each of the two players are dealt one half of a shuffled deck of cards.  ||
|| + Each turn, each player draws one card from their deck.                   ||
|| + The player that drew the card with higher value gets both cards.         ||
|| + Both cards return to the winners deck.                                   ||
|| + If there is a draw, both players place the next three cards face down    ||
|| and then another card face-up. The owner of the higher face-up             ||
|| card gets all the cards on the table.                                      ||
||                                                                            ||
|| HOW TO WIN:                                                                ||
|| + The player who collects all the cards wins.                              ||
||                                                                            ||
|| CONTROLS:                                                                  ||
|| + Press [Enter] to draw a new card until we have a winner.                 ||
|| + Type 'help' to receive available comands                                 ||
||                                                                            ||
|| Have fun!                                                                  ||
================================================================================");
while (true)
{
    SoundPlayer keygen = new SoundPlayer(@"C:\Users\borko\Downloads\Keygen.wav");
    SoundPlayer fear = new SoundPlayer(@"C:\Users\borko\Downloads\Fear.wav");
    SoundPlayer goreNest = new SoundPlayer(@"C:\Users\borko\Downloads\SuperGoreMaze.wav");
    SoundPlayer CastleVein = new SoundPlayer(@"C:\Users\borko\Downloads\CastleVein.wav");
    SoundPlayer divineIntervation = new SoundPlayer(@"C:\Users\borko\Downloads\Divine Intervention.wav");
    SoundPlayer wethands = new SoundPlayer(@"C:\Users\borko\Downloads\WetHands.wav");
    SoundPlayer thickofIt = new SoundPlayer(@"C:\Users\borko\Downloads\ThickOfIt.wav");
    SoundPlayer nothing = new SoundPlayer(@"C:\Users\borko\Downloads\nothing.wav");
    Methods gameLogic = new Methods();

    List<Card> deck = gameLogic.GenerateDeck();

    gameLogic.ShuffleDeck(ref deck);

    Queue<Card> firstPlayerDeck = new Queue<Card>();
    Queue<Card> secondPlayerDeck = new Queue<Card>();

    gameLogic.DealCardsToPlayers(ref deck, ref firstPlayerDeck, ref secondPlayerDeck);

    Card firstPlayerCard = null;
    Card secondPlayerCard = null;

    int Moves = 0;

    while (!gameLogic.GameHasWinner(ref firstPlayerDeck, ref secondPlayerDeck, ref Moves))
    {
        
        string command = Console.ReadLine();
        if (command.Equals("Win", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("You have chosen to simply win. Congratulations!");
            firstPlayerDeck = new Queue<Card>(gameLogic.GenerateDeck());
            secondPlayerDeck.Clear();
            break;
        }
        if (command.Equals("Lose", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("You have chosen to lose! Better luck next time.");
            firstPlayerDeck.Clear();
            secondPlayerDeck = new Queue<Card>(gameLogic.GenerateDeck());
            break;
        }
        if (command.Equals("settings", StringComparison.OrdinalIgnoreCase))
        {
            gameLogic.OpenSettingsMenu(keygen, divineIntervation, CastleVein, fear, goreNest, thickofIt, nothing, wethands);
            continue;
        }
        if (command.Equals("help", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("type 'Win' to win the game");
            Console.WriteLine("type 'Lose' to lose the game");
            Console.WriteLine("Type 'settings' to open the settings menu, or press [Enter] to continue.");
            continue;
        }

        
        Console.Clear();

        gameLogic.DrawPlayersCards(ref firstPlayerCard, ref secondPlayerCard, ref firstPlayerDeck, ref secondPlayerDeck);

        Queue<Card> pool = new Queue<Card>();

        pool.Enqueue(firstPlayerCard);
        pool.Enqueue(secondPlayerCard);

        gameLogic.ProcessWar(ref pool, ref firstPlayerCard, ref secondPlayerCard, ref firstPlayerDeck, ref secondPlayerDeck);
        gameLogic.DetermineRoundWinner(pool, ref firstPlayerCard, ref secondPlayerCard, ref firstPlayerDeck, ref secondPlayerDeck);

        Console.WriteLine("==================================================================");
        Console.WriteLine($"First player currently has {firstPlayerDeck.Count} cards.");
        Console.WriteLine($"Second player currently has {secondPlayerDeck.Count} cards.");
        Console.WriteLine("==================================================================");
        Moves++;
    }
    

    Console.WriteLine("Do you want to play another game? yes/no");
    if (!Console.ReadLine().Equals(("yes").ToLower(), StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine("Thank you for playing! Goodbye!");
        break;
    }
    Console.Clear();
}