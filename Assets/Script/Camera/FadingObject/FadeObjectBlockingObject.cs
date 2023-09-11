using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeObjectBlockingObject : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform _target;
    [SerializeField] private Camera _camera;
    [SerializeField] [Range(0, 1f)] private float _fadeAlpha = default;
    [SerializeField] [Range(0, 5f)] private float _fadeSpeed = default;
    [SerializeField] private bool _retainShadows = true; 
    [SerializeField] private Vector3 _targetPositionOffset = Vector3.up;

    public TMPro.TextMeshProUGUI _textMeshPro;
    private List<FadingObject> _objectsBlockingView = new List<FadingObject>();
    private Dictionary<FadingObject, Coroutine> _runningCoroutines = new Dictionary<FadingObject, Coroutine>();
    private RaycastHit[] _hits = new RaycastHit[10];

    private void Awake()
    {
        StartCoroutine(CheckForObjects());
    }

    private IEnumerator CheckForObjects()
    {
        while (true) 
        {
            int hits = Physics.RaycastNonAlloc(_camera.transform.position, (_target.transform.position + _targetPositionOffset - _camera.transform.position).normalized, _hits, Vector3.Distance(_camera.transform.position, _target.transform.position + _targetPositionOffset), _layerMask);
          
            if (hits > 0)
            {
                for (int i = 0; i < hits; i++)
                {
                    FadingObject fadingObject = GetFadingObjectFromHit(_hits[i]);

                    if (fadingObject != null && !_objectsBlockingView.Contains(fadingObject))
                    {
                        if (_runningCoroutines.ContainsKey(fadingObject))
                        {
                            if (_runningCoroutines[fadingObject] != null)
                            {
                                StopCoroutine(_runningCoroutines[fadingObject]);
                            }

                            _runningCoroutines.Remove(fadingObject);
                        }

                        _runningCoroutines.Add(fadingObject, StartCoroutine(FadeObjectOut(fadingObject)));
                        _objectsBlockingView.Add(fadingObject);
                    }
                }
            }

            FadeObjectsNoLongerBeingHit();

            ClearHits();

            yield return null;
        }
    }

    private void FadeObjectsNoLongerBeingHit()
    {
        List<FadingObject> objectsToRemove = new List<FadingObject>(_objectsBlockingView.Count);

        foreach (FadingObject fadingObject in _objectsBlockingView)
        {
            bool objectIsBeingHit = false;
            for (int i = 0; i < _hits.Length; i++)
            {
                FadingObject hitFadingObject = GetFadingObjectFromHit(_hits[i]);
                if (hitFadingObject != null && fadingObject == hitFadingObject)
                {
                    objectIsBeingHit = true;
                    break;
                }
            }

            if (!objectIsBeingHit)
            {
                if (_runningCoroutines.ContainsKey(fadingObject))
                {
                    if (_runningCoroutines[fadingObject] != null)
                    {
                        StopCoroutine(_runningCoroutines[fadingObject]);
                    }
                    _runningCoroutines.Remove(fadingObject);
                }

                _runningCoroutines.Add(fadingObject, StartCoroutine(FadeObjectIn(fadingObject)));
                 objectsToRemove.Add(fadingObject);
            }
        }

        foreach(FadingObject removeObject in objectsToRemove)
        {
            _objectsBlockingView.Remove(removeObject);
        }
    }

    private IEnumerator FadeObjectOut(FadingObject FadingObject)
    {
        foreach (Material material in FadingObject.Materials)
        {
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.SetInt("_Surface", 1);

            material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

            material.SetShaderPassEnabled("DepthOnly", false);
            material.SetShaderPassEnabled("SHADOWCASTER", _retainShadows);

            material.SetOverrideTag("RenderType", "Transparent");

            material.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
            material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        }

        float time = 0;

        while (FadingObject.Materials[0].color.a > _fadeAlpha)
        {
            foreach (Material material in FadingObject.Materials)
            {
                if (material.HasProperty("_Color"))
                {
                    material.color = new Color(
                        material.color.r,
                        material.color.g,
                        material.color.b,
                        Mathf.Lerp(FadingObject.InitialAlpha, _fadeAlpha, time * _fadeSpeed)
                    );
                    _textMeshPro.text = material.color.ToString();
                }
            }

            time += Time.deltaTime;
            yield return null;
        }

        if (_runningCoroutines.ContainsKey(FadingObject))
        {
            StopCoroutine(_runningCoroutines[FadingObject]);
            _runningCoroutines.Remove(FadingObject);
        }
    }

    private IEnumerator FadeObjectIn(FadingObject FadingObject)
    {
        float time = 0;

        while (FadingObject.Materials[0].color.a < FadingObject.InitialAlpha)
        {
            foreach (Material material in FadingObject.Materials)
            {
                if (material.HasProperty("_Color"))
                {
                    material.color = new Color(
                        material.color.r,
                        material.color.g,
                        material.color.b,
                        Mathf.Lerp(_fadeAlpha, FadingObject.InitialAlpha, time * _fadeSpeed)
                    );
                }
            }

            time += Time.deltaTime;
            yield return null;
        }

        foreach (Material material in FadingObject.Materials)
        {
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            material.SetInt("_ZWrite", 1);
            material.SetInt("_Surface", 0);

            material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;

            material.SetShaderPassEnabled("DepthOnly", true);
            material.SetShaderPassEnabled("SHADOWCASTER", true);

            material.SetOverrideTag("RenderType", "Opaque");

            material.DisableKeyword("_SURFACE_TYPE_TRANSPARENT");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        }

        if (_runningCoroutines.ContainsKey(FadingObject))
        {
            StopCoroutine(_runningCoroutines[FadingObject]);
            _runningCoroutines.Remove(FadingObject);
        }
    }

    private void ClearHits()
    {
        System.Array.Clear(_hits, 0, _hits.Length);
    }

    private FadingObject GetFadingObjectFromHit(RaycastHit Hit)
    {
        return Hit.collider != null ? Hit.collider.GetComponent<FadingObject>() : null;
    }
}
