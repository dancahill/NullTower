using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuTurret : ClickableAbstract {

    public SceneFader sceneFader;


    public override void ClickAction() {
        sceneFader.FadeTo("RiskMap");

    }
}
