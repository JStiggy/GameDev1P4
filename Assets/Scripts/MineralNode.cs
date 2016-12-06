using UnityEngine;
using System.Collections;

public class MineralNode : MonoBehaviour {

    public Mesh exhaustedNode;
	public GameObject []chunks;
	public int numChunks = 5;
	public Material material;

	void start(){
		chunks = new GameObject[numChunks];
	}
	
    public void consumeNode()
    {
        gameObject.layer = 0;
        this.GetComponent<MeshFilter>().mesh = exhaustedNode;
		this.GetComponent<Renderer>().material.shader = Shader.Find("Diffuse");
		this.GetComponent<Renderer>().material = material;
		spawnChunks();
    }
	
	void spawnChunks(){
		for(int i=0;i<numChunks;i++){
			//spawn a random number of chunks
			int numSpawn = Random.Range(0,3);
			for(int j=0;j<numSpawn;j++){
				Object tmp = Instantiate(chunks[i], new Vector3(transform.position.x,
				transform.position.y, transform.position.z),Quaternion.identity);
				Destroy(tmp, 5.0f);
			}
		}
	}

}
