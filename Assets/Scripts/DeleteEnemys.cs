using UnityEngine;

public class DeleteEnemys : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.GetComponentInChildren<Units>() != null || collision.GetComponentInChildren<Bonus>() != null) && !(collision.GetComponentInChildren<Units>() is Character))
        {
            Destroy(collision.gameObject);
        }
    }
}
