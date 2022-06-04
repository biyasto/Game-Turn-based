using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveSet : MonoBehaviour
{
    // Start is called before the first frame update

    protected Unit player;
    public int cd1Max;
    public int cd2Max;
    public int cdUltMax;

    public string moveName1;
    public string moveName2;
    public string moveNameUlt;
    //protected
    private int _cd1 = 0;
    private int _cd2 = 0;
    private int _cdUlt = 0;
    protected virtual void Start()
    {
        player = GetComponent<Unit>();
        _cdUlt = cdUltMax;
    }

    public  void ReduceCd()
    {
        if (_cd1 > 0) _cd1--;
        else _cd1 = 0;
        if (_cd2 > 0) _cd2--;
        else _cd2 = 0;
        if (_cdUlt > 0) _cdUlt--;
        else _cdUlt = 0;
    }
    protected  bool CheckCd(int index)
    {
        switch (index)
        {
            case 1: if(_cd1==0) {SetCd(index); return true;}

                break;
            case 2: if(_cd2==0) {SetCd(index); return true;}

                break;
            case 3: if(_cdUlt==0) {SetCd(index); return true;}

                break;
        }

        return false;
    }
    public virtual bool Move1()
    {
        if (!CheckCd(1)) return false;
        SetCd(1);
        return true;
    }
    
    public virtual bool Move2()
    {
        if (!CheckCd(2)) return false;
        SetCd(2);
        return true;
    }

  
    public virtual bool Ultimate()
    {
        if (!CheckCd(3)) return false;
        SetCd(3);
        return true;
    }
    protected  void SetCd(int index)
    {
        switch (index)
        {
            case 1: if(_cd1==0) _cd1 = cd1Max;
                break;
            case 2: if(_cd2==0) _cd2 = cd2Max;
                break;
            case 3: if (_cdUlt == 0) _cdUlt = cdUltMax;
                break;
        }
    }

    
}
