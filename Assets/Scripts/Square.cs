using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class Square : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Detects if the user has clicked on the right square
    private void OnMouseDown()
    {
        if (spriteRenderer.color == Color.cyan)
            ClickPmBoosterGame.instance.IsRightSquare(true);
    }
}