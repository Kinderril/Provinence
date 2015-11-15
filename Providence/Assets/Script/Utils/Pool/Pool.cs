using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Pool
{
    private List<PoolElement> FlyNumberInUIpool = new List<PoolElement>();
    private List<PoolElement> FlyNumberInGamepool = new List<PoolElement>();
    private DataBaseController dataBaseController;

    public Pool(DataBaseController dataBaseController)
    {
        this.dataBaseController = dataBaseController;
        Prewarm();
    }

    private void Prewarm()
    {
        for (int i = 0; i < 10; i++)
        {
            var element = dataBaseController.GetItem(dataBaseController.FlyNumberWIthDependence);
            element.gameObject.SetActive(false);
            FlyNumberInGamepool.Add(element);
        }
        for (int i = 0; i < 10; i++)
        {

            var element = dataBaseController.GetItem(dataBaseController.FlyingNumber);
            element.gameObject.SetActive(false);
            FlyNumberInUIpool.Add(element);
        }
    }

    public T GetItemFromPool<T>(PoolType poolType,Vector3 pos = default(Vector3)) where T :PoolElement
    {
        PoolElement element = null;
        switch (poolType)
        {
            case PoolType.flyNumberInGame:
                element = GetNoUsed(FlyNumberInGamepool);
                //element = FlyNumberInGamepool.FirstOrDefault(x => !x.IsUsing);
                if (element == null)
                {
                    Debug.Log("Creat new");
                    element = dataBaseController.GetItem(dataBaseController.FlyNumberWIthDependence);
                    FlyNumberInGamepool.Add(element );
                }
                break;
            case PoolType.flyNumberInUI:
                element = GetNoUsed(FlyNumberInUIpool);
                //element = FlyNumberInUIpool.FirstOrDefault(x => !x.IsUsing) ;
                if (element == null)
                {
                    Debug.Log("Creat new");
                    element = dataBaseController.GetItem(dataBaseController.FlyingNumber);
                    FlyNumberInUIpool.Add(element );
                }
                break;
        }
        element.transform.localPosition = pos;
        element.Init();
        return element as T;
    }

    private PoolElement GetNoUsed(List<PoolElement> lis)
    {
        for (int i = 0; i < lis.Count; i++)
        {
            var e = lis[i];
            if (!e.IsUsing)
                return e;
        }
        return null;
    }
}

