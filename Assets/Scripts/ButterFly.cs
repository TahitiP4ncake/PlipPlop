using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterFly : MonoBehaviour
{
    public Transform target;

    private Vector3 offset;


    public float maxBreakTime;

    private float breakTime;

    private float timer;

    public float maxFlyTimer;
    private float flyTime;
    
    
    public bool flying;

    public bool canTakeBreak;

    public float switchDirection;


    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        target = transform.parent;
        transform.parent = null;
        offset = transform.position - target.position;
        
        flyTime = Random.Range(maxFlyTimer / 2, maxFlyTimer);
        StartCoroutine(ChangeDirection());
    }

    IEnumerator ChangeDirection()
    {
        while (true)
        {
            direction = Random.insideUnitSphere * 3;
            
            
            yield return new WaitForSecondsRealtime(Random.Range(switchDirection/2,switchDirection*2));
        }
    }

    void FixedUpdate()
    {

        transform.forward = Vector3.Lerp(transform.forward, ((target.position + offset + direction) - transform.position).normalized, Time.fixedDeltaTime*5);
        
        
        if (!canTakeBreak)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + offset + direction + Random.insideUnitSphere*2, Time.fixedDeltaTime);
            return;
        }

            
        
        

        timer += Time.fixedDeltaTime;

        if (flying)
        {
            if (timer >= flyTime)
            {
                timer = 0;
                flying = false;

                breakTime = Random.Range(maxBreakTime / 2, maxBreakTime);
            }
            
            transform.position = Vector3.Lerp(transform.position, target.position + offset + direction + Random.insideUnitSphere*2, Time.fixedDeltaTime);

        }
        else
        {
            if (timer >= breakTime)
            {
                timer = 0;

                flying = true;
                
                flyTime = Random.Range(maxFlyTimer / 2, maxFlyTimer);

            }
            
            transform.position = Vector3.Lerp(transform.position, target.position , Time.fixedDeltaTime);

        }
        
    }
    
    
}
