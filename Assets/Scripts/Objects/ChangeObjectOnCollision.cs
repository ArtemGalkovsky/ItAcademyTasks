using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(SpriteRenderer))]
public class ChangeObjectOnCollision : MonoBehaviour
{
    [SerializeField] private bool _changeScale = false;
    [SerializeField] private bool _changeColor = false;
    [SerializeField] private bool _changeShape = false;
    
    [SerializeField] private Vector2 _scaleRange = new Vector2(0.3f, 3f);
    [SerializeField] private Sprite[] _shapeSprites;

    private SpriteRenderer _spriteRenderer;
    
    private void Awake()
    {
        if (_shapeSprites.Length <= 0 && _changeShape)
        {
            Debug.LogError("No shape sprite found");
        }
        
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D _)
    {
        if (_changeColor)
        {
            _spriteRenderer.color = new Color(Random.Range(0f, 255f)/255.0f, Random.Range(0f, 255f)/255.0f, Random.Range(0f, 255f)/255.0f);
        }

        if (_changeScale)
        {
            transform.localScale = Random.Range(_scaleRange.x, _scaleRange.y) * Vector3.one;
        }

        if (_changeShape)
        {
            _spriteRenderer.sprite = _shapeSprites[Random.Range(0, _shapeSprites.Length)];
        }
    }
}
