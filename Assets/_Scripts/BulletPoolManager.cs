using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Bonus - make this class a Singleton!

[System.Serializable]
public class BulletPoolManager
{
    static BulletPoolManager privatePool;

    public static BulletPoolManager publicPool
    {
        get { if (privatePool == null) { privatePool = new BulletPoolManager(); } return privatePool; }
    }

    private BulletPoolManager()
    {
        sceneBullets = new List<List<GameObject>>();
        lastActive = new int[(int)BulletTypeEnum.TOTAL];

        for (int i = 0; i < (int)BulletTypeEnum.TOTAL; ++i)
        {
            lastActive[i] = 0;
            sceneBullets.Add(new List<GameObject>());
        }
        //Debug.Log(bullet);
    }

    public int bulletCluster = 30;
    int []lastActive;

    //TODO: create a structure to contain a collection of bullets
    List<List<GameObject>> sceneBullets;

    // Start is called before the first frame update
    void Start()
    {
        // TODO: add a series of bullets to the Bullet Pool

    }

    void InstantiateBulletcluster(Transform parent, BulletTypeEnum bulletType)
    {
        for (int i = 0; i < bulletCluster; ++i)
        {
            GameObject NEW_BULLET = BulletFactory.publicFactory.GetNewBullet(bulletType);
            //NEW_BULLET = GameObject.Instantiate(bullet);
            NEW_BULLET.name = "Inactive";
            NEW_BULLET.transform.SetParent(parent);
            sceneBullets[(int)bulletType].Add(NEW_BULLET);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    //TODO: modify this function to return a bullet from the Pool
    public GameObject GetBullet(Transform parent, BulletTypeEnum bulletType)
    {
        //Debug.Log(bulletType);
        if (bulletType == BulletTypeEnum.TOTAL)
            return null;

        int t = (int)bulletType;

        //int firstChecked = lastActive;
        for (int i = 0; i < sceneBullets[t].Count; ++lastActive[t], ++i)
        {
            if (lastActive[t] >= sceneBullets[t].Count)
                lastActive[t] -= sceneBullets[t].Count;
            if (!sceneBullets[t][lastActive[t]].activeSelf)
            {
                sceneBullets[t][lastActive[t]].SetActive(true);
                sceneBullets[t][lastActive[t]].name = "ACTIVE " + bulletType.ToString();
                return sceneBullets[t][lastActive[t]];
            }
        }

        int previousSize = sceneBullets[t].Count;

        InstantiateBulletcluster(parent, bulletType);
        sceneBullets[t][previousSize].SetActive(true);
        //sceneBullets[previousSize].transform.SetParent(parent);
        sceneBullets[t][previousSize].name = "ACTIVE " + bulletType.ToString();
        return sceneBullets[t][previousSize];

        //return bullet;
    }

    //TODO: modify this function to reset/return a bullet back to the Pool 
    public void ResetBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bullet.name = "Inactive";
        //bullet.transform.SetParent(null);
    }
}

public class BulletFactory
{
    static BulletFactory privateFactory;
    public static BulletFactory publicFactory
    {
        get { if (privateFactory == null) { privateFactory = new BulletFactory(); } return privateFactory; }
    }

    private BulletFactory()
    {
        bullet = Resources.Load<GameObject>("Prefabs/Bullet");
        pellet = Resources.Load<GameObject>("Prefabs/Pellet");
        jimmy = Resources.Load<GameObject>("Prefabs/Jimmy");
    }

    public GameObject bullet;
    public GameObject pellet;
    public GameObject jimmy;

    public GameObject GetNewBullet(BulletTypeEnum bulletType)
    {
        GameObject NEW_BULLET = null;

        switch (bulletType)
        {
            case BulletTypeEnum.BULLET:
                NEW_BULLET = GameObject.Instantiate(bullet);
                break;
            case BulletTypeEnum.PELLET:
                NEW_BULLET = GameObject.Instantiate(pellet);
                break;
            case BulletTypeEnum.JIMMY:
                NEW_BULLET = GameObject.Instantiate(jimmy);
                break;
            default:
                break;
        }

        return NEW_BULLET;
    }
}

public enum BulletTypeEnum
{
    BULLET,
    PELLET,
    JIMMY,
    TOTAL
}
