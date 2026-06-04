using UnityEngine;

public class HighlightOnApproach : MonoBehaviour
{
    public float radius = 3f;
    public Material highlightMaterial;
    private Material originalMat;
    private Renderer rend;
    private Transform player;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalMat = rend.material;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);
        rend.material = dist < radius ? highlightMaterial : originalMat;
    }
}