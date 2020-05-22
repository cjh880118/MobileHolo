using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABC : MonoBehaviour
{
    List<int> listtest = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        listtest.Add(0);
        listtest.Add(1);
        listtest.Add(2);
        listtest.Add(3);

        listtest.RemoveAt(0);
        listtest.RemoveAt(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
