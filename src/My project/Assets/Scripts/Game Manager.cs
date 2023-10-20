using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public partial class GameManager : MonoBehaviour
{
    public GameObject PlayAreaObject;
    public GameObject DiscardAreaObject;
    public GameObject HandArea;
    public GameObject EnemyArea;
    public NotificationsManager NotificationsManager;

    public GameObject Player;

    public int HandSize = 5;
    public List<GameObject> AvailableCards = new List<GameObject>();
    public List<GameObject> Deck = new List<GameObject>();
    public List<GameObject> Hand = new List<GameObject>();
    public List<GameObject> PlayArea = new List<GameObject>();
    public List<GameObject> DiscardPile = new List<GameObject>();

    public List<GameObject> AvailableEnemies = new List<GameObject>();
    public List<GameObject> EnemyQueue = new List<GameObject>();
    public List<GameObject> InPlayEnemies = new List<GameObject>();
    public List<GameObject> DeadEnemies = new List<GameObject>();

    public int TurnCount = 0;
    public Enums.GameState CurrentGameState;
    public Enums.Phase CurrentPhase;
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
        EnemyArea = GameObject.Find("EnemyArea");
        NotificationsManager = GameObject.Find("NotificationsManager").GetComponent<NotificationsManager>();

        ChangeGameState(Enums.GameState.Start);

        foreach (var availableCard in AvailableCards)
        {
            GameObject playerCard = Instantiate(availableCard, new Vector3(0, 0, 0), Quaternion.identity);
            AddToDeck(playerCard);
        }

        ShuffleDeck();

        foreach (var availableEnemy in AvailableEnemies)
        {
            GameObject enemy = Instantiate(availableEnemy, new Vector3(0, 0, 0), Quaternion.identity);
            EnemyQueue.Add(enemy);
        }

        //TODO draw hand
        ChangeGameState(Enums.GameState.PlayerTurn);
        ChangePhase(Enums.Phase.Start);
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
        ChangePhase(Enums.Phase.End);
    }
    

    public void ChangeGameState(Enums.GameState state)
    {
        CurrentGameState = state;
        //TODO emit?
        OnGameStateChanged();
    }

    public void ChangePhase(Enums.Phase phase)
    {
        CurrentPhase = phase;
        //TODO emit?
        OnPhaseChanged();
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

    public void SpawnEnemy()
    {
        GameObject enemy;

        if (EnemyQueue.Count > 0)
        {
            enemy = EnemyQueue[0];
        }
        else
        {
            Debug.Log("No enemies left to spawn");
            return;
        }

        InPlayEnemies.Add(enemy);
        enemy.transform.SetParent(EnemyArea.transform, false);
        enemy.GetComponent<BaseEnemy>().OnPlay();
    }

    protected virtual void OnGameStateChanged()
    {
        var notification = new NotificationData()
        {
            Sender = this,
            Data = CurrentGameState
        };

        NotificationsManager.PostNotification(notification, "OnGameStateChanged");
    }

    protected virtual void OnPhaseChanged()
    {
        var notification = new NotificationData()
        {
            Sender = this,
            Data = CurrentPhase
        };

        NotificationsManager.PostNotification(notification, "OnPhaseChanged");
    }
}
