using UnityEngine;

public class LaserControl : MonoBehaviour
{
    public static LaserControl instance;

    private GameObject _laser;
    [SerializeField] private Behaviour _handLaserScript;
    
    [SerializeField] private LayerMask _laserActiveLayers;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SearchLaser();
		OnOffLaser(true);
    }

    private void Update()
    {
        Ray raycast = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(raycast, out hit, 30f, _laserActiveLayers))
        {
            OnOffLaser(true);
        }
        else
        {
            OnOffLaser(false);
        }
    }

    private void SearchLaser()
    {
        foreach (Transform child in transform)
        {
            if (child.name == "New Game Object")
            {
                _laser = child.gameObject;
                break;
            }
        }
    }

    private void OnOffLaser(bool active)
    {
        _laser.SetActive(active);
        _handLaserScript.enabled = active;
    }
}