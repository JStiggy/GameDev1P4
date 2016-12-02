using UnityEngine;
using System.Collections;

public class MineralNode : MonoBehaviour {

    public Mesh exhaustedNode;

    public void consumeNode()
    {
        gameObject.layer = 0;
        this.GetComponent<MeshFilter>().mesh = exhaustedNode;
    }

}
