using UnityEngine;

public class HW2PlayerDie : MonoBehaviour
{
    // Tags and UI References
    private string enemyTag = "Enemy";
    public GameObject endPanel;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == enemyTag)
        {
            gameObject.SetActive(false);
            if (endPanel != null)
            {
                endPanel.SetActive(true);
            }
        }
    }
}
