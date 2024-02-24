using UnityEngine;
using UnityEngine.Playables;

public class CutScene : MonoBehaviour
{
    void Update()
    {
        if(gameObject.GetComponent<PlayableDirector>().time >= 11)
        {
            Character character = FindObjectOfType<Character>();
            character.enabled = true;
            Enemy[] objects = FindObjectsOfType<Enemy>();
            foreach (Enemy enem in objects)
            {
                enem.enabled = true;
            }
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        Character character = FindObjectOfType<Character>();
        character.enabled = false;
        Enemy[] objects = FindObjectsOfType<Enemy>();
        foreach(Enemy enem in objects)
        {
            enem.enabled = false;
        }
    }

}
