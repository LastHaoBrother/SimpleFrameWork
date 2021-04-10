using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolTest : MonoBehaviour
{
    ObjectPool<A> pool = new ObjectPool<A>(GetA, RelaseA);



    // Start is called before the first frame update
    void Start()
    {
        Dictionary<int, A> dic = new Dictionary<int, A>(16);
        for (int i = 0; i < 1000; i++)
        {
            A a = pool.Get(); //从对象池中获取对象
            a.a = i;
            a.b = 3.5f;

            A item = null;
            if (dic.TryGetValue(a.a, out item))
            {
                pool.Release(item); //值会被覆盖，所以覆盖前收回对象
            }

            dic[a.a] = a;

            int removeKey = Random.Range(0, 10);
            if (dic.TryGetValue(removeKey, out item))
            {
                pool.Release(item); //移除时收回对象
                dic.Remove(removeKey);
            }
        }

        Dictionary<int, List<A>> dic2 = new Dictionary<int, List<A>>(1000);
        for (int i = 0; i < 1000; i++)
        {
            List<A> arrayA = ListPool<A>.Get(); // 从对象池中分配List内存空间

            dic2.Add(i, arrayA);

            List<A> item = null;
            int removeKey = Random.Range(0, 1000);
            if (dic2.TryGetValue(removeKey, out item))
            {
                ListPool<A>.Release(item); // 移除时收回对象
                dic.Remove(removeKey);
            }
        }
    }



    static void GetA(A a)
    {
        Debug.Log("g a");
    }

    static void RelaseA(A a)
    {
        Debug.LogWarning("r a");
    }
}

public class A
{
    public int a;
    public float b;
}
