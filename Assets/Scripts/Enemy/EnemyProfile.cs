using UnityEngine;
using System.Collections;

public class EnemyProfile : MonoBehaviour {
    public int Health, Damage, Speed;
    public string Special; //Dodge, Regen, Fly, Ground, Slow, Shield, Troll, Shooter, Ninja, Healer

   /* void ActiveSpecial()
    {
        if (Special == "Dodge")
        {

        } else
       if (Special == "Regen")
        {

        } else
       if (Special == "Fly")
        {

        }
        if (Special == "Ground")
        {

        }
        else
        if (Special == "Slow")
        {

        }
        else
        if (Special == "Shield")
        {

        }
        else
        if (Special == "Troll")
        {

        }
        else
        if (Special == "Shooter")
        {

        }
        else
        if (Special == "Ninja")
        {

        }
        else
        if (Special == "Healer")
        {

        }
    }*/

    void Death()
    {
        Destroy(gameObject);
        //tower.GetComponent<TowerAttack>().targetEnemy = null;
    }

    public void TakingDamage(int Damage)
    {
        Health -= Damage;
        if (Health <= 0)
        {
            Death();
        }
    }
    void OnTriggerEnter2D(Collider2D obj){
      if (obj.gameObject.tag == "BULLET"){
        TakingDamage(1);
        Destroy(obj);

      }

    }

        // Use this for initialization
        void Start () {

	}

	// Update is called once per frame
	void Update () {

    }
}
