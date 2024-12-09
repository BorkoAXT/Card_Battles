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
||                                                                            ||
|| Have fun!                                                                  ||
================================================================================");
while (true)
{
    SoundPlayer keygen = new SoundPlayer(@"C:\Users\borko\Downloads\WetHands.wav");
    SoundPlayer fear = new SoundPlayer(@"C:\Users\borko\Downloads\the-only-thing-they-fear-is-you.wav");
    SoundPlayer goreNest = new SoundPlayer(@"C:\Users\borko\Downloads\Mick Gordon - The Super Gore Nest (DOOM Eternal - Gamerip) [REUPLOAD].wav");
    SoundPlayer CastleVein = new SoundPlayer(@"C:\Users\borko\Downloads\Castle Vein.wav");
    SoundPlayer divineIntervation = new SoundPlayer(@"C:\Users\borko\Downloads\Divine Intervention.wav");
    SoundPlayer wethands = new SoundPlayer(@"C:\Users\borko\Downloads\WetHands.wav");
    SoundPlayer thickofIt = new SoundPlayer(@"C:\Users\borko\Downloads\KSI - Thick Of It (Lyrics) ft. Trippie Redd.wav");
    wethands.Play();

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
        string num = Console.ReadLine();
        Console.Clear();

        gameLogic.DrawPlayersCards(ref firstPlayerCard, ref secondPlayerCard, ref firstPlayerDeck, ref secondPlayerDeck);

        Queue<Card> pool = new Queue<Card>();

        pool.Enqueue(firstPlayerCard);
        pool.Enqueue(secondPlayerCard);
        if (num == "Win")
        {
            Console.WriteLine("Second player does not have enough cards to continue. First player wins the game!");
            firstPlayerDeck = new Queue<Card>(firstPlayerDeck.Concat(pool));
            secondPlayerDeck.Clear();
            return;

        }
        else if (num == "Lose")
        {
            Console.WriteLine("First player does not have enough cards to continue. Second player wins the game!");
            secondPlayerDeck = new Queue<Card>(secondPlayerDeck.Concat(pool));
            firstPlayerDeck.Clear();
            return;
        }
        else if (num == "Songs")
        {
            Console.WriteLine("These are the songs:");
            Console.WriteLine("1. ULTRAKILL: Keygen Church");
            Console.WriteLine("2. ULTRAKILL: Divine Intervetion");
            Console.WriteLine("3. ULTRAKILL: Castle vein");
            Console.WriteLine("4. DOOM: The only thing they fear is you");
            Console.WriteLine("5. DOOM: The super gore nest");
            Console.WriteLine("6. KSI: Thick of it");
            int number = int.Parse(Console.ReadLine());
            if (number == 1)
            {
                keygen.Play();
            }
            else if (number == 2)
            {
                divineIntervation.Play();
                Console.Clear();
            }
            else if (number == 3)
            {
                CastleVein.Play();
                Console.Clear();

            }
            else if (number == 4)
            {
                fear.Play();
                Console.Clear();
            }
            else if (number == 5)
            {
                goreNest.Play();
                Console.Clear();
            }
            else if (number == 6)
            {
                thickofIt.Play();
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Invalid song");
                Console.Clear();
            }
            
                
            
        }
        

        gameLogic.ProcessWar(ref pool, ref firstPlayerCard, ref secondPlayerCard, ref firstPlayerDeck, ref secondPlayerDeck);
        gameLogic.DetermineRoundWinner(pool, ref firstPlayerCard, ref secondPlayerCard, ref firstPlayerDeck, ref secondPlayerDeck);

        Console.WriteLine("==================================================================");
        Console.WriteLine($"First player currently has {firstPlayerDeck.Count} cards.");
        Console.WriteLine($"Second player currently has {secondPlayerDeck.Count} cards.");
        Console.WriteLine("==================================================================");
        Moves++;
    }
}