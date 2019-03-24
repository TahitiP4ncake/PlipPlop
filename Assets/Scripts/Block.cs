using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
   private void Start()
   {
      Ground();
   }


   public void Possess()
   {
      
   }

   public void Drop(bool _grounded)
   {
      if (_grounded)
      {
         Ground();
      }
   }

   void Ground()
   {
      RaycastHit hit;
      if (Physics.Raycast(transform.position + new Vector3(0,.1f,0), Vector3.down, out hit, 10))
      {
         transform.SetParent(hit.collider.gameObject.transform);
      }
   }
}
