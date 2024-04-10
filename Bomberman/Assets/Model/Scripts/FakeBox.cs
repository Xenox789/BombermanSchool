using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeBox : MonoBehaviour
{
    [SerializeField] private GameObject fakeBoxPrefab;
    [SerializeField] private int remainingFakeBoxes = 0;
    private int placedFakeBoxes = 0;
    [SerializeField] private KeyCode inputKey;

    private void Update()
    {
        if (remainingFakeBoxes > 0 && Input.GetKeyDown(inputKey))
        {
            PlaceFakeBox();
        }
    }

    public void AddFakeBox()
    {
        remainingFakeBoxes += 3;

        /* Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        GameObject fakeBox = Instantiate(fakeBoxPrefab, position, Quaternion.identity); */
    }

    public void PlaceFakeBox()
    {
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        GameObject fakeBox = Instantiate(fakeBoxPrefab, position, Quaternion.identity);

        placedFakeBoxes++;
        remainingFakeBoxes--;
    }

    public void Delete()
    {
        remainingFakeBoxes++;
        placedFakeBoxes--;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Extras"))
        {
            other.isTrigger = false;
        }

    }
}
