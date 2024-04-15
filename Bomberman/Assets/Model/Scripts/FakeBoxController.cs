using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeBoxCrontroller : MonoBehaviour
{

    public GameObject fakeBoxPrefab;
    public KeyCode inputKey;
    public int remainingFakeBox = 1;

    public void PickUpFakeBox(){
        remainingFakeBox += 3;
    }

    public void AddFakeBox(){
        remainingFakeBox++;
    }

    private void Update()
    {
        if (remainingFakeBox > 0 && Input.GetKeyDown(inputKey)) {
            PlaceFakeBox();
        }
        

    }
    private void PlaceFakeBox(){
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        
        
        Instantiate(fakeBoxPrefab, position, Quaternion.identity);
        remainingFakeBox--;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
<<<<<<< HEAD
        if (other.gameObject.layer == LayerMask.NameToLayer("FakeBox")) {
=======
        if (other.gameObject.layer == LayerMask.NameToLayer("Box")) {
>>>>>>> Bomb
            other.isTrigger = false;
        }

    }
}
