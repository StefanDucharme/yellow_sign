using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Start,
        PlayerTurn,
        EnemyTurn,
        End
    }

    public enum Phase
    {
        Start,
        Draw,
        Main,
        Resolve,
        End
    }

    public int HandSize = 5;
    public List<GameObject> AvailableCards = new List<GameObject>();
    public List<GameObject> Deck = new List<GameObject>();
    public List<GameObject> Hand = new List<GameObject>();
    public List<GameObject> PlayArea = new List<GameObject>();
    public List<GameObject> DiscardPile = new List<GameObject>();
    public GameObject PlayerArea;
    public int TurnCount = 0;
    public GameState CurrentGameState;
    public Phase CurrentPhase;
    public bool DisableInput = true;
    

    //public Transform[] cardslots;
    //public bool[] availableCardSlots;

    //public TextMeshProUGUI deckSizeText;
    //public TextMeshProUGUI discardPileText;

    void Start()
    {

        ChangeGameState(GameState.Start);
        foreach (var availableCard in AvailableCards)
        {
            GameObject playerCard = Instantiate(availableCard, new Vector3(0, 0, 0), Quaternion.identity);
            AddToDeck(playerCard);
        }

        ShuffleDeck();

        //TODO draw hand
        ChangeGameState(GameState.PlayerTurn);
        ChangePhase(Phase.Start);
    }
    
    void Update()
    {
        //deckSizeText.text = deck.Count.ToString();
        //discardPileText.text = discardPile.Count.ToString();
    }

    private void AddToDeck(GameObject playerCard)
    {
        Deck.Add(playerCard);
        playerCard.GetComponent<BaseCard>().ChangeState(BaseCard.State.InDeck);
    }

    public void ChangeGameState(GameState state)
    {
        CurrentGameState = state;
        //TODO emit?
    }

    public void ChangePhase(Phase phase)
    {
        CurrentPhase = phase;
        //TODO emit?
    }

    public void ShuffleDeck()
    {
        //randomize the Deck list
        for (int i = 0; i < Deck.Count; i++)
        {
            GameObject temp = Deck[i];
            int randomIndex = Random.Range(i, Deck.Count);
            Deck[i] = Deck[randomIndex];
            Deck[randomIndex] = temp;
        }
    }

    public void DrawCard()
    {
        Debug.Log("draw card from game manager");

        if (Deck.Count <= 0 && DiscardPile.Count >= 1)
        {
            ShuffleDiscardIntoDeck();
        }


        if (Deck.Count >= 1)
        {
            MoveCardFromDeckToHand(0);
        }
    }

    public void ShuffleDiscardIntoDeck()
    {
        if (DiscardPile.Count >= 1)
        {
            //foreach item in discard pile, add to deck
            foreach (var card in DiscardPile)
            {
                Deck.Add(card);
                card.GetComponent<BaseCard>().ChangeState(BaseCard.State.InDeck);
            }

            DiscardPile.Clear();
            ShuffleDeck();
        }
    }

    public void MoveCardFromDeckToHand(int position)
    {
        GameObject card = Deck[position];
        Deck.RemoveAt(position);
        Hand.Add(card);
        card.transform.SetParent(PlayerArea.transform, false);
        card.GetComponent<BaseCard>().ChangeState(BaseCard.State.InHand);
    }

    //move card from hand to play area
    public void MoveCardFromHandToPlayArea(int position)
    {
        GameObject card = Hand[position];
        Hand.Remove(card);
        PlayArea.Add(card);
        card.GetComponent<BaseCard>().ChangeState(BaseCard.State.InPlay);
    }

    //move card from play area to discard pile
    public void MoveCardFromPlayAreaToDiscardPile(int position)
    {
        GameObject card = PlayArea[position];
        DiscardPile.Add(card);
        card.GetComponent<BaseCard>().ChangeState(BaseCard.State.InDiscard);
    }
    
}
