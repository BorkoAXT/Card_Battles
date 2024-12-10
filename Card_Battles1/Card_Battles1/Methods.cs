using System.Media;
namespace GameOfWar

{
    public class Methods
    {


        public void ShuffleDeck(ref List<Card> deck)
        {
            Random random = new Random();
            for (int i = 0; i < deck.Count; i++)
            {
                int firstCardIndex = random.Next(deck.Count);

                Card tempCard = deck[firstCardIndex];
                deck[firstCardIndex] = deck[i];
                deck[i] = tempCard;
            }
        }

        public void DealCardsToPlayers(ref List<Card> deck, ref Queue<Card> firstPlayerDeck, ref Queue<Card> secondPlayerDeck)
        {
            bool toFirst = true;
            while (deck.Any())
            {
                if (toFirst)
                {
                    firstPlayerDeck.Enqueue(deck[0]);
                }
                else
                {
                    secondPlayerDeck.Enqueue(deck[0]);
                }

                deck.RemoveAt(0);
                toFirst = !toFirst;
            }
        }


        public bool GameHasWinner(ref Queue<Card> firstDeck, ref Queue<Card> secondDeck, ref int totalMoves)
        {
            if (firstDeck.Count < 4)
            {
                Console.WriteLine($"After a total of {totalMoves} moves, the second player has won!");
                return true;
            }
            if (secondDeck.Count < 4)
            {
                Console.WriteLine($"After a total of {totalMoves} moves, the first player has won!");
                return true;
            }
            return false;

        }

        public void DrawPlayersCards(ref Card firstPlayerCard, ref Card secondPlayerCard, ref Queue<Card> firstPlayerDeck, ref Queue<Card> secondPlayerDeck)
        {
            firstPlayerCard = firstPlayerDeck.Dequeue();
            Console.WriteLine($"First player has drawn: {firstPlayerCard}");
            secondPlayerCard = secondPlayerDeck.Dequeue();
            Console.WriteLine($"Second player has drawn: {secondPlayerCard}");

        }

        public void AddCardsToWinnerDeck(Queue<Card> loserDeck, Queue<Card> winner)
        {
            while (loserDeck.Count > 0)
            {
                winner.Enqueue(loserDeck.Dequeue());
            }
        }

        public void AddWarCardsToPool(Queue<Card> pool, ref Queue<Card> firstPlayerDeck, ref Queue<Card> secondPlayerDeck)
        {
            for (int i = 0; i < 3; i++)
            {
                pool.Enqueue(firstPlayerDeck.Dequeue());
                pool.Enqueue(secondPlayerDeck.Dequeue());
            }
        }

        public void DetermineRoundWinner(Queue<Card> allCards, ref Card firstPlayerCard, ref Card secondPlayerCard, ref Queue<Card> firstPlayerDeck, ref Queue<Card> secondPlayerDeck)
        {
            if ((int)firstPlayerCard.Face > (int)secondPlayerCard.Face)
            {
                Console.WriteLine("The first player has won the cards!");

                foreach (var card in allCards)
                {
                    firstPlayerDeck.Enqueue(card);
                }
            }
            else
            {
                Console.WriteLine("The second player has won the cards!");

                foreach (var card in allCards)
                {
                    secondPlayerDeck.Enqueue(card);
                }
            }
        }
                
        
        public List<Card> GenerateDeck()
        {
            List<Card> deck = new List<Card>();
            CardFace[] faces = (CardFace[])Enum.GetValues(typeof(CardFace));
            CardSuit[] suits = (CardSuit[])Enum.GetValues(typeof(CardSuit));

            for (int suite = 0; suite < suits.Length; suite++)
            {
                for (int face = 0; face < faces.Length; face++)
                {
                    CardFace currentFace = faces[face];
                    CardSuit currentSuit = suits[suite];
                    deck.Add(new Card
                    {
                        Face = currentFace,
                        Suite = currentSuit

                    }
                    );
                }
            }
            return deck;
        }

        public void ProcessWar(ref Queue<Card> pool, ref Card firstPlayerCard, ref Card secondPlayerCard, ref Queue<Card> firstPlayerDeck, ref Queue<Card> secondPlayerDeck)
        {
            while ((int)firstPlayerCard.Face == (int)secondPlayerCard.Face)
            {
                Console.WriteLine("WAR! ");

                if (firstPlayerDeck.Count < 4)
                {
                    Console.WriteLine("First player does not have enough cards to continue. Second player wins the game!");
                    secondPlayerDeck = new Queue<Card>(secondPlayerDeck.Concat(pool));
                    firstPlayerDeck.Clear();
                    return;
                }

                if (secondPlayerDeck.Count < 4)
                {
                    Console.WriteLine("Second player does not have enough cards to continue. First player wins the game!");
                    firstPlayerDeck = new Queue<Card>(firstPlayerDeck.Concat(pool));
                    secondPlayerDeck.Clear();
                    return;
                }

                AddWarCardsToPool(pool, ref firstPlayerDeck, ref secondPlayerDeck);

                firstPlayerCard = firstPlayerDeck.Dequeue();
                secondPlayerCard = secondPlayerDeck.Dequeue();

                Console.WriteLine($"First player has drawn: {firstPlayerCard}");
                Console.WriteLine($"Second player has drawn: {secondPlayerCard}");

                pool.Enqueue(firstPlayerCard);
                pool.Enqueue(secondPlayerCard);
            }
        }
        
        
            public void OpenSettingsMenu(SoundPlayer keygen, SoundPlayer divineIntervation, SoundPlayer CastleVein, SoundPlayer fear, SoundPlayer goreNest, SoundPlayer thickofIt, SoundPlayer nothing, SoundPlayer wethands)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Settings Menu:");
                    Console.WriteLine("1. Change Song");
                    Console.WriteLine("2. Change Background Color");
                    Console.WriteLine("3. Exit Settings");
                    Console.Write("Enter your choice: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            ChangeSong(keygen, divineIntervation, CastleVein, fear, goreNest, thickofIt, nothing, wethands);
                            break;

                        case "2":
                            ChangeBackgroundColor(keygen, divineIntervation, CastleVein, fear, goreNest, thickofIt, nothing, wethands);
                            break;

                        case "3":
                            Console.Clear();
                            return;

                        default:
                            Console.WriteLine("Invalid choice, please try again.");
                        Thread.Sleep(1000);
                            break;
                    }
                }
            }

            public void ChangeSong(SoundPlayer keygen, SoundPlayer divineIntervation, SoundPlayer CastleVein, SoundPlayer fear, SoundPlayer goreNest, SoundPlayer thickofIt, SoundPlayer nothing, SoundPlayer wethands)
            {
                Console.Clear();
                Console.WriteLine("Select a song:");
                Console.WriteLine("1. ULTRAKILL: Keygen Church");
                Console.WriteLine("2. ULTRAKILL: Divine Intervention");
                Console.WriteLine("3. ULTRAKILL: Castle Vein");
                Console.WriteLine("4. DOOM: The Only Thing They Fear Is You");
                Console.WriteLine("5. DOOM: The Super Gore Nest");
                Console.WriteLine("6. KSI: Thick of It");
                Console.WriteLine("7. Minecraft: Wet Hands");
                Console.WriteLine("8. None");
                Console.WriteLine();
                Console.WriteLine("type 'back' to go back.");

                string command = Console.ReadLine();

            if (command == "1")
            {
                keygen.Play();
            }
            else if (command == "2")
            {
                divineIntervation.Play();
            }
            else if (command == "3")
            {
                CastleVein.Play();
            }
            else if (command == "4")
            {
                fear.Play();
            }
            else if (command == "5")
            {
                goreNest.Play();
            }
            else if (command == "6") {
                thickofIt.Play();
            }
            else if (command == "7")
            {
                wethands.Play();
            }
            else if (command == "8")
            {
                nothing.Play();
            }
            else if (command == "back") { 
                OpenSettingsMenu(keygen, divineIntervation, CastleVein, fear, goreNest, thickofIt, nothing, wethands);
                Thread.Sleep(0);
            }
            else
            {
                
                Console.WriteLine("Invalid selection");
                Thread.Sleep(1000);
                ChangeSong(keygen, divineIntervation, CastleVein, fear, goreNest, thickofIt, nothing, wethands);
            }
            
             
            
        }

            public void ChangeBackgroundColor(SoundPlayer keygen, SoundPlayer divineIntervation, SoundPlayer CastleVein, SoundPlayer fear, SoundPlayer goreNest, SoundPlayer thickofIt, SoundPlayer nothing, SoundPlayer wethands)
            {
                Console.Clear();
                Console.WriteLine("Select a background color:");
                Console.WriteLine("1. Black");
                Console.WriteLine("2. Blue");
                Console.WriteLine("3. Green");
                Console.WriteLine("4. Red");
                Console.WriteLine("5. Yellow");
                Console.WriteLine();
                Console.WriteLine("type 'back' to go back.");

                string colorChoice = Console.ReadLine();

                switch (colorChoice)
                {
                    case "1":
                        Console.BackgroundColor = ConsoleColor.Black;
                        break;
                    case "2":
                        Console.BackgroundColor = ConsoleColor.Blue;
                        break;
                    case "3":
                        Console.BackgroundColor = ConsoleColor.Green;
                        break;
                    case "4":
                        Console.BackgroundColor = ConsoleColor.Red;
                        break;
                    case "5":
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        break;
                    case "back":
                    OpenSettingsMenu(keygen, divineIntervation, CastleVein, fear, goreNest, thickofIt, nothing, wethands);
                    Thread.Sleep(0);
                    break;
                    default:
                        Console.WriteLine("Invalid color selection!");

                        
                    Thread.Sleep(1000);
                    ChangeBackgroundColor(keygen, divineIntervation, CastleVein, fear, goreNest, thickofIt, nothing, wethands);

                    break;
                }

                Console.Clear(); 
            }
        }


    }
