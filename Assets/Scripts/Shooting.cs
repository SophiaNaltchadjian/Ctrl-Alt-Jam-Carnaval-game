using UnityEngine;

public class Shooting : MonoBehaviour
{
        [SerializeField] private GameObject projetil;
    private Camera cam;
    private Vector3 mousePos;
    public Transform bulletTransform;
    public bool canFire;
    private float timer;
    public float timeBetweenfiring;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ );

        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenfiring)
            {
                canFire = true;
                timer = 0;
            }
        }

        if (Input.GetMouseButtonDown(0) && canFire)
        {
            canFire = false;
            Instantiate(projetil, bulletTransform.transform.position, transform.rotation);
        }
    }
        }

