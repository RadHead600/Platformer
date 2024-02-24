using UnityEngine;

public class RangeActive : MonoBehaviour
{
    [SerializeField] private Transform _activePos;
    [SerializeField] private float _ativeRange;
    [SerializeField] private LayerMask _entityLayer;

    private Unit[] _scripts;

    private void Start()
    {
        _scripts = gameObject.GetComponentsInChildren<Unit>();
        ChangeActiveUnit(_scripts, false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_activePos.position, _ativeRange);
    }

    void Update()
    {
        Collider2D[] monsters = Physics2D.OverlapCircleAll(_activePos.position, _ativeRange, _entityLayer);

        if (monsters.Length >= 0.8)
        {
            ChangeActiveUnit(_scripts, true);
            return;
        }

        ChangeActiveUnit(_scripts, false);
    }

    private void ChangeActiveUnit(Unit[] scripts, bool isActive)
    {
        foreach (Unit script in scripts)
        {
            script.enabled = isActive;
        }
    }
}
