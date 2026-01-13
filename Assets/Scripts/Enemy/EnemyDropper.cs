using UnityEngine;

public class EnemyDropper : MonoBehaviour
{
    [SerializeField] private GameObject dropItemPrefab;
    [SerializeField] private Vector3 dropOffset = new Vector3(0, 0.5f, 0);

    public void DropItem()
    {
        if (dropItemPrefab == null)
        {
            Debug.LogWarning("No drop item prefab assigned on " + gameObject.name);
            return;
        }

        Instantiate(dropItemPrefab, transform.position + dropOffset, Quaternion.identity);
    }
}
