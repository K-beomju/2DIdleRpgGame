using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPool : MonoBehaviour
{
    [SerializeField]
    private Poolable poolObj_Goblin;
    [SerializeField]
    private Poolable poolObj_Skeleton;
    [SerializeField]
    private Poolable poolObj_Bat;


    [SerializeField]
    private int allocateCount;

    private Stack<Poolable> poolStack = new Stack<Poolable>();

    private int count;


    void Start()
    {

        Allocate();
    }
    public void Allocate()
    {

        for (int i = 0; i <= allocateCount; i++)
        {

            if (poolObj_Skeleton.enabled)
            {
                 Poolable allocateObj_Skeletone = Instantiate(poolObj_Skeleton, this.gameObject.transform);
                allocateObj_Skeletone.Create(this);
                poolStack.Push(allocateObj_Skeletone);


            }
            if (poolObj_Goblin.enabled)
            {
                 Poolable allocateObj_Goblin = Instantiate(poolObj_Goblin, this.gameObject.transform);
                allocateObj_Goblin.Create(this);
                poolStack.Push(allocateObj_Goblin);

            }
            if(poolObj_Bat.enabled)
            {
                Poolable allocateObj_Bat = Instantiate(poolObj_Bat, this.gameObject.transform);
                allocateObj_Bat.Create(this);
                poolStack.Push(allocateObj_Bat);


            }

        }
    }

    public GameObject Pop()
    {
        Poolable obj = poolStack.Pop();
        obj.gameObject.SetActive(true);
        return obj.gameObject;
    }
    public void Push(Poolable obj)
    {
        obj.gameObject.SetActive(false);
        poolStack.Push(obj);
    }

}
