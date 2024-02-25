using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class CutScene : MonoBehaviour
{
    [SerializeField] private PlayableDirector _playableDirector;

    private Character _character;
    private Enemy[] _enemies;

    private void Start()
    {
        _character = FindObjectOfType<Character>();
        _enemies = FindObjectsOfType<Enemy>();
        _character.enabled = false;
        ChangeEnemyEnabled(false);
        StartCoroutine(EndCutScene());
    }

    private IEnumerator EndCutScene()
    {
        yield return new WaitForSeconds(((float)_playableDirector.duration) - 0.5f);

        _character.enabled = true;
        ChangeEnemyEnabled(true);
        Destroy(gameObject);
    }

    private void ChangeEnemyEnabled(bool isEnable)
    {
        foreach (Enemy enemy in _enemies)
        {
            enemy.enabled = isEnable;
        }

    }
}
