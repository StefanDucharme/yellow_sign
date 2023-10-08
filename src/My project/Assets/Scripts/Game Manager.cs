using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.UIElements;

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
    
    public GameObject PlayAreaObject;
    public GameObject DiscardAreaObject;
    public GameObject HandArea;

    public int HandSize = 5;
    public List<GameObject> AvailableCards = new List<GameObject>();
    public List<GameObject> Deck = new List<GameObject>();
    public List<GameObject> Hand = new List<GameObject>();
    public List<GameObject> PlayArea = new List<GameObject>();
    public List<GameObject> DiscardPile = new List<GameObject>();
    
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
        PlayAreaObject = GameObject.Find("PlayArea");
        DiscardAreaObject = GameObject.Find("DiscardArea");
        HandArea = GameObject.Find("HandArea");

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

    public void EndTurn()
    {
        CurrentPhase = Phase.End;
        //TODO emit?
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
            var card = Deck[0];
            MoveCardFromDeckToHand(card);
        }
    }

    public void ShuffleDiscardIntoDeck()
    {
        if (DiscardPile.Count >= 1)
        {
            //foreach item in discard pile, add to deck
            foreach (var card in DiscardPile)
            {
                MoveCardFromDiscardPileToDeck(card, false);
            }

            DiscardPile.Clear();
            ShuffleDeck();
        }
    }

    public void MoveCardFromDeckToHand(GameObject card)
    {
        var foundCard = Deck.FirstOrDefault(c => c.GetInstanceID() == card.GetInstanceID());
        

        Deck.Remove(foundCard);
        Hand.Add(card);
        card.transform.SetParent(HandArea.transform, false);
        card.GetComponent<BaseCard>().OnDraw();
    }

    //move card from hand to play area
    public void MoveCardFromHandToPlayArea(GameObject card)
    {
        Debug.Log("move card from hand to play area");
        var foundCard = Hand.FirstOrDefault(c => c.GetInstanceID() == card.GetInstanceID());
        

        Hand.Remove(foundCard);
        PlayArea.Add(card);
        card.transform.SetParent(PlayAreaObject.transform, false);
        card.GetComponent<BaseCard>().OnPlay();
        MoveCardFromPlayAreaToDiscardPile(card);
    }

    //move card from play area to discard pile
    public void MoveCardFromPlayAreaToDiscardPile(GameObject card)
    {
        var foundCard = PlayArea.FirstOrDefault(c => c.GetInstanceID() == card.GetInstanceID());
        

        PlayArea.Remove(foundCard);
        DiscardPile.Add(card);
        card.transform.SetParent(DiscardAreaObject.transform, false);
        card.GetComponent<BaseCard>().OnDiscard();
    }

    //move card from discard pile to deck
    public void MoveCardFromDiscardPileToDeck(GameObject card, bool removeCard = true)
    {
        var foundCard = DiscardPile.FirstOrDefault(c => c.GetInstanceID() == card.GetInstanceID());
        

        if (removeCard)
        {
            DiscardPile.Remove(foundCard);
        }

        Deck.Add(card);
        card.transform.SetParent(null, false);
        card.GetComponent<BaseCard>().OnReturnToDeck();
    }
    
}
