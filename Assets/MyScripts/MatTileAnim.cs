using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatTileAnim : MonoBehaviour
{
    [SerializeField] private float scrollSpeedx = 0.5f;
    [SerializeField] private float scrollSpeedy = 0f;

    private Renderer thisRenderer;

    // Start is called before the first frame update
    void Start()
    {
        thisRenderer = GetComponent<Renderer> ();
    }

    // Update is called once per frame
    void Update()
    {
        float offsetx = Time.time * scrollSpeedx;
        float offsety = Time.time * scrollSpeedy;

        thisRenderer.material.SetTextureOffset("_BaseMap", new Vector2(offsetx, offsety));

    }
}
