using UnityEngine;
[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
public class Tiling : MonoBehaviour {
    SpriteRenderer sr;

    bool scaling = false;
    Vector3 lastScale;

	// Use this for initialization
	void Start () {
	    sr = GetComponent<SpriteRenderer>();
        lastScale = transform.localScale;
	}

	// Update is called once per frame
	void Update () {
        if (!Application.isPlaying)
        {
            sr.sharedMaterial.SetFloat("RepeatX", Mathf.Round(transform.localScale.x));
            sr.sharedMaterial.SetFloat("RepeatY", Mathf.Round(transform.localScale.y));

            Vector3 scale = transform.localScale;
            scale.x = Mathf.Round(transform.localScale.x);
            scale.y = Mathf.Round(transform.localScale.y);
            transform.localScale = scale;

            Vector3 pos = transform.position;
            pos.x = Mathf.RoundToInt(pos.x);
            pos.y = Mathf.RoundToInt(pos.y);
            transform.position = pos;
        }
	}
}
