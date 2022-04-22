// Copyright 2017 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
using Mirror;
using UnityEngine;
using System.Collections;

public class platformSetup : NetworkBehaviour {
  public GameObject hmdPrefab, controllerPrefab;

  public Transform hmdTargetVive, controllerLTargetVive, controllerRTargetVive;
  public Transform hmdTargetOculus, controllerLTargetOculus, controllerRTargetOculus;
  public GameObject hiresCamSelect;

  manipulator[] manips = new manipulator[2];
  void Awake() {

        if (!isLocalPlayer) return;

    masterControl MC = GetComponent<masterControl>();

    if (MC.currentPlatform == masterControl.platform.Vive) {

            NetworkServer.Spawn(Instantiate(hmdPrefab, hmdTargetVive, false));
      manips[0] = (Instantiate(controllerPrefab, controllerLTargetVive, false) as GameObject).GetComponentInChildren<manipulator>();
            NetworkServer.Spawn(manips[0].gameObject);
      manips[1] = (Instantiate(controllerPrefab, controllerRTargetVive, false) as GameObject).GetComponentInChildren<manipulator>();
            NetworkServer.Spawn(manips[1].gameObject);

            manips[0].transform.parent.localPosition = Vector3.zero;
      manips[1].transform.parent.localPosition = Vector3.zero;

      if (UnityEngine.XR.XRSettings.loadedDeviceName == "Oculus") oculusSwitch();

      manips[0].SetDeviceIndex(0);
      manips[1].SetDeviceIndex(1);

            manips[0].toggleTips(false); // FIXME this was in Start() but these are not present on the server
        }
  }

    void Update()
    {
        if (!isLocalPlayer) return;
        OVRPlugin.Controller control = OVRPlugin.GetActiveController(); //get current controller scheme
        if ((OVRPlugin.Controller.Hands == control) || (OVRPlugin.Controller.LHand == control) || (OVRPlugin.Controller.RHand == control))
        { //if current controller is hands disable the controllers
            manips[0].gameObject.SetActive(false);
            manips[1].gameObject.SetActive(false);
        }
        else
        {
            manips[0].gameObject.SetActive(true);
            manips[1].gameObject.SetActive(true);
        }
    }

    void oculusSwitch() {
    manips[0].invertScale(); // this actually makes the controller model L and R handed.
    manips[0].changeHW("oculus");
    manips[1].changeHW("oculus");
  }

  void Start() {
        }

}