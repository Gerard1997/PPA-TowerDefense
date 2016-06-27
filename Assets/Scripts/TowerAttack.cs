using UnityEngine;
using System.Collections;


public class TowerAttack : MonoBehaviour {


public GameObject bullet;
public float bulletSpeed;
public float attackSpeed;
private GameObject bulletG;
private  float xPos;
private float yPos;
private float unitFactor;
private float rotationZ;
public GameObject wave;
private float timer = 0;
private GameObject[] enemyes;
public float towerRange;

	// Use this for initialization
	void Start () {
		wave = GameObject.Find("Main Camera");
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (wave.GetComponent<WaveScreen>().onPlay){
			StartCoroutine(SelectTarget());
			if (targetEnemy != null){
				timer += Time.deltaTime;
				//Rotacion
				rotationZ = Mathf.Atan2((targetEnemy.transform.position.y - transform.position.y), (targetEnemy.transform.position.x - transform.position.x))* Mathf.Rad2Deg;
				transform.eulerAngles = new Vector3(0,0,rotationZ);
				//Ataque
				if (timer >= attackSpeed){
					bulletG = Instantiate(bullet,transform.position,transform.rotation) as GameObject;
					Debug.Log(targetEnemy);
					xPos = targetEnemy.transform.position.x - transform.position.x;
					yPos = targetEnemy.transform.position.y - transform.position.y;
					unitFactor = Mathf.Sqrt((xPos*xPos)+(yPos*yPos));
					bulletG.GetComponent<Rigidbody2D>().AddForce(new Vector2(xPos/unitFactor,yPos/unitFactor) * 10 * bulletSpeed);
					DamageEnemy();
					Destroy(bulletG, 3);
					timer=0;
				}
			}else{
				transform.eulerAngles = new Vector3(0,0,0);
			}
		}
	}

	private GameObject enemy;
	private GameObject targetEnemy;
	private Vector3 pos;
	private float relativePosX;
	private float relativePosY;
	private float dist;
	private float dist2 = 0;
	private bool areEnemy = true;

	IEnumerator SelectTarget(){
		enemyes = GameObject.FindGameObjectsWithTag("ENEMY");
		if (enemyes.Length > 1){
			foreach (GameObject enemy in enemyes){
				pos = enemy.transform.position;
				dist2 = dist;
				relativePosX = pos.x - transform.position.x;
				relativePosY = pos.y - transform.position.y;
				dist = Mathf.Sqrt(relativePosX*relativePosX  + relativePosY*relativePosY);
				if (dist < dist2 && dist < towerRange){
					targetEnemy = enemy;
				}
			}
		} else if (enemyes.Length == 1){
			pos = enemyes[0].transform.position;
			relativePosX = pos.x - transform.position.x;
			relativePosY = pos.y - transform.position.y;
			dist = Mathf.Sqrt(relativePosX*relativePosX  + relativePosY*relativePosY);
			if(dist < towerRange){
				Debug.Log(dist);
				targetEnemy = enemyes[0];
				Debug.Log(targetEnemy);
			}else{
				targetEnemy = null;
			}
		}
		yield return new WaitForSeconds(0.1f);
	}

	void DamageEnemy(){
		//TODO: daño a enemigos
		if (targetEnemy != null){
				targetEnemy.GetComponent<EnemyProfile>().TakingDamage(1);
		}

	}


}
