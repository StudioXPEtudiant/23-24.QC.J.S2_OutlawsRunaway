using UnityEngine;
using UnityEngine.UI;
 
public class JetPack : MonoBehaviour
{
    public float jetpackForce = 10f;
    public float heightLimit = 11.5f;
    public float fuel = 100f;
    public float fuelDecreaseRate = 10f;
    public float fuelDecreaseDelay = 1f;
    public float fuelRefillRate = 10f;
    public float fuelRefillDelay = 3f;
    public FuelDisplay fuelDisplay;
 
    private Rigidbody rb;
    private bool isHoldingSpacebar = false;
    private float spacebarHeldDuration = 0f;
    private float timeSinceFuelDepletion = 0f;
    private float fuelRefillDelayLeft = 0f;
 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fuelDisplay.UpdateFuelBar(fuel, 100f);
    }
 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isHoldingSpacebar = true;
        }
 
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isHoldingSpacebar = false;
            spacebarHeldDuration = 0f;
        }
    }
 
    void FixedUpdate()
    {
        if (isHoldingSpacebar && fuel > 0f)
        {
            spacebarHeldDuration += Time.fixedDeltaTime;
            if (spacebarHeldDuration > fuelDecreaseDelay)
            {
                fuel -= fuelDecreaseRate * Time.fixedDeltaTime;
                fuelDisplay.UpdateFuelBar(fuel, 100f);
            }
            rb.AddForce(Vector3.up * jetpackForce, ForceMode.Acceleration);
        }
        else
        {
            spacebarHeldDuration = 0f;
        }
 
        if (fuel <= 0f)
        {
            timeSinceFuelDepletion += Time.fixedDeltaTime;
            if (timeSinceFuelDepletion > fuelRefillDelay)
            {
                fuelRefillDelayLeft = fuelRefillDelay;
            }
        }
        else
        {
            timeSinceFuelDepletion = 0f;
        }
 
        if (fuelRefillDelayLeft > 0f)
        {
            fuelRefillDelayLeft -= Time.fixedDeltaTime;
            fuel = Mathf.Clamp(fuel + fuelRefillRate * Time.fixedDeltaTime, 0f, 100f);
            fuelDisplay.UpdateFuelBar(fuel, 100f);
        }
 
        if (transform.position.y > heightLimit)
        {
            transform.position = new Vector3(transform.position.x, heightLimit, transform.position.z);
        }
 
        fuelDisplay.UpdateFuelBar(100f, fuel);
    }
}