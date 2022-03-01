using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public enum CustomerType
    {
        YOUNG=4,
        OLD=2
    }

    public enum CustomerState
    {
        INLINE,
        TRANSACTION,
        LEAVE
    }



    [SerializeField]
    private float distance;
    [SerializeField]
    private GameObject target;

    [Header("Movement")]
    public float maxDistanceWithCustomer = 1.0f;
    public float maxDistanceWithATM = 0.6f;
    public CustomerType currentCustomerType;
    public float baseSpeed = 0.1f;

    [Header("Customer State")]
    public CustomerState currentCustomerState = CustomerState.INLINE;

    [Header("Transaction State")]
    private List<int> TransactionComplexity = new List<int>() { 1, 2, 3 };
    public float currentSeconds;
    private float transactionTotalTime;

    private RaycastHit hit;
    private bool flag = true;



    // Update is called once per frame
    void Update()
    {
        if (this.CompareTag("old")) currentCustomerType = CustomerType.OLD;
        if (this.CompareTag("young")) currentCustomerType = CustomerType.YOUNG;


        if (Physics.Raycast(transform.position, -Vector3.right, out hit))
        {
            distance = hit.distance;
            target = hit.collider.gameObject;
            Debug.DrawRay(transform.position, hit.collider.transform.position - transform.position, Color.red);

            // inline stage
            if (currentCustomerState == CustomerState.INLINE)
            {
                print("Inline");
                if ((hit.collider.CompareTag("old") || hit.collider.CompareTag("young")) && Vector3.Distance(hit.collider.transform.position, transform.position) > maxDistanceWithCustomer)
                {
                    CustomerFollow();
                }
                else if (hit.collider.CompareTag("ATM") && Vector3.Distance(hit.collider.transform.position, transform.position) > maxDistanceWithATM)
                {
                    CustomerFollow();
                }
            }
            
        }

        // Transaction stage
        if (currentCustomerState == CustomerState.TRANSACTION)
        {
            print("Transaction");
            TransactionTime();
        }

        // Leave state
        if (currentCustomerState == CustomerState.LEAVE)
        {
            LeaveToExit();
        }

    }

    private void LeaveToExit()
    {
        this.transform.Translate(Vector3.back * Time.deltaTime * baseSpeed * (int)currentCustomerType);
    }

    private bool TransactionTime()
    {
        int index = 0;
        if (flag)
        {
            index = Random.Range(0, TransactionComplexity.Count);
            transactionTotalTime = TransactionComplexity[index];
            print(transactionTotalTime);
            flag = false;
        }


        if (currentSeconds < transactionTotalTime)
        {
            currentSeconds += Time.deltaTime;
            return false;
        }

        currentCustomerState = CustomerState.LEAVE;
        currentSeconds = 0;
        return true;
        
    }

    private void CustomerFollow()
    {
        transform.position = Vector3.MoveTowards(transform.position,
            hit.collider.transform.position,
            Time.deltaTime * baseSpeed * (int) currentCustomerType);
        if(hit.collider.CompareTag("ATM") && hit.distance < 0.4f)
        {
            currentCustomerState = CustomerState.TRANSACTION;
        }
    }
}
