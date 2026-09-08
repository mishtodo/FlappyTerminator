using System.Collections;
using UnityEngine;

public class Laserer : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private LayerMask _laserMask;
    [SerializeField] private float _laserDistance = 20f;
    [SerializeField] private float _laserDuration = 0.2f;

    private Coroutine _laserVisualRoutine;

    public void ShootLaser()
    {
        Vector2 direction = _firePoint.right;
        Vector3 endPosition;

        RaycastHit2D hit = Physics2D.Raycast(_firePoint.position, direction, _laserDistance, _laserMask);

        if (hit.collider != null)
        {
            endPosition = hit.point;

            // if (hit.collider.TryGetComponent(out EnemyHealth enemy)) { enemy.TakeDamage(10); }
            //Debug.Log($"Лазер попал в: {hit.collider.name}");
        }
        else
        {
            endPosition = _firePoint.position + (Vector3)(direction * _laserDistance);
        }

        if (_laserVisualRoutine != null) StopCoroutine(_laserVisualRoutine);
        RestartCoroutine(endPosition);
    }
        
    public void StopCoroutine()
    {
        if (_laserVisualRoutine != null)
            StopAllCoroutines();
    }

    private void RestartCoroutine(Vector3 end)
    {
        StartCoroutine(ShowLaserRoutine(_firePoint.position, end));
    }

    private IEnumerator ShowLaserRoutine(Vector3 start, Vector3 end)
    {
        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(0, start);
        _lineRenderer.SetPosition(1, end);

        yield return new WaitForSeconds(_laserDuration);

        _lineRenderer.enabled = false;
    }
}
