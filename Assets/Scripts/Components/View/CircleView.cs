using UnityEngine;
using System.Collections;

public class CircleView : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private CircleCollider2D _collider2D;
    [SerializeField] SpriteRenderer _renderer;
    [SerializeField] private ParticleSystem _onDestroyFX;

    public int ColorIndex { get; private set; }
    public Vector2 PosInMatrix { get; private set; }
    public void Init(Color color, int colorIndex)
    {
        _onDestroyFX.startColor = color;
        _renderer.color = color;
        ColorIndex = colorIndex;
    }

    public void SetMatrixPos(Vector2 pos)
    {
        PosInMatrix = pos;
    }

    public void Undocking()
    {
        _rb.isKinematic = false;
        transform.parent = null;
    }

    public void Destroy()
    {
        if (_onDestroyFX != null) _onDestroyFX.Play();
        _rb.isKinematic = true;
        StartCoroutine(DestroyAfterDelay());
        
      
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(0.5f); 
        Destroy(this.gameObject);
    }
}
