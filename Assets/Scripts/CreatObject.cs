using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatObject : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] objects;
    private float crtTime;
    void Start()
    {
        StartCoroutine("creatObj");
        
    }
    IEnumerator creatObj ()
    {
        while(true)
        {
            crtTime = Random.Range(5f,9f);
            if(crtTime < 8f)
            {
                int rdmObj = Random.Range(0, objects.Length);
                GameObject creationObj = objects[rdmObj];
                Instantiate 
                (
                    creationObj, 
                    new Vector3 (this.transform.position.x , creationObj.transform.position.y ,this.transform.position.z), 
                    creationObj.transform.rotation
                );
            }
            yield return new WaitForSeconds(crtTime);
        }
    }

}
