using UnityEngine;

public class Toggle : MonoBehaviour
{
    [SerializeField] private GameObject object1;   // GameObject, used to link to it to be capable to disable/enable it
    [SerializeField] private GameObject object2;   // GameObject, used to link to it to be capable to disable/enable it
    [SerializeField] private GameObject object3;   // GameObject, used to link to it to be capable to disable/enable it


    public void Object1T()
    {
        object1.SetActive(true);   //Enable 
        object2.SetActive(false);  //Disable 
    }

    public void Object2T()
    {
        object1.SetActive(false);  //Disable 
        object2.SetActive(true);   //Enable 
    }

    public void Objects1T2F()
    {
        object1.SetActive(true);     //Enable 
        object2.SetActive(false);  //Disable 
        object3.SetActive(false);  //Disable 
    }
}
