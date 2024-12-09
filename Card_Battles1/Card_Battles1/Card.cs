using GameOfWar;

public class Card
{
    public CardFace Face { get; set; }
    public CardSuit Suite { get; set; }


    private static readonly Dictionary<CardSuit, string> SuitSymbols = new Dictionary<CardSuit, string>
    {
        { CardSuit.Spade, "♠" },
        { CardSuit.Club, "♣" },
        { CardSuit.Heart, "♥" },
        { CardSuit.Diamond, "♦" }
    };

    public override string ToString()
    {
        string faceValue = GetFaceValue(Face);
        string suitSymbol = SuitSymbols[Suite];

        
        string card = $@"
  +-----+
  |    {suitSymbol}|  
  |  {faceValue,-2} |  
  |{suitSymbol}    |  
  +-----+";

        return card;
    }

    private string GetFaceValue(CardFace face)
    {
        switch (face)
        {
            case CardFace.Ace:
                return "A";
            case CardFace.Jack:
                return "J";
            case CardFace.Queen:
                return "Q";
            case CardFace.King:
                return "K";
            default:
                return ((int)face).ToString();
        }
    }
}
