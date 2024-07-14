using UnityEngine;

public class BlockColorChange : MonoBehaviour
{
    private Renderer blockRenderer;

    void Start()
    {
        blockRenderer = GetComponent<Renderer>();
        if (blockRenderer == null)
        {
            Debug.LogError("Renderer component not found on the block.");
            return;
        }
        else
        {
            Debug.Log("Renderer component found on the block.");
            // Create a new instance of the material to allow independent color changes
            blockRenderer.material = new Material(blockRenderer.material);
            // Initial color change test
            ChangeColor(Color.green);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called with: " + other.name);
        if (other.CompareTag("RoboticArm"))
        {
            Debug.Log("Robotic arm detected. Changing color to blue.");
            ChangeColor(Color.blue);
        }
        else
        {
            Debug.Log("Other object detected: " + other.tag);
            ChangeColor(Color.red);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit called with: " + other.name);
        ResetColor();
    }

    private void ChangeColor(Color newColor)
    {
        if (blockRenderer != null)
        {
            Debug.Log("Changing color to: " + newColor);
            blockRenderer.material.color = newColor;
        }
        else
        {
            Debug.LogError("Block renderer is null.");
        }
    }

    private void ResetColor()
    {
        if (blockRenderer != null)
        {
            Debug.Log("Resetting color to white.");
            blockRenderer.material.color = Color.white; // Reset to the default color
        }
        else
        {
            Debug.LogError("Block renderer is null.");
        }
    }
}






