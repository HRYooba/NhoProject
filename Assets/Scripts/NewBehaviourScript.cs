using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    private enum Mode { Easy, Hard, Normal }
    Mode mode;

	// Use this for initialization
	void Start () {
        mode = Mode.Easy;
	}
	
	// Update is called once per frame
	void Update () {
        switch (mode) {
            case Mode.Easy:
                break;
            case Mode.Hard:
                break;
        }

	}
}
