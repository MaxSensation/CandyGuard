using UnityEngine;

public class BackgroundScrollerV2 : MonoBehaviour
{
    public float scroll_Speed;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        float y = Time.time * scroll_Speed * -1;
        Vector2 offset = new Vector2(0, y);
        meshRenderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
