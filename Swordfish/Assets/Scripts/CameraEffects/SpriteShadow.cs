using UnityEngine;

public class SpriteShadow : MonoBehaviour
{

    public Vector2 offset = new Vector2(-4f, -4f);
    public Material shadowMaterial;
    public Color shadowColor = new Color(29, 23, 43);

    SpriteRenderer sprRndCaster;
    SpriteRenderer sprRndShadow;
    Transform transCaster;
    Transform transShadow;
    
    private void Start()
    {
        transCaster = transform;
        sprRndCaster = GetComponent<SpriteRenderer>();

        // Creates the shadow gameobject.
        GameObject shadow = new GameObject();
        shadow.name = gameObject.name + " Shadow";
        transShadow = shadow.transform;
        transShadow.parent = transCaster;
        sprRndShadow = shadow.AddComponent<SpriteRenderer>();
        // Assigns the material and color.
        sprRndShadow.material = shadowMaterial;
        sprRndShadow.color = shadowColor;
        // Puts the shadow underneath the parent.
        sprRndShadow.sortingLayerName = "Shadow";
        sprRndShadow.sortingOrder = sprRndCaster.sortingOrder - 1;

    }

    private void LateUpdate()
    {
        transShadow.position = (Vector2)transCaster.position + offset;
        sprRndShadow.sprite = sprRndCaster.sprite;
    }

}
