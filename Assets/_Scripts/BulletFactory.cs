using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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