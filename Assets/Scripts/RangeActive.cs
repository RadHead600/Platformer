using UnityEngine;

public class RangeActive : MonoBehaviour
{
    [SerializeField]
    private Transform activePos;

    [SerializeField]
    private float ativeRange;

    [SerializeField]
    private LayerMask entityLayer;

    private bool switching;

    private void Start()
    {
        Units[] scripts = gameObject.GetComponentsInChildren<Units>();
        
        foreach (Units script in scripts)
        {
            script.enabled = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(activePos.position, ativeRange);
    }

    void Update()
    {
        Collider2D[] monsters = Physics2D.OverlapCircleAll(activePos.position, ativeRange, entityLayer);
        if (monsters.Length >= 0.8)
        {
            Units[] scripts = gameObject.GetComponentsInChildren<Units>();

            foreach (Units script in scripts)
            {
                script.enabled = true;
            }
        }
        else if(monsters.Length < 0.8)
        {
            Units[] scripts = gameObject.GetComponentsInChildren<Units>();

            foreach (Units script in scripts)
            {
                script.enabled = false;
            }
        }
    }
}
