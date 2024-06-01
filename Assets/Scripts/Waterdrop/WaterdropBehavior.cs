using System.Collections;
using UnityEngine;

public class WaterdropBehavior : MonoBehaviour, IWaterCollectable
{
    WaterdropSpawnBehavior _waterdropManager;

    private void Awake()
    {
        _waterdropManager = FindObjectOfType<WaterdropSpawnBehavior>();
    }

    private void OnEnable()
    {
        SetWaterdropActivity(true);
    }

    public float CollectWater(Transform collecter)
    {
        SetWaterdropActivity(false);

        StartCoroutine(MoveToCollecter(collecter));

        return 1.0f;
    }

    private void SetWaterdropActivity(bool isEnabled)
    {
        GetComponent<Collider2D>().enabled = isEnabled;
    }

    private IEnumerator MoveToCollecter(Transform collecter)
    {
        while (Vector2.Distance(transform.position, collecter.position) > 0.1f)
        {
            Vector3 direction = (collecter.position - transform.position).normalized;
            transform.position = transform.position + direction * Time.deltaTime * 5.0f;
            yield return null;
        }

        _waterdropManager.DespawnWaterdrop(gameObject);
    }
}

