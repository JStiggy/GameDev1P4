using UnityEngine;
using System.Collections;

public class MineralNode : MonoBehaviour {

    public Mesh exhaustedNode;
	public GameObject []chunks;
	public int numChunks = 5;

	void start(){
		chunks = new GameObject[numChunks];
	}
	
    public void consumeNode()
    {
        gameObject.layer = 0;
        this.GetComponent<MeshFilter>().mesh = exhaustedNode;
		this.GetComponent<Renderer>().material.shader = Shader.Find("Diffuse");
		spawnChunks();
    }
	
	void spawnChunks(){
		for(int i=0;i<numChunks;i++){
			Instantiate(chunks[i], new Vector3(transform.position.x,
			transform.position.y, transform.position.z),Quaternion.identity);
		}
	}

}
