using UnityEngine;
[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
public class Tiling : MonoBehaviour {

    SpriteRenderer sr;

	// Use this for initialization
	void Start () {
	    sr = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update () {
        sr.sharedMaterial.SetFloat("RepeatX", Mathf.Round(transform.localScale.x));
        sr.sharedMaterial.SetFloat("RepeatY", Mathf.Round(transform.localScale.y));

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Round(transform.localScale.x);
        scale.y = Mathf.Round(transform.localScale.y);
        transform.localScale = scale;

        Vector3 pos = transform.position;
        pos.x = Mathf.Round(pos.x);
        pos.y = Mathf.Round(pos.y);
        transform.position = pos;
	}
}
