using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonRotation : MonoBehaviour
{
    public Vector3 _maxRotation = new Vector3(0,0,65);
    public Vector3 _minRotation = new Vector3(0,0,0);
    private float offset = -51.6f;
    public GameObject ShootPoint;
    public GameObject Bullet;
    public float ProjectileSpeed = 0;
    public float MaxSpeed;
    public float MinSpeed;
    public GameObject PotencyBar;
    private float initialScaleX;
    private float pressedKeyTime = 0f;
    private float noPressedKey = 0f;
    Vector2 velocity;

    private void Awake()
    {
        initialScaleX = PotencyBar.transform.localScale.x;
    }
    void Update()
    {
        //PISTA: mireu TOTES les variables i feu-les servir
        var mousePos = Input.mousePosition;
        var direction = new Vector2(mousePos.x, mousePos.y);
        var angle = (Mathf.Atan2(direction.y, direction.x) * 180f / Mathf.PI + offset);
        if(angle > _maxRotation.z) 
        {
            transform.rotation = Quaternion.Euler(_maxRotation);
        }
        if(angle < _minRotation.z) 
        {
            transform.rotation = Quaternion.Euler(_minRotation);
        }
        else 
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        if (Input.GetMouseButton(0))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)) 
            {
				pressedKeyTime = Time.time;
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            noPressedKey = Time.time;
            ProjectileSpeed = (noPressedKey - pressedKeyTime) * 0.4f;
            var projectile = Instantiate(Bullet, ShootPoint.transform.position, Quaternion.identity);
            velocity = ProjectileSpeed * direction;
            if(velocity.x > MaxSpeed)
            {
                velocity.x = MaxSpeed;
            }
            Debug.Log(velocity);
            projectile.GetComponent<Rigidbody2D>().velocity = velocity; 
            ProjectileSpeed = 0f;
        }
        CalculateBarScale();
    }
    public void CalculateBarScale()
    {
        PotencyBar.transform.localScale = new Vector3(Mathf.Lerp(0, initialScaleX, ProjectileSpeed / MaxSpeed),
            PotencyBar.transform.localScale.y,
            PotencyBar.transform.localScale.z);
    }
}