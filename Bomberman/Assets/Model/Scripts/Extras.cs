
using UnityEngine;

public class Extras : MonoBehaviour
{

    public enum ItemType
    {
        ExtraBomb,
        SpeedIncrease,
        IncreaseRadius,

        fakeBoxExtra,
        ExtraInvioable,
    }
    
    public ItemType type;

    private void ExtraPickup(GameObject player)
    {
        switch (type)
        {
            case ItemType.ExtraBomb:
                player.GetComponent<Bomb>().AddBomb();
                break;

            case ItemType.IncreaseRadius:
                player.GetComponent<Bomb>().IncreaseRadius();
                break;

            case ItemType.SpeedIncrease:
                player.GetComponent<Player>().IncreaseSpeed();
                break;
            case ItemType.fakeBoxExtra:
                player.GetComponent<FakeBoxCrontroller>().PickUpFakeBox();
                break;
            case ItemType.ExtraInvioable:
                player.GetComponent<Player>().Inviolable();
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
