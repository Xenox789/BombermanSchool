
using UnityEngine;

public class Extras : MonoBehaviour
{

    public enum ItemType
    {
        ExtraBomb,
        SpeedIncrease,
        IncreaseRadius,
    }
    
    public ItemType type;

    private void ExtraPickup(GameObject player)
    {
        switch (type)
        {
            case ItemType.ExtraBomb:
                player.GetComponent<BombController>().AddBomb();
                break;

            case ItemType.IncreaseRadius:
                player.GetComponent<BombController>().IncreaseRadius();
                break;

            case ItemType.SpeedIncrease:
                player.GetComponent<Player>().IncreaseSpeed();
                break;
        }

        Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            ExtraPickup(other.gameObject);
            
        }
    }
    
}
