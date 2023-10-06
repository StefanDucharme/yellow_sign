using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCards : MonoBehaviour
{
    public GameObject Card1;
    public GameObject Card2;
    public GameObject PlayerArea;
    public GameObject EnemyArea;

    private List<GameObject> cards = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        cards.Add(Card1);
        cards.Add(Card2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject playerCard = Instantiate(cards[Random.Range(0,cards.Count)], new Vector3(0, 0, 0), Quaternion.identity);
            playerCard.transform.SetParent(PlayerArea.transform, false);

            GameObject enemyCard = Instantiate(cards[Random.Range(0, cards.Count)], new Vector3(0, 0, 0), Quaternion.identity);
            enemyCard.transform.SetParent(EnemyArea.transform, false);
        }         
        
    }

}
