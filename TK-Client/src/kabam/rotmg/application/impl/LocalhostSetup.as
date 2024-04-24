package kabam.rotmg.application.impl {
import com.company.assembleegameclient.parameters.Parameters;

import kabam.rotmg.application.api.ApplicationSetup;

public class LocalhostSetup implements ApplicationSetup {
    private const LOCALHOST:String = "http://127.0.0.1:2005";
    private const BUILD_LABEL:String = " <font color=\"#00FFFF\">LOCALHOST</font> <font color=\"#FF0000\">TKR</font> <font color=\"#FFFF00\">v{VERSION}.{MINOR}.{PATCH}</font>";

    public function getAppEngineUrl():String {
        return LOCALHOST;
    }

    public function getAppEngineUrlEncrypted():String {
        return LOCALHOST;
    }

    public function getBuildLabel():String {
        return this.BUILD_LABEL.replace("{VERSION}", Parameters.BUILD_VERSION).replace("{MINOR}", Parameters.MINOR_VERSION).replace("{PATCH}", Parameters.PATCH_VERSION);
    }

    public function useProductionDialogs():Boolean {
        return true;
    }

    public function areErrorsReported():Boolean {
        return false;
    }

    public function isDebug():Boolean {
        return true;
    }
}
}