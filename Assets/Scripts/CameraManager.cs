public class CameraManager{

    private static CameraManager instance;

    private CameraManager() {}

    public static CameraManager Instance {
        get {
            if (instance == null) {
                instance = new CameraManager();
            }
            return instance;
        }
    }

    private CameraManagerComponent _component;

    public void RegisterCameraManagerComponent(CameraManagerComponent component) {
        _component = component;
    }

    public void ShakeCamera(float intensity, float decay) {
        _component.Shaker.DoShake(intensity, decay);
    }
}
