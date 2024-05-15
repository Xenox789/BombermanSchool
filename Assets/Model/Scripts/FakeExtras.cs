using UnityEngine;

public class FakeExtras : MonoBehaviour
{

    public enum ItemType
    {
        SpeedDecrease,
        DecreaseRadius,
        UnablePlaceBomb,
        InstantPlaceBomb,
    }
    
    public ItemType type; 

    private void OnEnable()
    {
        type = (ItemType)Random.Range(0, 4);
    }
    private void FakeExtraPickup(GameObject player)
    {
        switch (type)
        {
            case ItemType.DecreaseRadius:
                player.GetComponent<BombController>().ActivateDecreaseRadius();
                break;
            case ItemType.SpeedDecrease:
                player.GetComponent<Player>().ActivateDecreaseSpeed();
                break;
            case ItemType.UnablePlaceBomb:
                player.GetComponent<BombController>().ActivateUnablePlaceBomb();
                break;
            case ItemType.InstantPlaceBomb:
                player.GetComponent<BombController>().ActivatePlaceBombInstantly();
                break;
           
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            FakeExtraPickup(other.gameObject);
            
        }
    }


}
