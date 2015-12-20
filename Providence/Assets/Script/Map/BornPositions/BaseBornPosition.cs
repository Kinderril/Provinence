using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class BaseBornPosition : MonoBehaviour
{

    public float chanceToWork = 0.9f;
    public float radius = 2;
    protected Map map;
    public int unitsCout = 1;
    protected bool work = false;
    public int ID;

    public virtual void Init(Map map)
    {
        this.map = map;
        var mesh = GetComponent<MeshRenderer>();
        if (mesh != null)
        {
            mesh.enabled = false;
        }
        work = UnityEngine.Random.Range(0f, 1f) < chanceToWork;
    }
}

