using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class TrailTracker : MonoBehaviour
{
    [SerializeField] private ScreenWrapper _wrapper;
    
    private TrailRenderer _trailRenderer;

    private void Awake()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    private void OnEnable()
    {
        _wrapper.Wrapped += OnWrapped;
    }

    private void OnDisable()
    {
        _wrapper.Wrapped -= OnWrapped;
    }

    private void OnWrapped()
    {
        _trailRenderer.Clear();
    }
}
