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

    }
}