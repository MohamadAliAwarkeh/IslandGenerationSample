using UnityEngine;

public class RaycastChecker : MonoBehaviour
{
    [Header("General Variables")]
    [SerializeField] private ProceduralPresets proceduralPresets;
    [SerializeField] private string tag;
    [SerializeField] private float rayDistance;

    //Components
    private IslandGeneration islandGeneration;
    private Transform parent;
    [SerializeField]private bool hasBeenPlaced;

    private void Start()
    {
        islandGeneration = IslandGeneration.instance.GetComponent<IslandGeneration>();
        parent = transform.parent;
    }

    private void Update() => AlignPosition();

    private void AlignPosition()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, rayDistance))
        {
            if (hit.collider.CompareTag(tag) && !hasBeenPlaced)
            {
                hasBeenPlaced = true;
                transform.position = new Vector3(transform.position.x, hit.point.y + 1f, transform.position.z);
                islandGeneration.CurrentAmountOfItems++;
            }
            if (!hit.collider.CompareTag(tag) && !hasBeenPlaced)
                transform.position = RepositionObject();
        }
    }

    private Vector3 RepositionObject()
    {
        return new Vector3(
            Random.Range(-proceduralPresets.xSpawnPos, proceduralPresets.xSpawnPos),
            parent.position.y,
            Random.Range(-proceduralPresets.zSpawnPos, proceduralPresets.zSpawnPos));
    }
}

public enum RaycastTags
{
    Island,
    Sand
}


