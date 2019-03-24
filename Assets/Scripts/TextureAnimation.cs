using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureAnimation : MonoBehaviour
{
    public Renderer rend;
    public Material mat;

    public int lastTextureIndex;

    public Texture[] ARMC;
    public Texture[] normals;
    
    void Start()
    {
        rend.material = Instantiate(mat);
        mat = rend.sharedMaterial;
    }

    public void ChangeTexture()
    {
        print("Change texture");
        
        int _index = Random.Range(0, ARMC.Length);

        int _iterations = 0;
        
        while (_index == lastTextureIndex)
        {
            _index = Random.Range(0, ARMC.Length);

            
            //Sécurité
            _iterations++;
            if (_iterations > 10)
            {
                break;
            }
        }

        lastTextureIndex = _index;
        
        
        
        mat.SetTexture("_ARMC", ARMC[lastTextureIndex]);
        mat.SetTexture("_Normal", normals[lastTextureIndex]);
        
        
    }
}
