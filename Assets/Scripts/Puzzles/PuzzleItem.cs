using UnityEngine;

public class PuzzleItem : MonoBehaviour
{
    public enum PuzzleType { Key, Torch, Block, Ice, CryptDoor }
    public PuzzleType puzzleType;

    private bool isCompleted = false;

    public bool IsCompleted
    {
        get { return isCompleted; }
    }

    public void Interact()
    {
        if (isCompleted) return;

        Debug.Log("Interacting with puzzle item: " + gameObject.name + " of type: " + puzzleType);

        switch (puzzleType)
        {
            case PuzzleType.Key:
                FindObjectOfType<Player>().pickUpKey();
                GlobalData.inventory.Add("Key");
                Destroy(gameObject);
                isCompleted = true;
                FindObjectOfType<Player>().addExperience(10);
                break;
            case PuzzleType.Torch:
                LightTorch();
                break;
            case PuzzleType.Block:
                PushBlock();
                break;
            case PuzzleType.Ice:
                BreakIce();
                break;
            case PuzzleType.CryptDoor:
                Unlock();
                break;
        }
    }

    private void LightTorch()
    {
        Debug.Log("Lighting torch: " + gameObject.name);
        GetComponent<SpriteRenderer>().color = Color.yellow; // Example of changing color to indicate it's lit
        Light torchLight = GetComponent<Light>();
        if (torchLight != null)
        {
            torchLight.intensity = 1f; // Turn on the light
        }
        isCompleted = true;
        FindObjectOfType<Player>().addExperience(20);
    }

    private void PushBlock()
    {
        Debug.Log("Pushing block: " + gameObject.name);
        // Implement logic to push the block
        // Example: Move the block to a specific position
        transform.position += new Vector3(1, 0, 0);
        isCompleted = true;
        FindObjectOfType<Player>().addExperience(30);
    }

    private void BreakIce()
    {
        Debug.Log("Breaking ice: " + gameObject.name);
        // Implement logic to break the ice
        Destroy(gameObject);
        isCompleted = true;
        FindObjectOfType<Player>().addExperience(40);
    }

    public void Unlock()
    {
        if (GlobalData.inventory.Contains("Key"))
        {
            Debug.Log("Unlocking crypt door: " + gameObject.name);
            Destroy(gameObject);
            isCompleted = true;
            FindObjectOfType<Player>().addExperience(50);
        }
        else
        {
            Debug.Log("Key not found in inventory.");
        }
    }
}
